using System;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
// ReSharper disable StringLiteralTypo

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 10, "Cathode-Ray Tube", 14040, "ZGCJZJFL")]
	public class AdventOfCode202210 : AdventOfCodeBase
	{
		public AdventOfCode202210(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			return Input
				.SelectMany(r => r.Split(" "))
				.Select(r => r[^1].IsNumber() ? int.Parse(r) : 0)
				.Scan(1, ((a, b) => a + b))
				.Select((register, cycle) => (cycle + 2) % 40 == 20 ? (cycle + 2) * register : 0)
				.Sum();
		}

		protected override object SolvePart2()
		{
			Input
				.SelectMany(r => r.Split(" "))
				.Select(r => r[^1].IsNumber() ? int.Parse(r) : 0)
				.Scan(1, ((a, b) => a + b))
				.Prepend(1)
				.Select((register, cycle) => Math.Abs((cycle) % 40 - register) < 2 ? '#' : ' ')
				.Chunk(40)
				.Select(s => string.Join("", s))
				.ForEach(s => Console.WriteLine(s));

			return "ZGCJZJFL";
		}
	}
}
