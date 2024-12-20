using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Mathematics;
using SuperLinq;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 11, "Monkey in the Middle", 51075L, 11741456163)]
	public class AdventOfCode202211 : AdventOfCodeBase
	{
		public AdventOfCode202211(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			return CalculateMonkeyBusiness(ParseMonkeys(), 20, m => m / 3);
		}

		protected override object SolvePart2()
		{
			var monkeys = ParseMonkeys();
			var modulo = monkeys.Select(x => x.Divisor).Aggregate(1L, (p, c) => p * c);

			return CalculateMonkeyBusiness(monkeys, 10000, m => m % modulo);
		}

		private List<Monkey> ParseMonkeys()
		{
			var monkeys = Input
				.Split("")
				.Select(lines => new Monkey
				{
					Items = lines.Skip(1).First().Split(": ")[1].Split(", ").Select(long.Parse).ToQueue(),
					Operation = lines.Skip(2).First().Split(": ")[1],
					Divisor = int.Parse(lines.Skip(3).First().Split("by ")[1]),
					TestTrue = int.Parse(lines.Skip(4).First().Split("to monkey ")[1]),
					TestFalse = int.Parse(lines.Skip(5).First().Split("to monkey ")[1])
				})
				.ToList();
			return monkeys;
		}

		private long CalculateMonkeyBusiness(List<Monkey> monkeys, int rounds, Func<long, long> modifier)
		{
			while (rounds-- > 0)
			{
				foreach (var monkey in monkeys)
				{
					while (monkey.Items.Count > 0)
					{
						var item = monkey.Items.Dequeue();
						var op = monkey.Operation.Split(" = ")[1].Replace("old", item.ToString());
						var worryLevel = modifier(Expressions.EvaluateExpression(op));

						monkeys[worryLevel % monkey.Divisor == 0 ? monkey.TestTrue : monkey.TestFalse].Items.Enqueue(worryLevel);
						monkey.InspectedItems++;
					}
				}
			}

			return monkeys.OrderByDescending(x => x.InspectedItems).Take(2).Aggregate(1L, (p, c) => p * c.InspectedItems);
		}
	}

	public class Monkey
	{
		public Queue<long> Items = new();
		public string Operation;
		public long Divisor;
		public int TestTrue;
		public int TestFalse;
		public long InspectedItems;
	}
}
