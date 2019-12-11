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
    [AdventOfCode(2019, 11, 1, "", 2172)]
    public class AdventOfCode2019111 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201911.txt");

            var position = (0, 0);
            var direction = RobotDirection.Up;
            var panels = new Dictionary<(int, int), long>();
            var vm = new IntcodeVM(data);

            panels.Add((0, 0), 1L);

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

            var minX = -panels.Keys.Min(x => x.Item1);
            var minY = -panels.Keys.Min(y => y.Item2);

            using (var pass = new Bitmap(1000, 1000))
            {
                for (var y = 0; y < 1000; y++)
                {
                    for (var x = 0; x < 1000; x++)
                        pass.SetPixel(x, y, Color.Black);
                }

                foreach (var panel in panels)
                {
                    if (panel.Value == 1)
                    {
                        pass.SetPixel(panel.Key.Item1 + minX + 50, panel.Key.Item2 + minY + 50, Color.White);
                    }
                }

                pass.Save(@".\2019\AdventOfCode2019112.png");
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
    }
}
