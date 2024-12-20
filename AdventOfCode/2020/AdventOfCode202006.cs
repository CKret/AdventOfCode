using AdventOfCode.Core;
using System.Linq;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 6, "Custom Customs", 6430, 3125)]
    public class AdventOfCode202006 : AdventOfCodeBase
    {
        public AdventOfCode202006(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            return string.Join(" ", Input)
                         .Split("  ")
                         .Sum(g => g.Replace(" ", "").ToCharArray().Distinct().Count());
        }

        protected override object SolvePart2()
        {

            return string.Join(" ", Input)
                         .Split("  ")
                         .Select(group => group.Split(" "))
                         .Select(group => group.First().Count(chr => group.All(person => person.Contains(chr))))
                         .Sum();
        }
    }
}
