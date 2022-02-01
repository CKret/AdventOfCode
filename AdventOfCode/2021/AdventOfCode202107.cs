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

            var maxPos = crabs.Max();
            var minPos = crabs.Min();

            var minFuel = int.MaxValue;
            for (var pos = minPos; pos <= maxPos; pos++)
            {
                var fuel = crabs.Sum(c => Math.Abs(c - pos));
                if (fuel < minFuel)
                {
                    minFuel = fuel;
                }
            }

            return minFuel;
        }

        protected override object SolvePart2()
        {
            var crabs = Input.First().Split(',').Select(int.Parse).ToList();

            var maxPos = crabs.Max();
            var minPos = crabs.Min();

            var minFuel = int.MaxValue;
            for (var pos = minPos; pos <= maxPos; pos++)
            {
                var fuel = crabs.Sum(c => Math.Abs(c - pos)*(Math.Abs(c - pos) + 1)/2);
                if (fuel < minFuel)
                {
                    minFuel = fuel;
                }
            }

            return minFuel;
        }
    }
}
