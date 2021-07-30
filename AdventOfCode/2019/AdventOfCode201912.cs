using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Core;
using AdventOfCode.Mathematics;
using MoreLinq.Extensions;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 12, "", 12466L, 360689156787864L)]
    public class AdventOfCode201912 : AdventOfCodeBase
    {
        public AdventOfCode201912(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var moons = new List<Moon>();

            foreach (var line in Input)
            {
                var regEx = Regex.Matches(line, @"-?\d+");
                var position = new Point3D
                {
                    X = int.Parse(regEx[0].Value, CultureInfo.CurrentCulture),
                    Y = int.Parse(regEx[1].Value, CultureInfo.CurrentCulture),
                    Z = int.Parse(regEx[2].Value, CultureInfo.CurrentCulture)
                };
                moons.Add(new Moon { Position = position, Velocity = new Point3D() });
            }

            for (var step = 0; step < 1000; step++)
            {
                for (var m1 = 0; m1 < moons.Count - 1; m1++)
                {
                    for (var m2 = m1 + 1; m2 < moons.Count; m2++)
                    {
                        if (moons[m1].Position.X != moons[m2].Position.X)
                        {
                            moons[m1].Velocity.X += moons[m1].Position.X < moons[m2].Position.X ? 1 : -1;
                            moons[m2].Velocity.X += moons[m2].Position.X < moons[m1].Position.X ? 1 : -1;
                        }

                        if (moons[m1].Position.Y != moons[m2].Position.Y)
                        {
                            moons[m1].Velocity.Y += moons[m1].Position.Y < moons[m2].Position.Y ? 1 : -1;
                            moons[m2].Velocity.Y += moons[m2].Position.Y < moons[m1].Position.Y ? 1 : -1;
                        }

                        if (moons[m1].Position.Z != moons[m2].Position.Z)
                        {
                            moons[m1].Velocity.Z += moons[m1].Position.Z < moons[m2].Position.Z ? 1 : -1;
                            moons[m2].Velocity.Z += moons[m2].Position.Z < moons[m1].Position.Z ? 1 : -1;
                        }
                    }
                }

                foreach (var moon in moons)
                {
                    moon.Position.X += moon.Velocity.X;
                    moon.Position.Y += moon.Velocity.Y;
                    moon.Position.Z += moon.Velocity.Z;
                }
            }

            return moons.Sum(m => m.Energy());
        }

        protected override object SolvePart2()
        {
            var moons = new List<Moon>();

            var xCycle = 0L;
            var yCycle = 0L;
            var zCycle = 0L;

            foreach (var line in Input)
            {
                var regEx = Regex.Matches(line, @"-?\d+");
                var position = new Point3D
                {
                    X = int.Parse(regEx[0].Value, CultureInfo.CurrentCulture),
                    Y = int.Parse(regEx[1].Value, CultureInfo.CurrentCulture),
                    Z = int.Parse(regEx[2].Value, CultureInfo.CurrentCulture)
                };
                moons.Add(new Moon { Position = position, Velocity = new Point3D() });
            }

            var firstXState = GetXPositions(moons);
            var firstYState = GetYPositions(moons);
            var firstZState = GetZPositions(moons);

            var count = 0L;
            while (true)
            {
                for (var m1 = 0; m1 < moons.Count - 1; m1++)
                {
                    for (var m2 = m1 + 1; m2 < moons.Count; m2++)
                    {
                        if (moons[m1].Position.X != moons[m2].Position.X)
                        {
                            moons[m1].Velocity.X += moons[m1].Position.X < moons[m2].Position.X ? 1 : -1;
                            moons[m2].Velocity.X += moons[m2].Position.X < moons[m1].Position.X ? 1 : -1;
                        }

                        if (moons[m1].Position.Y != moons[m2].Position.Y)
                        {
                            moons[m1].Velocity.Y += moons[m1].Position.Y < moons[m2].Position.Y ? 1 : -1;
                            moons[m2].Velocity.Y += moons[m2].Position.Y < moons[m1].Position.Y ? 1 : -1;
                        }

                        if (moons[m1].Position.Z != moons[m2].Position.Z)
                        {
                            moons[m1].Velocity.Z += moons[m1].Position.Z < moons[m2].Position.Z ? 1 : -1;
                            moons[m2].Velocity.Z += moons[m2].Position.Z < moons[m1].Position.Z ? 1 : -1;
                        }
                    }
                }

                count++;

                foreach (var moon in moons)
                {
                    moon.Position.X += moon.Velocity.X;
                    moon.Position.Y += moon.Velocity.Y;
                    moon.Position.Z += moon.Velocity.Z;
                }

                if (xCycle == 0 && GetXPositions(moons) == firstXState) xCycle = count;
                if (yCycle == 0 && GetYPositions(moons) == firstYState) yCycle = count;
                if (zCycle == 0 && GetZPositions(moons) == firstZState) zCycle = count;

                if (xCycle > 0 && yCycle > 0 && zCycle > 0) break;
            }

            return LeastCommonMultiple(new[] { xCycle, yCycle, zCycle });
        }


        private (long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long) GetAllPositions(List<Moon> moons)
        {
            return (moons[0].Position.X, moons[0].Position.Y, moons[0].Position.Z, moons[0].Velocity.X, moons[0].Velocity.Y, moons[0].Velocity.Z,
                moons[1].Position.X, moons[1].Position.Y, moons[1].Position.Z, moons[1].Velocity.X, moons[1].Velocity.Y, moons[1].Velocity.Z,
                moons[2].Position.X, moons[2].Position.Y, moons[2].Position.Z, moons[2].Velocity.X, moons[2].Velocity.Y, moons[2].Velocity.Z,
                moons[3].Position.X, moons[3].Position.Y, moons[3].Position.Z, moons[3].Velocity.X, moons[3].Velocity.Y, moons[3].Velocity.Z);
        }

        private (long, long, long, long, long, long, long, long) GetXPositions(List<Moon> moons)
        {
            return (moons[0].Position.X, moons[0].Velocity.X, moons[1].Position.X, moons[1].Velocity.X, moons[2].Position.X, moons[2].Velocity.X, moons[3].Position.X, moons[3].Velocity.X);
        }

        private (long, long, long, long, long, long, long, long) GetYPositions(List<Moon> moons)
        {
            return (moons[0].Position.Y, moons[0].Velocity.Y, moons[1].Position.Y, moons[1].Velocity.Y, moons[2].Position.Y, moons[2].Velocity.Y, moons[3].Position.Y, moons[3].Velocity.Y);
        }

        private (long, long, long, long, long, long, long, long) GetZPositions(List<Moon> moons)
        {
            return (moons[0].Position.Z, moons[0].Velocity.Z, moons[1].Position.Z, moons[1].Velocity.Z, moons[2].Position.Z, moons[2].Velocity.Z, moons[3].Position.Z, moons[3].Velocity.Z);
        }
        
        private class Moon
        {
            public Point3D Position { get; set; }
            public Point3D Velocity { get; set; }

            public long Energy()
            {
                return (Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z)) *
                       (Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z));
            }
        }

        private class Point3D
        {
            public long X { get; set; }
            public long Y { get; set; }
            public long Z { get; set; }
        }

        public static long LeastCommonMultiple(long[] numbers)
        {
            return numbers.Aggregate(LeastCommonMultiple);
        }

        public static long LeastCommonMultiple(long a, long b)
        {
            return Math.Abs(a * b) / a.GreatestCommonDivider(b);
        }
    }
}
