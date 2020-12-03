using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 20: Particle Swarm ---
    /// 
    /// Suddenly, the GPU contacts you, asking for help. Someone has asked it to
    /// simulate too many particles, and it won't be able to finish them all in
    /// time to render the next frame at this rate.
    /// 
    /// It transmits to you a buffer (your puzzle input) listing each particle in
    /// order (starting with particle 0, then particle 1, particle 2, and so on).
    /// For each particle, it provides the X, Y, and Z coordinates for the
    /// particle's position (p), velocity (v), and acceleration (a), each in the
    /// format &lt;X,Y,Z&gt;.
    /// 
    /// Each tick, all particles are updated simultaneously. A particle's
    /// properties are updated in the following order:
    /// 
    ///     - Increase the X velocity by the X acceleration.
    ///     - Increase the Y velocity by the Y acceleration.
    ///     - Increase the Z velocity by the Z acceleration.
    ///     - Increase the X position by the X velocity.
    ///     - Increase the Y position by the Y velocity.
    ///     I- ncrease the Z position by the Z velocity.
    /// 
    /// Because of seemingly tenuous rationale involving z-buffering, the GPU would
    /// like to know which particle will stay closest to position &lt;0,0,0&gt; in the
    /// long term. Measure this using the Manhattan distance, which in this
    /// situation is simply the sum of the absolute values of a particle's X, Y,
    /// and Z position.
    /// 
    /// For example, suppose you are only given two particles, both of which stay
    /// entirely on the X-axis (for simplicity). Drawing the current states of
    /// particles 0 and 1 (in that order) with an adjacent a number line and
    /// diagram of current X positions (marked in parenthesis), the following would
    /// take place:
    /// 
    /// p=&lt; 3,0,0&gt;, v=&lt; 2,0,0&gt;, a=&lt;-1,0,0&gt;    -4 -3 -2 -1  0  1  2  3  4
    /// p=&lt; 4,0,0&gt;, v=&lt; 0,0,0&gt;, a=&lt;-2,0,0&gt;                         (0)(1)
    /// 
    /// p=&lt; 4,0,0&gt;, v=&lt; 1,0,0&gt;, a=&lt;-1,0,0&gt;    -4 -3 -2 -1  0  1  2  3  4
    /// p=&lt; 2,0,0&gt;, v=&lt;-2,0,0&gt;, a=&lt;-2,0,0&gt;                      (1)   (0)
    /// 
    /// p=&lt; 4,0,0&gt;, v=&lt; 0,0,0&gt;, a=&lt;-1,0,0&gt;    -4 -3 -2 -1  0  1  2  3  4
    /// p=&lt;-2,0,0&gt;, v=&lt;-4,0,0&gt;, a=&lt;-2,0,0&gt;          (1)               (0)
    /// 
    /// p=&lt; 3,0,0&gt;, v=&lt;-1,0,0&gt;, a=&lt;-1,0,0&gt;    -4 -3 -2 -1  0  1  2  3  4
    /// p=&lt;-8,0,0&gt;, v=&lt;-6,0,0&gt;, a=&lt;-2,0,0&gt;                         (0)   
    /// 
    /// At this point, particle 1 will never be closer to &lt;0,0,0&gt; than particle 0,
    /// and so, in the long run, particle 0 will stay closest.
    /// 
    /// Which particle will stay closest to position &lt;0,0,0&gt; in the long term?
    /// 
    /// </summary>
    [AdventOfCode(2017, 20, "Particle Swarm - Part One", 243)]
    public class AdventOfCode2017201 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var particles = ParseInput(File.ReadAllLines("2017\\AdventOfCode201720.txt"));

            var minAcc = particles.Min(p => p.GetManhattanAcceleration());
            var closestDistance = particles.Where(p => p.GetManhattanAcceleration() == minAcc).Min(p => p.GetManhattanDistance());
            Result = particles.Single(p => p.GetManhattanAcceleration() == minAcc && p.GetManhattanDistance() == closestDistance).Id;
        }

        public static List<Particle> ParseInput(string[] lines)
        {
            var particles = new List<Particle>();

            for (var i = 0; i < lines.Length; i++)
            {
                var particleData = lines[i].Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                var splits = particleData[0].Split(',');
                var x = int.Parse(splits[0].TrimStart('p', '=', '<'), CultureInfo.InvariantCulture);
                var y = int.Parse(splits[1], CultureInfo.InvariantCulture);
                var z = int.Parse(splits[2].TrimEnd('>'), CultureInfo.InvariantCulture);

                splits = particleData[1].Split(',');
                var vx = int.Parse(splits[0].TrimStart('v', '=', '<'), CultureInfo.InvariantCulture);
                var vy = int.Parse(splits[1], CultureInfo.InvariantCulture);
                var vz = int.Parse(splits[2].TrimEnd('>'), CultureInfo.InvariantCulture);

                splits = particleData[2].Split(',');
                var ax = int.Parse(splits[0].TrimStart('a', '=', '<'), CultureInfo.InvariantCulture);
                var ay = int.Parse(splits[1], CultureInfo.InvariantCulture);
                var az = int.Parse(splits[2].TrimEnd('>'), CultureInfo.InvariantCulture);

                var particle = new Particle { Id = i, X = x, Y = y, Z = z, VelX = vx, VelY = vy, VelZ = vz, AccX = ax, AccY = ay, AccZ = az };
                particles.Add(particle);
            }

            return particles;
        }

        public AdventOfCode2017201(string sessionCookie) : base(sessionCookie) { }
    }

    public class Particle
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public int VelX { get; set; }
        public int VelY { get; set; }
        public int VelZ { get; set; }

        public int AccX { get; set; }
        public int AccY { get; set; }
        public int AccZ { get; set; }

        public void Move()
        {
            VelX += AccX;
            VelY += AccY;
            VelZ += AccZ;

            X += VelX;
            Y += VelY;
            Z += VelZ;
        }

        public int GetManhattanDistance(int x = 0, int y = 0, int z = 0)
        {
            return GetManhattanDistance(new Particle { X = x, Y = y, Z = z });
        }

        public int GetManhattanDistance(Particle p)
        {
            return Math.Abs(X - p.X) + Math.Abs(Y - p.Y) + Math.Abs(Z - p.Z);
        }

        public int GetManhattanAcceleration()
        {
            return Math.Abs(AccX) + Math.Abs(AccY) + Math.Abs(AccZ);
        }

        public bool ComparePosition(Particle other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }
    }
}
