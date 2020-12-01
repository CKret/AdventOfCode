using System.IO;
using AdventOfCode.Core;
using System.Collections.Generic;
using AdventOfCode.VMs;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 15, 2, "", null)]
    public class AdventOfCode2019152 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201915.txt");

            var map = new HashSet<(long, long)> { (0, 0) };
            var droids = new Queue<Droid>();
            droids.Enqueue(new Droid(0, 0, 0, new IntcodeVM(data)));

            Droid current = null;
            Droid oxygen = null;
            while (droids.Count > 0)
            {
                current = droids.Dequeue();

                for (var i = 1; i <= 4; i++)
                {
                    var next = current.TryGoStep((Droid.Direction)i, map);
                    if (next == null) continue; // Wall
                    if (next.Finsihed)          // Oxygen
                    {
                        oxygen = next;
                        droids.Clear();
                        break;
                    }

                    droids.Enqueue(next);       // Open path
                }
            }

            map.Clear();
            oxygen.Steps = 0;

            var filled = new Queue<Droid>();
            filled.Enqueue(oxygen);

            current = null;
            while (filled.Count > 0)
            {
                current = filled.Dequeue();

                for (var i = 1; i <= 4; i++)
                {
                    var next = current.TryGoStep((Droid.Direction) i, map);
                    if (next == null) continue;

                    filled.Enqueue(next);
                }
            }

            Result = current.Steps;
        }

        public AdventOfCode2019152(string sessionCookie) : base(sessionCookie) { }
    }
}
