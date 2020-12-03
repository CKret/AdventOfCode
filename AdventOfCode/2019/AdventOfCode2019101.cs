using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.Mathematics;

namespace AdventOfCode._2019
{
    /// <summary>
    /// --- Day 10: Monitoring Station ---
    /// 
    /// You fly into the asteroid belt and reach the Ceres monitoring station. The
    /// Elves here have an emergency: they're having trouble tracking all of the
    /// asteroids and can't be sure they're safe.
    /// 
    /// The Elves would like to build a new monitoring station in a nearby area of
    /// space; they hand you a map of all of the asteroids in that region (your
    /// puzzle input).
    /// 
    /// The map indicates whether each position is empty (.) or contains an
    /// asteroid (#). The asteroids are much smaller than they appear on the map,
    /// and every asteroid is exactly in the center of its marked position. The
    /// asteroids can be described with X,Y coordinates where X is the distance
    /// from the left edge and Y is the distance from the top edge (so the top-left
    /// corner is 0,0 and the position immediately to its right is 1,0).
    /// 
    /// Your job is to figure out which asteroid would be the best place to build a
    /// new monitoring station. A monitoring station can detect any asteroid to
    /// which it has direct line of sight - that is, there cannot be another
    /// asteroid exactly between them. This line of sight can be at any angle, not
    /// just lines aligned to the grid or diagonally. The best location is the
    /// asteroid that can detect the largest number of other asteroids.
    /// 
    /// For example, consider the following map:
    /// 
    /// .#..#
    /// .....
    /// #####
    /// ....#
    /// ...##
    /// 
    /// The best location for a new monitoring station on this map is the
    /// highlighted asteroid at 3,4 because it can detect 8 asteroids, more than
    /// any other location. (The only asteroid it cannot detect is the one at 1,0;
    /// its view of this asteroid is blocked by the asteroid at 2,2.) All other
    /// asteroids are worse locations; they can detect 7 or fewer other asteroids.
    /// Here is the number of other asteroids a monitoring station on each asteroid
    /// could detect:
    /// 
    /// .7..7
    /// .....
    /// 67775
    /// ....7
    /// ...87
    ///
    /// Here is an asteroid (#) and some examples of the ways its line of sight
    /// might be blocked. If there were another asteroid at the location of a
    /// capital letter, the locations marked with the corresponding lowercase
    /// letter would be blocked and could not be detected:
    /// 
    /// #.........
    /// ...A......
    /// ...B..a...
    /// .EDCG....a
    /// ..F.c.b...
    /// .....c....
    /// ..efd.c.gb
    /// .......c..
    /// ....f...c.
    /// ...e..d..c
    /// 
    /// Here are some larger examples:
    /// 
    ///  - Best is 5,8 with 33 other asteroids detected:
    /// 
    ///    ......#.#.
    ///    #..#.#....
    ///    ..#######.
    ///    .#.#.###..
    ///    .#..#.....
    ///    ..#....#.#
    ///    #..#....#.
    ///    .##.#..###
    ///    ##...#..#.
    ///    .#....####
    /// 
    ///  - Best is 1,2 with 35 other asteroids detected:
    /// 
    ///    #.#...#.#.
    ///    .###....#.
    ///    .#....#...
    ///    ##.#.#.#.#
    ///    ....#.#.#.
    ///    .##..###.#
    ///    ..#...##..
    ///    ..##....##
    ///    ......#...
    ///    .####.###.
    ///
    ///  - Best is 6,3 with 41 other asteroids detected:
    /// 
    ///    .#..#..###
    ///    ####.###.#
    ///    ....###.#.
    ///    ..###.##.#
    ///    ##.##.#.#.
    ///    ....###..#
    ///    ..#.#..#.#
    ///    #..#.#.###
    ///    .##...##.#
    ///    .....#.#..
    /// 
    ///  - Best is 11,13 with 210 other asteroids detected:
    /// 
    ///    .#..##.###...#######
    ///    ##.############..##.
    ///    .#.######.########.#
    ///    .###.#######.####.#.
    ///    #####.##.#.##.###.##
    ///    ..#####..#.#########
    ///    ####################
    ///    #.####....###.#.#.##
    ///    ##.#################
    ///    #####.##.###..####..
    ///    ..######..##.#######
    ///    ####.##.####...##..#
    ///    .#####..#.######.###
    ///    ##...#.##########...
    ///    #.##########.#######
    ///    .####.#.###.###.#.##
    ///    ....##.##.###..#####
    ///    .#.#.###########.###
    ///    #.#.#.#####.####.###
    ///    ###.##.####.##.#..##
    /// 
    /// Find the best location for a new monitoring station. How many other
    /// asteroids can be detected from that location?
    /// </summary>
    [AdventOfCode(2019, 10, "Day 10: Monitoring Station - Part 1", 267)]
    public class AdventOfCode2019101 : AdventOfCodeBase
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

