using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2024, 4, "", 2507, 1969)]
    public class AdventOfCode202404 : AdventOfCodeBase
    {
        public AdventOfCode202404(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var count = 0;
            var data = new List<string>(Input);
            var maxY = data.Count;
            var maxX = data[0].Length;

            for (var y = 0; y < maxY; y++)
            {
                for (var x = 0; x < maxX; x++)
                {
                    count += CountXMAS(x, y, data);
                }
            }

            return count;
        }

        protected override object SolvePart2()
        {
            var count = 0;
            var data = new List<string>(Input);
            var maxY = data.Count;
            var maxX = data[0].Length;

            for (var y = 1; y < maxY - 1; y++)
            {
                for (var x = 1; x < maxX - 1; x++)
                {
                    if (IsXMAS(x, y, data)) count++;
                }
            }

            return count;
        }

        protected int CountXMAS(int x, int y, List<string> data)
        {
            var count = 0;
            if (data[y][x] != 'X') return 0;

            // Left
            if (x >= 3 && data[y][x - 1] == 'M' && data[y][x - 2] == 'A' && data[y][x - 3] == 'S') count++;

            // Right
            if (x <= data[y].Length - 4 && data[y][x + 1] == 'M' && data[y][x + 2] == 'A' && data[y][x + 3] == 'S') count++;

            // Up
            if (y >= 3 && data[y - 1][x] == 'M' && data[y - 2][x] == 'A' && data[y - 3][x] == 'S') count++;

            // Down
            if (y <= data.Count - 4 && data[y + 1][x] == 'M' && data[y + 2][x] == 'A' && data[y + 3][x] == 'S') count++;

            // Up-Left
            if (x >= 3 && y >= 3 && data[y - 1][x - 1] == 'M' && data[y - 2][x - 2] == 'A' && data[y - 3][x - 3] == 'S') count++;

            // Up-Right
            if (x <= data[y].Length - 4 && y >= 3 && data[y - 1][x + 1] == 'M' && data[y - 2][x + 2] == 'A' && data[y - 3][x + 3] == 'S') count++;

            // Down-Left
            if (x >= 3 && y <= data.Count - 4 && data[y + 1][x - 1] == 'M' && data[y + 2][x - 2] == 'A' && data[y + 3][x - 3] == 'S') count++;

            // Down-Right
            if (x <= data[y].Length - 4 && y <= data.Count - 4 && data[y + 1][x + 1] == 'M' && data[y + 2][x + 2] == 'A' && data[y + 3][x + 3] == 'S') count++;

            return count;
        }

        protected bool IsXMAS(int x, int y, List<string> data)
        {
            if (data[y][x] != 'A') return false;

            if (data[y - 1][x - 1] == 'M' && data[y + 1][x + 1] == 'S' && data[y - 1][x + 1] == 'M' && data[y + 1][x - 1] == 'S') return true;
            if (data[y - 1][x - 1] == 'M' && data[y + 1][x + 1] == 'S' && data[y - 1][x + 1] == 'S' && data[y + 1][x - 1] == 'M') return true;
            if (data[y - 1][x - 1] == 'S' && data[y + 1][x + 1] == 'M' && data[y - 1][x + 1] == 'M' && data[y + 1][x - 1] == 'S') return true;
            if (data[y - 1][x - 1] == 'S' && data[y + 1][x + 1] == 'M' && data[y - 1][x + 1] == 'S' && data[y + 1][x - 1] == 'M') return true;

            return false;
        }
    }
}
