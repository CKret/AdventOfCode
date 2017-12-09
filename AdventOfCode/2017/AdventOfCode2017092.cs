using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2017, 9, 2, "", 7539)]
    public class AdventOfCode2017092 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var isGarbage = false;
            var input = File.ReadAllText("2017/AdventOfCode201709.txt").ToCharArray();
            var nGarbage = 0;
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

                if (isGarbage)
                {
                    nGarbage++;
                    continue;
                }

                if (c == '<') isGarbage = true;
            }

            Result = nGarbage;
        }
    }
}
