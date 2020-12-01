﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
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
                .ToDictionary(c => (X: x++, Y: y), c => c), c => (x, y) = (0, y + 1))
                .SelectMany(i => i.Key)
                .ToDictionary(i => i.Key, i => i.Value);

            var keys = "abcdefghijklmnopqrstuvwxyz".ToList();
            var doors = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();

            var gotKeys = new List<char>();
            var openedDoors = new List<char>();

            var entrance = map.Single(p => p.Value == '@');

            //var manhattanKeys = allKeys.Select(k => NumberTheory.ManhattanDistance(entrance.Key, k.Key));
            //var manhattanDoors = allDoors.Select(k => NumberTheory.ManhattanDistance(entrance.Key, k.Key));

            var steps = 0;

            // The loop.

            var currentLocation = entrance.Key;
            while (keys.Count > 0)
            {
                var allKeys = map.Where(p => keys.Contains(p.Value)).ToList();
                var allDoors = map.Where(p => doors.Contains(p.Value)).ToList();

                //var possibleLocations = map.Where(p => p.Value != '#').ToDictionary(p => p.Key, p => p.Value);
                var possibleLocations = map.Where(p => p.Value != '#' && !doors.Contains(p.Value) && !gotKeys.Contains(p.Value)).ToDictionary(p => p.Key, p => p.Value);
                var pathCosts = GetPathCosts(map, possibleLocations);

                var costsToKeys = new Dictionary<char, LinkedList<PathFindingBase<(int, int)>>>();

                foreach (var key in allKeys)
                {
                    var shortPath = Dijkstras.CalculateShortestPathBetween(currentLocation, key.Key, pathCosts);
                    if (shortPath.Count > 0)
                        costsToKeys.Add(key.Value, shortPath);
                }

                var closest = costsToKeys.First(c => c.Value.Count == costsToKeys.Min(p => p.Value.Count));
                map[currentLocation] = '.';
                map[closest.Value.Last().Destination] = '.';

                gotKeys.Add(closest.Key);
                keys.Remove(closest.Key);
                doors.Remove(closest.Key.ToString(CultureInfo.InvariantCulture).ToUpperInvariant()[0]);

                steps += closest.Value.Count;
                currentLocation = closest.Value.Last().Destination;
            }
            // Find cost to closest key and door or cost to available door.

        }

        public static List<PathFindingBase<(int, int)>> GetPathCosts(Dictionary<(int, int), char> map, Dictionary<(int X, int Y), char> possibleLocations)
        {

            var pathCosts = new List<PathFindingBase<(int, int)>>();
            foreach (var source in possibleLocations)
            {
                var destination = (source.Key.X + 1, source.Key.Y);
                if (possibleLocations.ContainsKey(destination))
                {
                    var pfb = new PathFindingBase<(int, int)>
                    {
                        Source = source.Key,
                        Destination = destination,
                        Cost = 1
                    };

                    pathCosts.Add(pfb);
                }

                destination = (source.Key.X - 1, source.Key.Y);
                if (possibleLocations.ContainsKey(destination))
                {
                    var pfb = new PathFindingBase<(int, int)>
                    {
                        Source = source.Key,
                        Destination = destination,
                        Cost = 1
                    };

                    pathCosts.Add(pfb);
                }

                destination = (source.Key.X, source.Key.Y + 1);
                if (possibleLocations.ContainsKey(destination))
                {
                    var pfb = new PathFindingBase<(int, int)>
                    {
                        Source = source.Key,
                        Destination = destination,
                        Cost = 1
                    };

                    pathCosts.Add(pfb);
                }

                destination = (source.Key.X, source.Key.Y - 1);
                if (possibleLocations.ContainsKey(destination))
                {
                    var pfb = new PathFindingBase<(int, int)>
                    {
                        Source = source.Key,
                        Destination = destination,
                        Cost = 1
                    };

                    pathCosts.Add(pfb);
                }
            }

            return pathCosts;
        }

        public AdventOfCode2019181(string sessionCookie) : base(sessionCookie) { }
    }
}
