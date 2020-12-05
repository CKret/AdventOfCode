using System;
using System.Collections.Generic;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 5, "", 894, 579)]
    public class AdventOfCode202005 : AdventOfCodeBase
    {
        public AdventOfCode202005(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            var timer = new Stopwatch();

            var seats = (from seat in Input
                         let row = Convert.ToInt32(string.Join("", seat.Substring(0, 7).Select(Replace)), 2)
                         let col = Convert.ToInt32(string.Join("", seat.Substring(7, 3).Select(Replace)), 2)
                         select row * 8 + col).ToArray();

            timer.Start();
            ResultPart1 = seats.Max();
            timer.Stop();
            TimePart1 = timer.ElapsedTicks.ToMilliseconds();

            timer.Start();
            ResultPart2 = Enumerable.Range(0, 1024).Single(r => seats.All(s => r != s) && seats.Contains(r - 1) && seats.Contains(r + 1)); ;
            timer.Stop();
            TimePart2 = timer.ElapsedTicks.ToMilliseconds();
        }

        private char Replace(char c) => (c == 'F' || c == 'L') ? '0' : '1';
    }
}
