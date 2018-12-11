using AdventOfCode.Core;

namespace AdventOfCode._2018
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2018, 11, 1, "", "235,63")]
    public class AdventOfCode2018111 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var serial = 5034;

            var fuelCells = new (int, int)[300, 300];
            for (var y = 1; y <= 300; y++)
            {
                for (var x = 1; x <= 300; x++)
                {
                    fuelCells[x - 1, y - 1] = (x, y);
                }
            }

            var max = 0;
            var maxCoord = (0, 0);

            var powers = new int[300, 300];

            for (var y = 0; y < 300 - 3; y++)
            {
                for (var x = 0; x < 300 - 3; x++)
                {
                    var coord = fuelCells[x, y];
                    var rackId = coord.Item1 + 10;
                    var power = rackId * coord.Item2;
                    var ser = power + serial;
                    var mul = ser * rackId;
                    var digit = (mul / 100) % 10;
                    var num = digit - 5;

                    powers[x, y] = num;


                }
            }

            for (var y = 0; y < 300 - 3; y++)
            {
                for (var x = 0; x < 300 - 3; x++)
                {
                    // select 3x3
                    var sum = 0;
                    for (var j = 0; j < 3; j++)
                    {
                        for (var i = 0; i < 3; i++)
                        {
                            sum += powers[x + i, y + j];
                        }
                    }

                    if (sum > max)
                    {
                        max = sum;
                        maxCoord = (x + 1, y + 1);
                    }

                }
            }

            Result = maxCoord.Item1 + "," + maxCoord.Item2;
        }
    }
}