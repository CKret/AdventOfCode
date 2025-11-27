using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SuperLinq;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 10, "Elves Look, Elves Say", 360154, 5103798)]
    public class AdventOfCode201510 : AdventOfCodeBase
    {
        public AdventOfCode201510(string sessionCookie) : base(sessionCookie) { }

        protected override async Task<object> SolvePart1(CancellationToken cancellationToken)
        {
            return Enumerable.Range(1, 40)
                .Aggregate("1113122113".Select(c => c - '0').ToArray(),
                    (acc, _) => acc
                        .GroupAdjacent(n => n)
                        .SelectMany(g => new[] { g.Count(), g.First() })
                        .ToArray())
                .Length;
        }

        protected override async Task<object> SolvePart2(CancellationToken cancellationToken)
        {
            return Enumerable.Range(1, 50)
                .Aggregate("1113122113".Select(c => c - '0').ToArray(),
                    (acc, _) => acc
                        .GroupAdjacent(n => n)
                        .SelectMany(g => new[] { g.Count(), g.First() })
                        .ToArray())
                .Length;
        }
    }
}
