﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 18: Duet ---
    ///
    /// --- Part Two ---
    /// 
    /// As you congratulate yourself for a job well done, you notice that the
    /// documentation has been on the back of the tablet this entire time. While
    /// you actually got most of the instructions correct, there are a few key
    /// differences. This assembly code isn't about sound at all - it's meant to be
    /// run twice at the same time.
    /// 
    /// Each running copy of the program has its own set of registers and follows
    /// the code independently - in fact, the programs don't even necessarily run
    /// at the same speed. To coordinate, they use the send (snd) and receive (rcv)
    /// instructions:
    /// 
    ///     - snd X sends the value of X to the other program. These values wait in
    ///       a queue until that program is ready to receive them. Each program has
    ///       its own message queue, so a program can never receive a message it
    ///       sent.
    ///     - rcv X receives the next value and stores it in register X. If no
    ///       values are in the queue, the program waits for a value to be sent to
    ///       it. Programs do not continue to the next instruction until they have
    ///       received a value. Values are received in the order they are sent.
    /// 
    /// Each program also has its own program ID (one 0 and the other 1);
    /// the register p should begin with this value.
    /// 
    /// For example:
    /// 
    /// snd 1
    /// snd 2
    /// snd p
    /// rcv a
    /// rcv b
    /// rcv c
    /// rcv d
    /// 
    /// Both programs begin by sending three values to the other. Program 0 sends
    /// 1, 2, 0; program 1 sends 1, 2, 1. Then, each program receives a value (both
    /// 1) and stores it in a, receives another value (both 2) and stores it in b,
    /// and then each receives the program ID of the other program (program 0
    /// receives 1; program 1 receives 0) and stores it in c. Each program now sees
    /// a different value in its own copy of register c.
    /// 
    /// Finally, both programs try to rcv a fourth time, but no data is waiting for
    /// either of them, and they reach a deadlock. When this happens, both programs
    /// terminate.
    /// 
    /// It should be noted that it would be equally valid for the programs to run
    /// at different speeds; for example, program 0 might have sent all three
    /// values and then stopped at the first rcv before program 1 executed even its
    /// first instruction.
    /// 
    /// Once both of your programs have terminated (regardless of what caused them
    /// to do so), how many times did program 1 send a value?
    ///  
    /// </summary>
    [AdventOfCode(2017, 18, "Duet - Part Two", 6858)]
    public class AdventOfCode2017182 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var instructions = ParseInstructions(File.ReadAllLines("2017\\AdventOfCode201718.txt"));

            var p0 = new Program(0, instructions);
            var p1 = new Program(1, instructions);

            while (p0.ExecuteInstruction(p1.Queue) || p1.ExecuteInstruction(p0.Queue));

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

        public AdventOfCode2017182(string sessionCookie) : base(sessionCookie) { }
    }
}
