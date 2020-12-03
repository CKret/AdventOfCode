using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.VMs;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 11, "", 2172)]
    public class AdventOfCode2019111 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201911.txt");

            var position = (0, 0);
            var direction = RobotDirection.Up;
            var panels = new Dictionary<(int, int), long>();
            var vm = new IntcodeVM(data);

            var status = IntcodeVM.HaltMode.Unknown;
            while (status != IntcodeVM.HaltMode.Terminated)
            {
                var currentPanelColor = panels.ContainsKey(position) ? panels[position] : 0L;

                status = vm.Execute(new long[] { currentPanelColor });
                var panelColor = vm.Output.Dequeue();
                var turn = (int) vm.Output.Dequeue();

                if (panels.ContainsKey(position)) panels[position] = panelColor;
                else panels.Add(position, panelColor);

                direction = TurnRobot(direction, turn);

                position = MoveRobot(direction, position);
            }

            Result = panels.Count;
        }

        private (int, int) MoveRobot(RobotDirection currentDirection, (int, int) currentPosition)
        {
            switch (currentDirection)
            {
                case RobotDirection.Up:
                    currentPosition.Item1 += 1;
                    return currentPosition;
                case RobotDirection.Left:
                    currentPosition.Item2 -= 1;
                    return currentPosition;
                case RobotDirection.Down:
                    currentPosition.Item1 -= 1;
                    return currentPosition;
                case RobotDirection.Right:
                    currentPosition.Item2 += 1;
                    return currentPosition;
                default:
                    return currentPosition;
            }
        }

        private RobotDirection TurnRobot(RobotDirection currentDirection, int turnDirection)
        {
            switch (currentDirection)
            {
                case RobotDirection.Up:
                    if (turnDirection == 0) return RobotDirection.Left;
                    return RobotDirection.Right;
                case RobotDirection.Left:
                    if (turnDirection == 0) return RobotDirection.Down;
                    return RobotDirection.Up;
                case RobotDirection.Down:
                    if (turnDirection == 0) return RobotDirection.Right;
                    return RobotDirection.Left;
                case RobotDirection.Right:
                    if (turnDirection == 0) return RobotDirection.Up;
                    return RobotDirection.Down;
                default:
                    return RobotDirection.Up;
            }
        }

        private enum RobotDirection
        {
            Up,
            Left,
            Down,
            Right
        }

        public AdventOfCode2019111(string sessionCookie) : base(sessionCookie) { }
    }
}
