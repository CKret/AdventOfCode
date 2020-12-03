using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 17, "", 1304)]
    public class AdventOfCode2015171 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var containers = File.ReadAllLines("2015\\AdventOfCode201517.txt").Select(int.Parse).ToList();

            Result = FillContainers(new List<int>(), containers, 150).ToList().Count;
        }

        public static IEnumerable<List<int>> FillContainers(IReadOnlyCollection<int> usedContainers, IReadOnlyList<int> unusedContainers, int targetVolume)
        {
            var remainingVolume = targetVolume - usedContainers.Sum();
            for (var n = 0; n < unusedContainers.Count; n++)
            {
                var s = unusedContainers[n];
                if (unusedContainers[n] > remainingVolume) continue;
                var newUsedContainers = usedContainers.ToList();
                newUsedContainers.Add(s);
                if (s == remainingVolume)
                {
                    yield return newUsedContainers;
                }
                else
                {
                    var newUnusedContainers = unusedContainers.Skip(n + 1).ToList();
                    foreach (var d in FillContainers(newUsedContainers, newUnusedContainers, targetVolume))
                    {
                        yield return d;
                    }
                }
            }
        }

        public AdventOfCode2015171(string sessionCookie) : base(sessionCookie) { }
    }
}
