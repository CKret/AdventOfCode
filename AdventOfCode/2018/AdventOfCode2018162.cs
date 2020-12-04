using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using AdventOfCode.VMs;

namespace AdventOfCode._2018
{
    /// <summary>
    /// --- Day 16: Chronal Classification ---
    /// 
    /// --- Part Two ---
    /// 
    /// Using the samples you collected, work out the number of each opcode and
    /// execute the test program (the second section of your puzzle input).
    /// 
    /// What value is contained in register 0 after executing the test program?
    /// </summary>
    [AdventOfCode(2018, 16, "Chronal Classification - Part 2", 622)]
    public class AdventOfCode2018162 : AdventOfCodeBase
    {
        public AdventOfCode2018162(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            var data = File.ReadAllText(@"2018\AdventOfCode201816.txt").Split("\n\n\n");
            var samples = data[0]
                          .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
                          .Select(
                              s =>
                              {
                                  var set = Regex.Matches(s, @"\d+")
                                                 .Cast<Match>()
                                                 .Select(m => int.Parse(m.Value))
                                                 .ToArray();

                                  var br = set.Take(4).ToArray();
                                  var ins = set.Skip(4).Take(4).ToArray();
                                  var ar = set.Skip(8).ToArray();

                                  return new ChronalInstructionEngine.ChronalInstructionSample(br, ar, new ChronalInstructionEngine.ChronalInstruction(ins));
                              })
                          .ToArray();

            var code = data[1]
                       .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                       .Select(
                           s =>
                           {
                               return new ChronalInstructionEngine.ChronalInstruction(Regex.Matches(s, @"\d+")
                                           .Cast<Match>()
                                           .Select(m => int.Parse(m.Value))
                                           .ToArray());
                           })
                       .ToArray();

            var opCodes = Enumerable.Range(0, 16).ToDictionary(i => i, i => new List<int>());

            foreach (var s in samples)
            {
                opCodes[s.OpCode].AddRange(s.PossibleInstructions().Where(v => !opCodes[s.OpCode].Contains(v)));
            }

            var realOpCodes = opCodes.OrderBy(o => o.Value.Count).ToList();
            for (var i = 0; i < realOpCodes.Count; i++)
            {
                for (var j = 0; j < realOpCodes.Count; j++)
                {
                    if (i == j) continue;
                    realOpCodes[j].Value.Remove(realOpCodes[i].Value.Single());
                    realOpCodes = realOpCodes.OrderBy(o => o.Value.Count).ToList();
                }
            }

            var translation = realOpCodes.OrderBy(r => r.Key).Select(d => d.Value.Single()).ToArray();
            var engine = new ChronalInstructionEngine(translation);
            var registers = code.Aggregate(new[] { 0, 0, 0, 0 }, (current, c) => engine.Execute(current, c).Register);

            Result = registers[0];
        }
    }
}
