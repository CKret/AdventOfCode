using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 7, "Day 7: Handy Haversacks", 208, 1664)]
    public class AdventOfCode202007 : AdventOfCodeBase
    {
        public AdventOfCode202007(string sessionCookie) : base(sessionCookie) { }
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
            var bags = Input.Select(x => Regex.Match(x, @"(.*) bags contain(?: (\d+ .*?) bags?[,.])*"))
                         .ToDictionary(
                             m => m.Groups[1].Value,                    // Bag color
                             m => m.Groups[2].Captures
                                   .Select(b => b.Value.Split(' ', 2))
                                   .ToDictionary(
                                       x => x[1],                       // Child bag color
                                       x => int.Parse(x[0])             // Child bag count
                                   )
                         );

            bool ParentBags(string colour) => bags[colour].ContainsKey("shiny gold") || bags[colour].Keys.Any(ParentBags);
            return bags.Keys.Count(ParentBags);
        }

        private object SolvePart2()
        {
            var bags = Input.Select(x => Regex.Match(x, @"(.*) bags contain(?: (\d+ .*?) bags?[,.])*"))
                            .ToDictionary(
                                m => m.Groups[1].Value,                    // Bag color
                                m => m.Groups[2].Captures
                                      .Select(b => b.Value.Split(' ', 2))
                                      .ToDictionary(
                                          x => x[1],                       // Child bag color
                                          x => int.Parse(x[0])             // Child bag count
                                      )
                            );

            int CountChildBags(string colour) => bags[colour].Sum(bag => bag.Value * (1 + CountChildBags(bag.Key)));
            return CountChildBags("shiny gold");
        }
    }
}
