using System.Collections.Generic;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using System.Diagnostics;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 8, "Day 8: Handheld Halting", 1384, 761)]
    public class AdventOfCode202008 : AdventOfCodeBase
    {
        public AdventOfCode202008(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            var timer = new Stopwatch();
            var data = Input;

            timer.Start();
            ResultPart1 = SolvePart1();
            timer.Stop();
            TimePart1 = timer.ElapsedTicks.ToMilliseconds();

            timer.Start();
            ResultPart2 = SolvePart2();
            timer.Stop();
            TimePart2 = timer.ElapsedTicks.ToMilliseconds();
        }

        private object SolvePart1()
        {
            var acc = 0;
            var ipValues = new List<int>();
            var ip = 0;

            while (true)
            {
                if (ipValues.Contains(ip)) break;
                ipValues.Add(ip);

                var current = Input[ip];
                var op = current.Split(' ')[0];
                var value = int.Parse(current.Split(' ')[1]);
                switch (op)
                {
                    case "acc": acc += value;
                        break;
                    case "jmp": ip += value - 1;
                        break;
                    case "nop":
                        break;
                }
                ip++;
            }

            return acc;
        }

        private object SolvePart2()
        {
            var program = new string[Input.Length];

            var acc = 0;
            for (var row = 0; row < Input.Length; row++)
            {
                var op = Input[row].Split(' ')[0];
                if (op == "acc") continue;

                Input.CopyTo(program, 0);
                program[row] = op == "jmp" ? program[row].Replace("jmp", "nop") : program[row].Replace("nop", "jmp");

                acc = 0;
                if (IsLoop(program)) continue;
                return RunProgram(program);
            }

            return acc;
        }

        private int RunProgram(string[] program)
        {
            var acc = 0;
            var ip = 0;

            while (ip >= 0 && ip < program.Length)
            {
                var current = program[ip];
                var op = current.Split(' ')[0];
                var value = int.Parse(current.Split(' ')[1]);
                switch (op)
                {
                    case "acc":
                        acc += value;
                        break;
                    case "jmp":
                        ip += value - 1;
                        break;
                    case "nop":
                        break;
                }
                ip++;

                if (ip < 0 || ip >= Input.Length) return acc;
            }

            return 0;
        }

        private bool IsLoop(string[] program)
        {
            var ipValues = new List<int>();
            var ip = 0;

            while (true)
            {
                var acc = 0;

                if (ipValues.Contains(ip)) return true;
                ipValues.Add(ip);

                var current = program[ip];
                var op = current.Split(' ')[0];
                var value = int.Parse(current.Split(' ')[1]);
                switch (op)
                {
                    case "acc":
                        acc += value;
                        break;
                    case "jmp":
                        ip += value - 1;
                        break;
                    case "nop":
                        break;
                }
                ip++;

                if (ip < 0 || ip >= Input.Length) return false;
            }
        }
    }
}
