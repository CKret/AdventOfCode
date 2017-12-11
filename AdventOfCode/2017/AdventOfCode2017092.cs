using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 9: Stream Processing ---
    /// 
    /// --- Part Two ---
    /// 
    /// Now, you're ready to remove the garbage.
    /// 
    /// To prove you've removed it, you need to count all of the characters within
    /// the garbage. The leading and trailing &lt; and &gt; don't count, nor do any
    /// canceled characters or the ! doing the canceling.
    /// 
    ///     - &lt;&gt;, 0 characters.
    ///     - &lt;random characters&gt;, 17 characters.
    ///     - &lt;&lt;&lt;&lt;&gt;, 3 characters.
    ///     - &lt;{!&gt;}&gt;, 2 characters.
    ///     - &lt;!!&gt;, 0 characters.
    ///     - &lt;!!!&gt;&gt;, 0 characters.
    ///     - &lt;{o"i!a,&lt;{i&lt;a&gt;, 10 characters.
    /// 
    /// How many non-canceled characters are within the garbage in your puzzle input?
    /// 
    /// </summary>
    [AdventOfCode(2017, 9, 2, "Stream Processing - Part Two", 7539)]
    public class AdventOfCode2017092 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var isGarbage = false;
            var input = File.ReadAllText("2017\\AdventOfCode201709.txt").ToCharArray();
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
