using System;
using System.Data;
using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 11: Hex Ed ---
    /// 
    /// Crossing the bridge, you've barely reached the other side of the stream
    /// when a program comes up to you, clearly in distress. "It's my child
    /// process," she says, "he's gotten lost in an infinite grid!"
    /// 
    /// Fortunately for her, you have plenty of experience with infinite grids.
    /// 
    /// Unfortunately for you, it's a hex grid.
    /// 
    /// The hexagons ("hexes") in this grid are aligned such that adjacent hexes
    /// can be found to the north, northeast, southeast, south, southwest, and
    /// northwest:
    /// 
    ///   \ n  /
    /// nw +--+ ne
    ///   /    \
    /// -+      +-
    ///   \    /
    /// sw +--+ se
    ///   / s  \
    /// 
    /// You have the path the child process took. Starting where he started, you
    /// need to determine the fewest number of steps required to reach him. (A
    /// "step" means to move from the hex you are in to any adjacent hex.)
    /// 
    /// For example:
    /// 
    ///     ne,ne,ne is 3 steps away.
    ///     ne,ne,sw,sw is 0 steps away (back where you started).
    ///     ne,ne,s,s is 2 steps away (se,se).
    ///     se,sw,se,sw,sw is 3 steps away (s,s,sw).
    /// 
    /// </summary>
    [AdventOfCode(2017, 11, 1, "Hex Ed - Part One", 685)]
    public class AdventOfCode2017111 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var x = 0;
            var y = 0;

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
            }

            Result = Math.Abs((y - x) / 2 + x);
        }
    }
}
