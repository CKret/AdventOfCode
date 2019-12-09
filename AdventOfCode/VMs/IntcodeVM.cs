using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.VMs
{
    public class IntcodeVM
    {
        private readonly long[] program;
        private readonly long[] memory;
        private long instrPtr;
        private long relativeBase;

        public Queue<long> Input = new Queue<long>();
        public Queue<long> Output = new Queue<long>();

        public IntcodeVM(string instructions)
        {
            if (instructions == null)
            {
                throw new ArgumentNullException("instructions");
            }
            program = instructions.Split(',').Select(long.Parse).ToArray();
            memory = new long[program.Length + 100000];
            ResetVM();
        }

        public void ResetVM()
        {
            program.CopyTo(memory, 0);
            Input.Clear();
            Output.Clear();
            instrPtr = 0;
            relativeBase = 0;
        }

        public HaltMode Execute(long[] inputs)
        {
            foreach (var i in inputs)
                Input.Enqueue(i);

            return Execute();
        }

        public HaltMode Execute()
        {
            while (true)
            {
                var modes = GetModes();
                switch (GetOpCode())
                {
                    case 1:             // Addition
                        if (modes[2] == 2)
                            memory[relativeBase + memory[instrPtr + 3]] = GetParamValue(1) + GetParamValue(2);
                        else
                            memory[memory[instrPtr + 3]] = GetParamValue(1) + GetParamValue(2);
                        instrPtr += 4;
                        break;
                    
                    case 2:             // Multiplication
                        if (modes[2] == 2)
                            memory[relativeBase + memory[instrPtr + 3]] = GetParamValue(1) * GetParamValue(2);
                        else
                            memory[memory[instrPtr + 3]] = GetParamValue(1) * GetParamValue(2);
                        instrPtr += 4;
                        break;
                    
                    case 3:             // Input
                        if (Input.Count == 0) return HaltMode.WaitingForInput;    // If there is no input we must wait.
                        if (modes[0] == 2)
                            memory[relativeBase + memory[instrPtr + 1]] = Input.Dequeue();
                        else
                            memory[memory[instrPtr + 1]] = Input.Dequeue();
                        //WriteMemory(GetParamValue(1), Input.Dequeue());
                        instrPtr += 2;
                        break;
                    
                    case 4:             // Output
                        Output.Enqueue(GetParamValue(1));
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
                        if (modes[2] == 2)
                            memory[relativeBase + memory[instrPtr + 3]] = GetParamValue(1) < GetParamValue(2) ? 1 : 0;
                        else
                            memory[memory[instrPtr + 3]] = GetParamValue(1) < GetParamValue(2) ? 1 : 0;
                        instrPtr += 4;
                        break;
                    
                    case 8:             // Equals
                        if (modes[2] == 2)
                            memory[relativeBase + memory[instrPtr + 3]] = GetParamValue(1) == GetParamValue(2) ? 1 : 0;
                        else
                            memory[memory[instrPtr + 3]] = GetParamValue(1) == GetParamValue(2) ? 1 : 0;
                        instrPtr += 4;
                        break;

                    case 9:
                        relativeBase += GetParamValue(1);
                        instrPtr += 2;
                        break;

                    case 99:            // End of program
                        return HaltMode.Terminated;

                    default:
                        throw new Exception($"Invalid Op-Code: {GetOpCode()}");
                }
            }
        }

        public long ReadMemory(long address) { return memory[address]; }

        public void WriteMemory(long address, long value) { memory[address] = value; }

        public void ClearMemory(long address) { memory[address] = 0; }

        private long GetOpCode() { return memory[instrPtr] % 100; }

        private long[] GetModes()
        {
            var modes = new List<long>();
            var op = memory[instrPtr];
            modes.Add(op / 100 % 10);
            modes.Add(op / 1000 % 10);
            modes.Add(op / 10000 % 10);

            return modes.ToArray();
        }

        private long GetParamValue(long param)
        {
            var modes = GetModes();
            switch (modes[param - 1])
            {
                case 0:
                    return ReadMemory(memory[instrPtr + param]);                          // Position mode
                case 1:
                    return ReadMemory(instrPtr + param);                           // Immediate mode
                case 2:
                    return ReadMemory(relativeBase + memory[instrPtr + param]);    // Relative mode 
                default:
                    return 0;
            }
        }

        private void SetParamValue(long param, long value)
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

        public enum HaltMode
        {
            Unknown,
            Terminated,
            WaitingForInput
        }
    }
}
