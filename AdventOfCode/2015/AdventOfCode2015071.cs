using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// This year, Santa brought little Bobby Tables a set of wires and bitwise logic gates! Unfortunately, little Bobby is a little under the recommended age range, and he needs help assembling the circuit.
    /// 
    /// Each wire has an identifier (some lowercase letters) and can carry a 16-bit signal (a number from 0 to 65535). A signal is provided to each wire by a gate, another wire, or some specific value. Each wire can only get a signal from one source, but can provide its signal to multiple destinations. A gate provides no signal until all of its inputs have a signal.
    /// 
    /// The included instructions booklet describes how to connect the parts together: x AND y -&gt; z means to connect wires x and y to an AND gate, and then connect its output to wire z.
    /// </summary>
    [AdventOfCode(2015, 7, 1, "In little Bobby's kit's instructions booklet (provided as your puzzle input), what signal is ultimately provided to wire a?", 956)]
    public class AdventOfCode2015071 : AdventOfCodeBase
    {
        Dictionary<string, string[]> instructions = new Dictionary<string, string[]>();

        public override void Solve()
        {
            instructions = File.ReadAllLines("2015/AdventOfCode201507.txt")
                .Select(i => i.Split(' '))
                .ToDictionary(i => i.Last());

            Result = EvalInput("a");
        }

        ushort EvalInput(string wire)
        {
            ushort Eval(string x) => char.IsLetter(x[0]) ? EvalInput(x) : ushort.Parse(x);
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

            instructions[wire] = new[] { value.ToString(), "->", wire };
            return value;
        }
    }
}
