using System.Collections.Generic;
using System.IO;
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
    [AdventOfCode(2017, 18, 1, "Duet - Part Two", 4601L)]
    public class AdventOfCode2017181 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var instructions = File.ReadAllLines("2017\\AdventOfCode201718.txt");

            var registers = new Dictionary<string, long>();

            var currentSound = 0L;
            for (var i = 0L; i >= 0 && i < instructions.Length; i++)
            {
                var cur = instructions[i].Split();
                var instr = cur[0];
                var opA = cur[1];
                var opB = string.Empty;
                if (cur.Length > 2) opB = cur[2];

                if (!registers.ContainsKey(opA)) registers.Add(opA, 0);
                switch (instr)
                {
                    case "snd":
                        if (int.TryParse(opA, out var val)) currentSound = int.Parse(opA);
                        else currentSound = registers[opA];
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
                        if (registers[opA] != 0) Result = currentSound;
                        break;
                    case "jgz":
                        if (!long.TryParse(opA, out var valA)) valA = registers[opA];
                        if (valA > 0)
                        {
                            if (int.TryParse(opB, out val)) i += val - 1;
                            else i += registers[opB] - 1;
                        }
                        break;
                }

                if (Result != null) break;
            }
        }
    }
}
