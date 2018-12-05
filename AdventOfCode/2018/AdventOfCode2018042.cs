using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2018
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2018, 4, 2, "", 141071)]
    public class AdventOfCode2018042 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var input = File.ReadAllLines(@"2018\AdventOfCode201804.txt");

            var guards = new List<Guard>();
            Guard currentGuard = null;
            //var sleepStart = DateTime.MinValue;
            //var sleepEnd = DateTime.MinValue;
            var sorted = new SortedList<DateTime, string>();

            foreach (var line in input)
            {
                var current = line.ReplaceAll(new[] { "[", "]", "#" }, "").Split(' ');
                var currentDate = DateTime.Parse(current[0] + " " + current[1]);
                sorted.Add(currentDate, line);
            }

            foreach (var line in sorted.Values)
            {
                var current = line.ReplaceAll(new[] { "[", "]", "#" }, "").Split(' ');
                var currentDate = DateTime.Parse(current[0] + " " + current[1]);
                if (current[2] == "Guard")
                {
                    var id = int.Parse(current[3]);
                    if (guards.All(g => g.Id != id))
                    {
                        currentGuard = new Guard { Id = id };
                        guards.Add(currentGuard);
                    }
                    else
                    {
                        currentGuard = guards.Single(g => g.Id == id);
                    }

                    currentGuard.ShiftStarts.Add(currentDate);
                }
                else if (current[2] == "falls")
                {
                    currentGuard.FallsAsleep.Add(currentDate);
                }
                else if (current[2] == "wakes")
                {
                    currentGuard.WakesUp.Add(currentDate);
                }
            }

            foreach (var guard in guards)
            {
                foreach (var falls in guard.FallsAsleep)
                {
                    var wakes = guard.WakesUp.FirstOrDefault(w => w > falls && w <= falls?.AddDays(1));
                    if (wakes == null) wakes = falls?.AddMinutes((int)(60 - falls?.Minute));

                    guard.SleepTimes.Add((wakes.Value - falls.Value));

                    for (var m = falls.Value.Minute; m < wakes.Value.Minute; m++)
                    {
                        guard.SleepMinutes[m]++;
                    }
                }
            }

            var max = 0;
            Guard maxGuard = null;
            foreach (var guard in guards)
            {
                var current = guard.SleepMinutes.Max();
                if (current > max)
                {
                    max = current;
                    maxGuard = guard;
                }
            }

            var maxMinute = maxGuard.SleepMinutes.Max();
            var index = Array.IndexOf(maxGuard.SleepMinutes, maxMinute);

            Result = maxGuard.Id * index;
        }
    }
}
