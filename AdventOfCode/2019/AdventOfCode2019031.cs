using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using AdventOfCode.Mathematics;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 3, 1, "", 1285)]
    public class AdventOfCode2019031 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllLines(@"2019\AdventOfCode201903.txt");

            var wire1 = data[0].Split(',');
            var wire2 = data[1].Split(',');


            var grid1 = new Dictionary<(int, int), int>();
            var grid2 = new Dictionary<(int, int), int>();

            var x = 0;
            var y = 0;
            foreach(var unit in wire1)
            {
                var direction = unit[0];
                var count = int.Parse(unit.Substring(1));

                switch(direction)
                {
                    case 'U':
                        for (var c = 0; c < count; c++) { grid1.TryAdd((++y, x), c); }
                        break;
                    case 'D':
                        for (var c = 0; c < count; c++) { grid1.TryAdd((--y, x), c); }
                        break;
                    case 'L':
                        for (var c = 0; c < count; c++) { grid1.TryAdd((y, --x), c); }
                        break;
                    case 'R':
                        for (var c = 0; c < count; c++) { grid1.TryAdd((y, ++x), c); }
                        break;
                }
            }

            x = 0;
            y = 0;
            foreach (var unit in wire2)
            {
                var direction = unit[0];
                var count = int.Parse(unit.Substring(1));

                switch (direction)
                {
                    case 'U':
                        for (var c = 0; c < count; c++) { grid2.TryAdd((++y, x), c); }
                        break;
                    case 'D':
                        for (var c = 0; c < count; c++) { grid2.TryAdd((--y, x), c); }
                        break;
                    case 'L':
                        for (var c = 0; c < count; c++) { grid2.TryAdd((y, --x), c); }
                        break;
                    case 'R':
                        for (var c = 0; c < count; c++) { grid2.TryAdd((y, ++x), c); }
                        break;
                }
            }

            var intersections = grid1.Keys.Intersect(grid2.Keys);
            Result = intersections.Min(p => NumberTheory.ManhattanDistance((0, 0), p));
        }
    }
}
