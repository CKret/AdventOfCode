using System;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 2, "", 1598415, 3812909)]
    public class AdventOfCode201502 : AdventOfCodeBase
    {
        public AdventOfCode201502(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var sum = 0;
            foreach (var line in Input)
            {
                var parts = line.Split('x').Select(int.Parse).ToArray();
                var s1 = parts[0] * parts[1];
                var s2 = parts[0] * parts[2];
                var s3 = parts[1] * parts[2];

                var smallest = Math.Min(s1, Math.Min(s2, s3));

                sum += 2 * s1 + 2 * s2 + 2 * s3 + smallest;

            }

            return sum;
        }

        protected override object SolvePart2()
        {
            var sum = 0;
            foreach (var line in Input)
            {
                var parts = line.Split('x').Select(int.Parse).ToArray();

                var sides = parts.Where(s => s != parts.Max()).ToList();
                while (sides.Count != 2)
                    sides.Add(parts.Max());

                sum += sides[0] + sides[0] + sides[1] + sides[1] + parts.Aggregate((c, n) => c * n);

            }
            
            return  sum;
        }
    }
}
