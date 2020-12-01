using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Core;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 12, 1, "", 12466L)]
    public class AdventOfCode2019121 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllLines(@"2019\AdventOfCode201912.txt");

            var moons = new List<Moon>();

            foreach (var line in data)
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

            Result = moons.Sum(m => m.Energy());
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

        public AdventOfCode2019121(string sessionCookie) : base(sessionCookie) { }
    }
}
