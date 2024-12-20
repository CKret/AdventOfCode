using System;
using System.Collections.Generic;
using AdventOfCode.Core;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 14, "Docking Data", 14722016054794L, 3618217244644L)]
    public class AdventOfCode202014 : AdventOfCodeBase
    {
        public AdventOfCode202014(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var memory = new Dictionary<long, long>();
            var bitmask = string.Empty;
            foreach (var line in Input)
            {
                if (line.StartsWith("mask = "))
                {
                    bitmask = line.Split(" = ")[1];
                    continue;
                }

                var mask1 = Convert.ToInt64(bitmask.Replace('X', '0'), 2);
                var mask2 = Convert.ToInt64(bitmask.Replace('X', '1'), 2);
                var data = Regex.Match(line, @"^mem\[([0-9]+)\] = ([0-9]+)$")
                                .Groups.Values.Select(x => x.Value)
                                .ToArray();

                var memoryAddress = long.Parse(data[1]);
                var value = long.Parse(data[2]);

                memory[memoryAddress] = (value | mask1) & mask2;
            }


            return memory.Sum(x => x.Value);
        }

        protected override object SolvePart2()
        {
            var memory = new Dictionary<long, long>();
            var bitmask = string.Empty;
            foreach (var line in Input)
            {
                if (line.StartsWith("mask = "))
                {
                    bitmask = line.Split(" = ")[1];
                    continue;
                }

                var data = Regex.Match(line, @"^mem\[([0-9]+)\] = ([0-9]+)$")
                                .Groups.Values.Select(x => x.Value)
                                .ToArray();

                var baseAddress = ApplyAddressMask(Convert.ToString(long.Parse(data[1]), 2).PadLeft(36, '0'), bitmask);
                var addresses = GenerateAdresses(baseAddress);
                var value = long.Parse(data[2]);

                foreach (var address in addresses)
                {
                    var memoryAddress = Convert.ToInt64(address, 2);
                    memory[memoryAddress] = value;
                }
            }

            return memory.Sum(x => x.Value);
        }

        private string ApplyAddressMask(string baseAddress, string bitmask)
        {
            return string.Join("", bitmask.Select((c, i) => c == '0' ? baseAddress[i] : c));
        }

        private static IEnumerable<string> GenerateAdresses(string adress)
        {
            if (!adress.Any(c => c.Equals('X')))
            {
                return new List<string> { adress };
            }

            var adress0 = ReplaceFirstMatch(adress, "X", "0");
            var adress1 = ReplaceFirstMatch(adress, "X", "1");
            return GenerateAdresses(adress0).Concat(GenerateAdresses(adress1));
        }
        private static string ReplaceFirstMatch(string adress, string oldValue, string newValue)
        {
            var index = adress.IndexOf(oldValue, StringComparison.InvariantCulture);
            return index < 0 ? adress : adress.Remove(index, oldValue.Length).Insert(index, newValue);
        }
    }
}
