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
            var program = Input;

            for (var row = 0; row < Input.Length; row++)
            {
                var op = program[row].Split(' ')[0];
                if (op == "acc") continue;

                var tmp = program[row];
                program[row] = op == "jmp" ? program[row].Replace("jmp", "nop") : program[row].Replace("nop", "jmp");

                if (!IsLoop(program))
                    return RunProgram(program);

                program[row] = tmp;
            }

            return 0;
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
            }

            return acc;
        }

        private bool IsLoop(string[] program)
        {
            var ipValues = new List<int>();
            for (var ip = 0; ip >= 0 && ip < program.Length; ip++)
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
            }

            return false;
        }
    }
}
