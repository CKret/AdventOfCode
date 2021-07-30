using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 3, "Toboggan Trajectory", 284L, 3510149120L)]
    public class AdventOfCode202003 : AdventOfCodeBase
    {
        public AdventOfCode202003(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var (kX, kY) = (3, 1);
            return CountTrees(Input, kX, kY);
        }

        protected override object SolvePart2()
        {
            var slopes = new[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };
            return slopes.Aggregate(1L, (a, b) => a * CountTrees(Input, b.Item1, b.Item2));
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
