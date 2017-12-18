using System.Collections.Generic;
using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 18: Duet ---
    /// 
    /// You discover a tablet containing some strange assembly code labeled simply
    /// "Duet". Rather than bother the sound card with it, you decide to run the
    /// code yourself. Unfortunately, you don't see any documentation, so you're
    /// left to figure out what the instructions mean on your own.
    /// 
    /// It seems like the assembly is meant to operate on a set of registers that
    /// are each named with a single letter and that can each hold a single
    /// integer. You suppose each register should start with a value of 0.
    /// 
    /// There aren't that many instructions, so it shouldn't be hard to figure out
    ///  what they do. Here's what you determine:
    /// 
    ///     - snd X plays a sound with a frequency equal to the value of X.
    ///     - set X Y sets register X to the value of Y.
    ///     - add X Y increases register X by the value of Y.
    ///     - mul X Y sets register X to the result of multiplying the value
    ///       contained in register X by the value of Y.
    ///     - mod X Y sets register X to the remainder of dividing the value
    ///       contained in register X by the value of Y (that is, it sets X to the
    ///       result of X modulo Y).
    ///     - rcv X recovers the frequency of the last sound played, but only when
    ///       the value of X is not zero. (If it is zero, the command does nothing.)
    ///     - jgz X Y jumps with an offset of the value of Y, but only if the value
    ///       of X is greater than zero. (An offset of 2 skips the next instruction,
    ///       an offset of -1 jumps to the previous instruction, and so on.)
    /// 
    /// Many of the instructions can take either a register (a single letter) or a
    /// number. The value of a register is the integer it contains; the value of a
    /// number is that number.
    /// 
    /// After each jump instruction, the program continues with the instruction to
    /// which the jump jumped. After any other instruction, the program continues
    /// with the next instruction. Continuing (or jumping) off either end of the
    /// program terminates it.
    /// 
    /// For example:
    /// 
    /// set a 1
    /// add a 2
    /// mul a a
    /// mod a 5
    /// snd a
    /// set a 0
    /// rcv a
    /// jgz a -1
    /// set a 1
    /// jgz a -2
    /// 
    ///     - The first four instructions set a to 1, add 2 to it, square it, and
    ///       then set it to itself modulo 5, resulting in a value of 4.
    ///     - Then, a sound with frequency 4 (the value of a) is played.
    ///     - After that, a is set to 0, causing the subsequent rcv and jgz
    ///       instructions to both be skipped (rcv because a is 0, and jgz because a
    ///       is not greater than 0).
    ///     - Finally, a is set to 1, causing the next jgz instruction to activate,
    ///       jumping back two instructions to another jump, which jumps again to
    ///       the rcv, which ultimately triggers the recover operation.
    /// 
    /// At the time the recover operation is executed, the frequency of the last
    /// sound played is 4.
    /// 
    /// What is the value of the recovered frequency (the value of the most
    /// recently played sound) the first time a rcv instruction is executed with a
    /// non-zero value?
    /// 
    /// </summary>
    [AdventOfCode(2017, 18, 2, "Duet - Part One", 6858)]
    public class AdventOfCode2017182 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var instructions = File.ReadAllLines("2017\\AdventOfCode201718.txt");
            var currInstr = new int[2];

            var registers = new Dictionary<string, long>[2];
            registers[0] = new Dictionary<string, long> {{"p", 0}};
            registers[1] = new Dictionary<string, long> {{"p", 1}};

            var programQueue = new Queue<long>[2];
            programQueue[0] = new Queue<long>();
            programQueue[1] = new Queue<long>();

            var count = 0;
            while (true)
            {
                // program 0
                var cur = instructions[currInstr[0]].Split();
                var instr = cur[0];
                var opA = cur[1];
                var opB = string.Empty;
                if (cur.Length > 2) opB = cur[2];
                var jmp0 = 0;
                if (currInstr[0] >= 0 && currInstr[0] < instructions.Length)
                    jmp0 = ExecuteInstruction(instr, opA, opB, registers[0], programQueue[0], programQueue[1]);

                // program 1
                cur = instructions[currInstr[1]].Split();
                instr = cur[0];
                opA = cur[1];
                if (cur.Length > 2) opB = cur[2];
                var jmp1= 0;
                if (currInstr[1] >= 0 && currInstr[1] < instructions.Length)
                {
                    if (instr == "snd") count++;
                    jmp1 = ExecuteInstruction(instr, opA, opB, registers[1], programQueue[1], programQueue[0]);
                }

                if (jmp0 == 0 && jmp1 == 0) break;

                currInstr[0] += jmp0;
                currInstr[1] += jmp1;
            }

            Result = count;
        }

        private static int ExecuteInstruction(string instr, string opA, string opB, Dictionary<string, long> registers, Queue<long> queue, Queue<long> otherQueue)
        {
            if (!registers.ContainsKey(opA)) registers.Add(opA, 0);
            switch (instr)
            {
                case "snd":
                    if (int.TryParse(opA, out var val)) otherQueue.Enqueue(int.Parse(opA));
                    else otherQueue.Enqueue(registers[opA]);
                    break;
                case "set":
                    if (int.TryParse(opB, out val)) registers[opA] = val;
                    else registers[opA] = registers[opB];
                    break;
                case "add":
                    if (int.TryParse(opB, out val)) registers[opA] += val;
                    else registers[opA] += registers[opB];
                    break;
                case "mul":
                    if (int.TryParse(opB, out val)) registers[opA] *= val;
                    else registers[opA] *= registers[opB];
                    break;
                case "mod":
                    if (int.TryParse(opB, out val)) registers[opA] %= val;
                    else registers[opA] %= registers[opB];
                    break;
                case "rcv":
                    if (queue.Count == 0) return 0;
                    registers[opA] = queue.Dequeue();
                    break;
                case "jgz":
                    if (!long.TryParse(opA, out var valA)) valA = registers[opA];
                    if (valA > 0)
                    {
                        if (int.TryParse(opB, out val)) return val;
                        return (int) registers[opB];
                    }
                    break;
            }

            return 1;
        }
    }
}
