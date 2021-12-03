using System.Linq;
using AdventOfCode.Core;
using MoreLinq;

namespace AdventOfCode._2021
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2021, 1, "Sonar Sweep", 1532, 1571)]
    public class AdventOfCode202101 : AdventOfCodeBase
    {
        public AdventOfCode202101(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var measures = Input.Select(int.Parse).ToArray();
            return measures.Skip(1).Zip(measures, (cur, prev) => cur > prev ? 1 : 0).Sum();
        }

        protected override object SolvePart2()
        {
            var measures = Input.Select(int.Parse).Window(3).Select(w => w.Sum()).ToArray();
            return measures.Skip(1).Zip(measures, (cur, prev) => cur > prev ? 1 : 0).Sum();
        }
    }
}
