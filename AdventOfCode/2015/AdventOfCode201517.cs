using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 17, "", 1304, 18)]
    public class AdventOfCode201517 : AdventOfCodeBase
    {
        public AdventOfCode201517(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var containers = Input.Select(int.Parse).ToList();
            return FillContainers(new List<int>(), containers, 150).ToList().Count;
        }

        protected override object SolvePart2()
        {
            var containers = Input.Select(int.Parse).ToList();

            var combinations = FillContainers(new List<int>(), containers, 150).ToList();
            var min = combinations.Min(c => c.Count);
            return combinations.Count(c => c.Count == min);
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
    }
}
