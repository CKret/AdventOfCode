using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 3, 2, "", 3510149120L)]
    public class AdventOfCode2020032 : AdventOfCodeBase
    {
        public AdventOfCode2020032(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            var map = Input;
            var slopes = new[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };

            Result = slopes.Aggregate(1L, (a, b) => a * CountTrees(map, b.Item1, b.Item2));
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
