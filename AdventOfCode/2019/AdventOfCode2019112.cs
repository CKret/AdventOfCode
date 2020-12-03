using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.VMs;
using Tesseract;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 1, "", "JELEFGHP")]
    public class AdventOfCode2019112 : AdventOfCodeBase
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
                var turn = (int)vm.Output.Dequeue();

                if (panels.ContainsKey(position)) panels[position] = panelColor;
                else panels.Add(position, panelColor);

                direction = TurnRobot(direction, turn);

                position = MoveRobot(direction, position);
            }

            var minX = panels.Keys.Min(x => x.Item1);
            var minY = panels.Keys.Min(y => y.Item2);
            var maxX = panels.Keys.Max(x => x.Item1);
            var maxY = panels.Keys.Max(y => y.Item2);
            var imgWidth = maxX - minX + 20;
            var imgHeight = maxY - minY + 20;
            using (var image = new Bitmap(imgWidth, imgHeight))
            {
                for (var y = 0; y < imgHeight; y++)
                {
                    for (var x = 0; x < imgWidth; x++)
                        image.SetPixel(x, y, Color.Black);
                }

                foreach (var panel in panels)
                {
                    if (panel.Value == 1)
                    {
                        image.SetPixel(panel.Key.Item1 - minX + 10, panel.Key.Item2 - minY + 10, Color.White);
                    }
                }
                image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                var bigImage = new Bitmap(image, new Size(image.Width * 4, image.Height * 4));
                bigImage.Save(@".\2019\AdventOfCode2019112.png");

                using (var engine = new TesseractEngine(@".\_ExternalDependencies\tessdata", "eng", EngineMode.Default))
                using (var pix = PixConverter.ToPix(bigImage))
                using (var page = engine.Process(pix))
                {
                    Result = page.GetText().Trim('\n');
                }

                bigImage.Dispose();
            }
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

        public AdventOfCode2019112(string sessionCookie) : base(sessionCookie) { }
    }
}
