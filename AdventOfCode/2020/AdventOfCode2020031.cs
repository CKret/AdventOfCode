using AdventOfCode.Core;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 3, 1, "", 284)]
    public class AdventOfCode2020031 : AdventOfCodeBase
    {
        public AdventOfCode2020031(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            var map = Input;
            var (kX, kY, mapWidth) = (3, 1, map[0].Length);
            
            var nTrees = 0;
            for (var (x, y) = (0, 0); y < map.Length; x += kX, y += kY)
            {
                if (map[y][x % mapWidth] == '#') nTrees++;
            }

            Result = nTrees;
        }
    }
}
