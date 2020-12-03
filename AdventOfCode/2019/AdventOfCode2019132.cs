using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode.Core;
using AdventOfCode.VMs;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 13, "", 15957L)]
    public class AdventOfCode2019132 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201913.txt");
            var vm = new IntcodeVM(data);

            vm.Write(0, 2);
            var paddle = (X: -1L, Y: -1L);
            var ball = (X: -1L, Y: -1L);

            while (vm.Execute() != IntcodeVM.HaltMode.Terminated)
            {
                var newState = ProcessOutput(vm);

                if (newState.Paddle.X != -1 && newState.Paddle.Y != -1) paddle = newState.Paddle;
                if (newState.Ball.X != -1 && newState.Ball.Y != -1) ball = newState.Ball;

                if (ball.X < paddle.X) vm.Input.Enqueue(-1);
                else if (ball.X > paddle.X) vm.Input.Enqueue(1);
                else vm.Input.Enqueue(0);
            }

            var finalState = ProcessOutput(vm);

            Result = finalState.Score;
        }

        private ((long X, long Y) Paddle, (long X, long Y) Ball, long Score) ProcessOutput(IntcodeVM vm)
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

        private (long X, long Y) GetBallCoordinates(List<long> currentState)
        {
            if (currentState == null) throw new ArgumentNullException(nameof(currentState));

            var ball = currentState.FindIndex(t => t == 4);
            return ball >= 2 ? (currentState[ball - 2], currentState[ball - 1]) : (-1, -1);
        }

        private (long X, long Y) GetPaddleCoordinates(List<long> currentState)
        {
            if (currentState == null) throw new ArgumentNullException(nameof(currentState));

            var paddle = currentState.FindIndex(t => t == 3);
            return paddle >= 2 ? (currentState[paddle - 2], currentState[paddle - 1]) : (-1, -1);
        }

        private long GetCurrentScore(List<long> currentState)
        {
            if (currentState == null) throw new ArgumentNullException(nameof(currentState));

            var score = currentState.FindIndex(t => t == -1);
            return score >= 0 ? currentState[score + 2] : 0;
        }

        public AdventOfCode2019132(string sessionCookie) : base(sessionCookie) { }
    }
}
