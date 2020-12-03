using System.Collections.Generic;
using System.Globalization;
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
    ///     It contains a pair of any two letters that appears at least twice in the string without overlapping, like xyxy (xy) or aabcdefgaa (aa), but not like aaa (aa, but it overlaps).
    ///     It contains at least one letter which repeats with exactly one letter between them, like xyx, abcdefeghi (efe), or even aaa.
    /// </summary>
    [AdventOfCode(2015, 5, "How many strings are nice?", 51)]
    public class AdventOfCode2015052 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var doubles = new List<string>();

            for (var a = 'a'; a <= 'z'; a++)
                for (var b = 'a'; b <= 'z'; b++)
                    doubles.Add(a.ToString(CultureInfo.InvariantCulture) + b);

            var sum = 0;
            foreach (var line in File.ReadAllLines("2015/AdventOfCode201505.txt"))
            {
                var hasDoubles = false;
                foreach (var d in doubles)
                {
                    var b = line.Replace(d, "");
                    var c = (line.Length - b.Length) / d.Length;
                    if (c > 1) hasDoubles = true;
                }

                if (!hasDoubles) continue;

                for (var i = 0; i < line.Length - 2; i++)
                {
                    if (line[i] == line[i + 2])
                    {
                        sum++;
                        break;
                    }
                }
            }

            Result = sum;
        }

        public AdventOfCode2015052(string sessionCookie) : base(sessionCookie) { }
    }
}
