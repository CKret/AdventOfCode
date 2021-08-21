using System.Collections.Generic;
using System;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.VMs;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 13, "", 324, 15957L)]
    public class AdventOfCode201913 : AdventOfCodeBase
    {
        public AdventOfCode201913(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var vm = new IntcodeVM(Input[0]);
            var result = vm.Execute();

            return vm.Output.Where((t, i) => i % 3 == 2).Count(x => x == 2);
        }

        protected override object SolvePart2()
        {
            var vm = new IntcodeVM(Input[0]);

            vm.Write(0, 2);
            var paddle = (X: -1L, Y: -1L);
            var ball = (X: -1L, Y: -1L);

            while (vm.Execute() != IntcodeVM.HaltMode.Terminated)
            {
                var (Paddle, Ball, _) = ProcessOutput(vm);

                if (Paddle.X != -1 && Paddle.Y != -1) paddle = Paddle;
                if (Ball.X != -1 && Ball.Y != -1) ball = Ball;

                if (ball.X < paddle.X) vm.Input.Enqueue(-1);
                else if (ball.X > paddle.X) vm.Input.Enqueue(1);
                else vm.Input.Enqueue(0);
            }

            var finalState = ProcessOutput(vm);

            return finalState.Score;
        }

        private static ((long X, long Y) Paddle, (long X, long Y) Ball, long Score) ProcessOutput(IntcodeVM vm)
        {
            var score = 0L;
            var paddle = (X: -1L, Y: -1L);
            var ball = (X: -1L, Y: -1L);

            while (vm.Output.Count > 0)
            {
                var (x, y, value) = (vm.Output.Dequeue(), vm.Output.Dequeue(), vm.Output.Dequeue());

                if (x == -1 && y == 0)  // Score
                {
                    score = value;
                }
                else
                {
                    switch (value)
                    {
                        case 0:                 // Empty
                            break;
                        case 1:                 // Wall
                            break;
                        case 2:                 // Block
                            break;
                        case 3:                 // Paddle
                            paddle = (x, y);
                            break;
                        case 4:                 // Ball
                            ball = (x, y);
                            break;
                    }
                }
            }

            return (paddle, ball, score);

        }
    }
}
