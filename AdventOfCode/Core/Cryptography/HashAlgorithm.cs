using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core.Cryptography
{
    public static class HashAlgorithm
    {
        public static IEnumerable<byte> KnotHash(string input, int rounds = 64)
        {
            int skipSize = 0, pos = 0;
            var row = input.Select(c => (byte)c).ToList();
            row.AddRange(new byte[] { 17, 31, 73, 47, 23 });

            var sparseHash = Enumerable.Range(0, 256).Select(i => (byte)i).ToArray();

            for (var round = 0; round < rounds; round++)
            {
                foreach (var sequenceLength in row)
                {
                    for (var i = 0; i < sequenceLength / 2; i++)
                    {
                        var tmp = sparseHash[(sequenceLength - i - 1 + pos) % sparseHash.Length];
                        sparseHash[(sequenceLength - i - 1 + pos) % sparseHash.Length] = sparseHash[(pos + i) % sparseHash.Length];
                        sparseHash[(pos + i) % sparseHash.Length] = tmp;
                    }

                    pos = (pos + sequenceLength + skipSize++) % sparseHash.Length;
                }
            }

            var denseHash = new byte[16];
            for (var i = 0; i < 16; i++)
            {
                for (var j = 0; j < 16; j++)
                {
                    denseHash[i] ^= sparseHash[16 * i + j];
                }
            }

            return denseHash;
        }
    }
}
