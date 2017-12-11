using System;
using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 11: Hex Ed ---
    /// 
    /// --- Part Two ---
    /// 
    /// How many steps away is the furthest he ever got from his starting position?
    /// 
    /// </summary>
    [AdventOfCode(2017, 11, 2, "Hex Ed - Part Two", 1457)]
    public class AdventOfCode2017112 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var x = 0;
            var y = 0;
            var max = 0;

            foreach (var step in File.ReadAllText("2017\\AdventOfCode201711.txt").Split(','))
            {
                y += step.Length == 1 ? (step[0] == 'n' ? 2 : -2) : (step[0] == 'n' ? 1 : -1);
                x += step.Length == 2 ? (step[1] == 'e' ? 1 : -1) : 0;

                var current = Math.Abs((y - x) / 2 + x);
                if (current > max)
                    max = current;
            }

            Result = max;
        }
    }
}
