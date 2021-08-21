﻿using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Core;
using Tesseract;

namespace AdventOfCode._2018
{
    /// <summary>
    /// --- Day 10: The Stars Align ---
    ///
    /// It's no use; your navigation system simply isn't capable of providing
    /// walking directions in the arctic circle, and certainly not in 1018.
    /// 
    /// The Elves suggest an alternative. In times like these, North Pole rescue
    /// operations will arrange points of light in the sky to guide missing Elves
    /// back to base. Unfortunately, the message is easy to miss: the points move
    /// slowly enough that it takes hours to align them, but have so much momentum
    /// that they only stay aligned for a second. If you blink at the wrong time,
    /// it might be hours before another message appears.
    /// 
    /// You can see these points of light floating in the distance, and record
    /// their position in the sky and their velocity, the relative change in
    /// position per second (your puzzle input). The coordinates are all given from
    /// your perspective; given enough time, those positions and velocities will
    /// move the points into a cohesive message!
    /// 
    /// Rather than wait, you decide to fast-forward the process and calculate what
    /// the points will eventually spell.
    /// 
    /// For example, suppose you note the following points:
    /// 
    /// position=< 9,  1> velocity=< 0,  2>
    /// position=< 7,  0> velocity=<-1,  0>
    /// position=< 3, -2> velocity=<-1,  1>
    /// position=< 6, 10> velocity=<-2, -1>
    /// position=< 2, -4> velocity=< 2,  2>
    /// position=<-6, 10> velocity=< 2, -2>
    /// position=< 1,  8> velocity=< 1, -1>
    /// position=< 1,  7> velocity=< 1,  0>
    /// position=<-3, 11> velocity=< 1, -2>
    /// position=< 7,  6> velocity=<-1, -1>
    /// position=<-2,  3> velocity=< 1,  0>
    /// position=<-4,  3> velocity=< 2,  0>
    /// position=<10, -3> velocity=<-1,  1>
    /// position=< 5, 11> velocity=< 1, -2>
    /// position=< 4,  7> velocity=< 0, -1>
    /// position=< 8, -2> velocity=< 0,  1>
    /// position=<15,  0> velocity=<-2,  0>
    /// position=< 1,  6> velocity=< 1,  0>
    /// position=< 8,  9> velocity=< 0, -1>
    /// position=< 3,  3> velocity=<-1,  1>
    /// position=< 0,  5> velocity=< 0, -1>
    /// position=<-2,  2> velocity=< 2,  0>
    /// position=< 5, -2> velocity=< 1,  2>
    /// position=< 1,  4> velocity=< 2,  1>
    /// position=<-2,  7> velocity=< 2, -2>
    /// position=< 3,  6> velocity=<-1, -1>
    /// position=< 5,  0> velocity=< 1,  0>
    /// position=<-6,  0> velocity=< 2,  0>
    /// position=< 5,  9> velocity=< 1, -2>
    /// position=<14,  7> velocity=<-2,  0>
    /// position=<-3,  6> velocity=< 2, -1>
    /// 
    /// Each line represents one point. Positions are given as <X, Y> pairs: X
    /// represents how far left (negative) or right (positive) the point appears,
    /// while Y represents how far up (negative) or down (positive) the point
    /// appears.
    /// 
    /// At 0 seconds, each point has the position given. Each second, each point's
    /// velocity is added to its position. So, a point with velocity <1, -2> is
    /// moving to the right, but is moving upward twice as quickly. If this point's
    /// initial position were <3, 9>, after 3 seconds, its position would become
    /// <6, 3>.
    /// 
    /// Over time, the points listed above would move like this:
    ///
    /// Initially:
    /// ........#.............
    /// ................#.....
    /// .........#.#..#.......
    /// ......................
    /// #..........#.#.......#
    /// ...............#......
    /// ....#.................
    /// ..#.#....#............
    /// .......#..............
    /// ......#...............
    /// ...#...#.#...#........
    /// ....#..#..#.........#.
    /// .......#..............
    /// ...........#..#.......
    /// #...........#.........
    /// ...#.......#..........
    /// 
    /// After 1 second:
    /// ......................
    /// ......................
    /// ..........#....#......
    /// ........#.....#.......
    /// ..#.........#......#..
    /// ......................
    /// ......#...............
    /// ....##.........#......
    /// ......#.#.............
    /// .....##.##..#.........
    /// ........#.#...........
    /// ........#...#.....#...
    /// ..#...........#.......
    /// ....#.....#.#.........
    /// ......................
    /// ......................
    /// 
    /// After 2 seconds:
    /// ......................
    /// ......................
    /// ......................
    /// ..............#.......
    /// ....#..#...####..#....
    /// ......................
    /// ........#....#........
    /// ......#.#.............
    /// .......#...#..........
    /// .......#..#..#.#......
    /// ....#....#.#..........
    /// .....#...#...##.#.....
    /// ........#.............
    /// ......................
    /// ......................
    /// ......................
    /// 
    /// After 3 seconds:
    /// ......................
    /// ......................
    /// ......................
    /// ......................
    /// ......#...#..###......
    /// ......#...#...#.......
    /// ......#...#...#.......
    /// ......#####...#.......
    /// ......#...#...#.......
    /// ......#...#...#.......
    /// ......#...#...#.......
    /// ......#...#..###......
    /// ......................
    /// ......................
    /// ......................
    /// ......................
    /// 
    /// After 4 seconds:
    /// ......................
    /// ......................
    /// ......................
    /// ............#.........
    /// ........##...#.#......
    /// ......#.....#..#......
    /// .....#..##.##.#.......
    /// .......##.#....#......
    /// ...........#....#.....
    /// ..............#.......
    /// ....#......#...#......
    /// .....#.....##.........
    /// ...............#......
    /// ...............#......
    /// ......................
    /// ......................
    /// After 3 seconds, the message appeared briefly: HI. Of course, your message
    /// will be much longer and will take many more seconds to appear.
    /// 
    /// What message will eventually appear in the sky?
    /// 
    /// --- Part Two ---
    /// 
    /// Good thing you didn't have to wait, because that would have taken a long
    /// time - much longer than the 3 seconds in the example above.
    /// 
    /// Impressed by your sub-hour communication capabilities, the Elves are
    /// curious: exactly how many seconds would they have needed to wait for that
    /// message to appear?
    /// </summary>
    // ReSharper disable once StringLiteralTypo
    [AdventOfCode(2018, 10, "The Stars Align ", "RPNNXFZR", 10946)]
    public class AdventOfCode201810 : AdventOfCodeBase
    {
        public AdventOfCode201810(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var lights = File.ReadAllLines(@"2018\AdventOfCode201810.txt")
                             .Select(line => Regex.Matches(line, @"-?\d+"))
                             .Select(matches => new LightSignal
                             {
                                 PositionX = int.Parse(matches[0].Value, CultureInfo.InvariantCulture),
                                 PositionY = int.Parse(matches[1].Value, CultureInfo.InvariantCulture),
                                 VelocityX = int.Parse(matches[2].Value, CultureInfo.InvariantCulture),
                                 VelocityY = int.Parse(matches[3].Value, CultureInfo.InvariantCulture)
                             })
                             .ToList();

            var previousMinY = long.MaxValue;

            while (true)
            {
                long currentY = (lights.Max(x => x.PositionY) - lights.Min(x => x.PositionY));
                if (currentY > previousMinY) break;

                previousMinY = currentY;

                foreach (var l in lights)
                {
                    l.PositionX += l.VelocityX;
                    l.PositionY += l.VelocityY;
                }
            }

            foreach (var l in lights)
            {
                l.PositionX -= l.VelocityX;
                l.PositionY -= l.VelocityY;
            }

            var translateX = 0 - lights.Min(x => x.PositionX);
            var translateY = 0 - lights.Min(x => x.PositionY);
            var width = lights.Max(x => x.PositionX) + translateX + 1 + 20;
            var height = lights.Max(x => x.PositionY) + translateY + 1 + 20;

            using var image = new Bitmap(width, height);
            foreach (var l in lights)
            {
                image.SetPixel(l.PositionX + translateX + 10, l.PositionY + translateY + 10, Color.Blue);
            }

            // ReSharper disable once StringLiteralTypo
            using var engine = new TesseractEngine(@".\_ExternalDependencies\tessdata_legacy", "eng", EngineMode.TesseractOnly);    // Uses legaxy OCR. To use LSTM neural network use \tessdata_ltsm.
            using var pix = PixConverter.ToPix(image);
            using var page = engine.Process(pix);
            return page.GetText().Trim('\n');
        }

        protected override object SolvePart2()
        {
            var lights = File.ReadAllLines(@"2018\AdventOfCode201810.txt")
                             .Select(line => Regex.Matches(line, @"-?\d+"))
                             .Select(matches => new LightSignal
                             {
                                 PositionX = int.Parse(matches[0].Value, CultureInfo.InvariantCulture),
                                 PositionY = int.Parse(matches[1].Value, CultureInfo.InvariantCulture),
                                 VelocityX = int.Parse(matches[2].Value, CultureInfo.InvariantCulture),
                                 VelocityY = int.Parse(matches[3].Value, CultureInfo.InvariantCulture)
                             })
                             .ToList();

            var previousMinY = long.MaxValue;
            var messageTime = 0;

            while (true)
            {
                long currentY = (lights.Max(x => x.PositionY) - lights.Min(x => x.PositionY));
                if (currentY > previousMinY) break;

                previousMinY = currentY;

                foreach (var l in lights)
                {
                    l.PositionX += l.VelocityX;
                    l.PositionY += l.VelocityY;
                }

                messageTime++;
            }

            return --messageTime;
        }
    }

    public class LightSignal
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int VelocityX { get; set; }
        public int VelocityY { get; set; }
    }
}
