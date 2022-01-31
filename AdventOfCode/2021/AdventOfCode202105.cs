using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2021
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2021, 5, "Hydrothermal Venture", 5585, 17193)]
    public class AdventOfCode202105 : AdventOfCodeBase
    {
        public AdventOfCode202105(string sessionCookie) : base(sessionCookie) { }

        private IEnumerable<(int x1, int y1, int x2, int y2)> Lines => Input
            .Select(line => line
                .Split(" -> ", StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray())
                .ToArray())
            .Select(c => (x1: c[0][0], y1: c[0][1], x2: c[1][0], y2: c[1][1]))
            .ToArray();

        protected override object SolvePart1()
        {
            return Lines
                .SelectMany(line => LineCoordinates(line))
                .GroupBy(c => c)
                .Count(g => g.Count() > 1);
        }

        protected override object SolvePart2()
        {
            return Lines
                .SelectMany(line => LineCoordinates(line, true))
                .GroupBy(c => c)
                .Count(g => g.Count() > 1);
        }

        protected static IEnumerable<(int x, int y)> LineCoordinates((int x1, int y1, int x2, int y2) line, bool returnDiagonal = false)
        {
            var (x1, y1, x2, y2) = line;
            var slopeX = Math.Sign(x2 - x1);
            var slopeY = Math.Sign(y2 - y1);

            if (!returnDiagonal && slopeX != 0 && slopeY != 0)
            {
                yield break;
            }

            for (int x = x1, y = y1; x != x2 + slopeX || y != y2 + slopeY; x += slopeX, y += slopeY)
            {
                yield return (x, y);
            }
        }
    }
}
