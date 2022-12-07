using System.Collections.Generic;
using System.Linq;
using SuperLinq;

namespace AdventOfCode.Mathematics.Cryptography
{
    public static class HashAlgorithm
    {
        public static IEnumerable<byte> KnotHash(string input, int rounds = 64)
        {
            int skipSize = 0, pos = 0;
            var row = input.Select(c => (byte) c).Concat(new byte[] { 17, 31, 73, 47, 23 }).ToList();

            var sparseHash = Enumerable.Range(0, 256).Select(i => (byte) i).ToArray();

            foreach (var sequenceLength in row.Repeat(rounds))
            {
                for (var i = 0; i < sequenceLength / 2; i++)
                {
                    var tmp = sparseHash[(sequenceLength - i - 1 + pos) % sparseHash.Length];
                    sparseHash[(sequenceLength - i - 1 + pos) % sparseHash.Length] = sparseHash[(pos + i) % sparseHash.Length];
                    sparseHash[(pos + i) % sparseHash.Length] = tmp;
                }

                pos = (pos + sequenceLength + skipSize++) % sparseHash.Length;
            }

            return Enumerable.Range(0, 16).Select(i => sparseHash.Skip(16 * i).Take(16).Aggregate((curr, b) => (byte)(curr ^ b))).ToArray();
        }
    }
}
