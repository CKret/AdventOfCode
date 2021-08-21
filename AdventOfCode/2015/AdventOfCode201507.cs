using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 7, "", 956, 40149)]
    public class AdventOfCode201507 : AdventOfCodeBase
    {
        Dictionary<string, string[]> instructions = new Dictionary<string, string[]>();

        public AdventOfCode201507(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            instructions = Input
                .Select(i => i.Split(' '))
                .ToDictionary(i => i.Last());

            return EvalInput("a");
        }

        protected override object SolvePart2()
        {
            instructions = Input
                .Select(i => i.Split(' '))
                .ToDictionary(i => i.Last());

            var val = EvalInput("a").ToString(CultureInfo.InvariantCulture);

            instructions = Input
                .Select(i => i.Split(' '))
                .ToDictionary(i => i.Last());

            instructions["b"] = new[] { val, "->", "b" };

            return EvalInput("a");
        }

        ushort EvalInput(string wire)
        {
            ushort Eval(string x) => char.IsLetter(x[0]) ? EvalInput(x) : ushort.Parse(x, CultureInfo.InvariantCulture);
            ushort Assign(string[] x) => Eval(x[0]);
            ushort And(string[] x) => (ushort) (Eval(x[0]) & Eval(x[2]));
            ushort Or(string[] x) => (ushort) (Eval(x[0]) | Eval(x[2]));
            ushort Lshift(string[] x) => (ushort) (Eval(x[0]) << Eval(x[2]));
            ushort Rshift(string[] x) => (ushort) (Eval(x[0]) >> Eval(x[2]));
            ushort Not(string[] x) => (ushort) ~Eval(x[1]);

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
                    else throw new InvalidDataException("Unrecognised command");
                    break;
            }

            instructions[wire] = new[] { value.ToString(CultureInfo.InvariantCulture), "->", wire };
            return value;
        }
    }
}
