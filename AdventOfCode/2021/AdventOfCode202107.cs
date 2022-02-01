using System;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2021
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2021, 7, "The Treachery of Whales", 357353, 104822130)]
    public class AdventOfCode202107 : AdventOfCodeBase
    {
        public AdventOfCode202107(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var crabs = Input.First().Split(',').Select(int.Parse).ToList();

            return Enumerable.Range(crabs.Min(), crabs.Max())
                .Select(p => crabs.Sum(c => Math.Abs(c - p)))
                .Min();
        }

        protected override object SolvePart2()
        {
            var crabs = Input.First().Split(',').Select(int.Parse).ToList();

            return Enumerable.Range(crabs.Min(), crabs.Max())
                .Select(p => crabs.Sum(c => Math.Abs(c - p) * (Math.Abs(c - p) + 1) / 2))
                .Min();
        }
    }
}
