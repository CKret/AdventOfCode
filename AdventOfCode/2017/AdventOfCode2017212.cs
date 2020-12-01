using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 21: Fractal Art ---
    /// 
    /// --- Part Two ---
    /// 
    /// How many pixels stay on after 18 iterations?
    /// 
    /// </summary>
    [AdventOfCode(2017, 21, 2, "Fractal Art - Part Two", 2810258)]
    public class AdventOfCode2017212 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var inputRules = File.ReadAllLines("2017\\AdventOfCode201721.txt");
            var rulesMap = new Dictionary<string, string>();

            foreach (var rule in inputRules)
            {
                var expansion = rule.Split(new[] { " => " }, StringSplitOptions.RemoveEmptyEntries);

                var from = expansion[0];
                var to = expansion[1];


                rulesMap.TryAdd(from, to);
                rulesMap.TryAdd(AdventOfCode2017211.FlipHorizontal(from), to);
                rulesMap.TryAdd(AdventOfCode2017211.FlipVertical(from), to);

                for (var i = 0; i < 3; i++)
                {
                    var newFrom = AdventOfCode2017211.Rotate(from);
                    rulesMap.TryAdd(newFrom, to);
                    rulesMap.TryAdd(AdventOfCode2017211.FlipHorizontal(newFrom), to);
                    rulesMap.TryAdd(AdventOfCode2017211.FlipVertical(newFrom), to);

                    from = newFrom;
                }
            }

            var grid = new[]
            {
                ".#.",
                "..#",
                "###",
            };

            grid = AdventOfCode2017211.CreateArt(18, grid, rulesMap);

            Result = grid.Sum(p => p.Sum(q => q == '#' ? 1 : 0));
        }

        public AdventOfCode2017212(string sessionCookie) : base(sessionCookie) { }
    }
}
