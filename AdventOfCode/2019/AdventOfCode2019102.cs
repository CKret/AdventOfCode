using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using static MoreLinq.Extensions.RepeatExtension;

namespace AdventOfCode._2019
{
    /// <summary>
    /// --- Day 10: Monitoring Station ---
    ///
    /// --- Part Two ---
    /// 
    /// Once you give them the coordinates, the Elves quickly deploy an Instant
    /// Monitoring Station to the location and discover the worst: there are simply
    /// too many asteroids.
    /// 
    /// The only solution is complete vaporization by giant laser.
    /// 
    /// Fortunately, in addition to an asteroid scanner, the new monitoring station
    /// also comes equipped with a giant rotating laser perfect for vaporizing
    /// asteroids. The laser starts by pointing up and always rotates clockwise,
    /// vaporizing any asteroid it hits.
    /// 
    /// If multiple asteroids are exactly in line with the station, the laser only
    /// has enough power to vaporize one of them before continuing its rotation. In
    /// other words, the same asteroids that can be detected can be vaporized, but
    /// if vaporizing one asteroid makes another one detectable, the newly-detected
    /// asteroid won't be vaporized until the laser has returned to the same
    /// position by rotating a full 360 degrees.
    /// 
    /// For example, consider the following map, where the asteroid with the new
    /// monitoring station (and laser) is marked X:
    /// 
    /// .#....#####...#..
    /// ##...##.#####..##
    /// ##...#...#.#####.
    /// ..#.....X...###..
    /// ..#.#.....#....##
    /// 
    /// The first nine asteroids to get vaporized, in order, would be:
    /// 
    /// .#....###24...#..
    /// ##...##.13#67..9#
    /// ##...#...5.8####.
    /// ..#.....X...###..
    /// ..#.#.....#....##
    ///
    /// Note that some asteroids (the ones behind the asteroids marked 1, 5, and 7)
    /// won't have a chance to be vaporized until the next full rotation. The laser
    /// continues rotating; the next nine to be vaporized are:
    /// 
    /// .#....###.....#..
    /// ##...##...#.....#
    /// ##...#......1234.
    /// ..#.....X...5##..
    /// ..#.9.....8....76
    /// 
    /// The next nine to be vaporized are then:
    /// 
    /// .8....###.....#..
    /// 56...9#...#.....#
    /// 34...7...........
    /// ..2.....X....##..
    /// ..1..............
    /// Finally, the laser completes its first full rotation (1 through 3), a
    /// second rotation (4 through 8), and vaporizes the last asteroid (9) partway
    /// through its third rotation:
    /// 
    /// ......234.....6..
    /// ......1...5.....7
    /// .................
    /// ........X....89..
    /// .................
    /// 
    /// In the large example above (the one with the best monitoring station
    /// location at 11,13):
    /// 
    ///  - The 1st asteroid to be vaporized is at 11,12.
    ///  - The 2nd asteroid to be vaporized is at 12,1.
    ///  - The 3rd asteroid to be vaporized is at 12,2.
    ///  - The 10th asteroid to be vaporized is at 12,8.
    ///  - The 20th asteroid to be vaporized is at 16,0.
    ///  - The 50th asteroid to be vaporized is at 16,9.
    ///  - The 100th asteroid to be vaporized is at 10,16.
    ///  - The 199th asteroid to be vaporized is at 9,6.
    ///  - The 200th asteroid to be vaporized is at 8,2.
    ///  - The 201st asteroid to be vaporized is at 10,9.
    ///  - The 299th and final asteroid to be vaporized is at 11,1.
    /// 
    /// The Elves are placing bets on which will be the 200th asteroid to be
    /// vaporized. Win the bet by determining which asteroid that will be; what d
    /// you get if you multiply its X coordinate by 100 and then add its Y
    /// coordinate? (For example, 8,2 becomes 802.)
    /// </summary>
    [AdventOfCode(2019, 10, 2, "Day 10: Monitoring Station - Part 2", 1309)]
    public class AdventOfCode2019102 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllLines(@"2019\AdventOfCode201910.txt");

            var map = data.Select((line, y) => line.Select((chr, x) => (X: x, Y: y, IsAsteroid: chr == '#')).ToArray()).ToArray();

            var mapWidth = map[0].Length;
            var mapHeight = map.Length;

            var asteroidField = new AsteroidField
            {
                Asteroids = map.SelectMany(s => s)
                               .Where(s => s.IsAsteroid)
                               .Select(s => new Asteroid { Sector = new Sector { X = s.X, Y = s.Y } })
                               .ToList()
            };

            var station = asteroidField.Asteroids.Aggregate((a1, a2) =>
                a1.VisibleAsteroids(asteroidField.Asteroids, mapWidth, mapHeight).Count >
                a2.VisibleAsteroids(asteroidField.Asteroids, mapWidth, mapHeight).Count ? a1 : a2);

            var queues = asteroidField.Asteroids
                                      .Where(a => a != station)
                                      .Select(a => (a.Sector, Angle: station.AngleTo(a), Distance: station.DistanceTo(a)))
                                      .ToLookup(a => a.Angle)
                                      .OrderByDescending(a => a.Key)
                                      .Select(a => new Queue<(Sector Sector, double Angle, double Distance)>(a.OrderBy(b => b.Distance)))
                                      .ToList();

            Result = queues.Repeat()
                           .SelectMany(NextInQueue)
                           .Skip(199)
                           .Take(1)
                           .Select(a => a.Sector.X * 100 + a.Sector.Y)
                           .Single();
        }

        private static IEnumerable<(Sector Sector, double Angle, double Dist)> NextInQueue(Queue<(Sector Sector, double Angle, double Dist)> queue)
        {
            if (queue.Count > 0)
                yield return queue.Dequeue();
        }

        public AdventOfCode2019102(string sessionCookie) : base(sessionCookie) { }
    }
}
