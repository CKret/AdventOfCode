using System.Collections.Generic;
using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2017, 4, 1, "", 451)]
    public class AdventOfCode2017041 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var sum = 0;
            foreach (var line in File.ReadAllLines("2017/AdventOfCode201704.txt"))
            {
                var splits = line.Split();
                var passwords = new List<string>();
                foreach (var word in splits)
                {
                    if (passwords.Contains(word)) break;
                    passwords.Add(word);
                }
                if (passwords.Count == splits.Length) sum++;
            }

            Result = sum;
        }
    }
}
