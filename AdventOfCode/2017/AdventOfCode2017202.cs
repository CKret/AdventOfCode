using System.Collections.Generic;
using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 20: Particle Swarm ---
    /// 
    /// --- Part Two ---
    /// 
    /// To simplify the problem further, the GPU would like to remove any particles
    /// that collide. Particles collide if their positions ever exactly match.
    /// Because particles are updated simultaneously, more than two particles can
    /// collide at the same time and place. Once particles collide, they are
    /// removed and cannot collide with anything else after that tick.
    /// 
    /// For example:
    /// 
    /// p=&lt;-6,0,0&gt;, v=&lt; 3,0,0&gt;, a=&lt; 0,0,0&gt;    
    /// p=&lt;-4,0,0&gt;, v=&lt; 2,0,0&gt;, a=&lt; 0,0,0&gt;    -6 -5 -4 -3 -2 -1  0  1  2  3
    /// p=&lt;-2,0,0&gt;, v=&lt; 1,0,0&gt;, a=&lt; 0,0,0&gt;    (0)   (1)   (2)            (3)
    /// p=&lt; 3,0,0&gt;, v=&lt;-1,0,0&gt;, a=&lt; 0,0,0&gt;
    /// 
    /// p=&lt;-3,0,0&gt;, v=&lt; 3,0,0&gt;, a=&lt; 0,0,0&gt;    
    /// p=&lt;-2,0,0&gt;, v=&lt; 2,0,0&gt;, a=&lt; 0,0,0&gt;    -6 -5 -4 -3 -2 -1  0  1  2  3
    /// p=&lt;-1,0,0&gt;, v=&lt; 1,0,0&gt;, a=&lt; 0,0,0&gt;             (0)(1)(2)      (3)   
    /// p=&lt; 2,0,0&gt;, v=&lt;-1,0,0&gt;, a=&lt; 0,0,0&gt;
    /// 
    /// p=&lt; 0,0,0&gt;, v=&lt; 3,0,0&gt;, a=&lt; 0,0,0&gt;    
    /// p=&lt; 0,0,0&gt;, v=&lt; 2,0,0&gt;, a=&lt; 0,0,0&gt;    -6 -5 -4 -3 -2 -1  0  1  2  3
    /// p=&lt; 0,0,0&gt;, v=&lt; 1,0,0&gt;, a=&lt; 0,0,0&gt;                       X (3)      
    /// p=&lt; 1,0,0&gt;, v=&lt;-1,0,0&gt;, a=&lt; 0,0,0&gt;
    /// 
    /// ------destroyed by collision------    
    /// ------destroyed by collision------    -6 -5 -4 -3 -2 -1  0  1  2  3
    /// ------destroyed by collision------                      (3)         
    /// p=&lt; 0,0,0&gt;, v=&lt;-1,0,0&gt;, a=&lt; 0,0,0&gt;
    /// 
    /// In this example, particles 0, 1, and 2 are simultaneously destroyed at the
    /// time and place marked X. On the next tick, particle 3 passes through
    /// unharmed.
    /// 
    /// How many particles are left after all collisions are resolved?
    /// 
    /// </summary>
    [AdventOfCode(2017, 20, 2, "Particle Swarm - Part Two", 648)]
    public class AdventOfCode2017202 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var particles = AdventOfCode2017201.ParseInput(File.ReadAllLines("2017\\AdventOfCode201720.txt"));

            // A mnore precise solution is to check if any trajectories are intersecting.
            // Looping 40 times will be sufficient for now.
            var count = 40;
            while (count-- > 0)
            {
                var collision = new List<int>();

                for (var i = 0; i < particles.Count - 1; i++)
                {
                    for (var j = i + 1; j < particles.Count; j++)
                    {
                        if (particles[i].ComparePosition(particles[j]))
                        {
                            if (!collision.Contains(particles[i].Id)) collision.Add(particles[i].Id);
                            if (!collision.Contains(particles[j].Id)) collision.Add(particles[j].Id);
                        }
                    }
                }

                particles.RemoveAll(p => collision.Contains(p.Id));

                foreach (var particle in particles)
                {
                    particle.Move();
                }
            }

            Result = particles.Count;
        }

        public AdventOfCode2017202(string sessionCookie) : base(sessionCookie) { }
    }
}
