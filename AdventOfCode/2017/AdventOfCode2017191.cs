using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    using System;

    /// <summary>
    /// --- Day 19: A Series of Tubes ---
    /// 
    /// Somehow, a network packet got lost and ended up here. It's trying to follow
    /// a routing diagram (your puzzle input), but it's confused about where to go.
    /// 
    /// Its starting point is just off the top of the diagram. Lines (drawn with |,
    /// -, and +) show the path it needs to take, starting by going down onto the
    /// only line connected to the top of the diagram. It needs to follow this path
    /// until it reaches the end (located somewhere within the diagram) and stop
    /// there.
    /// 
    /// Sometimes, the lines cross over each other; in these cases, it needs to
    /// continue going the same direction, and only turn left or right when there's
    /// no other option. In addition, someone has left letters on the line; these
    /// also don't change its direction, but it can use them to keep track of where
    /// it's been. For example:
    /// 
    ///      |          
    ///      |  +--+    
    ///      A  |  C    
    ///  F---|----E|--+ 
    ///      |  |  |  D 
    ///      +B-+  +--+ 
    /// 
    /// Given this diagram, the packet needs to take the following path:
    /// 
    /// - Starting at the only line touching the top of the diagram, it must g
    ///   down, pass through A, and continue onward to the first +.
    /// - Travel right, up, and right, passing through B in the process.
    /// - Continue down (collecting C), right, and up (collecting D).
    /// - Finally, go all the way left through E and stopping at F.
    /// 
    /// Following the path to the end, the letters it sees on its path are ABCDEF.
    /// 
    /// The little packet looks up at you, hoping you can help it find the way.
    /// What letters will it see (in the order it would see them) if it follows the
    /// path? (The routing diagram is very wide; make sure you view it without lin
    ///  wrapping.)
    /// 
    /// </summary>
    [AdventOfCode(2017, 19, 1, "A Series of Tubes - Part One", "DTOUFARJQ")]
    public class AdventOfCode2017191 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var input = File.ReadAllLines("2017\\AdventOfCode201719.txt");

            var startPos = input[0].IndexOf('|');
            var grid = input.Select(a => a.ToCharArray().ToList()).ToList();

            int x = startPos, y = 0, dx = 0, dy = 1;
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToList();
            var encounters = new List<char>();

            while (Move(ref x, ref y, ref dx, ref dy, grid, letters, encounters))
            {
            }

            Result = string.Join(string.Empty, encounters);
        }

        internal bool Move(ref int x, ref int y, ref int dx, ref int dy, List<List<char>> grid, List<char> letters, List<char> encounters)
        {
            // up or down
            if (dy != 0) 
            {
                y += dy;
                if (y >= grid.Count || grid[y][x] == ' ') return false;
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
                else if (letters.Contains(grid[y][x]))
                {
                    encounters.Add(grid[y][x]);
                }
            }

            // left or right
            else if (dx != 0)
            {
                x += dx;
                if (grid[y][x] == ' ') return false;
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
                else if (letters.Contains(grid[y][x]))
                {
                    encounters.Add(grid[y][x]);
                }
            }

            return true;
        }
    }
}
