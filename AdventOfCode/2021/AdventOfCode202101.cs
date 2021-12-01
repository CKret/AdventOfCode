using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2021
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2021, 1, "", 1532, 1571)]
    public class AdventOfCode202101 : AdventOfCodeBase
    {
        public AdventOfCode202101(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var measures = Input.Select(int.Parse).ToArray();

            var count = 0;
            for (var i = 1; i < measures.Length; i++)
            {
                if (measures[i] > measures[i - 1]) count++;
            }

            return count;
        }

        protected override object SolvePart2()
        {
            var measures = Input.Select(int.Parse).ToArray();

            var count = 0;
            for (var i = 0; i < measures.Length - 3; i++)
            {
                var sum1 = measures[i] + measures[i + 1] +  measures[i + 2];
                var sum2 = measures[i + 1] + measures[i + 2] + measures[i + 3];
                if (sum2 > sum1) count++;
            }

            return count;
        }
    }
}
