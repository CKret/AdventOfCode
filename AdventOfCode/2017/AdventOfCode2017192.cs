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

            var grid = input.Select(a => a.ToCharArray().ToList()).ToList();

            int x = input[0].IndexOf('|'), y = 0, dx = 0, dy = 1, count = 0;

            do { count++; } while (AdventOfCode2017191.Move(ref x, ref y, ref dx, ref dy, grid));

            Result = count;
        }

        public AdventOfCode2017192(string sessionCookie) : base(sessionCookie) { }
    }
}
