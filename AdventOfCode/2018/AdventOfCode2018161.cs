using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using AdventOfCode.VMs;

namespace AdventOfCode._2018
{
    /// <summary>
    /// --- Day 16: Chronal Classification ---
    /// 
    /// As you see the Elves defend their hot chocolate successfully, you go back
    /// to falling through time. This is going to become a problem.
    /// 
    /// If you're ever going to return to your own time, you need to understand how
    /// this device on your wrist works. You have a little while before you reach
    /// your next destination, and with a bit of trial and error, you manage to
    /// pull up a programming manual on the device's tiny screen.
    /// 
    /// According to the manual, the device has four registers (numbered 0 through
    /// 3) that can be manipulated by instructions containing one of 16 opcodes.
    /// The registers start with the value 0.
    /// 
    /// Every instruction consists of four values: an opcode, two inputs (named A
    /// and B), and an output (named C), in that order. The opcode specifies the
    /// behavior of the instruction and how the inputs are interpreted. The output,
    /// C, is always treated as a register.
    /// 
    /// In the opcode descriptions below, if something says "value A", it means to
    /// take the number given as A literally. (This is also called an "immediate"
    /// value.) If something says "register A", it means to use the number given as
    /// A to read from (or write to) the register with that number. So, if the
    /// opcode addi adds register A and value B, storing the result in register C,
    /// and the instruction addi 0 7 3 is encountered, it would add 7 to the value
    /// contained by register 0 and store the sum in register 3, never modifying
    /// registers 0, 1, or 2 in the process.
    /// 
    /// Many opcodes are similar except for how they interpret their arguments. The
    /// opcodes fall into seven general categories:
    ///
    /// Addition:
    /// 
    /// - addr (add register) stores into register C the result of adding
    ///   register A and register B.
    /// - addi (add immediate) stores into register C the result of adding
    ///   register A and value B.
    /// 
    /// Multiplication:
    /// 
    /// - mulr (multiply register) stores into register C the result of
    ///   multiplying register A and register B.
    /// - muli (multiply immediate) stores into register C the result of
    ///   multiplying register A and value B.
    /// 
    /// Bitwise AND:
    /// 
    /// - banr (bitwise AND register) stores into register C the result of the
    ///   bitwise AND of register A and register B.
    /// - bani (bitwise AND immediate) stores into register C the result of the
    ///   bitwise AND of register A and value B.
    /// 
    /// Bitwise OR:
    /// 
    /// - borr (bitwise OR register) stores into register C the result of the
    ///   bitwise OR of register A and register B.
    /// - bori (bitwise OR immediate) stores into register C the result of the
    ///   bitwise OR of register A and value B.
    /// 
    /// Assignment:
    /// 
    /// - setr (set register) copies the contents of register A into register C.
    ///   (Input B is ignored.)
    /// - seti (set immediate) stores value A into register C. (Input B is
    ///   ignored.)
    /// 
    /// Greater-than testing:
    /// 
    /// - gtir (greater-than immediate/register) sets register C to 1 if value A
    ///   is greater than register B. Otherwise, register C is set to 0.
    /// - gtri (greater-than register/immediate) sets register C to 1 if
    ///   register A is greater than value B. Otherwise, register C is set to 0.
    /// - gtrr (greater-than register/register) sets register C to 1 if register
    ///   A is greater than register B. Otherwise, register C is set to 0.
    /// 
    /// Equality testing:
    /// 
    /// - eqir (equal immediate/register) sets register C to 1 if value A is
    ///   equal to register B. Otherwise, register C is set to 0.
    /// - eqri (equal register/immediate) sets register C to 1 if register A is
    ///   equal to value B. Otherwise, register C is set to 0.
    /// - eqrr (equal register/register) sets register C to 1 if register A is
    ///   equal to register B. Otherwise, register C is set to 0.
    ///
    /// Unfortunately, while the manual gives the name of each opcode, it doesn't
    /// seem to indicate the number. However, you can monitor the CPU to see the
    /// contents of the registers before and after instructions are executed to try
    /// to work them out. Each opcode has a number from 0 through 15, but the
    /// manual doesn't say which is which. For example, suppose you capture the
    /// following sample:
    /// 
    /// Before: [3, 2, 1, 1]
    /// 9 2 1 2
    /// After:  [3, 2, 2, 1]
    /// 
    /// This sample shows the effect of the instruction 9 2 1 2 on the registers.
    /// Before the instruction is executed, register 0 has value 3, register 1 has
    /// value 2, and registers 2 and 3 have value 1. After the instruction is
    /// executed, register 2's value becomes 2.
    /// 
    /// The instruction itself, 9 2 1 2, means that opcode 9 was executed with A=2,
    /// B=1, and C=2. Opcode 9 could be any of the 16 opcodes listed above, but
    /// only three of them behave in a way that would cause the result shown in the
    /// sample:
    /// 
    /// - Opcode 9 could be mulr: register 2 (which has a value of 1) times
    ///   register 1 (which has a value of 2) produces 2, which matches the
    ///   value stored in the output register, register 2.
    /// - Opcode 9 could be addi: register 2 (which has a value of 1) plus value
    ///   1 produces 2, which matches the value stored in the output register,
    ///   register 2.
    /// - Opcode 9 could be seti: value 2 matches the value stored in the output
    ///   register, register 2; the number given for B is irrelevant.
    /// 
    /// None of the other opcodes produce the result captured in the sample.
    /// Because of this, the sample above behaves like three opcodes.
    /// 
    /// You collect many of these samples (the first section of your puzzle input).
    /// The manual also includes a small test program (the second section of your
    /// puzzle input) - you can ignore it for now.
    /// 
    /// Ignoring the opcode numbers, how many samples in your puzzle input behave
    /// like three or more opcodes?
    /// </summary>
    [AdventOfCode(2018, 16, "Chronal Classification - Part 1", 560)]
    public class AdventOfCode2018161 : AdventOfCodeBase
    {
        public AdventOfCode2018161(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            var samples = File.ReadAllText(@"2018\AdventOfCode201816.txt").Split("\n\n\n")[0]
                        .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
                        .Select(
                            s =>
                            {
                                var set = Regex.Matches(s, @"\d+")
                                               .Cast<Match>()
                                               .Select(m => int.Parse(m.Value))
                                               .ToArray();

                                var br = set.Take(4).ToArray();
                                var ins = set.Skip(4).Take(4).ToArray();
                                var ar = set.Skip(8).ToArray();

                                return new ChronalInstructionEngine.ChronalInstructionSample(br, ar, new ChronalInstructionEngine.ChronalInstruction(ins));
                            }).ToArray();

            Result = samples.Count(i => i.PossibleInstructions().Count() >= 3);
        }
    }

