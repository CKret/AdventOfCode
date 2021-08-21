using System;
using System.Collections.Generic;
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
    /// </summary>
    [AdventOfCode(2017, 4, "High-Entropy Passphrase", 451, 223)]
    public class AdventOfCode201704 : AdventOfCodeBase
    {
        public AdventOfCode201704(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var sum = 0;
            foreach (var line in Input)
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

            return sum;
        }

        protected override object SolvePart2()
        {
            var sum = 0;
            foreach (var line in Input)
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

            return sum;
        }

        private static bool IsPermutation(string a, string b)
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
    }
}
