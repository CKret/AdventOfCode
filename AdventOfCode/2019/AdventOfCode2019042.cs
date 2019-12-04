using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2019
{
    /// <summary>
    /// --- Day 4: Secure Container ---
    /// 
    /// --- Part Two ---
    /// 
    /// An Elf just remembered one more important detail: the two adjacent matching
    /// digits are not part of a larger group of matching digits.
    /// 
    /// Given this additional criterion, but still ignoring the range rule, the
    /// following are now true:
    /// 
    ///  - 112233 meets these criteria because the digits never decrease and all
    ///    repeated digits are exactly two digits long.
    ///  - 123444 no longer meets the criteria (the repeated 44 is part of a
    ///    larger group of 444).
    ///  - 111122 meets the criteria (even though 1 is repeated more than twice,
    /// it still contains a double 22).
    /// 
    /// How many different passwords within the range given in your puzzle input
    /// meet all of the criteria?
    /// </summary>
    [AdventOfCode(2019, 4, 2, "How many different passwords within the range given in your puzzle input meet all of the criteria?", 292)]
    public class AdventOfCode2019042 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201904.txt");

            var rangeStart = int.Parse(data.Split('-')[0]);
            var rangeEnd = int.Parse(data.Split('-')[1]);

            var count = 0;
            for (var i = rangeStart; i <= rangeEnd; i++)
            {
                if (HasDouble(i) && IsDecreasing(i) && IsExactly2Repeating(i)) count++;
            }

            Result = count;
        }

        protected bool IsExactly2Repeating(int num)
        {
            var str = num.ToString();

            var curr = string.Empty;
            curr += str[0];

            for (var i = 1; i < str.Length; i++)
            {
                if (str[i] == str[i - 1])
                {
                    curr += str[i];
                }
                else
                {
                    if (curr.Length == 2) return true;
                    curr = "" + str[i];
                }
            }

            return curr.Length == 2;
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
