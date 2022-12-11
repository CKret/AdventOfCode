using System;
using System.Collections.Generic;

namespace AdventOfCode.Mathematics
{
	public static class Expressions
	{
		private static readonly Dictionary<string, int> DefaultOperatorPrecedence = new()
		{
			["*"] = 2,
			["/"] = 2,
			["+"] = 1,
			["-"] = 1,
		};

		public static long EvaluateExpression(string prefix, IReadOnlyDictionary<string, int> operatorPrecedence = null)
		{
			operatorPrecedence ??= DefaultOperatorPrecedence;

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

		public static IEnumerable<string> ToPostfix(string infix, IReadOnlyDictionary<string, int> operatorPrecedence = null)
		{
			operatorPrecedence ??= DefaultOperatorPrecedence;

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
}
