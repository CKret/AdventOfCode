using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

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
            for (var y = 0; y < Input.Length; y++)
            {
                for (var x = 0; x < Input[y].Length; x++)
                {
                    count += CountXMAS(x, y);
                }
            }

            return count;
        }

        protected override object SolvePart2()
        {
            var count = 0;
            for (var y = 1; y < Input.Length - 1; y++)
            {
                for (var x = 1; x < Input[y].Length - 1; x++)
                {
                    if (IsXMAS(x, y)) count++;
                }
            }

            return count;
        }

        protected int CountXMAS(int x, int y)
        {
            var count = 0;
            if (Input[y][x] != 'X') return 0;

            // Left
            if (x >= 3)
            {
                if (Input[y][x] == 'X' && Input[y][x - 1] == 'M' && Input[y][x - 2] == 'A' && Input[y][x - 3] == 'S')
                    count++;
            }

            // Right
            if (x <= Input[y].Length - 4)
            {
                if (Input[y][x] == 'X' && Input[y][x + 1] == 'M' && Input[y][x + 2] == 'A' && Input[y][x + 3] == 'S')
                    count++;
            }

            // Up
            if (y >= 3)
            {
                if (Input[y][x] == 'X' && Input[y - 1][x] == 'M' && Input[y - 2][x] == 'A' && Input[y - 3][x] == 'S')
                    count++;
            }

            // Down
            if (y <= Input.Length - 4)
            {
                if (Input[y][x] == 'X' && Input[y + 1][x] == 'M' && Input[y + 2][x] == 'A' && Input[y + 3][x] == 'S')
                    count++;
            }

            // Up-Left
            if (x >= 3 && y >= 3)
            {
                if (Input[y][x] == 'X' && Input[y - 1][x - 1] == 'M' && Input[y - 2][x - 2] == 'A' && Input[y - 3][x - 3] == 'S')
                    count++;
            }

            // Up-Right
            if (x <= Input[y].Length - 4 && y >= 3)
            {
                if (Input[y][x] == 'X' && Input[y - 1][x + 1] == 'M' && Input[y - 2][x + 2] == 'A' && Input[y - 3][x + 3] == 'S')
                    count++;
            }

            // Down-Left
            if (x >= 3 && y <= Input.Length - 4)
            {
                if (Input[y][x] == 'X' && Input[y + 1][x - 1] == 'M' && Input[y + 2][x - 2] == 'A' && Input[y + 3][x - 3] == 'S')
                    count++;
            }

            // Down-Right
            if (x <= Input[y].Length - 4 && y <= Input.Length - 4)
            {
                if (Input[y][x] == 'X' && Input[y + 1][x + 1] == 'M' && Input[y + 2][x + 2] == 'A' && Input[y + 3][x + 3] == 'S')
                    count++;
            }

            return count;
        }

        protected bool IsXMAS(int x, int y)
        {
            if (Input[y][x] != 'A') return false;

            if (Input[y - 1][x - 1] == 'M' && Input[y + 1][x + 1] == 'S' && Input[y - 1][x + 1] == 'M' && Input[y + 1][x - 1] == 'S') return true;
            if (Input[y - 1][x - 1] == 'M' && Input[y + 1][x + 1] == 'S' && Input[y - 1][x + 1] == 'S' && Input[y + 1][x - 1] == 'M') return true;
            if (Input[y - 1][x - 1] == 'S' && Input[y + 1][x + 1] == 'M' && Input[y - 1][x + 1] == 'M' && Input[y + 1][x - 1] == 'S') return true;
            if (Input[y - 1][x - 1] == 'S' && Input[y + 1][x + 1] == 'M' && Input[y - 1][x + 1] == 'S' && Input[y + 1][x - 1] == 'M') return true;

            return false;
        }
    }
}
