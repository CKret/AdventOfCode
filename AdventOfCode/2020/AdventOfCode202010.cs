using System;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 10, "Day 10: Adapter Array", 2244, null)]
    public class AdventOfCode202010 : AdventOfCodeBase
    {
        public AdventOfCode202010(string sessionCookie) : base(sessionCookie) { }

        public override void Solve()
        {
            var timer = new Stopwatch();

            timer.Start();
            ResultPart1 = SolvePart1();
            timer.Stop();
            TimePart1 = timer.ElapsedTicks.ToMilliseconds();

            timer.Start();
            ResultPart2 = SolvePart2();
            timer.Stop();
            TimePart2 = timer.ElapsedTicks.ToMilliseconds();
        }

        private object SolvePart1()
        {
            var data = Input.Select(int.Parse).ToArray();
            Array.Sort(data);

            var maxJolts = data.Max() + 3;
            var prev = 0;
            var diffs = new int[3];

            foreach (var current in data)
            {
                diffs[current - prev - 1]++;
                prev = current;
            }

            diffs[maxJolts - data[^1] - 1]++;

            return diffs[0] * diffs[2];
        }

        private object SolvePart2()
        {
            var data = Input.Select(int.Parse).ToList();
            var maxJolts = data.Max() + 3;
            data.AddRange(new[] { 0, maxJolts });
            var jolts = data.ToArray();
            Array.Sort(jolts);

            var acc = new long[jolts.Length];
            acc[0] = 1;

            for (var i = 0; i < acc.Length - 1; i++)
            {
                for (var j = 1; j <= 3; j++)
                {
                    if (i + j >= acc.Length) continue;
                    if (jolts[i + j] <= jolts[i] + 3) acc[i + j] += acc[i];
                }
            }

            return acc.Last();
        }
    }
}
