using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 2: Corruption Checksum ---
    /// 
    /// --- Part Two ---
    /// 
    /// "Great work; looks like we're on the right track after all. Here's a star
    /// for your effort." However, the program seems a little worried. Can programs
    /// be worried?
    /// 
    /// "Based on what we're seeing, it looks like all the User wanted is some
    /// information about the evenly divisible values in the spreadsheet.
    /// Unfortunately, none of us are equipped for that kind of calculation - most
    /// of us specialize in bitwise operations."
    /// 
    /// It sounds like the goal is to find the only two numbers in each row where
    /// one evenly divides the other - that is, where the result of the division
    /// operation is a whole number. They would like you to find those numbers on
    /// each line, divide them, and add up each line's result.
    /// 
    /// For example, given the following spreadsheet:
    /// 
    /// 5 9 2 8
    /// 9 4 7 3
    /// 3 8 6 5
    /// 
    ///     - In the first row, the only two numbers that evenly divide are 8 and 2;
    ///       the result of this division is 4.
    ///     - In the second row, the two numbers are 9 and 3; the result is 3.
    ///     - In the third row, the result is 2.
    /// 
    /// In this example, the sum of the results would be 4 + 3 + 2 = 9.    
    /// 
    /// What is the sum of each row's result in your puzzle input?
    /// </summary>
    [AdventOfCode(2017, 2, 2, "Corruption Checksum - Part Two", 246)]
    public class AdventOfCode2017022 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var sum = File.ReadAllLines("2017\\AdventOfCode201702.txt")
                .Select(line => line.Split().Select(int.Parse).ToArray())
                .Select(s => s
                    .Select((n, i) => s
                        .Where((x, j) => i != j)
                        .Where(d => n % d == 0)
                        .Sum(d => n / d))
                    .Sum())
                .Sum();

            Result = sum;
        }

        public AdventOfCode2017022(string sessionCookie) : base(sessionCookie) { }
    }
}
