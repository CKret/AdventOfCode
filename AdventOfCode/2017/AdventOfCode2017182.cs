using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
            var instructions = ParseInstructions(File.ReadAllLines("2017\\AdventOfCode201718.txt"));

            var p0 = new Program(0, instructions);
            var p1 = new Program(1, instructions);

            while (p0.ExecuteInstruction(p1.Queue) || p1.ExecuteInstruction(p0.Queue))
            {
            }

            Result = p1.SndCount;
        }

        internal struct Duet
        {
            public string Instruction { get; set; }
            public string OperandDestination { get; set; }
            public string OperandValue { get; set; }
        }

        internal List<Duet> ParseInstructions(string[] instructions)
        {
            return instructions.Select(instruction => instruction.Split()).Select(i => new Duet {Instruction = i[0], OperandDestination = i[1], OperandValue = i.Length > 2 ? i[2] : string.Empty}).ToList();
        }

        internal class Program
        {
            public int ProgramId { get; }
            public ConcurrentQueue<long> Queue { get; } = new ConcurrentQueue<long>();
            public int SndCount { get; private set; }
            public int RcvCount { get; private set; }

            private readonly List<Duet> instructions;
            private readonly Dictionary<string, long> registers = new Dictionary<string, long>();
            private int EIP;

            public Program(int pId, List<Duet> instructions)
            {
                this.ProgramId = pId;
                this.instructions = instructions;
                this.registers.Add("p", pId);
            }

            public bool ExecuteInstruction(ConcurrentQueue<long> otherQueue)
            {
                if (EIP < 0 || EIP >= instructions.Count) return false;

                if (!registers.ContainsKey(instructions[EIP].OperandDestination)) registers.Add(instructions[EIP].OperandDestination, 0);
                switch (instructions[EIP].Instruction)
                {
                    case "snd":
                        otherQueue.Enqueue(ValueOf(instructions[EIP].OperandDestination));
                        SndCount++;
                        break;
                    case "set":
                        registers[instructions[EIP].OperandDestination] = ValueOf(instructions[EIP].OperandValue);
                        break;
                    case "add":
                        registers[instructions[EIP].OperandDestination] += ValueOf(instructions[EIP].OperandValue);
                        break;
                    case "mul":
                        registers[instructions[EIP].OperandDestination] *= ValueOf(instructions[EIP].OperandValue);
                        break;
                    case "mod":
                        registers[instructions[EIP].OperandDestination] %= ValueOf(instructions[EIP].OperandValue);
                        break;
                    case "rcv":
                        if (!Queue.TryDequeue(out var val)) return false;
                        registers[instructions[EIP].OperandDestination] = val;
                        RcvCount++;
                        break;
                    case "jgz":
                        if (ValueOf(instructions[EIP].OperandDestination) > 0) EIP += (int) ValueOf(instructions[EIP].OperandValue) - 1;
                        break;
                }

                EIP++;
                return true;
            }

            internal long ValueOf(string reg)
            {
                if (!long.TryParse(reg, out var val))
                    val = registers[reg];

                return val;
            }
        }
    }
}
