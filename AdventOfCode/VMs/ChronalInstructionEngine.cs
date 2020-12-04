using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode.VMs
{
    public class ChronalInstructionEngine
    {
        private int[] Translation { get; }
        private int IPReg { get; }

        public ChronalInstructionEngine(int ipReg = -1)
        {
            Translation = Enumerable.Range(0, 16).ToArray();
            IPReg = ipReg;
        }

        public ChronalInstructionEngine(int[] translation, int ipReg = -1)
        {
            Translation = translation;
            IPReg = ipReg;
        }

        public (int InstructionPointer, int[] Register) Execute(int[] registersBefore, ChronalInstruction instruction, int instructionPointer = 0)
        {
            if (IPReg >= 0) registersBefore[IPReg] = instructionPointer;

            var registersAfter = new int[registersBefore.Length];
            registersBefore.CopyTo(registersAfter, 0);

            var opCode = Translation[instruction.OpCode];
            var a = instruction.A;
            var b = instruction.B;
            var c = instruction.C;

            registersAfter[c] = opCode switch
            {
                0 => registersBefore[a] + registersBefore[b],               // addr
                1 => registersBefore[a] + b,                                // addi
                2 => registersBefore[a] * registersBefore[b],               // mulr
                3 => registersBefore[a] * b,                                // muli
                4 => registersBefore[a] & registersBefore[b],               // banr
                5 => registersBefore[a] & b,                                // bani
                6 => registersBefore[a] | registersBefore[b],               // borr
                7 => registersBefore[a] | b,                                // bori
                8 => registersBefore[a],                                    // setr
                9 => a,                                                     // seti
                10 => a > registersBefore[b] ? 1 : 0,                       // gtir
                11 => registersBefore[a] > b ? 1 : 0,                       // gtri
                12 => registersBefore[a] > registersBefore[b] ? 1 : 0,      // gtrr
                13 => a == registersBefore[b] ? 1 : 0,                      // eqir
                14 => registersBefore[a] == b ? 1 : 0,                      // eqri
                15 => registersBefore[a] == registersBefore[b] ? 1 : 0,     // eqrr
                _ => registersAfter[c]
            };

            if (IPReg >= 0) instructionPointer = registersAfter[IPReg];

            return (instructionPointer + 1, registersAfter);
        }
        public class ChronalInstructionSample
        {
            private readonly int[] registersBefore;
            private readonly int[] registersAfter;
            private readonly ChronalInstruction instruction;

            public int OpCode => instruction.OpCode;

            public ChronalInstructionSample(int[] registersBefore, int[] registersAfter, ChronalInstruction instruction)
            {
                if (registersBefore.Length != 4) throw new ArgumentException("Parameter must be an array of length 4.", nameof(registersBefore));
                if (registersAfter.Length != 4) throw new ArgumentException("Parameter must be an array of length 4.", nameof(registersAfter));
                if (instruction == null) throw new ArgumentException("Parameter must not be null.", nameof(instruction));

                this.registersBefore = registersBefore;
                this.registersAfter = registersAfter;
                this.instruction = instruction;
            }

            public IEnumerable<int> PossibleInstructions()
            {
                var offset = (16 - instruction.OpCode) % 16;
                var range = Enumerable.Range(offset, 16 - offset).ToList();
                range.AddRange(Enumerable.Range(0, offset));
                var translation = range.ToArray();

                for (var i = 0; i < 16; i++)
                {
                    var engine = new ChronalInstructionEngine(translation);
                    if (engine.Execute(registersBefore, instruction).Register.SequenceEqual(registersAfter))
                        yield return i;

                    translation = translation.RotateLeft();
                }
            }
        }

        public class ChronalInstruction
        {
            private readonly int[] instruction;

            public int OpCode => instruction[0];
            public int A => instruction[1];
            public int B => instruction[2];
            public int C => instruction[3];

            public ChronalInstruction(int[] instruction)
            {
                if (instruction.Length != 4) throw new ArgumentException("Array must be of length 4.", nameof(instruction));
                this.instruction = instruction;
            }
        }
    }
}
