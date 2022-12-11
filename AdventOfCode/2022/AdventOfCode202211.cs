using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
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
			var monkeys = Input
				.Split("")
				.Select(lines => new Monkey
				{
					Items = lines.Skip(1).First().Split(": ")[1].Split(", ").Select(long.Parse).ToQueue(),
					Operation = lines.Skip(2).First().Split(": ")[1],
					Divisible = int.Parse(lines.Skip(3).First().Split("by ")[1]),
					TestTrue = int.Parse(lines.Skip(4).First().Split("to monkey ")[1]),
					TestFalse = int.Parse(lines.Skip(5).First().Split("to monkey ")[1])
				})
				.ToList();

			return CalculateMonkeyBusiness(monkeys, 20, m => m / 3);
		}

		protected override object SolvePart2()
		{
			var monkeys = Input
				.Split("")
				.Select(lines => new Monkey
				{
					Items = lines.Skip(1).First().Split(": ")[1].Split(", ").Select(long.Parse).ToQueue(),
					Operation = lines.Skip(2).First().Split(": ")[1],
					Divisible = int.Parse(lines.Skip(3).First().Split("by ")[1]),
					TestTrue = int.Parse(lines.Skip(4).First().Split("to monkey ")[1]),
					TestFalse = int.Parse(lines.Skip(5).First().Split("to monkey ")[1])
				})
				.ToList();

			var modulo = monkeys.Select(x => x.Divisible).Aggregate(1L, (p, c) => p * c);

			return CalculateMonkeyBusiness(monkeys, 10000, m => m % modulo);
		}

		private long CalculateMonkeyBusiness(List<Monkey> monkeys, int rounds, Func<long, long> modifier)
		{
			while(rounds-- > 0)
			{
				foreach (var monkey in monkeys)
				{
					while (monkey.Items.Count > 0)
					{
						var item = monkey.Items.Dequeue();
						var op = monkey.Operation.Split(" = ")[1].Replace("old", item.ToString());
						var worryLevel = modifier(EvaluateExpression(op, new Dictionary<string, int>
						{
							["*"] = 2,
							["/"] = 2,
							["+"] = 1,
							["-"] = 1,
						}));

						if (worryLevel % monkey.Divisible == 0) monkeys[monkey.TestTrue].Items.Enqueue(worryLevel);
						else monkeys[monkey.TestFalse].Items.Enqueue(worryLevel);
						monkey.InspectedItems++;
					}
				}
			}

			return monkeys.OrderByDescending(x => x.InspectedItems).Take(2).Aggregate(1L, (p, c) => p * c.InspectedItems);

		}
		private static long EvaluateExpression(string prefix, IReadOnlyDictionary<string, int> operatorPrecedence)
		{
			var postfix = ToPostfix(prefix, operatorPrecedence);
			var argStack = new Stack<long>();
			foreach (var token in postfix)
			{
				switch (token)
				{
					case "*":
						argStack.Push(argStack.Pop() * argStack.Pop());
						break;
					case "/":
						argStack.Push(argStack.Pop() / argStack.Pop());
						break;
					case "+":
						argStack.Push(argStack.Pop() + argStack.Pop());
						break;
					case "-":
						argStack.Push(argStack.Pop() - argStack.Pop());
						break;
					default:
						argStack.Push(long.Parse(token));
						break;
				}
			}

			return argStack.Pop();
		}

		private static IEnumerable<string> ToPostfix(string infix, IReadOnlyDictionary<string, int> operatorPrecedence)
		{
			var tokens = infix
									 .Replace("(", " ( ")
									 .Replace(")", " ) ")
									 .Split(' ', StringSplitOptions.RemoveEmptyEntries);

			var stack = new Stack<string>();
			var output = new List<string>();

			foreach (var token in tokens)
			{
				if (operatorPrecedence.TryGetValue(token, out var op1))
				{
					while (stack.Count > 0 && operatorPrecedence.TryGetValue(stack.Peek(), out var op2))
					{
						if (op1.CompareTo(op2) <= 0)
						{
							output.Add(stack.Pop());
						}
						else
						{
							break;
						}
					}

					stack.Push(token);
				}
				else if (token == "(")
				{
					stack.Push(token);
				}
				else if (token == ")")
				{
					string top;
					while (stack.Count > 0 && (top = stack.Pop()) != "(")
					{
						output.Add(top);
					}
				}
				else
				{
					output.Add(token);
				}
			}

			while (stack.Count > 0)
			{
				output.Add(stack.Pop());
			}

			return output;
		}
	}

	public class Monkey
	{
		public int Id;
		public Queue<long> Items = new();
		public string Operation;
		public long Divisible;
		public int TestTrue;
		public int TestFalse;
		public long InspectedItems;
	}
}
