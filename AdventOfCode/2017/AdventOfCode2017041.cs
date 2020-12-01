using System.Collections.Generic;
using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 4: High-Entropy Passphrases ---
    /// 
    /// A new system policy has been put in place that requires all accounts to use
    /// a passphrase instead of simply a password. A passphrase consists of a
    /// series of words (lowercase letters) separated by spaces.
    /// 
    /// To ensure security, a valid passphrase must contain no duplicate words.
    /// 
    /// For example:
    /// 
    ///     - aa bb cc dd ee is valid.
    ///     - aa bb cc dd aa is not valid - the word aa appears more than once.
    ///     - aa bb cc dd aaa is valid - aa and aaa count as different words.
    /// 
    /// The system's full passphrase list is available as your puzzle input. How
    /// many passphrases are valid?
    /// 
    /// </summary>
    [AdventOfCode(2017, 4, 1, "High-Entropy Passphrase - Part One", 451)]
    public class AdventOfCode2017041 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var sum = 0;
            foreach (var line in File.ReadAllLines("2017\\AdventOfCode201704.txt"))
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

        public AdventOfCode2017041(string sessionCookie) : base(sessionCookie) { }
    }
}
