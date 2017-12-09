using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2017, 9, 1, "", 17537)]
    public class AdventOfCode2017091 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var totalScore = 0;
            var currentScore = 0;
            var isGarbage = false;
            var input = File.ReadAllText("2017/AdventOfCode201709.txt").ToCharArray();
            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];

                if (c == '!')
                {
                    i++;
                    continue;
                }

                if (isGarbage && c == '>')
                {
                    isGarbage = false;
                    continue;
                }

                if (isGarbage) continue;

                if (c == '<')
                {
                    isGarbage = true;
                    continue;
                }

                if (c == '{')
                {
                    currentScore++;
                    continue;
                }

                if (c == '}')
                {
                    totalScore += currentScore;
                    currentScore--;
                }
            }

            Result = totalScore;
        }
    }
}
