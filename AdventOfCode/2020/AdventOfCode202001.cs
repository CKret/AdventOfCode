using System.Linq;
using AdventOfCode.Core;

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

        public override void Solve()
        {
            data = Input.Select(int.Parse).ToArray();
            base.Solve();
        }

        protected override object SolvePart1()
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

        protected override object SolvePart2()
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
