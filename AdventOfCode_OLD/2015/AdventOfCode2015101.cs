using System.Linq;
using AdventOfCode.Core;
using MoreLinq;

namespace AdventOfCode._2015
{
    /// <summary>
    /// Today, the Elves are playing a game called look-and-say.
    /// They take turns making sequences by reading aloud the previous sequence
    /// and using that reading as the next sequence. For example, 211 is read as
    /// "one two, two ones", which becomes 1221 (1 2, 2 1s).
    /// 
    /// Look-and-say sequences are generated iteratively, using the previous value
    /// as input for the next step. For each step, take the previous value, and
    /// replace each run of digits (like 111) with the number of digits (3)
    /// followed by the digit itself (1).
    /// </summary>
    [AdventOfCode(2015, 10, 1, "Starting with the digits in your puzzle input, apply this process 40 times. What is the length of the result?", 360154)]
    public class AdventOfCode2015101 : AdventOfCodeBase
    {
        public override void Solve()
        {
            Result = Enumerable.Range(1, 40)
                .Aggregate("1113122113".Select(c => c - '0').ToArray(),
                    (acc, _) => acc
                        .GroupAdjacent(n => n)
                        .SelectMany(g => new[] {g.Count(), g.First()})
                        .ToArray())
                .Count();
        }
    }
}
