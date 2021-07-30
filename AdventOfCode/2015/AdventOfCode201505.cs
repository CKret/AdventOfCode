using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 5, "", 236, 51)]
    public class AdventOfCode201505 : AdventOfCodeBase
    {
        public AdventOfCode201505(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var s = new[] { "ab", "cd", "pq", "xy" };
            var vowels = "aeiou";

            var sum = 0;
            foreach (var line in Input)
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

            return sum;
        }

        protected override object SolvePart2()
        {
            var doubles = new List<string>();

            for (var a = 'a'; a <= 'z'; a++)
                for (var b = 'a'; b <= 'z'; b++)
                    doubles.Add(a.ToString(CultureInfo.InvariantCulture) + b);

            var sum = 0;
            foreach (var line in Input)
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

            return sum;
        }
    }
}
