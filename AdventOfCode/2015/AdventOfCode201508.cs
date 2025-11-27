using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 8, "Matchsticks", 1371, 2117)]
    public class AdventOfCode201508 : AdventOfCodeBase
    {
        public AdventOfCode201508(string sessionCookie) : base(sessionCookie) { }

        protected override async Task<object> SolvePart1(CancellationToken cancellationToken)
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

        protected override async Task<object> SolvePart2(CancellationToken cancellationToken)
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
