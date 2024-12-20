using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2021
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2021, 2, "Dive!", 2272262, 2134882034)]
    public class AdventOfCode202102 : AdventOfCodeBase
    {
        public AdventOfCode202102(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var horizontalPosition = 0;
            var verticalPosition = 0;

            var commands = Input.Select(x => x.Split()).Select(c => new { Direction = c[0], Value = int.Parse(c[1]) });

            foreach (var command in commands)
            {
                if (command.Direction == "forward") horizontalPosition += command.Value;
                else if (command.Direction == "up") verticalPosition -= command.Value;
                else if (command.Direction == "down") verticalPosition += command.Value;
            }

            return horizontalPosition * verticalPosition;
        }

        protected override object SolvePart2()
        {
            var horizontalPosition = 0;
            var verticalPosition = 0;
            var aim = 0;

            var commands = Input.Select(x => x.Split()).Select(c => new { Direction = c[0], Value = int.Parse(c[1]) });

            foreach (var command in commands)
            {
                if (command.Direction == "forward")
                {
                    horizontalPosition += command.Value;
                    verticalPosition += aim * command.Value;
                }
                else if (command.Direction == "up") aim -= command.Value;
                else if (command.Direction == "down") aim += command.Value;
            }

            return horizontalPosition * verticalPosition;
        }
    }
}
