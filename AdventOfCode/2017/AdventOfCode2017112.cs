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
            int x = 0, y = 0, max = 0;

            foreach (var step in File.ReadAllText("2017\\AdventOfCode201711.txt").Split(','))
            {
                y += step.Length == 1 ? (step[0] == 'n' ? 2 : -2) : (step[0] == 'n' ? 1 : -1);
                x += step.Length == 2 ? (step[1] == 'e' ? 1 : -1) : 0;

                max = Math.Max(max, Math.Abs((y - x) / 2 + x));
            }

            Result = max;
        }
    }
}
