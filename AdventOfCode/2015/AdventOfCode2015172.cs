using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 17, "", 18)]
    public class AdventOfCode2015172 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var containers = File.ReadAllLines("2015\\AdventOfCode201517.txt").Select(int.Parse).ToList();

            var combinations = AdventOfCode2015171.FillContainers(new List<int>(), containers, 150).ToList();
            var min = combinations.Min(c => c.Count);
            Result = combinations.Count(c => c.Count == min);
        }

        public AdventOfCode2015172(string sessionCookie) : base(sessionCookie) { }
    }
}
