using System.Collections.Generic;
using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 23: Coprocessor Conflagration ---
    /// 
    /// You decide to head directly to the CPU and fix the printer from there. As
    /// you get close, you find an experimental coprocessor doing so much work that
    /// the local programs are afraid it will halt and catch fire. This would cause
    /// serious issues for the rest of the computer, so you head in and see what
    /// you can do.
    /// 
    /// The code it's running seems to be a variant of the kind you saw recently on
    /// that tablet. The general functionality seems very similar, but some of the
    /// instructions are different:
    /// 
    ///     - set X Y sets register X to the value of Y.
    ///     - sub X Y decreases register X by the value of Y.
    ///     - mul X Y sets register X to the result of multiplying the value
    ///       contained in register X by the value of Y.
    ///     - jnz X Y jumps with an offset of the value of Y, but only if the value
    ///       of X is not zero. (An offset of 2 skips the next instruction, an
    ///       offset of -1 jumps to the previous instruction, and so on.)
    /// 
    /// Only the instructions listed above are used. The eight registers here,
    /// named a through h, all start at 0.
    /// 
    /// The coprocessor is currently set to some kind of debug mode, which allows
    /// for testing, but prevents it from doing any meaningful work.
    /// 
    /// If you run the program (your puzzle input), how many times is the mul
    /// instruction invoked?
    /// 
    /// </summary>
    [AdventOfCode(2017, 23, 1, "Coprocessor Conflagration - Part One", 6241L)]
    public class AdventOfCode2017231 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var instructions = File.ReadAllLines("2017\\AdventOfCode201723.txt");

            var registers = new Dictionary<string, long>()
            {
                {"a", 0},
                {"b", 0},
                {"c", 0},
                {"d", 0},
                {"e", 0},
                {"f", 0},
                {"g", 0},
                {"h", 0}
            };

            var count = 0L;
            for (var i = 0L; i >= 0 && i < instructions.Length; i++)
            {
                var cur = instructions[i].Split();
                var instr = cur[0];
                var opA = cur[1];
                var opB = string.Empty;
                if (cur.Length > 2) opB = cur[2];

                switch (instr)
                {
                    case "set":
                        if (int.TryParse(opB, out var val)) registers[opA] = val;
                        else registers[opA] = registers[opB];
                        break;
                    case "sub":
                        if (int.TryParse(opB, out val)) registers[opA] -= val;
                        else registers[opA] -= registers[opB];
                        break;
                    case "mul":
                        count++;
                        if (int.TryParse(opB, out val)) registers[opA] *= val;
                        else registers[opA] *= registers[opB];
                        break;
                    case "jnz":
                        if (!long.TryParse(opA, out var valA)) valA = registers[opA];
                        if (valA != 0)
                        {
                            if (int.TryParse(opB, out val)) i += val - 1;
                            else i += registers[opB] - 1;
                        }
                        break;
                }
            }

            Result = count;
        }
    }
}
