using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core.Cryptography
{
    public static class HashAlgorithm
    {
        public static IEnumerable<byte> KnotHash(string input, int rounds = 64)
        {
            var skipSize = 0;
            var pos = 0;
            var row = input.Select(c => (byte)c).ToList();
            row.AddRange(new byte[] { 17, 31, 73, 47, 23 });

            var list = Enumerable.Range(0, 256).Select(i => (byte)i).ToArray();

            for (var i = 0; i < rounds; i++)
            {
                foreach (var len in row)
                {
                    var overflow = 0;
                    List<byte> sub;
                    if (pos + len > list.Length)
                    {
                        overflow = (pos + len) % list.Length;
                        sub = list.Skip(pos).ToList();
                        sub.AddRange(list.Take(overflow));
                    }
                    else
                    {
                        sub = list.Skip(pos).Take(len).ToList();
                    }

                    // reverse
                    var reverse = sub.ToArray();
                    Array.Reverse(reverse);

                    // Put together again
                    if (overflow > 0)
                    {
                        sub = reverse.Skip(reverse.Length - overflow).Take(overflow).ToList();
                        sub.AddRange(list.Skip(overflow).Take(list.Length - len).ToList());
                        sub.AddRange(reverse.Take(reverse.Length - overflow).ToList());
                    }
                    else
                    {
                        sub = list.Take(pos).ToList();
                        sub.AddRange(reverse);
                        sub.AddRange(list.Skip(pos + len));
                    }

                    list = sub.ToArray();

                    pos = (pos + len + skipSize) % list.Length;
                    skipSize++;
                }

            }

            var dense = new byte[16];
            for (var i = 0; i < 16; i++)
            {
                for (var j = 0; j < 16; j++)
                {
                    dense[i] ^= list[16 * i + j];
                }

            }

            return dense;
        }
    }
}
