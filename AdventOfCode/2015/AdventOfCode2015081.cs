using System.IO;
using System.Text.RegularExpressions;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// Space on the sleigh is limited this year, and so Santa will be bringing his list as a digital copy. He needs to know how much space it will take up when stored.
    /// 
    /// It is common in many programming languages to provide a way to escape special characters in strings. For example, C, JavaScript, Perl, Python, and even PHP handle special characters in very similar ways.
    /// 
    /// However, it is important to realize the difference between the number of characters in the code representation of the string literal and the number of characters in the in-memory string itself.
    /// </summary>
    [AdventOfCode(2015, 8, "What is the number of characters of code for string literals minus the number of characters in memory for the values of the strings in total for the entire file?", 1371)]
    public class AdventOfCode2015081 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var codeLen = 0L;
            var memLen = 0L;
            foreach (var line in File.ReadAllLines("2015/AdventOfCode201508.txt"))
            {
                codeLen += line.Length;
                memLen += Regex.Replace(line.Replace("\\\\", ".").Replace("\\\"", ".").Trim('\"'), @"[\\][x]..", ".").Length;
            }


            Result = (int) (codeLen - memLen);
        }

        public AdventOfCode2015081(string sessionCookie) : base(sessionCookie) { }
    }
}
