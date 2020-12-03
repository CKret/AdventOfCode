using System.Diagnostics;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 1, "Day 1: Report Repair", 1016964L, 182588480L)]
    public class AdventOfCode202001 : AdventOfCodeBase
    {
        private const int TargetValue = 2020;

        public AdventOfCode202001(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            var data = Input.Select(int.Parse).ToArray();

            var timer = new Stopwatch();
            timer.Start();
            ResultPart1 = SolvePart1(data);
            TimePart1 = timer.ElapsedTicks.ToMilliseconds();

            timer.Restart();
            ResultPart2 = SolvePart2(data);
            timer.Stop();
            TimePart2 = timer.ElapsedTicks.ToMilliseconds();
        }

        private object SolvePart1(int[] data)
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

        private object SolvePart2(int[] data)
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
