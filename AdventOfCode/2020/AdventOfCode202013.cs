using System.Collections.Generic;
using System.Linq;
using SuperLinq;

namespace AdventOfCode._2020
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2020, 13, "Shuttle Search", 2845, 487905974205117L)]
    public class AdventOfCode202013 : AdventOfCodeBase
    {
        public AdventOfCode202013(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var earliest = int.Parse(Input.First());

            return Input
                   .Last(line => !string.IsNullOrWhiteSpace(line))
                   .Split(',')
                   .Where(id => id != "x")
                   .Select(int.Parse)
                   .Select(b => (bus: b, firstTimeAfter: (earliest / b + 1) * b - earliest))
                   .PartialSortBy(1, x => x.firstTimeAfter)
                   .Select(x => x.bus * x.firstTimeAfter)
                   .First();
        }

        protected override object SolvePart2()
        {
            return Input
                   .Last(line => !string.IsNullOrWhiteSpace(line))
                   .Split(',')
                   .Select((id, i) => new KeyValuePair<int, int>(i, (id == "x") ? 1 : int.Parse(id)))
                   .Aggregate(new { Time = 0L, LCM = 1L },
                       (acc, curr) =>
                           new
                           {
                               Time = Enumerable.Range(0, int.MaxValue)
                                                .Select(x => acc.Time + (acc.LCM * x))
                                                .First((x) => (x + curr.Key) % curr.Value == 0),
                               LCM = acc.LCM * curr.Value
                           }
                   )
                   .Time;
        }
    }
}
