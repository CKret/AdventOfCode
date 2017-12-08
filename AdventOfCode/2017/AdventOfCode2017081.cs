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
    /// You receive a signal directly from the CPU. Because of your recent
    /// assistance with jump instructions, it would like you to compute the result
    /// of a series of unusual register instructions.
    /// 
    /// Each instruction consists of several parts: the register to modify, whether
    /// to increase or decrease that register's value, the amount by which to
    /// increase or decrease it, and a condition. If the condition fails, skip the
    /// instruction without modifying the register. The registers all start at 0.
    /// The instructions look like this:
    /// 
    /// b inc 5 if a &gt; 1
    /// a inc 1 if b &lt; 5
    /// c dec -10 if a &gt;= 1
    /// c inc -20 if c == 10
    /// 
    /// These instructions would be processed as follows:
    /// 
    ///     - Because a starts at 0, it is not greater than 1, and so b is not
    ///       modified.
    ///     - a is increased by 1 (to 1) because b is less than 5 (it is 0).
    ///     - c is decreased by -10 (to 10) because a is now greater than or equal
    ///       to 1 (it is 1).
    ///     - c is increased by -20 (to -10) because c is equal to 10.
    /// 
    /// After this process, the largest value in any register is 1.
    /// 
    /// You might also encounter &lt;= (less than or equal to) or != (not equal to).
    /// However, the CPU doesn't have the bandwidth to tell you what all the
    /// registers are named, and leaves that to you to determine.
    /// 
    /// What is the largest value in any register after completing the instructions
    /// in your puzzle input?
    /// 
    /// </summary>
    [AdventOfCode(2017, 8, 1, "I Heard You Like Registers - Part One", 5946)]
    public class AdventOfCode2017081 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var registers = new Dictionary<string, int>();
            var max = 0;
            foreach (var line in File.ReadAllLines("2017/AdventOfCode201708.txt"))
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
            }

            Result = registers.Max(x => x.Value);
        }
    }
}
