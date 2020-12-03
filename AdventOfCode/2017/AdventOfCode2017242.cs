using System.Collections.Immutable;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 24: Electromagnetic Moat ---
    /// 
    /// --- Part Two ---
    /// 
    /// The bridge you've built isn't long enough; you can't jump the rest of the
    /// way.
    /// 
    /// In the example above, there are two longest bridges:
    /// 
    ///     - 0/2--2/2--2/3--3/4
    ///     - 0/2--2/2--2/3--3/5
    /// 
    /// Of them, the one which uses the 3/5 component is stronger; its strength is
    /// 0+2 + 2+2 + 2+3 + 3+5 = 19.
    /// 
    /// What is the strength of the longest bridge you can make? If you can make
    /// multiple bridges of the longest length, pick the strongest one.
    /// </summary>
    [AdventOfCode(2017, 24, "Electromagnetic Moat - Part Two", 1673)]
    public class AdventOfCode2017242 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var input = File.ReadAllLines("2017\\AdventOfCode201724.txt").Select(l => l.Split('/').Select(int.Parse).ToArray()).Select(c => (c[0], c[1])).ToImmutableList();

            Result = BuildLongBridge(input).Item1;
        }

        public (int, int) BuildLongBridge(IImmutableList<(int, int)> components, int cur = 0, int max = 0, int len = 0)
        {
            return components.Where(x => x.Item1 == cur || x.Item2 == cur)
                .Select(x => BuildLongBridge(components.Remove(x), x.Item1 == cur ? x.Item2 : x.Item1, max + x.Item1 + x.Item2, len + 1))
                .Concat(new []{ (max, len) })
                .OrderByDescending(x => x.Item2)
                .ThenByDescending(x => x.Item1)
                .First();
        }

        public AdventOfCode2017242(string sessionCookie) : base(sessionCookie) { }
    }
}
