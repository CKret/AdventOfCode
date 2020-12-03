using System;
using AdventOfCode.Core;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;


namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 14, "", 337862)]
    public class AdventOfCode2019142 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllLines(@"2019\AdventOfCode201914.txt");

            var reactions = data
                .Select(l => l.Split(new[] { " => " }, StringSplitOptions.RemoveEmptyEntries))
                .Select(a => new { Formula = a[0].Split(new [] { ", " }, StringSplitOptions.RemoveEmptyEntries), Component = a[1].Split(' ') })
                .Select(t =>  (Component: t.Formula.Select(i => i.Split(' ')).ToDictionary(a => a[1], a => long.Parse(a[0], CultureInfo.CurrentCulture)), Result: (Component: t.Component[1], Amount: long.Parse(t.Component[0], CultureInfo.CurrentCulture)) ))
                .ToDictionary(r => r.Result.Component, r => r);

            var stock = new Dictionary<string, long>  { { "ORE", 1000000000000L } };

            var ore = stock["ORE"];

            var amount = 1000000;
            while (amount > 0)
            {
                var stockBackup = stock.ToDictionary(x => x.Key, x => x.Value);
                while (MakeComponent(reactions, stock, "FUEL", amount))
                {
                    stockBackup = stock.ToDictionary(x => x.Key, x => x.Value);
                }

                stock = stockBackup;
                amount /= 10;
            }

            Result = stock["FUEL"];
        }

        private bool MakeComponent(Dictionary<string, (Dictionary<string, long> Formula, (string Component, long Amount) Result)> reactions, Dictionary<string, long> stock, string component, long amount)
        {
            var reaction = reactions[component];
            var nBatches = (long)Math.Ceiling(amount / (double) reaction.Result.Amount);
            if (reaction.Formula.Any(i => GetStock(stock, i.Key) < nBatches * i.Value && i.Key == "ORE")) return false;

            while (reaction.Formula.Any(i => GetStock(stock, i.Key) < nBatches * i.Value))
            {
                var create = reaction.Formula.First(i => GetStock(stock, i.Key) < nBatches * i.Value);
                var n = nBatches * create.Value - GetStock(stock, create.Key);
                if (!MakeComponent(reactions, stock, create.Key, n))
                {
                    return false;
                }
            }

            foreach (var i in reaction.Formula)
            {
                AddStock(stock, i.Key, -nBatches * i.Value);
            }

            AddStock(stock, component, nBatches * reaction.Result.Amount);

            return true;
        }

        private void AddStock(Dictionary<string, long> components, string component, long amount)
        {
            if (components.ContainsKey(component)) components[component] += amount;
            else components.Add(component, amount);
        }

        private long GetStock(Dictionary<string, long> components, string component)
        {
            return components.ContainsKey(component) ? components[component] : 0;
        }

        public AdventOfCode2019142(string sessionCookie) : base(sessionCookie) { }
    }
}
