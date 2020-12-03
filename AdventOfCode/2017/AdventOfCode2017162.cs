using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 16: Permutation Promenade ---
    /// 
    /// --- Part Two ---
    /// 
    /// Now that you're starting to get a feel for the dance moves, you turn your
    /// attention to the dance as a whole.
    /// 
    /// Keeping the positions they ended up in from their previous dance, the
    /// programs perform it again and again: including the first dance, a total of
    /// one billion (1000000000) times.
    /// 
    /// In the example above, their second dance would begin with the order baedc,
    /// and use the same dance moves:
    /// 
    ///     - s1, a spin of size 1: cbaed.
    ///     - x3/4, swapping the last two programs: cbade.
    ///     - pe/b, swapping programs e and b: ceadb.
    /// 
    /// In what order are the programs standing after their billion dances?
    /// 
    /// </summary>
    [AdventOfCode(2017, 16, "Permutation Promenade - Part 2", "fdnphiegakolcmjb")]
    public class AdventOfCode2017162 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var programs = "abcdefghijklmnop".ToCharArray().ToList();
            var commandsArray = File.ReadAllText("2017\\AdventOfCode201716.txt").Split(',');

            // Fake-Memoize so that we can detect cycles, if any.
            var lookup = new List<string> { string.Concat(programs) };

            for (var i = 0L; i < 1000000000; i++)
            {
                programs = AdventOfCode2017161.DoDaDance(programs, commandsArray);

                // If we find a cycle then get the period and return the programs order in the state (1000000000 % periodLength).
                var programString = string.Concat(programs);
                if (lookup.Contains(programString))
                {
                    Result = lookup[1000000000 % lookup.Count];
                    break;
                }
                lookup.Add(programString);
            }
        }

        public AdventOfCode2017162(string sessionCookie) : base(sessionCookie) { }
    }
}
