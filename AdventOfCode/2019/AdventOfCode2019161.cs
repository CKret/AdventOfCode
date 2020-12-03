using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 16, "", 78009100)]
    public class AdventOfCode2019161 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201916.txt");

            var basePattern = new[] {0, 1, 0, -1};
            var fft = data.Select(x => int.Parse(x.ToString())).ToList();

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

            Result = val;
        }

        public AdventOfCode2019161(string sessionCookie) : base(sessionCookie) { }
    }
}
