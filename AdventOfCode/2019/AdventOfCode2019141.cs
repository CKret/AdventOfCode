using System;
using System.IO;
using AdventOfCode.Core;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 14, "", 337862)]
    public class AdventOfCode2019141 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllLines(@"2019\AdventOfCode201914.txt");

            var reactions = data
                .Select(l => l.Split(new[] { " => " }, StringSplitOptions.RemoveEmptyEntries))
                .Select(a => new { Formula = a[0].Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries), Component = a[1].Split(' ') })
                .Select(t => new { Formula = t.Formula.Select(i => i.Split(' ')).ToDictionary(a => a[1], a => long.Parse(a[0], CultureInfo.CurrentCulture)), Result = (Formula: t.Component[1], Amount: long.Parse(t.Component[0], CultureInfo.CurrentCulture)) })
                .ToDictionary(r => r.Result.Formula, r => r);

            var fuel = reactions["FUEL"];
            var components = new Dictionary<string, long> { { "FUEL", fuel.Result.Amount } };

            while (components.Any(k => k.Key != "ORE" && k.Value > 0))
            {
                var needed = components.First(k => k.Key != "ORE" && k.Value > 0);
                var reaction = reactions[needed.Key];
                components[needed.Key] -= reaction.Result.Amount;
                foreach (var c in reaction.Formula)
                {
                    if (components.ContainsKey(c.Key))
                        components[c.Key] += c.Value;
                    else
                        components.Add(c.Key, c.Value);
                }
            }

            Result = components["ORE"];
        }

        public AdventOfCode2019141(string sessionCookie) : base(sessionCookie) { }
    }
}
