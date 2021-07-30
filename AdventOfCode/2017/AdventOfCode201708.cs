using AdventOfCode.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// --- Part Two ---
    /// 
    /// To be safe, the CPU also needs to know the highest value held in any
    /// register during this process so that it can decide how much memory to
    /// allocate to these operations. For example, in the above instructions, the
    /// highest value ever held was 10 (in register c after the third instruction
    /// was evaluated).
    /// </summary>
    [AdventOfCode(2017, 8, "I Heard You Like Registers", 5946, 6026)]
    public class AdventOfCode201708 : AdventOfCodeBase
    {
        public AdventOfCode201708(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var registers = new Dictionary<string, int>();
            foreach (var line in Input)
            {
                var splits = line.Split();
                var reg = splits[0];
                var instr = splits[1];
                var value = int.Parse(splits[2], CultureInfo.InvariantCulture);
                var otherReg = splits[4];
                var compare = splits[5];
                var compareValue = int.Parse(splits[6], CultureInfo.InvariantCulture);

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

            return registers.Max(x => x.Value);
        }

        protected override object SolvePart2()
        {
            var registers = new Dictionary<string, int>();
            var max = 0;
            foreach (var line in Input)
            {
                var splits = line.Split();
                var reg = splits[0];
                var instr = splits[1];
                var value = int.Parse(splits[2], CultureInfo.InvariantCulture);
                var otherReg = splits[4];
                var compare = splits[5];
                var compareValue = int.Parse(splits[6], CultureInfo.InvariantCulture);

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

            return max;
        }
    }
}
