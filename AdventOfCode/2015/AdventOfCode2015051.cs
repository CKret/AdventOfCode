using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// Santa needs help figuring out which strings in his text file are naughty or nice.
    /// 
    /// A nice string is one with all of the following properties:
    /// 
    ///     It contains at least three vowels (aeiou only), like aei, xazegov, or aeiouaeiouaeiou.
    ///     It contains at least one letter that appears twice in a row, like xx, abcdde (dd), or aabbccdd (aa, bb, cc, or dd).
    ///     It does not contain the strings ab, cd, pq, or xy, even if they are part of one of the other requirements.
    /// </summary>
    [AdventOfCode(2015, 5, 1, "How many strings are nice?", 236)]
    public class AdventOfCode2015051 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var s = new[] {"ab", "cd", "pq", "xy"};
            var vowels = "aeiou";

            var sum = 0;
            foreach (var line in File.ReadAllLines("2015/AdventOfCode201505.txt"))
            {
                if (s.Any(line.Contains)) continue;
                if (line.Count(c => vowels.Contains(c)) < 3) continue;

                for (var i = 0; i < line.Length - 1; i++)
                {
                    if (line[i] == line[i + 1])
                    {
                        sum++;
                        break;
                    }
                }
            }

            Result = sum;
        }

        public AdventOfCode2015051(string sessionCookie) : base(sessionCookie) { }
    }
}
