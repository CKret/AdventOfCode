using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 1, "Report Repair", 1016964L, 182588480L)]
    public class AdventOfCode202001 : AdventOfCodeBase
    {
        private const int TargetValue = 2020;
        private int[] data;

        public AdventOfCode202001(string sessionCookie) : base(sessionCookie) { }

        public override async Task SolveAsync(CancellationToken cancellationToken)
        {
            data = Input.Select(int.Parse).ToArray();
            await base.SolveAsync(cancellationToken);
        }

        protected override async Task<object> SolvePart1(CancellationToken cancellationToken)
        {
            foreach (var a in data)
            {
                if (data.Contains(TargetValue - a))
                {
                    return a * (TargetValue - a);
                }
            }

            return null;
        }

        protected override async Task<object> SolvePart2(CancellationToken cancellationToken)
        {
            for (var i = 0; i < data.Length - 2; i++)
            {
                var a = data[i];
                for (var j = i + 1; j < data.Length - 1; j++)
                {
                    var b = data[j];
                    if (a + b >= TargetValue) continue;

                    if (data.Contains((TargetValue - a - b)))
                    {
                        return a * b * (TargetValue - a - b);
                    }
                }
            }

            return null;
        }
    }
}
