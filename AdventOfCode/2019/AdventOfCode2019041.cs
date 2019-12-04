using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2019
{
    /// <summary>
    /// --- Day 4: Secure Container ---
    /// 
    /// You arrive at the Venus fuel depot only to discover it's protected by a
    /// password. The Elves had written the password on a sticky note, but someone
    /// threw it out.
    /// 
    /// However, they do remember a few key facts about the password:
    /// 
    ///  - It is a six-digit number.
    ///  - The value is within the range given in your puzzle input.
    ///  - Two adjacent digits are the same (like 22 in 122345).
    ///  - Going from left to right, the digits never decrease; they only ever
    ///    increase or stay the same (like 111123 or 135679).
    /// 
    /// Other than the range rule, the following are true:
    /// 
    ///  - 111111 meets these criteria (double 11, never decreases).
    ///  - 223450 does not meet these criteria (decreasing pair of digits 50).
    ///  - 123789 does not meet these criteria (no double).
    /// 
    /// How many different passwords within the range given in your puzzle input
    /// meet these criteria?
    /// </summary>
    [AdventOfCode(2019, 4, 1, "How many different passwords within the range given in your puzzle input meet these criteria?", 466)]
    public class AdventOfCode2019041 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201904.txt");

            var rangeStart = int.Parse(data.Split('-')[0]);
            var rangeEnd = int.Parse(data.Split('-')[1]);

            var count = 0;
            for (var i = rangeStart; i <= rangeEnd; i++)
            {
                if (HasDouble(i) && IsDecreasing(i)) count++;
            }

            Result = count;
        }

        protected bool HasDouble(int num)
        {
            var str = num.ToString();

            for (var i = 0; i < str.Length - 1; i++)
            {
                if (str[i] == str[i + 1]) return true;
            }

            return false;
        }

        protected bool IsDecreasing(int num)
        {
            var tmp = num % 10;
            while (num > 0)
            {
                num = num / 10;
                if (num % 10 > tmp) return false;
                tmp = num % 10;
            }

            return true;
        }
    }
}
