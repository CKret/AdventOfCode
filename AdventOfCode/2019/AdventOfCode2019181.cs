using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.Mathematics;
using AdventOfCode.Mathematics.PathFinding;
using AdventOfCode.Mathematics.PathFinding.Core;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 18, 1, "", null)]
    public class AdventOfCode2019181 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllLines(@"2019\AdventOfCode201918.txt");

            var (x, y) = (0, 0);
            var map = data
                .ToDictionary(l => l.ToArray()
                .ToDictionary(c => (x++, y), c => c), c => (x, y) = (0, y + 1))
                .SelectMany(i => i.Key)
                .ToDictionary(i => i.Key, i => i.Value);

            var keys = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            var doors = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            var entrance = map.Single(p => p.Value == '@');
            var allKeys = map.Where(p => keys.Contains(p.Value)).ToList();
            var allDoors = map.Where(p => doors.Contains(p.Value)).ToList();

            var manhattanKeys = allKeys.Select(k => NumberTheory.ManhattanDistance(entrance.Key, k.Key));

            var dijkstra = Dijkstras.CalculateShortestPathBetween(entrance.Key, allKeys[0].Key, new List<PathFindingBase<(int, int)>>());


        }
    }
}
