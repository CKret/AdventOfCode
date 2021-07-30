using System.Text.RegularExpressions;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 8, "", 1371, 2117)]
    public class AdventOfCode201508 : AdventOfCodeBase
    {
        public AdventOfCode201508(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var codeLen = 0L;
            var memLen = 0L;
            foreach (var line in Input)
            {
                codeLen += line.Length;
                memLen += Regex.Replace(line.Replace("\\\\", ".").Replace("\\\"", ".").Trim('\"'), @"[\\][x]..", ".").Length;
            }


            return codeLen - memLen;
        }

        protected override object SolvePart2()
        {
            var codeLen = 0L;
            var escLen = 0L;
            foreach (var line in Input)
            {
                codeLen += line.Length;
                escLen += ("\"" + line.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"").Length;
            }


            return escLen - codeLen;
        }
    }
}
