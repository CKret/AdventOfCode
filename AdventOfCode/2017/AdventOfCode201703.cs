using System;
using System.Collections.Generic;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 3: Spiral Memory ---
    /// 
    /// You come across an experimental new kind of memory stored on an infinite
    /// two-dimensional grid.
    /// 
    /// Each square on the grid is allocated in a spiral pattern starting at a
    /// location marked 1 and then counting up while spiraling outward. For
    /// example, the first few squares are allocated like this:
    /// 
    /// 17  16  15  14  13
    /// 18   5   4   3  12
    /// 19   6   1   2  11
    /// 20   7   8   9  10
    /// 21  22  23---&gt; ...
    /// 
    /// While this is very space-efficient (no squares are skipped), requested data
    /// must be carried back to square 1 (the location of the only access port for
    /// this memory system) by programs that can only move up, down, left, or
    /// right. They always take the shortest path: the Manhattan Distance between
    /// the location of the data and square 1.
    /// 
    /// For example:
    /// 
    ///     - Data from square 1 is carried 0 steps, since it's at the access port.
    ///     - Data from square 12 is carried 3 steps, such as: down, left, left.
    ///     - Data from square 23 is carried only 2 steps: up twice.
    ///     - Data from square 1024 must be carried 31 steps.
    /// 
    /// How many steps are required to carry the data from the square identified in
    /// your puzzle input all the way to the access port?
    /// 
    /// --- Part Two ---
    /// 
    /// As a stress test on the system, the programs here clear the grid and then
    /// store the value 1 in square 1.  Then, in the same allocation order as shown
    /// above, they store the sum of the values in all adjacent squares, including
    /// diagonals.
    /// 
    /// So, the first few squares' values are chosen as follows:
    /// 
    ///     - Square 1 starts with the value 1.
    ///     - Square 2 has only one adjacent filled square (with value 1), so it
    ///       also stores 1.
    ///     - Square 3 has both of the above squares as neighbors and stores the sum
    ///       of their values, 2.
    ///     - Square 4 has all three of the aforementioned squares as neighbors and
    ///       stores the sum of their values, 4.
    ///     - Square 5 only has the first and fourth squares as neighbors, so it
    ///       gets the value 5.
    /// 
    /// Once a square is written, its value does not change. Therefore, the first
    /// few squares would receive the following values:
    /// 
    /// 147  142  133  122   59
    /// 304    5    4    2   57
    /// 330   10    1    1   54
    /// 351   11   23   25   26
    /// 362  747  806---&gt;   ...
    /// 
    /// What is the first value written that is larger than your puzzle input?
    /// </summary>
    [AdventOfCode(2017, 3, "Spiral Memory", 326, 363010)]
    public class AdventOfCode201703 : AdventOfCodeBase
    {
        public AdventOfCode201703(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            const int val = 361527;

            var prevRoot = (int)Math.Ceiling(Math.Sqrt(val));
            var root = prevRoot % 2 != 0 ? prevRoot : prevRoot + 1;
            var mid = (root - 1) / 2;
            var side = val - (int)Math.Pow(root - 2, 2);
            var offset = side % (root - 1);

            return mid + Math.Abs(offset - mid);
        }

        protected override object SolvePart2()
        {
            return Spiral(361527);
        }

        public static int Spiral(int input)
        {
            var spiral = new Dictionary<(int, int), int>();
            var x = 0;
            var y = 0;

            spiral.Add((x, y), 1);

            while (spiral[(x, y)] <= input)
            {
                if (Math.Abs(x) <= Math.Abs(y) && (x != y || x >= 0))
                    x += y >= 0 ? 1 : -1;
                else
                    y += x >= 0 ? -1 : 1;

                CalculateNode(x, y, spiral);
            }

            return spiral[(x, y)];
        }

        public static void CalculateNode(int x, int y, Dictionary<(int, int), int> spiral)
        {
            spiral.Add((x, y), 0);

            spiral[(x, y)] += !spiral.ContainsKey((x + 1, y)) ? 0 : spiral[(x + 1, y)];
            spiral[(x, y)] += !spiral.ContainsKey((x + 1, y + 1)) ? 0 : spiral[(x + 1, y + 1)];
            spiral[(x, y)] += !spiral.ContainsKey((x, y + 1)) ? 0 : spiral[(x, y + 1)];
            spiral[(x, y)] += !spiral.ContainsKey((x - 1, y + 1)) ? 0 : spiral[(x - 1, y + 1)];
            spiral[(x, y)] += !spiral.ContainsKey((x - 1, y)) ? 0 : spiral[(x - 1, y)];
            spiral[(x, y)] += !spiral.ContainsKey((x - 1, y - 1)) ? 0 : spiral[(x - 1, y - 1)];
            spiral[(x, y)] += !spiral.ContainsKey((x, y - 1)) ? 0 : spiral[(x, y - 1)];
            spiral[(x, y)] += !spiral.ContainsKey((x + 1, y - 1)) ? 0 : spiral[(x + 1, y - 1)];
        }
    }
}
