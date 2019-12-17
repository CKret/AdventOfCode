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

            // ToDo: Refactor since solved by hand.
            var main = "A,B,A,B,A,C,B,C,A,C\x0A".ToArray();
            var funcA = "L,6,R,12,L,6\x0A".ToArray();
            var funcB = "R,12,L,10,L,4,L,6\x0A".ToArray();
            var funcC = "L,10,L,10,L,4,L,6\x0A".ToArray();

            vm.ResetVM();
            vm.Write(0, 2);

            foreach (var c in main) vm.Input.Enqueue(c);
            foreach (var c in funcA) vm.Input.Enqueue(c);
            foreach (var c in funcB) vm.Input.Enqueue(c);
            foreach (var c in funcC) vm.Input.Enqueue(c);

            vm.Input.Enqueue('n');
            vm.Input.Enqueue(10);

            vm.Execute();

            Result = vm.Output.Last();
        }
    }
}
