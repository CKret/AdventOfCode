using System.Diagnostics;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 3, "Day 3: Toboggan Trajectory", 284L, 3510149120L)]
    public class AdventOfCode202003 : AdventOfCodeBase
    {
        public AdventOfCode202003(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            var timer = new Stopwatch();
            timer.Start();

            var map = Input;
            var (kX, kY) = (3, 1);

            ResultPart1 = CountTrees(map, kX, kY);
            timer.Stop();
            TimePart1 = timer.ElapsedTicks.ToMilliseconds();

            timer.Restart();
            var slopes = new[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };
            ResultPart2 = slopes.Aggregate(1L, (a, b) => a * CountTrees(map, b.Item1, b.Item2));
            timer.Stop();
            TimePart2 = timer.ElapsedTicks.ToMilliseconds();
        }

        private int CountTrees(string[] map, int kX, int kY)
        {
            var mapWidth = map[0].Length;

            var nTrees = 0;
            for (var (x, y) = (0, 0); y < map.Length; x += kX, y += kY)
            {
                if (map[y][x % mapWidth] == '#') nTrees++;
            }

            return nTrees;
        }
    }
}
