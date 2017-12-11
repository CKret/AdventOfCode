using AdventOfCode.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 8: I Heard You Like Registers ---
    /// 
    /// --- Part Two ---
    /// 
    /// To be safe, the CPU also needs to know the highest value held in any
    /// register during this process so that it can decide how much memory to
    /// allocate to these operations. For example, in the above instructions, the
    /// highest value ever held was 10 (in register c after the third instruction
    /// was evaluated).
    /// 
    /// </summary>
    [AdventOfCode(2017, 8, 2, "I Heard You Like Registers - Part Two", 6026)]
    public class AdventOfCode2017082 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var registers = new Dictionary<string, int>();
            var max = 0;
            foreach (var line in File.ReadAllLines("2017\\AdventOfCode201708.txt"))
            {
                var splits = line.Split();
                var reg = splits[0];
                var instr = splits[1];
                var value = int.Parse(splits[2]);
                var otherReg = splits[4];
                var compare = splits[5];
                var compareValue = int.Parse(splits[6]);

                if (!registers.ContainsKey(reg))
                    registers.Add(reg, 0);
                if (!registers.ContainsKey(otherReg))
                    registers.Add(otherReg, 0);

                switch (compare)
                {
                    case "<":
                        if (registers[otherReg] < compareValue)
                            registers[reg] += instr == "inc" ? value : -value;
                        break;
                    case "<=":
                        if (registers[otherReg] <= compareValue)
                            registers[reg] += instr == "inc" ? value : -value;
                        break;
                    case "==":
                        if (registers[otherReg] == compareValue)
                            registers[reg] += instr == "inc" ? value : -value;
                        break;
                    case ">=":
                        if (registers[otherReg] >= compareValue)
                            registers[reg] += instr == "inc" ? value : -value;
                        break;
                    case ">":
                        if (registers[otherReg] > compareValue)
                            registers[reg] += instr == "inc" ? value : -value;
                        break;
                    case "!=":
                        if (registers[otherReg] != compareValue)
                            registers[reg] += instr == "inc" ? value : -value;
                        break;
                    default:
                        throw new InvalidOperationException(instr);
                }

                var currentMax = registers.Max(x => x.Value);
                if (currentMax > max) max = currentMax;
            }

            Result = max;
        }
    }
}
