using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.VMs;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 17, 2, "", 880360)]
    public class AdventOfCode2019172 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201917.txt");

            var map = new Dictionary<(int X, int Y), char>();
            var vm = new IntcodeVM(data);
            var (x, y) = (0, 0);

            var vmStatus = vm.Execute();

            while (vm.Output.Count > 0)
            {
                var o = vm.Output.Dequeue();
                if (o == 10) (x, y) = (0, y + 1);
                else map[(x++, y)] = (char) o;
            }

            var maxX = map.Max(p => p.Key.X);
            var maxY = map.Max(p => p.Key.Y);
            var bot = map.Single(p => p.Value == '<' || p.Value == '^' || p.Value == '>' || p.Value == 'v');
            var pos = bot.Key;
            var direction = bot.Value;

            var directions = new List<char>();

            var path = new Dictionary<(int X, int Y), int>();
            while (true)
            {
                if (path.ContainsKey(pos)) path[pos]++;
                else path.Add(pos, 1);

                if (direction == '>' && pos.X < maxX && map[(pos.X + 1, pos.Y)] == '#')
                {
                    pos.X++;
                    directions.Add('.');
                }
                else if (direction == '<' && pos.X > 0 && map[(pos.X - 1, pos.Y)] == '#')
                { 
                    pos.X--;
                    directions.Add('.');
                }
                else if (direction == 'v' && pos.Y < maxY && map[(pos.X, pos.Y + 1)] == '#')
                { 
                    pos.Y++;
                    directions.Add('.');
                }
                else if (direction == '^' && pos.Y > 0 && map[(pos.X, pos.Y - 1)] == '#')
                {
                    pos.Y--;
                    directions.Add('.');
                }
                else if (direction != '<' && pos.X < maxX && map[(pos.X + 1, pos.Y)] == '#')
                {
                    directions.Add(direction == '^' ? 'R' : 'L');
                    direction = '>';
                    pos.X++;
                }
                else if (direction != '>' && pos.X > 0 && map[(pos.X - 1, pos.Y)] == '#')
                {
                    directions.Add(direction == '^' ? 'L' : 'R');
                    direction = '<';
                    pos.X--;
                }
                else if (direction != '^' && pos.Y < maxY && map[(pos.X, pos.Y + 1)] == '#')
                {
                    directions.Add(direction == '>' ? 'R' : 'L');
                    direction = 'v';
                    pos.Y++;
                }
                else if (direction != 'v' && pos.Y > 0 && map[(pos.X, pos.Y - 1)] == '#')
                {
                    directions.Add(direction == '>' ? 'L' : 'R');
                    direction = '^';
                    pos.Y--;
                }
                else break;
            }

            var intersections = path.Where(p => p.Value == 2).ToList();
            Result = intersections.Aggregate(0, (cur, next) => cur + next.Key.X * next.Key.Y);

            var d = new List<string>();
            var count = 1;
            var curDir = '.';
            foreach (var c in directions)
            {
                if (c != '.')
                {
                    if (count > 1) d.Add(count.ToString(CultureInfo.InvariantCulture));
                    d.Add(c.ToString(CultureInfo.InvariantCulture));
                    curDir = c;
                    count = 1;
                }
                else count++;
            }
            if (count > 1) d.Add(count.ToString(CultureInfo.InvariantCulture));

            var res = string.Join(",", d);

            var routine = FindRoutine(string.Join("", d));

            // ToDo: Refactor since solved by hand.
            //var main = "A,B,A,B,A,C,B,C,A,C\x0A".ToArray();
            //var funcA = "L,6,R,12,L,6\x0A".ToArray();
            //var funcB = "R,12,L,10,L,4,L,6\x0A".ToArray();
            //var funcC = "L,10,L,10,L,4,L,6\x0A".ToArray();

            vm.ResetVM();
            vm.Write(0, 2);

            foreach (var c in routine[0].ToArray()) vm.Input.Enqueue(c);
            foreach (var c in routine[1].ToArray()) vm.Input.Enqueue(c);
            foreach (var c in routine[2].ToArray()) vm.Input.Enqueue(c);
            foreach (var c in routine[3].ToArray()) vm.Input.Enqueue(c);

            vm.Input.Enqueue('n');
            vm.Input.Enqueue(10);

            vm.Execute();

            Result = vm.Output.Last();
        }

        public static List<string> FindRoutine(string path)
        {
            var tokens = new List<string>();

            var cur = string.Empty;
            for (var i = 0; i < path.Length; i++)
            {
                var c = path[i];
                if ((c == 'L' || c == 'R') && cur.Length > 0)
                {
                    tokens.Add(cur);
                    cur = c.ToString();
                }
                else cur += c;
            }

            var candidates = new List<List<string>>();

            for (var i = 1; i < tokens.Count - 2; i++)
            {
                var s = path;
                var t1 = string.Join("", tokens.Take(i).ToArray());

                var count1 = 0;
                var c1 = s;
                var main1 = new List<char>();
                while (c1.StartsWith(t1))
                {
                    if (t1.Length > c1.Length) break;
                    c1 = c1.Substring(t1.Length);
                    count1++;
                    main1.Add('A');
                }

                for (var j = 1; j < tokens.Count - i * count1; j++)
                {
                    var t2 = string.Join("", tokens.Skip(i * count1).Take(j).ToArray());
                    var main2 = new List<char>(main1);

                    var count2 = 0;
                    var count2_1 = count1;
                    var c2 = c1;
                    while (true)
                    {
                        if (t2.Length > c2.Length) break;
                        if (c2.StartsWith(t2))
                        {
                            c2 = c2.Substring(t2.Length);
                            count2++;
                            main2.Add('B');
                        }
                        else if (c2.StartsWith(t1))
                        { 
                            c2 = c2.Substring(t1.Length);
                            count2_1++;
                            main2.Add('A');
                        }
                        else break;
                    }

                    for (var k = 1; k <= tokens.Count - i * count2_1 - j * count2; k++)
                    {
                        var t3 = string.Join("", tokens.Skip(i * count2_1).Skip(j * count2).Take(k).ToArray());
                        var main3 = new List<char>(main2);

                        var c3 = c2;
                        while (true)
                        {
                            if (t3.Length > c3.Length) break;
                            if (c3.StartsWith(t3))
                            {
                                c3 = c3.Substring(t3.Length);
                                main3.Add('C');
                            }
                            else if (c3.StartsWith(t2))
                            {
                                c3 = c3.Substring(t2.Length);
                                main3.Add('B');
                            }
                            else if (c3.StartsWith(t1))
                            {
                                c3 = c3.Substring(t1.Length);
                                main3.Add('A');
                            }
                            else break;

                            if (c3.Length == 0)
                            {
                                var r1 = string.Join(",", tokens.Take(i).ToArray());
                                var r2 = string.Join(",", tokens.Skip(i * count1).Take(j).ToArray());
                                var r3 = string.Join(",", tokens.Skip(i * count2_1).Skip(j * count2).Take(k).ToArray());

                                if (r1.Length <= 20 && r2.Length <= 20 && r3.Length <= 20)
                                {
                                    var m = string.Join(",", main3);
                                    r1 = r1.Replace("L", "L,").Replace("R", "R,");
                                    r2 = r2.Replace("L", "L,").Replace("R", "R,");
                                    r3 = r3.Replace("L", "L,").Replace("R", "R,");
                                    return new List<string> { m + (char) 10, r1 + (char) 10, r2 + (char) 10, r3 + (char) 10 };
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
