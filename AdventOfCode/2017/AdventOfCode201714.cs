using System;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.Mathematics.Cryptography;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 14: Disk Defragmentation ---
    /// 
    /// Suddenly, a scheduled job activates the system's disk defragmenter. Were
    /// the situation different, you might sit and watch it for a while, but today,
    /// you just don't have that kind of time. It's soaking up valuable system
    /// resources that are needed elsewhere, and so the only option is to help it
    /// finish its task as soon as possible.
    /// 
    /// The disk in question consists of a 128x128 grid; each square of the grid is
    /// either free or used. On this disk, the state of the grid is tracked by the
    /// bits in a sequence of knot hashes.
    /// 
    /// A total of 128 knot hashes are calculated, each corresponding to a single
    /// row in the grid; each hash contains 128 bits which correspond to individual
    /// grid squares. Each bit of a hash indicates whether that square is free (0)
    /// or used (1).
    /// 
    /// The hash inputs are a key string (your puzzle input), a dash, and a number
    /// from 0 to 127 corresponding to the row. For example, if your key string
    /// were flqrgnkx, then the first row would be given by the bits of the knot
    /// hash of flqrgnkx-0, the second row from the bits of the knot hash of
    /// flqrgnkx-1, and so on until the last row, flqrgnkx-127.
    /// 
    /// The output of a knot hash is traditionally represented by 32 hexadecimal
    /// digits; each of these digits correspond to 4 bits, for a total of
    /// 4 * 32 = 128 bits. To convert to bits, turn each hexadecimal digit to its
    /// equivalent binary value, high-bit first: 0 becomes 0000, 1 becomes 0001, e
    /// becomes 1110, f becomes 1111, and so on; a hash that begins with a0c2017...
    /// in hexadecimal would begin with 10100000110000100000000101110000... in
    /// binary.
    /// 
    /// Continuing this process, the first 8 rows and columns for key flqrgnkx
    /// appear as follows, using # to denote used squares, and . to denote free
    /// ones:
    /// 
    /// ##.#.#..--&gt;
    /// .#.#.#.#   
    /// ....#.#.   
    /// #.#.##.#   
    /// .##.#...   
    /// ##..#..#   
    /// .#...#..   
    /// ##.#.##.--&gt;
    /// |      |   
    /// V      V   
    /// 
    /// In this example, 8108 squares are used across the entire 128x128 grid.
    /// 
    /// Given your actual key string, how many squares are used?
    /// 
    /// --- Part Two ---
    /// 
    /// Now, all the defragmenter needs to know is the number of regions. A region
    /// is a group of used squares that are all adjacent, not including diagonals.
    /// Every used square is in exactly one region: lone used squares form their
    /// own isolated regions, while several adjacent squares all count as a single
    /// region.
    /// 
    /// In the example above, the following nine regions are visible, each marked
    /// with a distinct digit:
    /// 
    /// 11.2.3..--&gt;
    /// .1.2.3.4   
    /// ....5.6.   
    /// 7.8.55.9   
    /// .88.5...   
    /// 88..5..8   
    /// .8...8..   
    /// 88.8.88.--&gt;
    /// |      |   
    /// V      V   
    /// 
    /// Of particular interest is the region marked 8; while it does not appear
    /// contiguous in this small view, all of the squares marked 8 are connected
    /// when considering the whole 128x128 grid. In total, in this example, 1242
    /// regions are present.
    /// 
    /// How many regions are present given your key string?
    /// </summary>
    [AdventOfCode(2017, 14, "Disk Defragmentation", 8194, 1141)]
    public class AdventOfCode201714 : AdventOfCodeBase
    {
        public AdventOfCode201714(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            const string input = "uugsqrei";

            return Enumerable.Range(0, 128)
                .Sum(x =>
                    HashAlgorithm.KnotHash($"{input}-{x}")
                    .Aggregate(string.Empty, (current, b) => current + Convert.ToString(b, 2).PadLeft(8, '0'))
                    .Count(c => c == '1'));
        }

        protected override object SolvePart2()
        {
            const string input = "uugsqrei";
            var grid = CreateGrid(input);
            var visited = new bool[128, 128];

            // Traverse the grid by DFS to identify each region.
            return Enumerable.Range(0, 128)
                .Sum(y => Enumerable.Range(0, 128)
                    .Where(x => !visited[x, y] && grid[x][y] != '0').Sum(x =>
                    {
                        PaintRegion(x, y, grid, visited);
                        return 1;
                    }));
        }

        private static string[] CreateGrid(string input)
        {
            return Enumerable.Range(0, 128)
                .Select(i => HashAlgorithm.KnotHash($"{input}-{i}"))
                .Select(hash => string.Join(string.Empty, hash.Select(b => Convert.ToString(b, 2).PadLeft(8, '0'))))
                .ToArray();
        }

        private void PaintRegion(int x, int y, string[] grid, bool[,] visited)
        {
            if (visited[x, y]) return;

            visited[x, y] = true;

            if (grid[x][y] == '0') return;

            if (x > 0) PaintRegion(x - 1, y, grid, visited);
            if (x < 127) PaintRegion(x + 1, y, grid, visited);
            if (y > 0) PaintRegion(x, y - 1, grid, visited);
            if (y < 127) PaintRegion(x, y + 1, grid, visited);
        }
    }
}
