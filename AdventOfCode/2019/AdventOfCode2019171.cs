using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.VMs;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 17, "", 8928)]
    public class AdventOfCode2019171 : AdventOfCodeBase
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
                var c = (char) o;
                Console.Write(c);
                if (o == 10) (x, y) = (0, y + 1);
                else
                {
                    map[(x++, y)] = c;
                }
            }

            var maxX = map.Max(p => p.Key.X);
            var maxY = map.Max(p => p.Key.Y);
            var bot = map.Single(p => p.Value == '<' || p.Value == '^' || p.Value == '>' || p.Value == 'v');
            var pos = bot.Key;
            var direction = bot.Value;

            var path = new Dictionary<(int X, int Y), int>();
            while (true)
            {
                if (path.ContainsKey(pos)) path[pos]++;
                else path.Add(pos, 1);

                if (direction == '>' && pos.X < maxX && map[(pos.X + 1, pos.Y)] == '#')
                    pos.X++;
                else if (direction == '<' && pos.X > 0 && map[(pos.X - 1, pos.Y)] == '#')
                    pos.X--;
                else if (direction == 'v' && pos.Y < maxY && map[(pos.X, pos.Y + 1)] == '#')
                    pos.Y++;
                else if (direction == '^' && pos.Y > 0 && map[(pos.X, pos.Y - 1)] == '#')
                    pos.Y--;
                else if (direction != '<' && pos.X < maxX && map[(pos.X + 1, pos.Y)] == '#')
                {
                    direction = '>';
                    pos.X++;
                }
                else if (direction != '>' && pos.X > 0 && map[(pos.X - 1, pos.Y)] == '#')
                {
                    direction = '<';
                    pos.X--;
                }
                else if (direction != '^' && pos.Y < maxY && map[(pos.X, pos.Y + 1)] == '#')
                {
                    direction = 'v';
                    pos.Y++;
                }
                else if (direction != 'v' && pos.Y > 0 && map[(pos.X, pos.Y - 1)] == '#')
                {
                    direction = '^';
                    pos.Y--;
                }
                else break;
            }

            var intersections = path.Where(p => p.Value == 2).ToList();
            Result = intersections.Aggregate(0, (cur, next) => cur + next.Key.X * next.Key.Y);
        }

        public AdventOfCode2019171(string sessionCookie) : base(sessionCookie) { }
    }
}
