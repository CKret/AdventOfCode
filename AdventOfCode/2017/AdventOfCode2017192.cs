using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// --- Day 19: A Series of Tubes ---
    /// 
    /// --- Part Two ---
    /// 
    /// The packet is curious how many steps it needs to go.
    /// 
    /// For example, using the same routing diagram from the example above...
    /// 
    ///      |          
    ///      |  +--+    
    ///      A  |  C    
    ///  F---|--|-E---+ 
    ///      |  |  |  D 
    ///      +B-+  +--+ 
    /// 
    /// ...the packet would go:
    /// 
    /// - 6 steps down (including the first line at the top of the diagram).
    /// - 3 steps right.
    /// - 4 steps up.
    /// - 3 steps right.
    /// - 4 steps down.
    /// - 3 steps right.
    /// - 2 steps up.
    /// - 13 steps left (including the F it stops on).
    /// 
    /// This would result in a total of 38 steps.
    /// 
    /// How many steps does the packet need to go?
    /// 
    /// </summary>
    [AdventOfCode(2017, 19, 2, "A Series of Tubes - Part Two", 16642)]
    public class AdventOfCode2017192 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var input = File.ReadAllLines("2017\\AdventOfCode201719.txt");

            var startPos = input[0].IndexOf('|');
            var grid = input.Select(a => a.ToCharArray().ToList()).ToList();

            int x = startPos, y = 0, dx = 0, dy = 1;

            var count = 1;
            while (Move(ref x, ref y, ref dx, ref dy, grid))
            {
                count++;
            }

            Result = count;
        }

        internal bool Move(ref int x, ref int y, ref int dx, ref int dy, List<List<char>> grid)
        {
            // up or down
            if (dy != 0)
            {
                y += dy;
                if (y >= grid.Count || grid[y][x] == ' ')
                    return false;

                if (grid[y][x] == '+')
                {
                    if (x > 0 && x < grid[y].Count - 1)
                    {
                        if (grid[y][x - 1] == '-')
                        {
                            dx = -1;
                            dy = 0;
                        }
                        else if (grid[y][x + 1] == '-')
                        {
                            dx = 1;
                            dy = 0;
                        }
                    }
                }
            }

            // left or right
            else if (dx != 0)
            {
                x += dx;
                if (grid[y][x] == ' ')
                    return false;

                if (grid[y][x] == '+')
                {
                    if (y > 0 && y < grid.Count - 1)
                    {
                        if (grid[y - 1][x] == '|')
                        {
                            dy = -1;
                            dx = 0;
                        }
                        else if (grid[y + 1][x] == '|')
                        {
                            dy = 1;
                            dx = 0;
                        }
                    }
                }
            }

            return true;
        }
    }
}
