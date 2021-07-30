using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 16, "", 78009100, 37717791)]
    public class AdventOfCode201916 : AdventOfCodeBase
    {
        protected override object SolvePart1()
        {
            var basePattern = new[] { 0, 1, 0, -1 };
            var fft = Input[0].Select(x => int.Parse(x.ToString())).ToList();

            for (var phase = 0; phase < 100; phase++)
            {
                for (var c = 0; c < fft.Count; c++)
                {
                    var currentPattern = new List<int>();
                    foreach (var v in basePattern)
                    {
                        for (var j = 0; j < c + 1; j++)
                        {
                            currentPattern.Add(v);
                        }
                    }

                    //currentPattern.RemoveAt(0);
                    //var r = currentPattern.Repeat().Skip(1);

                    var sum = 0;
                    var p = 1;
                    for (var x = 0; x < fft.Count; x++)
                    {
                        var a = fft[x];
                        var b = currentPattern[p++ % currentPattern.Count];
                        sum += a * b;
                    }

                    fft[c] = Math.Abs(sum % 10);
                }
            }

            var val = 0;
            for (var x = 0; x < 8; x++)
            {
                val *= 10;
                val += fft[x];
            }

            return val;
        }

        protected override object SolvePart2()
        {
            var basePattern = new[] { 0, 1, 0, -1 };
            var fft = Input[0].Select(x => int.Parse(x.ToString())).ToList();
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

            return val;
        }

        public AdventOfCode201916(string sessionCookie) : base(sessionCookie) { }
    }
}
