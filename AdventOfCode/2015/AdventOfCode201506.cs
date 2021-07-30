using System.Globalization;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 6, "", 400410, 15343601)]
    public class AdventOfCode201506 : AdventOfCodeBase
    {
        public AdventOfCode201506(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var lights = new bool[1000][];

            for (var i = 0; i < 1000; i++)
                lights[i] = new bool[1000];

            var turn = false;
            var on = false;
            int startX, startY, endX, endY;

            foreach (var line in Input)
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

            return lights.SelectMany(l => l).Count(l => l);
        }

        protected override object SolvePart2()
        {
            var lights = new int[1000][];

            for (var i = 0; i < 1000; i++)
                lights[i] = new int[1000];

            foreach (var line in Input)
            {
                var instructions = line.Split(' ');

                var offset = 0;
                var on = false;
                var turn = instructions[0] == "turn";
                if (turn)
                {
                    on = instructions[1] == "on";
                    offset = 1;
                }

                var coords = instructions[1 + offset].Split(',');
                var startX = int.Parse(coords[0], CultureInfo.InvariantCulture);
                var startY = int.Parse(coords[1], CultureInfo.InvariantCulture);

                coords = instructions[3 + offset].Split(',');
                var endX = int.Parse(coords[0], CultureInfo.InvariantCulture);
                var endY = int.Parse(coords[1], CultureInfo.InvariantCulture);

                for (var y = startY; y <= endY; y++)
                {
                    for (var x = startX; x <= endX; x++)
                    {
                        if (turn && on) lights[y][x]++;
                        else if (turn && lights[y][x] > 0) lights[y][x]--;
                        else if (!turn) lights[y][x] += 2;
                    }
                }
            }

            return lights.SelectMany(l => l).Sum(l => l);
        }
    }
}
