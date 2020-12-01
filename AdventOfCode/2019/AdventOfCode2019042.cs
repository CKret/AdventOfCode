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
    /// --- Part Two ---
    /// 
    /// An Elf just remembered one more important detail: the two adjacent matching
    /// digits are not part of a larger group of matching digits.
    /// 
    /// Given this additional criterion, but still ignoring the range rule, the
    /// following are now true:
    /// 
    ///  - 112233 meets these criteria because the digits never decrease and all
    ///    repeated digits are exactly two digits long.
    ///  - 123444 no longer meets the criteria (the repeated 44 is part of a
    ///    larger group of 444).
    ///  - 111122 meets the criteria (even though 1 is repeated more than twice,
    /// it still contains a double 22).
    /// 
    /// How many different passwords within the range given in your puzzle input
    /// meet all of the criteria?
    /// </summary>
    [AdventOfCode(2019, 4, 2, "Secure Container - Part 2", 292)]
    public class AdventOfCode2019042 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201904.txt");

            var rangeStart = int.Parse(data.Split('-')[0], CultureInfo.CurrentCulture);
            var rangeEnd = int.Parse(data.Split('-')[1], CultureInfo.CurrentCulture);

            Result = Enumerable.Range(rangeStart, rangeEnd - rangeStart + 1)
                               .Where(n => n.ToString(CultureInfo.CurrentCulture).Window(2).All(x => x[0] <= x[1]))
                               .Count(n => n.ToString(CultureInfo.CurrentCulture).GroupAdjacent(x => x).Any(g => g.Count() == 2));
        }

        public AdventOfCode2019042(string sessionCookie) : base(sessionCookie) { }
    }
}
