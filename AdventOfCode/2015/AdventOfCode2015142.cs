using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 14, 2, "", 1102)]
    public class AdventOfCode2015142 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var maxSeconds = 2503;
            var reindeer = File.ReadAllLines("2015/AdventOfCode201514.txt").Select(line => Regex.Matches(line, @"[+-]?\d+").Cast<Match>().Select(x => int.Parse(x.Value, CultureInfo.InvariantCulture)).ToArray()).ToList();

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

            Result = distance.Max(x => x[2]);
        }

        public AdventOfCode2015142(string sessionCookie) : base(sessionCookie) { }
    }
}
