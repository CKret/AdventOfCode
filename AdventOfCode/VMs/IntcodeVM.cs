﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.VMs
{
    public class IntcodeVM
    {
        private readonly int[] program;
        private readonly int[] memory;
        private int instrPtr;

        private Queue<int> Inputs = new Queue<int>();
        private Queue<int> Outputs = new Queue<int>();

        private int currentModeParam1 = 0;
        private int currentModeParam2 = 0;
        private int currentModeParam3 = 0;

        public IntcodeVM(string instructions)
        {
            if (instructions == null)
            {
                throw new ArgumentNullException("instructions");
            }
            program = instructions.Split(',').Select(int.Parse).ToArray();
            memory = new int[program.Length];
            ResetVM();
        }

        public void ResetVM()
        {
            program.CopyTo(memory, 0);
            instrPtr = 0;
        }

        public int[] ExecuteProgram(int[] inputs)
        {
            foreach (var i in inputs)
                Inputs.Enqueue(i);

            return ExecuteProgram();
        }

        public int[] ExecuteProgram()
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
                    
                    case 3:             // Input
                        WriteMemory(memory[instrPtr + 1], Inputs.Dequeue());
                        instrPtr += 2;
                        break;
                    
                    case 4:             // Output
                        Outputs.Enqueue(memory[memory[instrPtr + 1]]);
                        instrPtr += 2;
                        break;
                    
                    case 5:             // Jump if true
                        if (GetParamValue(1) > 0) instrPtr = GetParamValue(2);
                        else instrPtr += 3;
                        break;

                    case 6:             // Jump if false
                        if (GetParamValue(1) == 0) instrPtr = GetParamValue(2);
                        else instrPtr += 3;
                        break;

                    case 7:             // Less than
                        memory[memory[instrPtr + 3]] = GetParamValue(1) < GetParamValue(2) ? 1 : 0;
                        instrPtr += 4;
                        break;
                    
                    case 8:             // Equals
                        memory[memory[instrPtr + 3]] = GetParamValue(1) == GetParamValue(2) ? 1 : 0;
                        instrPtr += 4;
                        break;

                    case 99:            // End of program
                        return Outputs.ToArray();

                    default:
                        throw new Exception($"Invalid Op-Code: {GetOpCode()}");
                }
            }
        }

        public int ReadMemory(int address) { return memory[address]; }

        public void WriteMemory(int address, int value) { memory[address] = value; }

        public void ClearMemory(int address) { memory[address] = 0; }

        private int GetOpCode() { return memory[instrPtr] % 100; }

        private int[] GetModes()
        {
            var modes = new List<int>();
            var op = memory[instrPtr];
            modes.Add(op / 100 % 10);
            modes.Add(op / 1000 % 10);
            modes.Add(op / 10000 % 10);

            return modes.ToArray();
        }

        private int GetParamValue(int param)
        {
            var modes = GetModes();
            return modes[param - 1] == 0 ? ReadMemory(memory[instrPtr + param]) : ReadMemory(instrPtr + param);
        }

        private void SetParamValue(int param, int value)
        {
            WriteMemory(GetParamValue(param), value);
        }

        private void Add()
        {
            memory[memory[instrPtr + 3]] = GetParamValue(1) + GetParamValue(2);
            //SetParamValue(3, GetParamValue(1) + GetParamValue(2));
            instrPtr += 4;
        }

        private void Sub()
        {
            memory[memory[instrPtr + 3]] = GetParamValue(1) - GetParamValue(2);
            //SetParamValue(3, GetParamValue(1) - GetParamValue(2));
            instrPtr += 4;
        }

        private void Mul()
        {
            memory[memory[instrPtr + 3]] = GetParamValue(1) * GetParamValue(2);
            //SetParamValue(3, GetParamValue(1) * GetParamValue(2));
            instrPtr += 4;
        }

        private void Div()
        {
            memory[memory[instrPtr + 3]] = GetParamValue(1) / GetParamValue(2);
            //SetParamValue(3, GetParamValue(1) / GetParamValue(2));
            instrPtr += 4;
        }
    }
}
