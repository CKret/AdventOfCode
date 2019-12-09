using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.VMs
{

    public enum HaltMode
    {
        Unknown,
        Terminated,
        WaitingForInput
    }

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
                throw new ArgumentNullException(nameof(instructions));
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
            if (inputs != null)
            {
                foreach (var i in inputs)
                    Input.Enqueue(i);
            }

            return Execute();
        }

        public HaltMode Execute()
        {
            while (true)
            {
                switch (GetOpCode())
                {
                    case 1:             // Addition
                        WriteValue(3, ReadValue(1) + ReadValue(2));
                        instrPtr += 4;
                        break;

                    case 2:             // Multiplication
                        WriteValue(3, ReadValue(1) * ReadValue(2));
                        instrPtr += 4;
                        break;

                    case 3:             // Input
                        if (Input.Count == 0) return HaltMode.WaitingForInput;          // If there is no input we must wait.
                        WriteValue(1, Input.Dequeue());
                        instrPtr += 2;
                        break;

                    case 4:             // Output
                        Output.Enqueue(ReadValue(1));
                        instrPtr += 2;
                        break;

                    case 5:             // Jump if true
                        if (ReadValue(1) > 0) instrPtr = ReadValue(2);
                        else instrPtr += 3;
                        break;

                    case 6:             // Jump if false
                        if (ReadValue(1) == 0) instrPtr = ReadValue(2);
                        else instrPtr += 3;
                        break;

                    case 7:             // Less than
                        WriteValue(3, ReadValue(1) < ReadValue(2) ? 1 : 0);
                        instrPtr += 4;
                        break;

                    case 8:             // Equals
                        WriteValue(3, ReadValue(1) == ReadValue(2) ? 1 : 0);
                        instrPtr += 4;
                        break;

                    case 9:             // Alter Relative Base
                        relativeBase += ReadValue(1);
                        instrPtr += 2;
                        break;

                    case 99:            // End of program
                        return HaltMode.Terminated;

                    default:
                        throw new Exception($"Invalid Op-Code: {GetOpCode()}");
                }
            }
        }

        public long Read(long address) { return memory[address]; }

        public void Write(long address, long value) { memory[address] = value; }

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

        public long ReadValue(long param)
        {
            var modes = GetModes();
            switch (modes[param - 1])
            {
                case 0:                                                             // Position mode
                    return Read(memory[instrPtr + param]);
                case 1:                                                             // Immediate mode
                    return Read(instrPtr + param);
                case 2:                                                             // Relative mode
                    return Read(relativeBase + memory[instrPtr + param]);
                default:
                    return 0;
            }
        }

        public void WriteValue(long param, long value)
        {
            var modes = GetModes();
            switch (modes[param - 1])
            {
                case 0:                                                             // Position mode
                    Write(memory[instrPtr + param], value);
                    break;
                case 1:                                                             // Immediate mode
                    Write(instrPtr + param, value);
                    break;
                case 2:                                                             // Relative mode
                    Write(relativeBase + memory[instrPtr + param], value);
                    break;
                default:
                    return;
            }
        }
    }
}
