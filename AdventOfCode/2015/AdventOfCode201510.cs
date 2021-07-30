using System.Linq;
using AdventOfCode.Core;
using MoreLinq;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 10, "", 360154, 5103798)]
    public class AdventOfCode201510 : AdventOfCodeBase
    {
        public AdventOfCode201510(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            return Enumerable.Range(1, 40)
                .Aggregate("1113122113".Select(c => c - '0').ToArray(),
                    (acc, _) => acc
                        .GroupAdjacent(n => n)
                        .SelectMany(g => new[] { g.Count(), g.First() })
                        .ToArray())
                .Count();
        }

        protected override object SolvePart2()
        {
            return Enumerable.Range(1, 50)
                .Aggregate("1113122113".Select(c => c - '0').ToArray(),
                    (acc, _) => acc
                        .GroupAdjacent(n => n)
                        .SelectMany(g => new[] { g.Count(), g.First() })
                        .ToArray())
                .Count();
        }
    }
}
