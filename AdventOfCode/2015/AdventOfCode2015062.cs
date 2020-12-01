﻿using System.Globalization;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// You just finish implementing your winning light pattern when you realize you mistranslated Santa's message from Ancient Nordic Elvish.
    /// 
    /// The light grid you bought actually has individual brightness controls; each light can have a brightness of zero or more. The lights all start at zero.
    /// 
    /// The phrase turn on actually means that you should increase the brightness of those lights by 1.
    /// 
    /// The phrase turn off actually means that you should decrease the brightness of those lights by 1, to a minimum of zero.
    /// 
    /// The phrase toggle actually means that you should increase the brightness of those lights by 2.
    /// 
    /// What is the total brightness of all lights combined after following Santa's instructions?
    /// </summary>
    [AdventOfCode(2015, 6, 2, "What is the total brightness of all lights combined after following Santa's instructions?", 15343601)]
    public class AdventOfCode2015062 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var lights = new int[1000][];

            for (var i = 0; i < 1000; i++)
                lights[i] = new int[1000];

            foreach (var line in File.ReadAllLines("2015/AdventOfCode201506.txt"))
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

            Result = lights.SelectMany(l => l).Sum(l => l);
        }

        public AdventOfCode2015062(string sessionCookie) : base(sessionCookie) { }
    }
}
