using System;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.Mathematics;

namespace AdventOfCode._2018
{
    /// <summary>
    /// --- Day 6: Chronal Coordinates ---
    /// 
    /// --- Part Two ---
    /// 
    /// On the other hand, if the coordinates are safe, maybe the best you can do
    /// is try to find a region near as many coordinates as possible.
    /// 
    /// For example, suppose you want the sum of the Manhattan distance to all of
    /// the coordinates to be less than 32. For each location, add up the distances
    /// to all of the given coordinates; if the total of those distances is less
    /// than 32, that location is within the desired region. Using the same
    /// coordinates as above, the resulting region looks like this:
    /// 
    /// ..........
    /// .A........
    /// ..........
    /// ...###..C.
    /// ..#D###...
    /// ..###E#...
    /// .B.###....
    /// ..........
    /// ..........
    /// ........F.
    /// 
    /// In particular, consider the highlighted location 4,3 located at the top
    /// middle of the region. Its calculation is as follows, where abs() is the
    /// absolute value function:
    /// 
    /// - Distance to coordinate A: abs(4-1) + abs(3-1) =  5
    /// - Distance to coordinate B: abs(4-1) + abs(3-6) =  6
    /// - Distance to coordinate C: abs(4-8) + abs(3-3) =  4
    /// - Distance to coordinate D: abs(4-3) + abs(3-4) =  2
    /// - Distance to coordinate E: abs(4-5) + abs(3-5) =  3
    /// - Distance to coordinate F: abs(4-8) + abs(3-9) = 10
    /// - Total distance: 5 + 6 + 4 + 2 + 3 + 10 = 30
    /// 
    /// Because the total distance to all coordinates (30) is less than 32, the
    /// location is within the region.
    /// 
    /// This region, which also includes coordinates D and E, has a total size of
    /// 16.
    /// 
    /// Your actual region will need to be much larger than this example, though,
    /// instead including all locations with a total distance of less than 10000.
    /// 
    /// What is the size of the region containing all locations which have a total
    /// distance to all given coordinates of less than 10000?
    /// </summary>
    [AdventOfCode(2018, 6, 2, "Chronal Coordinates - Part 2", 42036)]
    public class AdventOfCode2018062 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var coordinates = File.ReadAllLines(@"2018\AdventOfCode201806.txt")
                                  .Select(s => s.Split(new[] { ", " }, StringSplitOptions.None))
                                  .Select(s => s.Select(int.Parse).ToArray())
                                  .Select(s => (x: s[0], y: s[1]))
                                  .ToArray();

            var minX = coordinates.Min(c => c.x);
            var maxX = coordinates.Max(c => c.x);
            var minY = coordinates.Min(c => c.y);
            var maxY = coordinates.Max(c => c.y);

            var safeAreas = 0;

            foreach (var x in Enumerable.Range(minX, maxX - minX))
            foreach (var y in Enumerable.Range(minY, maxY - minX))
            {
                var distance = coordinates.Select(c => NumberTheory.ManhattanDistance((x, y), c)).Sum();
                if (distance < 10000)
                    safeAreas++;
            }

            Result = safeAreas;
        }
    }
}
