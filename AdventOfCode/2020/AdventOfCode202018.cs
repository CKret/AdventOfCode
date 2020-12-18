using System;
using System.Collections.Generic;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 18, "Day 18: Operation Order", 209335026987L, 33331817392479L)]
    public class AdventOfCode202018 : AdventOfCodeBase
    {
        public AdventOfCode202018(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            var timer = new Stopwatch();
            var data = Input;

            timer.Start();
            ResultPart1 = SolvePart1();
            timer.Stop();
            TimePart1 = timer.ElapsedTicks.ToMilliseconds();

            timer.Start();
            ResultPart2 = SolvePart2();
            timer.Stop();
            TimePart2 = timer.ElapsedTicks.ToMilliseconds();
        }

        private object SolvePart1()
        {
            return Input.Select(s => EvaluateExpression(s,
                            new Dictionary<string, int>
                            {
                                ["*"] = 1,
                                ["/"] = 1,
                                ["+"] = 1,
                                ["-"] = 1,
                            }))
                        .Sum();
        }

        private object SolvePart2()
        {
            return Input.Select(s => EvaluateExpression(s,
                            new Dictionary<string, int>
                            {
                                ["*"] = 1,
                                ["/"] = 1,
                                ["+"] = 2,
                                ["-"] = 2,
                            }))
                        .Sum();
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
}
