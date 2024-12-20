using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2024
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2024, 2, "Red-Nosed Reports", 242, 311)]
    public class AdventOfCode202402 : AdventOfCodeBase
    {
        public AdventOfCode202402(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            return Input
                .Select(line => line.Split(" ").Select(int.Parse))
                .Where(report => IsInDecresingOrder(report) || IsInIncreasingOrder(report))
                .Count();
        }

        protected override object SolvePart2()
        {
            return Input.Select(x => x.Split(" ").Select(int.Parse))
                .Where(report => IsInDecresingOrder(report) || IsInIncreasingOrder(report) || report
                    .Select((_, i) => report.Where((_, j) => j != i))
                    .Any(temp => IsInDecresingOrder(temp) || IsInIncreasingOrder(temp)))
                .Count();
        }


        protected bool IsInDecresingOrder(IEnumerable<int> list)
        {
            return list.Zip(list.Skip(1), (a, b) => new { a, b }).All(p => p.a > p.b && p.a - p.b <= 3);
        }

        protected bool IsInIncreasingOrder(IEnumerable<int> list)
        {
            return list.Zip(list.Skip(1), (a, b) => new { a, b }).All(p => p.a < p.b && p.b - p.a <= 3);
        }
    }
}
