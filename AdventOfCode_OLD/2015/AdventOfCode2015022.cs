using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// The elves are also running low on ribbon. Ribbon is all the same width, so they only
    /// have to worry about the length they need to order, which they would again like to be exact.
    /// 
    /// The ribbon required to wrap a present is the shortest distance around its sides, or the
    /// smallest perimeter of any one face. Each present also requires a bow made out of ribbon
    /// as well; the feet of ribbon required for the perfect bow is equal to the cubic feet of
    /// volume of the present. Don't ask how they tie the bow, though; they'll never tell.
       /// </summary>
    [AdventOfCode(2015, 2, 2, "How many total feet of ribbon should they order?", 3812909)]
    public class AdventOfCode2015022 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var sum = 0;
            foreach (var line in File.ReadAllLines("2015/AdventOfCode201502.txt"))
            {
                var parts = line.Split('x').Select(int.Parse).ToArray();

                var sides = parts.Where(s => s != parts.Max()).ToList();
                while (sides.Count != 2)
                    sides.Add(parts.Max());

                sum += sides[0] + sides[0] + sides[1] + sides[1] + parts.Aggregate((c, n) => c * n);

            }
            Result = sum;
        }
    }
}
