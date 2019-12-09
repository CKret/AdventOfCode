using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2019
{
    /// <summary>
    /// --- Day 3: Crossed Wires ---
    ///
    /// --- Part Two ---
    /// It turns out that this circuit is very timing-sensitive; you actually need
    /// to minimize the signal delay.
    /// 
    /// To do this, calculate the number of steps each wire takes to reach each
    /// intersection; choose the intersection where the sum of both wires' steps is
    /// lowest. If a wire visits a position on the grid multiple times, use the
    /// steps value from the first time it visits that position when calculating
    /// the total value of a specific intersection.
    /// 
    /// The number of steps a wire takes is the total number of grid squares the
    /// wire has entered to get to that location, including the intersection being
    /// considered. Again consider the example from above:
    /// 
    /// ...........
    /// .+-----+...
    /// .|.....|...
    /// .|..+--X-+.
    /// .|..|..|.|.
    /// .|.-X--+.|.
    /// .|..|....|.
    /// .|.......|.
    /// .o-------+.
    /// ...........
    /// 
    /// In the above example, the intersection closest to the central port is
    /// reached after 8+5+5+2 = 20 steps by the first wire and 7+6+4+3 = 20 steps
    /// by the second wire for a total of 20+20 = 40 steps.
    /// 
    /// However, the top-right intersection is better: the first wire takes only
    /// 8+5+2 = 15 and the second wire takes only 7+6+2 = 15, a total of 15+15 = 30
    /// steps.
    /// 
    /// Here are the best steps for the extra examples from above:
    /// 
    ///  - R75,D30,R83,U83,L12,D49,R71,U7,L72
    ///  - U62,R66,U55,R34,D71,R55,D58,R83 = 610 steps
    ///  - R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51
    ///  - U98,R91,D20,R16,D67,R40,U7,R15,U6,R7 = 410 steps
    /// 
    /// What is the fewest combined steps the wires must take to reach an
    /// intersection?
    /// </summary>
    [AdventOfCode(2019, 3, 2, "Crossed Wires - Part 2", 14228)]
    public class AdventOfCode2019032 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllLines(@"2019\AdventOfCode201903.txt");

            var wire1 = data[0].Split(',');
            var wire2 = data[1].Split(',');

            var grid1 = ParseWire(wire1);
            var grid2 = ParseWire(wire2);

            var intersections = grid1.Keys.Intersect(grid2.Keys);
            Result = intersections.Min(p => grid1[p] + grid2[p]);
        }

        protected Dictionary<(int, int), int> ParseWire(string[] wire)
        {
            var grid = new Dictionary<(int, int), int>();

            var x = 0;
            var y = 0;
            var s = 0;
            foreach (var unit in wire)
            {
                var direction = unit[0];
                var count = int.Parse(unit.Substring(1));

                switch (direction)
                {
                    case 'U':
                        for (var c = 0; c < count; c++) { grid.TryAdd((++y, x), ++s); }
                        break;
                    case 'D':
                        for (var c = 0; c < count; c++) { grid.TryAdd((--y, x), ++s); }
                        break;
                    case 'L':
                        for (var c = 0; c < count; c++) { grid.TryAdd((y, --x), ++s); }
                        break;
                    case 'R':
                        for (var c = 0; c < count; c++) { grid.TryAdd((y, ++x), ++s); }
                        break;
                }
            }

            return grid;
        }
    }
}
