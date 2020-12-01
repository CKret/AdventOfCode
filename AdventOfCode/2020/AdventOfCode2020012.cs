﻿using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 1, 2, "", 182588480L)]
    public class AdventOfCode2020012 : AdventOfCodeBase
    {
        public AdventOfCode2020012(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            var data = Input.Select(int.Parse).ToArray();

            for (var i = 0; i < data.Length - 2; i++)
            {
                var a = data[i];
                for (var j = i + 1; j < data.Length - 1; j++)
                {
                    var b = data[j];
                    if (a + b >= 2020) continue;

                    if (data.Contains((2020 - a - b)))
                    {
                        Result = a * b * (2020 - a - b);
                        return;
                    }
                }
            }
        }
    }
}