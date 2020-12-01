using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// Now, take the signal you got on wire a, override wire b to that signal, and reset the other wires (including wire a). What new signal is ultimately provided to wire a?
    /// </summary>
    [AdventOfCode(2015, 7, 2, "What new signal is ultimately provided to wire a?", 40149)]
    public class AdventOfCode2015072 : AdventOfCodeBase
    {
        Dictionary<string, string[]> instructions = new Dictionary<string, string[]>();

        public override void Solve()
        {
            instructions = File.ReadAllLines("2015/AdventOfCode201507.txt")
                .Select(i => i.Split(' '))
                .ToDictionary(i => i.Last());

            var val = EvalInput("a").ToString(CultureInfo.InvariantCulture);

            instructions = File.ReadAllLines("2015/AdventOfCode201507.txt")
                .Select(i => i.Split(' '))
                .ToDictionary(i => i.Last());

            instructions["b"] = new[] { val, "->", "b" };

            Result = (int) EvalInput("a");
        }

        ushort EvalInput(string wire)
        {
            ushort Eval(string x) => char.IsLetter(x[0]) ? EvalInput(x) : ushort.Parse(x, CultureInfo.InvariantCulture);
            ushort Assign(string[] x) => Eval(x[0]);
            ushort And(string[] x) => (ushort)(Eval(x[0]) & Eval(x[2]));
            ushort Or(string[] x) => (ushort)(Eval(x[0]) | Eval(x[2]));
            ushort Lshift(string[] x) => (ushort)(Eval(x[0]) << Eval(x[2]));
            ushort Rshift(string[] x) => (ushort)(Eval(x[0]) >> Eval(x[2]));
            ushort Not(string[] x) => (ushort)~Eval(x[1]);

            var currentInstruction = instructions[wire];

            ushort value;
            switch (currentInstruction[1])
            {
                case "->":
                    value = Assign(currentInstruction);
                    break;
                case "AND":
                    value = And(currentInstruction);
                    break;
                case "OR":
                    value = Or(currentInstruction);
                    break;
                case "LSHIFT":
                    value = Lshift(currentInstruction);
                    break;
                case "RSHIFT":
                    value = Rshift(currentInstruction);
                    break;
                default:
                    if (currentInstruction[0] == "NOT") value = Not(currentInstruction);
                    else throw new InvalidDataException("Unrecognized command");
                    break;
            }

            instructions[wire] = new[] { value.ToString(CultureInfo.InvariantCulture), "->", wire };
            return value;
        }
    }
}
