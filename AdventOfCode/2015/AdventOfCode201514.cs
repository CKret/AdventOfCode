using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 14, "", 2640, 1102)]
    public class AdventOfCode201514 : AdventOfCodeBase
    {
        public AdventOfCode201514(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var maxSeconds = 2503;
            var reindeer = Input.Select(line => Regex.Matches(line, @"[+-]?\d+").Cast<Match>().Select(x => int.Parse(x.Value, CultureInfo.InvariantCulture)).ToArray()).ToList();

            var distance = reindeer.Select(r => new int[2]).ToList();

            for (var second = 0; second < maxSeconds; second++)
            {
                for (var d = 0; d < distance.Count; d++)
                {
                    if (distance[d][1] >= 0) distance[d][0] += reindeer[d][0];
                    distance[d][1]++;
                    if (distance[d][1] == reindeer[d][1]) distance[d][1] = -reindeer[d][2];
                }
            }

            return distance.Max(x => x[0]);
        }

        protected override object SolvePart2()
        {
            var maxSeconds = 2503;
            var reindeer = Input.Select(line => Regex.Matches(line, @"[+-]?\d+").Cast<Match>().Select(x => int.Parse(x.Value, CultureInfo.InvariantCulture)).ToArray()).ToList();

            var distance = reindeer.Select(r => new int[3]).ToList();

            for (var second = 0; second < maxSeconds; second++)
            {
                for (var d = 0; d < distance.Count; d++)
                {
                    if (distance[d][1] >= 0) distance[d][0] += reindeer[d][0];
                    distance[d][1]++;
                    if (distance[d][1] == reindeer[d][1]) distance[d][1] = -reindeer[d][2];
                }

                var max = distance.Max(x => x[0]);
                foreach (var r in distance.Where(x => x[0] == max))
                {
                    r[2]++;
                }
            }

            return distance.Max(x => x[2]);
        }
    }
}
