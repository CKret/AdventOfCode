using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 16, "Flawed Frequency Transmission", 78009100, 37717791)]
    public class AdventOfCode201916 : AdventOfCodeBase
    {
        public AdventOfCode201916(string sessionCookie) : base(sessionCookie) { }

        protected override async Task<object> SolvePart1(CancellationToken cancellationToken)
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

        protected override async Task<object> SolvePart2(CancellationToken cancellationToken)
        {
            // Part 2: input repeated 10000 times, take message starting at offset given by first 7 digits
            var baseDigits = Input[0].Select(x => int.Parse(x.ToString())).ToArray();
            int L = baseDigits.Length;
            const int times = 10000;
            long N = (long)L * times;

            // compute offset from first 7 digits
            int offset = 0;
            for (var i = 0; i < 7; i++)
                offset = offset * 10 + baseDigits[i];

            if (offset < 0 || offset >= N)
                throw new InvalidOperationException("Calculated offset is out of the repeated signal range.");

            // The trick: when offset is in the second half of the signal (offset >= N/2),
            // the pattern for each position is all zeros until a long run of ones, so the
            // new value at position i depends only on the suffix sum from i to end.
            // This lets us simulate using only the suffix (from offset to end) which is
            // of length N - offset.

            if (offset < N / 2)
            {
                // Fallback: offset in first half would require a more complex algorithm.
                // For typical AoC inputs the offset lies in the second half, so throw a helpful error.
                throw new NotSupportedException("Offset is in the first half of the repeated signal; this fast method is not implemented for that case.");
            }

            int suffixLen = (int)(N - offset);
            var suffix = new int[suffixLen];

            // fill suffix with repeated baseDigits starting at offset
            for (int i = 0; i < suffixLen; i++)
            {
                // map global index (offset + i) back to baseDigits
                int idx = (offset + i) % L;
                suffix[i] = baseDigits[idx];
            }

            // perform 100 phases using suffix cumulative sums
            for (int phase = 0; phase < 100; phase++)
            {
                int running = 0;
                for (int i = suffixLen - 1; i >= 0; i--)
                {
                    running = (running + suffix[i]) % 10;
                    suffix[i] = running;
                }
            }

            // first eight digits of the message
            int val = 0;
            for (int i = 0; i < 8; i++)
            {
                val = val * 10 + suffix[i];
            }

            return val;
        }
    }
}
