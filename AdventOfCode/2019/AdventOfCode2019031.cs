using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using AdventOfCode.Mathematics;

namespace AdventOfCode._2019
{
    /// <summary>
    /// --- Day 3: Crossed Wires ---
    /// 
    /// The gravity assist was successful, and you're well on your way to the Venus
    /// refueling station. During the rush back on Earth, the fuel management
    /// system wasn't completely installed, so that's next on the priority list.
    /// 
    /// Opening the front panel reveals a jumble of wires. Specifically, two wires
    /// are connected to a central port and extend outward on a grid. You trace the
    /// path each wire takes as it leaves the central port, one wire per line of
    /// text (your puzzle input).
    /// 
    /// The wires twist and turn, but the two wires occasionally cross paths. To
    /// fix the circuit, you need to find the intersection point closest to the
    /// central port. Because the wires are on a grid, use the Manhattan distance
    /// for this measurement. While the wires do technically cross right at the
    /// central port where they both start, this point does not count, nor does a
    /// wire count as crossing with itself.
    /// 
    /// For example, if the first wire's path is R8,U5,L5,D3, then starting from
    /// the central port (o), it goes right 8, up 5, left 5, and finally down 3:
    /// 
    /// ...........
    /// ...........
    /// ...........
    /// ....+----+.
    /// ....|....|.
    /// ....|....|.
    /// ....|....|.
    /// .........|.
    /// .o-------+.
    /// ...........
    /// 
    /// Then, if the second wire's path is U7,R6,D4,L4, it goes up 7, right 6, down
    /// 4, and left 4:
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
    /// These wires cross at two locations (marked X), but the lower-left one is
    /// closer to the central port: its distance is 3 + 3 = 6.
    /// 
    /// Here are a few more examples:
    /// 
    ///  - R75,D30,R83,U83,L12,D49,R71,U7,L72
    ///  - U62,R66,U55,R34,D71,R55,D58,R83 = distance 159
    ///  - R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51
    ///  - U98,R91,D20,R16,D67,R40,U7,R15,U6,R7 = distance 135
    /// 
    /// What is the Manhattan distance from the central port to the closest
    /// intersection?
    /// </summary>
    [AdventOfCode(2019, 3, 1, "Crossed Wires - Part 1", 1285)]
    public class AdventOfCode2019031 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllLines(@"2019\AdventOfCode201903.txt");

            var wire1 = data[0].Split(',');
            var wire2 = data[1].Split(',');

            var grid1 = ParseWire(wire1);
            var grid2 = ParseWire(wire2);

            var intersections = grid1.Keys.Intersect(grid2.Keys);
            Result = intersections.Min(p => NumberTheory.ManhattanDistance((0, 0), p));
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

        public AdventOfCode2019031(string sessionCookie) : base(sessionCookie) { }
    }
}
