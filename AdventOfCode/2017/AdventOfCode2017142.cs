using System;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.Core.Cryptography;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 14: Disk Defragmentation ---
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
    /// 
    /// </summary>
    [AdventOfCode(2017, 14, 2, "Disk Defragmentation - Part 2", 1141)]
    public class AdventOfCode2017142 : AdventOfCodeBase
    {
        public override void Solve()
        {
            const string input = "uugsqrei";
            var grid = CreateGrid(input);

            var hash = HashAlgorithm.KnotHash(input + "-0");
            var a = string.Join(string.Empty, hash.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));

            var visited = new bool[128, 128];
            var regions = 0;

            // Traverse the grid by DFS to identify each region.
            for (var y = 0; y < visited.GetLength(1); y++)
            {
                for (var x = 0; x < visited.GetLength(0); x++)
                {
                    if (visited[x, y] || grid[x][y] == (byte) '0') continue;

                    FindRegion(x, y, grid, visited);
                    regions++;
                }
            }

            Result = regions;
        }

        private static string[] CreateGrid(string input)
        {
            return Enumerable.Range(0, 128)
                .Select(i => $"{input}-{i}").ToArray()
                .Select(s => HashAlgorithm.KnotHash(s))
                .Select(hash => string.Join(string.Empty, hash.Select(b => Convert.ToString(b, 2).PadLeft(8, '0'))))
                .ToArray();
        }

        private void FindRegion(int x, int y, string[] grid, bool[,] visited)
        {
            if (visited[x, y]) return;

            visited[x, y] = true;

            if (grid[x][y] == (byte) '0') return;

            if (x > 0) FindRegion(x - 1, y, grid, visited);
            if (x < 127) FindRegion(x + 1, y, grid, visited);
            if (y > 0) FindRegion(x, y - 1, grid, visited);
            if (y < 127) FindRegion(x, y + 1, grid, visited);
        }
    }
}
