using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2018
{
    /// <summary>
    ///--- Day 4: Repose Record ---
    /// 
    /// --- Part Two ---
    /// 
    /// Strategy 2: Of all guards, which guard is most frequently asleep on the
    /// same minute?
    /// 
    /// In the example above, Guard #99 spent minute 45 asleep more than any other
    /// guard or minute - three times in total. (In all other cases, any guard
    /// spent any minute asleep at most twice.)
    /// 
    /// What is the ID of the guard you chose multiplied by the minute you chose?
    /// (In the above example, the answer would be 99 * 45 = 4455.)
    /// </summary>
    [AdventOfCode(2018, 4, 2, "Repose Record - Part 2", 141071)]
    public class AdventOfCode2018042 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var input = File.ReadAllLines(@"2018\AdventOfCode201804.txt");

            var guards = new List<Guard>();
            Guard currentGuard = null;
            var sorted = new SortedList<DateTime, string>();

            foreach (var line in input)
            {
                var current = line.ReplaceAll(new[] { "[", "]", "#" }, "").Split(' ');
                var currentDate = DateTime.Parse(current[0] + " " + current[1], CultureInfo.InvariantCulture);
                sorted.Add(currentDate, line);
            }

            foreach (var line in sorted.Values)
            {
                var current = line.ReplaceAll(new[] { "[", "]", "#" }, "").Split(' ');
                var currentDate = DateTime.Parse(current[0] + " " + current[1], CultureInfo.InvariantCulture);
                if (current[2] == "Guard")
                {
                    var id = int.Parse(current[3], CultureInfo.InvariantCulture);
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
            var index = maxGuard.SleepMinutes.IndexOf(maxMinute);

            Result = maxGuard.Id * index;
        }
    }
}
