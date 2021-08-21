using System;
using AdventOfCode.Core;
using AdventOfCode.VMs;
using System.Collections.Generic;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 15, "", 354, 370)]
    public class AdventOfCode201915 : AdventOfCodeBase
    {
        public AdventOfCode201915(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var map = new HashSet<(long, long)> { (0, 0) };
            var droids = new Queue<Droid>();
            droids.Enqueue(new Droid(0, 0, 0, new IntcodeVM(Input[0])));

            while (droids.Count > 0)
            {
                var current = droids.Dequeue();

                for (var i = 1; i <= 4; i++)
                {
                    var next = current.TryGoStep((Droid.Direction)i, map);
                    if (next == null) continue; // Wall
                    if (next.Finsihed)          // Oxygen
                    {
                        return next.Steps;
                    }

                    droids.Enqueue(next);       // Open path
                }
            }

            return null;
        }

        protected override object SolvePart2()
        {
            var map = new HashSet<(long, long)> { (0, 0) };
            var droids = new Queue<Droid>();
            droids.Enqueue(new Droid(0, 0, 0, new IntcodeVM(Input[0])));

            Droid current;
            Droid oxygen = null;
            while (droids.Count > 0)
            {
                current = droids.Dequeue();

                for (var i = 1; i <= 4; i++)
                {
                    var next = current.TryGoStep((Droid.Direction)i, map);
                    if (next == null) continue; // Wall
                    if (next.Finsihed)          // Oxygen
                    {
                        oxygen = next;
                        droids.Clear();
                        break;
                    }

                    droids.Enqueue(next);       // Open path
                }
            }

            map.Clear();
            oxygen.Steps = 0;

            var filled = new Queue<Droid>();
            filled.Enqueue(oxygen);

            current = null;
            while (filled.Count > 0)
            {
                current = filled.Dequeue();

                for (var i = 1; i <= 4; i++)
                {
                    var next = current.TryGoStep((Droid.Direction)i, map);
                    if (next == null) continue;

                    filled.Enqueue(next);
                }
            }

            return current.Steps;
        }
    }

    public class Droid
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Steps { get; set; }
        public bool Finsihed { get; set; }
        public IntcodeVM VM { get; set; }

        public Droid(long x, long y, long steps, IntcodeVM vm, bool finished = false)
        {
            X = x;
            Y = y;
            Steps = steps;
            VM = vm ?? throw new ArgumentNullException(nameof(vm));
            Finsihed = finished;
        }

        public Droid TryGoStep(Direction direction, HashSet<(long, long)> map)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));

            VM.ResetVM();

            var (x, y) = direction switch
            {
                Direction.North => (X, Y + 1),
                Direction.South => (X, Y - 1),
                Direction.West => (X - 1, Y),
                Direction.East => (X + 1, Y),
                _ => throw new Exception("Unknown direction.")
            };

            if (map.Contains((x, y))) return null;
            map.Add((x, y));

            VM.Input.Enqueue((int) direction);
            VM.Execute();

            return VM.Output.Dequeue() switch
            {
                0 => null,
                1 => new Droid(x, y, Steps + 1, new IntcodeVM(VM.GetCurrentState())),
                2 => new Droid(x, y, Steps + 1, new IntcodeVM(VM.GetCurrentState()), true),
                _ => throw new Exception("Invalid output")
            };
        }

        public enum Direction
        {
            North = 1,
            South = 2,
            West = 3,
            East = 4
        }
    }
}
