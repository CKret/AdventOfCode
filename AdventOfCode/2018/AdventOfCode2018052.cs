using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2018
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2018, 5, 2, "", 6650)]
    public class AdventOfCode2018052 : AdventOfCodeBase
    {
        private readonly char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public override void Solve()
        {
            var data = File.ReadAllText(@"2018\AdventOfCode201805.txt");
            var shortest = int.MaxValue;

            foreach (var c in letters)
            {
                var current = data.ReplaceAll(new [] {c.ToString(), c.ToString().ToLower() }, "");
                current = React(current);

                if (current.Length < shortest)
                {
                    shortest = current.Length;
                }
            }

            Result = shortest;
        }

        private string React(string data)
        {
            for (var i = 0; i < data.Length - 1; i++)
            {
                if (data[i] == data[i + 1] + 0x20 || data[i] + 0x20 == data[i + 1])
                {
                    data = data.Remove(i, 2);
                    i = i == 0 ? -1 : i - 2;
                }
            }

            return data;
        }
    }
}
