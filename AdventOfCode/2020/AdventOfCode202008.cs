using System.Collections.Generic;
using AdventOfCode.Core;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 8, "Handheld Halting", 1384, 761)]
    public class AdventOfCode202008 : AdventOfCodeBase
    {
        public AdventOfCode202008(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
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

        protected override object SolvePart2()
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

        private static int RunProgram(string[] program)
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

        private static bool IsLoop(string[] program)
        {
            var ipValues = new List<int>();
            for (var ip = 0; ip >= 0 && ip < program.Length; ip++)
            {
                if (ipValues.Contains(ip)) return true;
                ipValues.Add(ip);

                var current = program[ip];
                var op = current.Split(' ')[0];
                var value = int.Parse(current.Split(' ')[1]);
                switch (op)
                {
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
