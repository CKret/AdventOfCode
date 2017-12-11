using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2017, 10, 1, "", 54675)]
    public class AdventOfCode2017101 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var skipSize = 0;
            var pos = 0;
            var lengths = "34,88,2,222,254,93,150,0,199,255,39,32,137,136,1,167".Split(',').Select(int.Parse);
            var list = Enumerable.Range(0, 256).ToArray();

            foreach (var len in lengths)
            {
                var overflow = 0;
                List<int> sub;
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

            Result = list[0] * list[1];
        }
    }
}
