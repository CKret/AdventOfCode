using System.Globalization;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// Because your neighbors keep defeating you in the holiday house decorating contest year after year, you've decided to deploy one million lights in a 1000x1000 grid.
    /// 
    /// Furthermore, because you've been especially nice this year, Santa has mailed you instructions on how to display the ideal lighting configuration.
    /// 
    /// Lights in your grid are numbered from 0 to 999 in each direction; the lights at each corner are at 0,0, 0,999, 999,999, and 999,0. The instructions include whether to turn on, turn off, or toggle various inclusive ranges given as coordinate pairs. Each coordinate pair represents opposite corners of a rectangle, inclusive; a coordinate pair like 0,0 through 2,2 therefore refers to 9 lights in a 3x3 square. The lights all start turned off.
    /// 
    /// To defeat your neighbors this year, all you have to do is set up your lights by doing the instructions Santa sent you in order.
    /// </summary>
    [AdventOfCode(2015, 6, 1, "After following the instructions, how many lights are lit?", 400410)]
    public class AdventOfCode2015061 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var lights = new bool[1000][];

            for (var i = 0; i < 1000; i++)
                lights[i] = new bool[1000];

            var turn = false;
            var on = false;
            int startX, startY, endX, endY;

            foreach (var line in File.ReadAllLines("2015/AdventOfCode201506.txt"))
            {
                var instructions = line.Split(' ');

                var offset = 0;
                turn = instructions[0] == "turn";
                if (turn)
                {
                    on = instructions[1] == "on";
                    offset = 1;
                }

                var coords = instructions[1 + offset].Split(',');
                startX = int.Parse(coords[0], CultureInfo.InvariantCulture);
                startY = int.Parse(coords[1], CultureInfo.InvariantCulture);

                coords = instructions[3 + offset].Split(',');
                endX = int.Parse(coords[0], CultureInfo.InvariantCulture);
                endY = int.Parse(coords[1], CultureInfo.InvariantCulture);

                for (var y = startY; y <= endY; y++)
                {
                    for (var x = startX; x <= endX; x++)
                    {
                        if (turn && on) lights[y][x] = true;
                        else if (turn) lights[y][x] = false;
                        else lights[y][x] = !lights[y][x];
                    }
                }
            }

            Result = lights.SelectMany(l => l).Count(l => l);
        }
    }
}
