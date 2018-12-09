using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2018
{
    /// <summary>
    /// --- Day 9: Marble Mania ---
    /// 
    /// --- Part Two ---
    /// 
    /// Amused by the speed of your answer, the Elves are curious:
    /// 
    /// What would the new winning Elf's score be if the number of the last marble
    /// were 100 times larger?
    /// </summary>
    [AdventOfCode(2018, 9, 2, "Marble Mania - Part 2", 3168033673L)]
    public class AdventOfCode2018092 : AdventOfCodeBase
    {
        public override void Solve()
        {
            const int nPlayers = 459;
            const int nMarbles = 7132000;

            var scores = new long[nPlayers];
            var circle = new LinkedList<long>();
            var currentMarble = circle.AddFirst(0);

            for (var i = 1; i < nMarbles; i++)
            {
                if (i % 23 == 0)
                {
                    scores[i % nPlayers] += i;
                    currentMarble = currentMarble.NthPrevious(7, circle.Last);
                    scores[i % nPlayers] += currentMarble.Value;
                    var removeMarble = currentMarble;
                    currentMarble = currentMarble.Next;
                    circle.Remove(removeMarble);
                }
                else
                {
                    currentMarble = circle.AddAfter(currentMarble?.Next ?? circle.First, i);
                }
            }

            Result = scores.Max();
        }
    }
}
