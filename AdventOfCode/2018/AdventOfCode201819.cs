using System.Collections.Generic;
using AdventOfCode.Core;
using System.Linq;
using AdventOfCode.VMs;

namespace AdventOfCode._2018
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2018, 19, "", 2280L, 30481920L)]
	public class AdventOfCode201819 : AdventOfCodeBase
	{
		public AdventOfCode201819(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var ipReg = int.Parse(Input.First().Replace("#ip ", ""));
			var instructions = Input.Skip(1)
															.Select(a => a.Split(' '))
															.Select(d => new ChronalInstructionEngine.ChronalInstruction(new[] { Translation[d[0]], int.Parse(d[1]), int.Parse(d[2]), int.Parse(d[3]) }))
															.ToArray();


			var engine = new ChronalInstructionEngine(ipReg);

			var instructionPointer = 0;
			var registers = new[] { 0, 0, 0, 0, 0, 0 };

			while (instructionPointer >= 0 && instructionPointer <= instructions.Length)
			{
				(instructionPointer, registers) = engine.Execute(registers, instructions[instructionPointer], instructionPointer);
			}

			return registers[0];
		}

		protected override object SolvePart2()
		{
			// Bruteforce took too long time so analysis of the program was necessary.
			// It calculates a number (10551288) and sums all factors (including 1 and 10551288).
			// The algorithm is essentially what is below but to speed it up you can limit the inner loop by the outer loop (target / i).

			//for (var i = 1L; i <= target; i++)
			//{
			//    for (var j = 1L; j <= target / i; j++)
			//    {
			//        if (i * j == target) sum += i;
			//    }
			//}

			// Change the program to run as the above. We can use addition to do division.
			// Problem is that we don't have enough registers. But hey... We can add as many as we want!
			// Added some instructions to do division with addition.
			// Runs 35-40x faster but still takes almost 3 minutes.
			// Note: Use AdventOfCode202019 Part 2 Optimized.txt as input.

			//var ipReg = int.Parse(Input[0].Replace("#ip ", ""));
			//var instructions = Input.Skip(1)
			//                        .Select(a => a.Split(' '))
			//                        .Select(d => new ChronalInstructionEngine.ChronalInstruction(new[] { Translation[d[0]], int.Parse(d[1]), int.Parse(d[2]), int.Parse(d[3]) }))
			//                        .ToArray();


			//var engine = new ChronalInstructionEngine(ipReg);

			//var instructionPointer = 0;
			//var registers = new[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

			//while (instructionPointer >= 0 && instructionPointer <= instructions.Length)
			//{
			//    (instructionPointer, registers) = engine.Execute(registers, instructions[instructionPointer], instructionPointer);
			//}

			//return registers[0];

			// However we can calculate this even faster if we use modulus.
			// There is no factor between [target / 2, target] so we can stop there and add target to the sum (we do that first actually). 

			var target = 10551288L;
			var sum = target;
			for (var i = 1L; i <= target / 2; i++)
			{
				if (target % i == 0) sum += i;
			}

			return sum;
		}

		private readonly Dictionary<string, int> Translation = new()
				{
						{ "addr", 0 },
						{ "addi", 1 },
						{ "mulr", 2 },
						{ "muli", 3 },
						{ "banr", 4 },
						{ "bani", 5 },
						{ "borr", 6 },
						{ "bori", 7 },
						{ "setr", 8 },
						{ "seti", 9 },
						{ "gtir", 10 },
						{ "gtri", 11 },
						{ "gtrr", 12 },
						{ "eqir", 13 },
						{ "eqri", 14 },
						{ "eqrr", 15 },
				};
	}
}
