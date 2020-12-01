using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 4: High-Entropy Passphrases ---
    /// 
    /// --- Part Two ---
    /// 
    /// For added security, yet another system policy has been put in place. Now, a
    /// valid passphrase must contain no two words that are anagrams of each other
    /// - that is, a passphrase is invalid if any word's letters can be rearranged
    /// to form any other word in the passphrase.
    /// 
    /// For example:
    /// 
    ///     - abcde fghij is a valid passphrase.
    ///     - abcde xyz ecdab is not valid - the letters from the third word can be
    ///       rearranged to form the first word.
    ///     - a ab abc abd abf abj is a valid passphrase, because all letters need
    ///       to be used when forming another word.
    ///     - iiii oiii ooii oooi oooo is valid.
    ///     - oiii ioii iioi iiio is not valid - any of these words can be
    ///       rearranged to form any other word.
    /// 
    /// Under this new system policy, how many passphrases are valid?
    /// 
    /// </summary>
    [AdventOfCode(2017, 4, 2, "High-Entropy Passphrase - Part Two", 223)]
    public class AdventOfCode2017042 : AdventOfCodeBase
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
                    var isAnagram = false;
                    foreach (var p in passwords)
                    {
                        isAnagram = IsPermutation(p, word);
                        if (isAnagram) break;
                    }
                    if (!isAnagram)
                        passwords.Add(word);
                }
                if (passwords.Count == splits.Length) sum++;
            }

            Result = sum;
        }

        private bool IsPermutation(string a, string b)
        {
            if (a.Length != b.Length)
                return false;

            var arr1 = a.ToCharArray();
            var arr2 = b.ToCharArray();

            Array.Sort(arr1);
            Array.Sort(arr2);

            var str1 = string.Join("", arr1);
            var str2 = string.Join("", arr2);

            return str1.Equals(str2, StringComparison.InvariantCulture);
        }

        public AdventOfCode2017042(string sessionCookie) : base(sessionCookie) { }
    }
}
