using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using System.Diagnostics;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 4, "", null, null)]
    public class AdventOfCode202004 : AdventOfCodeBase
    {
        public AdventOfCode202004(string sessionCookie) : base(sessionCookie) { }
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
            return null;
        }

        private object SolvePart2()
        {
            return null;
        }
    }
}
