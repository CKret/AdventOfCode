using System;
using System.Linq;

namespace AdventOfCode.VMs
{
    public class IntcodeVM
    {
        private readonly int[] program;
        private readonly int[] memory;
        private int instrPtr;

        public IntcodeVM(string instructions)
        {
            program = instructions.Split(',').Select(int.Parse).ToArray();
            memory = new int[program.Length];
            ResetVM();
        }

        public void ResetVM()
        {
            program.CopyTo(memory, 0);
            instrPtr = 0;
        }

        public void ExecuteProgram()
        {
            while (true)
            {
                switch (GetOpCode())
                {
                    case 1:             // Addition
                        Add();
                        break;
                    case 2:             // Multiplication
                        Mul();
                        break;
                    case 99:            // End of program
                        return;

                    default:
                        throw new Exception($"Invalid Op-Code: {memory[instrPtr]}");
                }
            }
        }

        public int ReadMemory(int addr) { return memory[addr]; }

        public void WriteMemory(int addr, int value) { memory[addr] = value; }

        public void ClearMemory(int addr) { memory[addr] = 0; }

        private int GetOpCode() { return memory[instrPtr++]; }

        private void Add()
        {
            memory[memory[instrPtr + 2]] = memory[memory[instrPtr]] + memory[memory[instrPtr + 1]];
            instrPtr += 3;
        }

        private void Sub()
        {
            memory[memory[instrPtr + 2]] = memory[memory[instrPtr]] - memory[memory[instrPtr + 1]];
            instrPtr += 3;
        }

        private void Mul()
        {
            memory[memory[instrPtr + 2]] = memory[memory[instrPtr]] * memory[memory[instrPtr + 1]];
            instrPtr += 3;
        }

        private void Div()
        {
            memory[memory[instrPtr + 2]] = memory[memory[instrPtr]] / memory[memory[instrPtr + 1]];
            instrPtr += 3;
        }
    }
}
