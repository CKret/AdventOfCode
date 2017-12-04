using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2017, 4, 2, "", 223)]
    public class AdventOfCode2017042 : AdventOfCodeBase
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

            return str1.Equals(str2);
        }
    }
}
