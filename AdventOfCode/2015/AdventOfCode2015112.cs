using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// --- Day 11: Corporate Policy ---
    /// 
    /// --- Part Two ---
    /// 
    /// Santa's password expired again. What's the next one?
    /// 
    /// </summary>
    [AdventOfCode(2015, 11, 2, "", "cqkaabcc")]
    public class AdventOfCode2015112 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var input = "cqjxxyzz".ToCharArray();

            while (true)
            {
                // Next password
                var x = input.Length - 1;
                while (x > 0 && ++input[x] > 'z') input[x--] = 'a';

                // Pass cannot contain i, o or l.
                if (input.Any(c => c == 'i' || c == 'o' || c == 'l')) continue;

                var possiblePass = false;
                for (var i = 0; i < input.Length - 2; i++)
                {
                    // 3 letters increase
                    if (input[i] == input[i + 1] - 1 && input[i] == input[i + 2] - 2)
                    {
                        possiblePass = true;
                        break;
                    }
                }

                // Two different non-overlapping pairs of letters (aa and bb... or ff and yy)
                if (possiblePass && DifferentPairs(input))
                {
                    Result = string.Join("", input);
                    break;
                }
            }
        }

        internal bool DifferentPairs(char[] pass)
        {
            var pairs = new List<char>();
            for (var i = 0; i < pass.Length - 1; i++)
            {
                if (pass[i] == pass[i + 1])
                {
                    if (!pairs.Contains(pass[i]))
                        pairs.Add(pass[i]);
                    i += 1;
                }
            }

            return pairs.Count == 2;
        }
    }
}
