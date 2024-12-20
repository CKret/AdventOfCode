using System;
using AdventOfCode.Core;
using System.Linq;
using AdventOfCode.Mathematics;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 12, "Rain Risk", 1482, 48739)]
    public class AdventOfCode202012 : AdventOfCodeBase
    {
        public AdventOfCode202012(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var instructions = Input.Select(i => new { Direction = i[0], Units =  int.Parse(i[1..]) });

            var (x, y) = (0, 0);
            var direction = 'E';
            var directionUnits = 90;

            foreach (var instruction in instructions)
            {
                if (instruction.Direction == 'F')
                {
                    switch (direction)
                    {
                        case 'E': x += instruction.Units;
                            break;
                        case 'W': x -= instruction.Units;
                            break;
                        case 'N': y -= instruction.Units;
                            break;
                        case 'S': y += instruction.Units;
                            break;
                    }
                }
                else if (instruction.Direction == 'E') x += instruction.Units;
                else if (instruction.Direction == 'W') x -= instruction.Units;
                else if (instruction.Direction == 'N') y -= instruction.Units;
                else if (instruction.Direction == 'S') y += instruction.Units;
                else if (instruction.Direction == 'R')
                {
                    directionUnits = (directionUnits + instruction.Units) % 360;
                    direction = directionUnits switch
                    {
                        0 => 'N',
                        90 => 'E',
                        180 => 'S',
                        270 => 'W',
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
                else if (instruction.Direction == 'L')
                {
                    directionUnits = (directionUnits + 360 - instruction.Units) % 360;
                    direction = directionUnits switch
                    {
                        0 => 'N',
                        90 => 'E',
                        180 => 'S',
                        270 => 'W',
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
            }

            return NumberTheory.ManhattanDistance((0, 0), (x, y));
        }

        protected override object SolvePart2()
        {
            var instructions = Input.Select(i => new { Direction = i[0], Units = int.Parse(i[1..]) });

            var (x, y) = (0, 0);
            var (waypointX, waypointY) = (10, -1);

            foreach (var instruction in instructions)
            {
                if (instruction.Direction == 'F')
                {
                    x += instruction.Units * waypointX;
                    y += instruction.Units * waypointY;
                }
                else if (instruction.Direction == 'E') waypointX += instruction.Units;
                else if (instruction.Direction == 'W') waypointX -= instruction.Units;
                else if (instruction.Direction == 'N') waypointY -= instruction.Units;
                else if (instruction.Direction == 'S') waypointY += instruction.Units;
                else if (instruction.Direction == 'R')
                {
                    switch (instruction.Units)
                    {
                        case 90:
                            var tmpX = waypointX;
                            waypointX = waypointY * -1;
                            waypointY = tmpX;
                            break;
                        case 180:
                            waypointX *= -1;
                            waypointY *= -1;
                            break;
                        case 270:
                            tmpX = waypointX;
                            waypointX = waypointY;
                            waypointY = tmpX * -1;
                            break;
                    }
                }
                else if (instruction.Direction == 'L')
                {
                    switch (instruction.Units)
                    {
                        case 90:
                            var tmpY = waypointY;
                            waypointY = waypointX * -1;
                            waypointX =  tmpY;
                            break;
                        case 180:
                            waypointX *= -1;
                            waypointY *= -1;
                            break;
                        case 270:
                            tmpY = waypointY;
                            waypointY = waypointX;
                            waypointX = tmpY * -1;
                            break;
                    }
                }
            }

            return NumberTheory.ManhattanDistance((0, 0), (x, y));
        }
    }
}
