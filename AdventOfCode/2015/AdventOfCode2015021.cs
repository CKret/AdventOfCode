using System;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// The elves are running low on wrapping paper, and so they need to submit an order for more.
    /// They have a list of the dimensions (length l, width w, and height h) of each present, and
    /// only want to order exactly as much as they need.
    /// 
    /// Fortunately, every present is a box (a perfect right rectangular prism), which makes
    /// calculating the required wrapping paper for each gift a little easier:
    /// find the surface area of the box, which is 2*l*w + 2*w*h + 2*h*l.
    /// The elves also need a little extra paper for each present: the area of the smallest side.
    /// </summary>
    [AdventOfCode(2015, 2, "All numbers in the elves' list are in feet. How many total square feet of wrapping paper should they order?", 1598415)]
    public class AdventOfCode2015021 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var sum = 0;
            foreach (var line in File.ReadAllLines("2015/AdventOfCode201502.txt"))
            {
                var parts = line.Split('x').Select(int.Parse).ToArray();
                var s1 = parts[0] * parts[1];
                var s2 = parts[0] * parts[2];
                var s3 = parts[1] * parts[2];

                var smallest = Math.Min(s1, Math.Min(s2, s3));

                sum += 2 * s1 + 2 * s2 + 2 * s3 + smallest;

            }
            Result = sum;
        }

        public AdventOfCode2015021(string sessionCookie) : base(sessionCookie) { }
    }
}
