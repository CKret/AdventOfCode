using System.Collections.Generic;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 9, "Day 9: Encoding Error", 18272118, 2186361)]
    public class AdventOfCode202009 : AdventOfCodeBase
    {
        public AdventOfCode202009(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            var timer = new Stopwatch();
            var data = Input;

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
            var index = 0;
            while (true)
            {
                var window = Input.Skip(index).Take(26).Select(int.Parse).ToArray();
                if (!IsSum(window))
                {
                    return (long) window.Last();
                }

                index++;
            }

            return 0;
        }

        private object SolvePart2()
        {
            var length = 2;
            while (true)
            {
                for (var index = 0; index < Input.Length - length; index++)
                {
                    var nums = Input.Skip(index).Take(length).Select(long.Parse).ToArray();
                    if (nums.Sum() == (long) ResultPart1)
                    {
                        return nums.Min() + nums.Max();
                    }
                }

                length++;
            }
        }

        private bool IsSum(IEnumerable<int> window)
        {
            var preamble = window.Take(25).ToArray();
            var num = window.Last();

            for (var a = 0; a < preamble.Length - 1; a++)
            {
                for (var b = a + 1; b < preamble.Length; b++)
                {
                    if (preamble[a] + preamble[b] == num) return true;
                }
            }

            return false;
        }
    }
}
