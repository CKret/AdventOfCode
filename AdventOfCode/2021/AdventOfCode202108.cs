using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2021
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2021, 8, "Seven Segment Search", 421, null)]
    public class AdventOfCode202108 : AdventOfCodeBase
    {
        public AdventOfCode202108(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            return Input
                .Select(line => line
                    .Split(" | ")
                    .Last()
                    .Split(' ')
                    .Where(s => s.Length is 7 or >= 2 and <= 4))
                .SelectMany(s => s)
                .Count();
        }

        protected override object SolvePart2()
        {
            return null;
        }
    }
}
