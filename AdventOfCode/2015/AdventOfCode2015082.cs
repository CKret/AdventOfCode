using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// Now, let's go the other way. In addition to finding the number of characters of code,
    /// you should now encode each code representation as a new string and find the number of
    /// characters of the new encoded representation, including the surrounding double quotes.
    /// </summary>
    [AdventOfCode(2015, 8, 2, "Find the total number of characters to represent the newly encoded strings minus the number of characters of code in each original string literal.", 2117)]
    public class AdventOfCode2015082 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var codeLen = 0L;
            var escLen = 0L;
            foreach (var line in File.ReadAllLines("2015/AdventOfCode201508.txt"))
            {
                codeLen += line.Length;
                escLen += ("\"" + line.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"").Length;
            }


            Result = (int) (escLen - codeLen);
        }
    }
}
