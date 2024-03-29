using System;
using AdventOfCode.Core;
using System.Linq;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 10, "Adapter Array", 2244L, 3947645370368L)]
    public class AdventOfCode202010 : AdventOfCodeBase
    {
        public AdventOfCode202010(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var jolts = Input.Select(int.Parse).ToArray();
            Array.Sort(jolts);

            var maxJolts = jolts.Max() + 3;
            var prev = 0;
            var diffs = new int[3];

            foreach (var current in jolts)
            {
                diffs[current - prev - 1]++;
                prev = current;
            }

            diffs[maxJolts - jolts[^1] - 1]++;

            return diffs[0] * diffs[2];
        }

        protected override object SolvePart2()
        {
            var jolts = Input.Select(int.Parse).ToArray();
            var maxJolts = jolts.Max() + 3;
            jolts = jolts.Concat(new[] { 0, maxJolts }).ToArray();
            Array.Sort(jolts);

            var acc = new long[jolts.Length];
            acc[0] = 1;

            for (var i = 0; i < acc.Length - 1; i++)
            {
                for (var j = 1; j <= 3; j++)
                {
                    if (i + j >= acc.Length) break;
                    if (jolts[i + j] <= jolts[i] + 3) acc[i + j] += acc[i];
                }
            }

            return acc.Last();
        }
    }
}
