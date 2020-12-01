using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.Mathematics;

namespace AdventOfCode._2015
{
    /// <summary>
    [AdventOfCode(2015, 13, 1, "", 709)]
    public class AdventOfCode2015131 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var input = File.ReadAllLines("2015\\AdventOfCode201513.txt");

            var data = new List<Tuple<string, string, int>>();
            var guests = new List<string>();

            foreach (var line in input)
            {
                var splits = line.Split();
                var who = splits[0];
                var plusOrMinus = splits[2];
                var value = splits[3];
                var toWhom = splits[10].TrimEnd('.');

                var realValue = plusOrMinus == "gain" ? int.Parse(value, CultureInfo.InvariantCulture) : -int.Parse(value, CultureInfo.InvariantCulture);

                data.Add(new Tuple<string, string, int>(who, toWhom, realValue));

                if (!guests.Contains(who)) guests.Add(who);
            }

            var nGuests = 8;
            var arr = Enumerable.Range(0, nGuests).ToArray();

            int[] me;
            var max = 0;
            do
            {
                var current = 0;
                for (var i = 0; i < arr.Length - 1; i++)
                {
                    current += data.Single(d => d.Item1 == guests[arr[i]] && d.Item2 == guests[arr[i + 1] % 8]).Item3;
                    current += data.Single(d => d.Item1 == guests[arr[i + 1]] && d.Item2 == guests[arr[i] % 8]).Item3;
                }
                current += data.Single(d => d.Item1 == guests[arr[0]] && d.Item2 == guests[arr[^1]]).Item3;
                current += data.Single(d => d.Item1 == guests[arr[^1]] && d.Item2 == guests[arr[0]]).Item3;

                if (current > max)
                {
                    max = current;
                    me = arr;
                }
            } while (NumberTheory.NextPermutation(ref arr));

            Result = max;
        }

        public AdventOfCode2015131(string sessionCookie) : base(sessionCookie) { }
    }
}