            Result = station.VisibleAsteroids(asteroidField.Asteroids, mapWidth, mapHeight).Count;
        }

        public AdventOfCode2019101(string sessionCookie) : base(sessionCookie) { }
    }

    public class Sector
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Asteroid
    {
        public Sector Sector { get; set; }

        public double AngleTo(Asteroid otherAsteroid)
        {
            if (otherAsteroid == null) throw new ArgumentNullException(nameof(otherAsteroid));

            var (x, y) = (otherAsteroid.Sector.X - this.Sector.X, otherAsteroid.Sector.Y - this.Sector.Y);
            return Math.Atan2(x, y);
        }

        public double DistanceTo(Asteroid otherAsteroid)
        {
            if (otherAsteroid == null) throw new ArgumentNullException(nameof(otherAsteroid));

            var (x, y) = (otherAsteroid.Sector.X - this.Sector.X, otherAsteroid.Sector.Y - this.Sector.Y);
            return Math.Sqrt(x * x + y * y);
        }

        public (int X, int Y) DeltaTo(Asteroid otherAsteroid)
        {
            if (otherAsteroid == null) throw new ArgumentNullException(nameof(otherAsteroid));

            return (otherAsteroid.Sector.X - this.Sector.X, otherAsteroid.Sector.Y - this.Sector.Y);
        }

        public List<Asteroid> VisibleAsteroids(List<Asteroid> asteroids, int mapWidth, int mapHeight)
        {
            if (asteroids == null) throw new ArgumentNullException(nameof(asteroids));

            var visibleAsteroids = new List<Asteroid>();

            foreach (var asteroid in asteroids)
            {
                if (asteroid == this) continue;

                var delta = this.DeltaTo(asteroid);

                if (delta.X == 0) delta.Y = Math.Sign(delta.Y);         // Vertical
                else if (delta.Y == 0) delta.X = Math.Sign(delta.X);    // Horizontal
                else
                {
                    var gcd = Math.Abs(delta.X).GreatestCommonDivider(Math.Abs(delta.Y));
                    if (gcd == 1)
                    {
                        visibleAsteroids.Add(asteroid);
                        continue;
                    }

                    delta = (delta.X / gcd, delta.Y / gcd);
                }

                var dx = this.Sector.X + delta.X;
                var dy = this.Sector.Y + delta.Y;

                var isVisible = true;
                while (dx >= 0 && dx < mapWidth && dy >= 0 && dy < mapHeight && (dx, dy) != (asteroid.Sector.X, asteroid.Sector.Y))
                {
                    if (asteroids.Any(a => a.Sector.X == dx && a.Sector.Y == dy))
                    {
                        isVisible = false;
                        break;
                    }

                    dx += delta.X;
                    dy += delta.Y;
                }

                if (isVisible) visibleAsteroids.Add(asteroid);
            }

            return visibleAsteroids;
        }
    }

    public class AsteroidField
    {
        public List<Asteroid> Asteroids = new List<Asteroid>();
    }
}
