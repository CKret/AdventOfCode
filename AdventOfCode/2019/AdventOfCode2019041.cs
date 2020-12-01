using System.Globalization;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using MoreLinq;

namespace AdventOfCode._2019
{
    /// <summary>
    /// --- Day 4: Secure Container ---
    /// 
    /// You arrive at the Venus fuel depot only to discover it's protected by a
    /// password. The Elves had written the password on a sticky note, but someone
    /// threw it out.
    /// 
    /// However, they do remember a few key facts about the password:
    /// 
    ///  - It is a six-digit number.
    ///  - The value is within the range given in your puzzle input.
    ///  - Two adjacent digits are the same (like 22 in 122345).
    ///  - Going from left to right, the digits never decrease; they only ever
    ///    increase or stay the same (like 111123 or 135679).
    /// 
    /// Other than the range rule, the following are true:
    /// 
    ///  - 111111 meets these criteria (double 11, never decreases).
    ///  - 223450 does not meet these criteria (decreasing pair of digits 50).
    ///  - 123789 does not meet these criteria (no double).
    /// 
    /// How many different passwords within the range given in your puzzle input
    /// meet these criteria?
    /// </summary>
    [AdventOfCode(2019, 4, 1, "Secure Container - Part 1", 466)]
    public class AdventOfCode2019041 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201904.txt");

            var rangeStart = int.Parse(data.Split('-')[0], CultureInfo.CurrentCulture);
            var rangeEnd = int.Parse(data.Split('-')[1], CultureInfo.CurrentCulture);

            Result = Enumerable.Range(rangeStart, rangeEnd - rangeStart + 1)
                               .Where(n => n.ToString(CultureInfo.CurrentCulture).Window(2).All(x => x[0] <= x[1]))
                               .Count(n => n.ToString(CultureInfo.CurrentCulture).GroupAdjacent(x => x).Any(g => g.Count() >= 2));
        }

        public AdventOfCode2019041(string sessionCookie) : base(sessionCookie) { }
    }
}
