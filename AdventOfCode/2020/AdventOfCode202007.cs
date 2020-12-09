using System.Collections.Generic;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using System.Diagnostics;
using System.Linq;

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
            var bags = Input.Select(b => b.Split(" contain "))
                            .Select(c => new { Bag = c[0].TrimEnd(" bags"), Contains = c[1].Split(", ").Select(x => x.TrimEnd('.').TrimEnd(" bag").TrimEnd(" bags")).ToArray() })
                            .ToDictionary(a => a.Bag, b => b.Contains);


            return GetParentBags(bags, "shiny gold").Distinct().Count(); ;
        }

        private object SolvePart2()
        {
            var bags = Input.Select(b => b.Split(" contain "))
                            .Select(c => new { Bag = c[0].TrimEnd(" bags"), Contains = c[1].Split(", ").Select(x => x.TrimEnd('.').TrimEnd(" bag").TrimEnd(" bags")).ToArray() })
                            .ToDictionary(a => a.Bag, b => b.Contains);


            var gold = GetChildBags(bags, "shiny gold");

            return gold;
        }

        private IEnumerable<string> GetParentBags(Dictionary<string, string[]> bags, string bag)
        {
            var parents = bags.Where(b => b.Value.Any(c => c.Contains(bag))).Select(b => b.Key).ToArray();

            foreach (var parent in parents)
            {
                yield return parent;

                foreach (var parent2 in GetParentBags(bags, parent))
                {
                    yield return parent2;
                }
            }
        }

        private int GetChildBags(Dictionary<string, string[]> bags, string currentBag)
        {
            var currentCount = 0;
            var children = bags[currentBag];

            foreach (var child in children)
            {
                if (child.Contains("no other")) return 0;

                var childBags = int.Parse(child.Substring(0, child.IndexOf(' ')));
                var childName = child.Substring(child.IndexOf(' ') + 1);
                var childBagsCount = GetChildBags(bags, childName);
                currentCount += childBags + childBagsCount * childBags;
            }

            return currentCount;
        }
    }
}
