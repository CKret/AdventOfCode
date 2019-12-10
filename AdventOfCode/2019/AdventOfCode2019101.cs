using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.Mathematics;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 10, 1, "", 267)]
    public class AdventOfCode2019101 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllLines(@"2019\AdventOfCode201910.txt");

            var mapWidth = data[0].Length;
            var mapHeight = data.Length;

            var map = new Dictionary<(int, int), Sector>();

            for (var y = 0; y < mapHeight; y++)
            {
                for (var x = 0; x < mapWidth; x++)
                {
                    var sector = new Sector {Coordinate = (x, y), Content = data[y][x]};
                    map.Add((x, y), sector);
                }
            }

            var visibleMaps = new Dictionary<(int, int), Dictionary<(int, int), char>>();
            for (var y = 0; y < mapHeight; y++)
            {
                for (var x = 0; x < mapWidth; x++)
                {
                    var curentSector = map[(x, y)];
                    if (curentSector.Content == '#')
                    {
                        visibleMaps.Add((x, y), curentSector.GetVisibleMap(map, mapWidth, mapHeight));
                    }
                }
            }

            var max = visibleMaps.Max(x => x.Value.Count(y => y.Value == '#'));

            var maxAsteroids = 0;
            var maxCoords = (0, 0);

            foreach (var a in map)
            {
                if (a.Value.NumAsteroids.HasValue && a.Value.NumAsteroids > maxAsteroids)
                {
                    maxAsteroids = a.Value.NumAsteroids.Value;
                    maxCoords = a.Key;
                }
            }

            Result = maxAsteroids;

            var station = map[(8, 3)];
            station.GetVisibleMap(map, mapWidth, mapHeight);

            var goal = 200;
            while (station.NumAsteroids < goal)
            {
                goal -= station.NumAsteroids.Value;
                foreach (var a in station.VisibleMap)
                    map.Remove(a.Key);
                station.GetVisibleMap(map, mapWidth, mapHeight);
            }

            foreach (var a in station.VisibleMap.Where(x => x.Value == '#'))
            {
                map[a.Key].Angle = -Math.Atan2(a.Key.Item1 - station.Coordinate.Item1,
                    a.Key.Item2 - station.Coordinate.Item2);
            }

            var v = map.OrderBy(x => x.Value.Angle);

            var count = 0;
            var angle = 0M;

            var atans = new Dictionary<(int, int), double>();
            var currentAngle = 0D;
            foreach (var s in map)
            {
                if (s.Value.Content != '#') continue;
                
                var atan2 = Math.Atan2(station.Coordinate.Item1 - s.Key.Item1, station.Coordinate.Item2 - s.Key.Item2) - (Math.PI / 180) * currentAngle;
                atans.Add(s.Key, atan2);
            }


            var min = atans.OrderBy(x => x.Value).First();

            //for (var dx = 0; station.Coordinate.Item1 + dx)
            var dx = 0;
            for (var dy = 1; station.Coordinate.Item2 - dy >= 0; dy++)
            {
                if (map[(station.Coordinate.Item1 + dx, dy)].Content == '#')
                {
                    count++;
                    map[(station.Coordinate.Item1 + dx, dy)].Content = '.';

                }

            }
        }
    }

    public class Sector
    {
        public (int, int) Coordinate { get; set; }
        public char Content { get; set; }

        public double Angle { get; set; }

        public Dictionary<(int, int), char> VisibleMap { get; private set; }

        public int? NumAsteroids => VisibleMap?.Values.Count(x => x == '#');

        public Dictionary<(int, int), char> GetVisibleMap(Dictionary<(int, int), Sector> map, int mapWidth, int mapHeight)
        {
            var visibleMap = new Dictionary<(int, int), char>();

            for (var y = 0; y < mapHeight; y++)
            {
                for (var x = 0; x < mapWidth; x++)
                {
                    // Skip self
                    if ((x, y) == Coordinate || !map.ContainsKey((x, y))) continue;
                    var sector = map[(x, y)];

                    if (sector.Content == '#')
                    {
                        if (!visibleMap.ContainsKey((x, y)))
                            visibleMap.Add((x, y), '#');

                        var delta = (x - Coordinate.Item1, y - Coordinate.Item2);
                        var gcd = Math.Abs(delta.Item1.GreatestCommonDivider(delta.Item2));

                        delta = (delta.Item1 / gcd, delta.Item2 / gcd);

                        var dx = sector.Coordinate.Item1 + delta.Item1;
                        var dy = sector.Coordinate.Item2 + delta.Item2;
                        while (dx >= 0 && dx < mapWidth && dy >= 0 && dy < mapHeight)
                        {
                            if (visibleMap.ContainsKey((dx, dy))) visibleMap[(dx, dy)] = 'H';
                            else visibleMap.Add((dx, dy), 'H');

                            dx += delta.Item1;
                            dy += delta.Item2;
                        }
                    }
                }
            }

            VisibleMap = visibleMap;
            return visibleMap;
        }
    }
}
