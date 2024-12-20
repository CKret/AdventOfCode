using System;
using AdventOfCode.Core;
using System.Linq;
using SuperLinq;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 9, "Encoding Error", 18272118, 2186361)]
    public class AdventOfCode202009 : AdventOfCodeBase
    {
        private int PreambleLength => 25;

        public AdventOfCode202009(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var nums = Input.Select(long.Parse).ToArray();
            return nums.Skip(PreambleLength)
                       .Where((x, i) => nums[i..(i + PreambleLength)].Subsets(2).All(l => l.Sum() != x))
                       .First();
        }

        protected override object SolvePart2()
        {
            var nums = Input.Select(long.Parse).ToArray();
            var endIndex = Array.IndexOf(nums, (long) ResultPart1);
            for (var length = 2; length < Input.Length; length++)
            {
                for (var i = 0; i < endIndex; i++)
                {
                    var window = nums[i..(i + length)];
                    if (window.Sum() == (long) ResultPart1) return window.Min() + window.Max();
                }
            }

            return 0;
        }
    }
}