    //public class ChronalInstructionEngine
    //{
    //    private int[] translation { get; }

    //    public ChronalInstructionEngine()
    //    {
    //        translation = Enumerable.Range(0, 16).ToArray();
    //    }

    //    public ChronalInstructionEngine(int[] translation)
    //    {
    //        this.translation = translation;
    //    }

    //    public int[] Execute(int[] registersBefore, ChronalInstruction instruction)
    //    {
    //        var registersAfter = new int[4];
    //        registersBefore.CopyTo(registersAfter, 0);

    //        var opCode = translation[instruction.OpCode];
    //        var a = instruction.A;
    //        var b = instruction.B;
    //        var c = instruction.C;

    //        if (opCode == 0) registersAfter[c] = registersBefore[a] + registersBefore[b];
    //        else if (opCode == 1) registersAfter[c] = registersBefore[a] + b;
    //        else if (opCode == 2) registersAfter[c] = registersBefore[a] * registersBefore[b];
    //        else if (opCode == 3) registersAfter[c] = registersBefore[a] * b;
    //        else if (opCode == 4) registersAfter[c] = registersBefore[a] & registersBefore[b];
    //        else if (opCode == 5) registersAfter[c] = registersBefore[a] & b;
    //        else if (opCode == 6) registersAfter[c] = registersBefore[a] | registersBefore[b];
    //        else if (opCode == 7) registersAfter[c] = registersBefore[a] | b;
    //        else if (opCode == 8) registersAfter[c] = registersBefore[a];
    //        else if (opCode == 9) registersAfter[c] = a;
    //        else if (opCode == 10) registersAfter[c] = a > registersBefore[b] ? 1 : 0;
    //        else if (opCode == 11) registersAfter[c] = registersBefore[a] > b ? 1 : 0;
    //        else if (opCode == 12) registersAfter[c] = registersBefore[a] > registersBefore[b] ? 1 : 0;
    //        else if (opCode == 13) registersAfter[c] = a == registersBefore[b] ? 1 : 0;
    //        else if (opCode == 14) registersAfter[c] = registersBefore[a] == b ? 1 : 0;
    //        else if (opCode == 15) registersAfter[c] = registersBefore[a] == registersBefore[b] ? 1 : 0;

    //        return registersAfter;
    //    }
    //}

    //public class ChronalInstructionSample
    //{
    //    private readonly int[] registersBefore;
    //    private readonly int[] registersAfter;
    //    private readonly ChronalInstruction instruction;

    //    public int OpCode => instruction.OpCode;

    //    public ChronalInstructionSample(int[] registersBefore, int[] registersAfter, ChronalInstruction instruction)
    //    {
    //        if (registersBefore.Length != 4) throw new ArgumentException("Parameter must be an array of length 4.", nameof(registersBefore));
    //        if (registersAfter.Length != 4) throw new ArgumentException("Parameter must be an array of length 4.", nameof(registersAfter));
    //        if (instruction == null) throw new ArgumentException("Parameter must not be null.", nameof(instruction));

    //        this.registersBefore = registersBefore;
    //        this.registersAfter = registersAfter;
    //        this.instruction = instruction;
    //    }

    //    public IEnumerable<int> PossibleInstructions()
    //    {
    //        var offset = (16 - instruction.OpCode) % 16;
    //        var range = Enumerable.Range(offset, 16 - offset).ToList();
    //        range.AddRange(Enumerable.Range(0, offset));
    //        var translation = range.ToArray();

    //        for (var i = 0; i < 16; i++)
    //        {
    //            var engine = new ChronalInstructionEngine(translation);
    //            if (engine.Execute(registersBefore, instruction).SequenceEqual(registersAfter))
    //                yield return i;

    //            translation = translation.RotateLeft();
    //        }
    //    }
    //}

    //public class ChronalInstruction
    //{
    //    private readonly int[] instruction;

    //    public int OpCode => instruction[0];
    //    public int A => instruction[1];
    //    public int B => instruction[2];
    //    public int C => instruction[3];

    //    public ChronalInstruction(int[] instruction)
    //    {
    //        if (instruction.Length != 4) throw new ArgumentException("Array must be of length 4.", nameof(instruction));
    //        this.instruction = instruction;
    //    }
    //}
}
