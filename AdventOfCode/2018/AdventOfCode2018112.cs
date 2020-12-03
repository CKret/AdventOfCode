using AdventOfCode.Core;

namespace AdventOfCode._2018
{
    /// <summary>
    /// --- Day 11: Chronal Charge ---
    ///
    /// --- Part Two ---
    /// 
    /// You discover a dial on the side of the device; it seems to let you select a
    /// square of any size, not just 3x3. Sizes from 1x1 to 300x300 are supported.
    /// 
    /// Realizing this, you now must find the square of any size with the largest
    /// total power. Identify this square by including its size as a third
    /// parameter after the top-left coordinate: a 9x9 square with a top-left
    /// corner of 3,5 is identified as 3,5,9.
    /// 
    /// For example:
    /// 
    /// - For grid serial number 18, the largest total square (with a total
    ///   power of 113) is 16x16 and has a top-left corner of 90,269, so its
    ///   identifier is 90,269,16.
    /// - For grid serial number 42, the largest total square (with a total
    ///   power of 119) is 12x12 and has a top-left corner of 232,251, so its
    ///   identifier is 232,251,12.
    /// 
    /// What is the X,Y,size identifier of the square with the largest total power?
    /// </summary>
    // ReSharper disable once StringLiteralTypo
    [AdventOfCode(2018, 1, "Chronal Charge - Part 2", "229,251,16")]
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

            var maxX = int.MinValue;
            var maxY = int.MinValue;
            var maxSize = int.MinValue;
            var max = int.MinValue;

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
                        if (total > max)
                        {
                            max = total;
                            maxX = x;
                            maxY = y;
                            maxSize = s;
                        }
                    }
                }
            }

            Result = (maxX - maxSize + 1) + "," + (maxY - maxSize + 1) + "," + maxSize;
        }

        public AdventOfCode2018112(string sessionCookie) : base(sessionCookie) { }
    }
}
