using System;
using System.Data;
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

            foreach (var step in File.ReadAllText("2017/AdventOfCode201711.txt").Split(','))
            {
                switch (step)
                {
                    case "s":
                        y -= 2;
                        break;
                    case "n":
                        y += 2;
                        break;
                    case "se":
                        y--;
                        x++;
                        break;
                    case "ne":
                        y++;
                        x++;
                        break;
                    case "sw":
                        y--;
                        x--;
                        break;
                    case "nw":
                        y++;
                        x--;
                        break;
                    default:
                        throw new InvalidExpressionException(step);
                }

                var current = Math.Abs((y - x) / 2 + x);
                if (current > max)
                    max = current;
            }

            Result = max;
        }
    }
}
