using System;
using AdventOfCode.Core;
using System.Linq;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 5, "", 894, 579)]
    public class AdventOfCode202005 : AdventOfCodeBase
    {
        private int[] seats;

        public AdventOfCode202005(string sessionCookie) : base(sessionCookie) { }

        public override void Solve()
        {
            seats = (from seat in Input
                     let row = Convert.ToInt32(string.Join("", seat.Substring(0, 7).Select(Replace)), 2)
                     let col = Convert.ToInt32(string.Join("", seat.Substring(7, 3).Select(Replace)), 2)
                     select row * 8 + col).ToArray();

            base.Solve();
        }

        protected override object SolvePart1()
{
            return seats.Max();
        }

        protected override object SolvePart2()
        {
            return Enumerable.Range(0, 1024).Single(r => seats.All(s => r != s) && seats.Contains(r - 1) && seats.Contains(r + 1));
        }

        private char Replace(char c) => (c == 'F' || c == 'L') ? '0' : '1';
    }
}
