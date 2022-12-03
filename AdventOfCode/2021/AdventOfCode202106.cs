using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2021
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2021, 6, "Lanternfish", 360610L, 1631629590423L)]
    public class AdventOfCode202106 : AdventOfCodeBase
    {
        public AdventOfCode202106(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            return GenerateLanternfish(80);
        }

        protected override object SolvePart2()
        {
            return GenerateLanternfish(256);
        }

        private object GenerateLanternfish(int nDays)
        {
            var lanternFishes = Input
                .First()
                .Split(',')
                .Select(int.Parse)
                .GroupBy(c => c)
                .Aggregate(new List<long>(new long[9]), (list, grp) =>
                {
                    list[grp.Key] = grp.Count();
                    return list;
                });

            for (var i = 0; i < nDays; i++)
            {
                var nExpired = lanternFishes[0];
                lanternFishes.RemoveAt(0);
                lanternFishes[6] += nExpired;
                lanternFishes.Add(nExpired);
            }

            return lanternFishes.Sum();
        }
    }
}
