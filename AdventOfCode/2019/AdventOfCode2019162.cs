using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using MoreLinq.Extensions;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 16, 2, "", 37717791)]
    public class AdventOfCode2019162 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201916.txt").Repeat(10000);
            
            var basePattern = new[] { 0, 1, 0, -1 };
            var fft = data.Select(x => int.Parse(x.ToString())).ToList();
            var res = new List<int>(fft);
            var table = new int[res.Count + 1];

            var off = 0;
            for (var x = 0; x < 7; x++)
            {
                off *= 10;
                off += fft[x];
            }

            for (var phase = 0; phase < 100; phase++)
            {
                table[0] = 0;
                for (var j = 0; j < fft.Count; j++)
                {
                    table[j + 1] = table[j] + fft[j];
                }

                for (var c = 0; c < fft.Count; c++)
                {
                    var expand = c + 1;
                    var current = 0;
                    var j = 0;
                    var j2 = expand - 1;
                    var index = ((j + 1) / expand) % 4;
                    while (j < fft.Count)
                    {
                        if (j2 >= table.Length) j2 = table.Length - 1;
                        if (basePattern[index] == 1) current += table[j2] - table[j];
                        else if (basePattern[index] == -1) current -= table[j2] - table[j];
                        j = j2;
                        j2 += expand;
                        index = (index + 1) % 4;
                    }

                    res[c] = Math.Abs(current) % 10;
                }

                fft = res;
            }

            var val = 0;
            for (var x = 0; x < 8; x++)
            {
                val *= 10;
                val += fft[off + x];
            }

            Result = val;
        }

        public AdventOfCode2019162(string sessionCookie) : base(sessionCookie) { }
    }
}
