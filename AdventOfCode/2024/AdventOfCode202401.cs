using System;
using System.Linq;

namespace AdventOfCode._2024
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2024, 1, "Historian Hysteria", 3246517, 29379307)]
    public class AdventOfCode202401 : AdventOfCodeBase
    {
        public AdventOfCode202401(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var parsedInput = Input.Select(x => x.Split("   ").Select(int.Parse));

            var list1 = parsedInput.Select(x => x.First()).OrderBy(x => x);
            var list2 = parsedInput.Select(x => x.Last()).OrderBy(x => x);

            return list1.Zip(list2, (a, b) => Math.Abs(a - b)).Sum();
        }

        protected override object SolvePart2()
        {
            var parsedInput = Input.Select(x => x.Split("   ").Select(int.Parse));

            var list1 = parsedInput.Select(x => x.First()).OrderBy(x => x);
            var list2 = parsedInput.Select(x => x.Last()).OrderBy(x => x);

            return list1
                .GroupBy(x => x)
                .Sum(g => g.Key * list2.Count(y => y == g.Key));
        }
    }
}
