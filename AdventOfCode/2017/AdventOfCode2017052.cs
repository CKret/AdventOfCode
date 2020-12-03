using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 5: A Maze of Twisty Trampolines, All Alike ---
    /// 
    /// --- Part Two ---
    /// 
    /// Now, the jumps are even stranger: after each jump, if the offset was three
    /// or more, instead decrease it by 1. Otherwise, increase it by 1 as before.
    /// 
    /// Using this rule with the above example, the process now takes 10 steps, and
    /// the offset values after finding the exit are left as 2 3 2 3 -1.
    /// 
    /// How many steps does it now take to reach the exit?
    /// 
    /// </summary>
    [AdventOfCode(2017, 5, "A Maze of Twisty Trampolines, All Alike - Part Two", 28707598)]
    public class AdventOfCode2017052 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var instr = File.ReadAllLines("2017\\AdventOfCode201705.txt").Select(int.Parse).ToList();

            var counter = 0;
            var i = 0;
            while (i < instr.Count && i >= 0)
            {
                if (instr[i] >= 3) i += instr[i]--;
                else i += instr[i]++;
                counter++;
            }

            Result = counter;
        }

        public AdventOfCode2017052(string sessionCookie) : base(sessionCookie) { }
    }
}
