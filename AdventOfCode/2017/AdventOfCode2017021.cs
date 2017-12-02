using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// As you walk through the door, a glowing humanoid shape yells in your direction.
    /// "You there! Your state appears to be idle. Come help us repair the corruption in
    /// this spreadsheet - if we take another millisecond, we'll have to display an hourglass cursor!"
    /// 
    /// The spreadsheet consists of rows of apparently-random numbers. To make sure the
    /// recovery process is on the right track, they need you to calculate the spreadsheet's
    /// checksum. For each row, determine the difference between the largest value and the
    /// smallest value; the checksum is the sum of all of these differences.
    /// </summary>
    [AdventOfCode(2017, 2, 1, "What is the checksum for the spreadsheet in your puzzle input?", 42378)]
    public class AdventOfCode2017021 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var sum = File.ReadAllLines("2017/AdventOfCode201702.txt")
                .Select(line => line.Split().Select(int.Parse))
                .Select(s => s.Max() - s.Min()).Sum();

            Result = sum;
        }
    }
}
