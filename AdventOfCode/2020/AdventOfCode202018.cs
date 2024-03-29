using System;
using System.Collections.Generic;
using AdventOfCode.Core;
using System.Linq;
using AdventOfCode.Mathematics;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 18, "Operation Order", 209335026987L, 33331817392479L)]
    public class AdventOfCode202018 : AdventOfCodeBase
    {
        public AdventOfCode202018(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            return Input.Select(s => Expressions.EvaluateExpression(s,
                            new Dictionary<string, int>
                            {
                                ["*"] = 1,
                                ["/"] = 1,
                                ["+"] = 1,
                                ["-"] = 1,
                            }))
                        .Sum();
        }

        protected override object SolvePart2()
        {
            return Input.Select(s => Expressions.EvaluateExpression(s,
                            new Dictionary<string, int>
                            {
                                ["*"] = 1,
                                ["/"] = 1,
                                ["+"] = 2,
                                ["-"] = 2,
                            }))
                        .Sum();
        }
    }
}
