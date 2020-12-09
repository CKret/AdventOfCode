using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 6, "Day 6: Custom Customs", 6430, 3125)]
    public class AdventOfCode202006 : AdventOfCodeBase
    {
        public AdventOfCode202006(string sessionCookie) : base(sessionCookie) { }
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
            return string.Join(" ", Input)
                         .Split("  ")
                         .Sum(g => g.Replace(" ", "").ToCharArray().Distinct().Count());
        }

        private object SolvePart2()
        {

            return string.Join(" ", Input)
                         .Split("  ")
                         .Select(group => group.Split(" "))
                         .Select(group => group.First().Count(chr => group.All(person => person.Contains(chr))))
                         .Sum();
        }
    }
}
