using AdventOfCode.Core;

namespace AdventOfCode._2018
{
    /// <summary>
    /// </summary>
    // ReSharper disable once StringLiteralTypo
    [AdventOfCode(2018, 11, 2, "", "229,251,16")]
    public class AdventOfCode2018112 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var serial = 5034;

            var sum = new int[301][];
            for (var i = 0; i < 301; i++)
            {
                sum[i] = new int[301];
            }

            var bx = int.MinValue;
            var by = int.MinValue;
            var bs = int.MinValue;
            var best = int.MinValue;

            for (var y = 1; y <= 300; y++)
            {
                for (var x = 1; x <= 300; x++)
                {
                    var id = x + 10;
                    var p = id * y + serial;
                    p = (p * id) / 100 % 10 - 5;
                    sum[y][x] = p + sum[y - 1][x] + sum[y][x - 1] - sum[y - 1][x - 1];
                }
            }
            for (var s = 1; s <= 300; s++)
            {
                for (var y = s; y <= 300; y++)
                {
                    for (var x = s; x <= 300; x++)
                    {
                        var total = sum[y][x] - sum[y - s][x] - sum[y][x - s] + sum[y - s][x - s];
                        if (total > best)
                        {
                            best = total;
                            bx = x;
                            by = y;
                            bs = s;
                        }
                    }
                }
            }

            Result = (bx - bs + 1) + "," + (by - bs + 1) + "," + bs;
        }
    }
}
