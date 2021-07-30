using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 16: Permutation Promenade ---
    /// 
    /// You come upon a very unusual sight; a group of programs here appear to be
    /// dancing.
    /// 
    /// There are sixteen programs in total, named a through p. They start by
    /// standing in a line: a stands in position 0, b stands in position 1, and so
    /// on until p, which stands in position 15.
    /// 
    /// The programs' dance consists of a sequence of dance moves:
    /// 
    ///     - Spin, written sX, makes X programs move from the end to the front, but
    ///       maintain their order otherwise. (For example, s3 on abcde produces
    ///       cdeab).
    ///     - Exchange, written xA/B, makes the programs at positions A and B swap
    ///       places.
    ///     - Partner, written pA/B, makes the programs named A and B swap places.
    /// 
    /// For example, with only five programs standing in a line (abcde), they could
    /// do the following dance:
    /// 
    ///     - s1, a spin of size 1: eabcd.
    ///     - x3/4, swapping the last two programs: eabdc.
    ///     - pe/b, swapping programs e and b: baedc.
    /// 
    /// After finishing their dance, the programs end up in order baedc.
    /// 
    /// You watch the dance for a while and record their dance moves (your puzzle
    /// input). In what order are the programs standing after their dance?
    /// 
    /// --- Part Two ---
    /// 
    /// Now that you're starting to get a feel for the dance moves, you turn your
    /// attention to the dance as a whole.
    /// 
    /// Keeping the positions they ended up in from their previous dance, the
    /// programs perform it again and again: including the first dance, a total of
    /// one billion (1000000000) times.
    /// 
    /// In the example above, their second dance would begin with the order baedc,
    /// and use the same dance moves:
    /// 
    ///     - s1, a spin of size 1: cbaed.
    ///     - x3/4, swapping the last two programs: cbade.
    ///     - pe/b, swapping programs e and b: ceadb.
    /// 
    /// In what order are the programs standing after their billion dances?
    /// </summary>
    [AdventOfCode(2017, 16, "Permutation Promenade", "ionlbkfeajgdmphc", "fdnphiegakolcmjb")]
    public class AdventOfCode201716 : AdventOfCodeBase
    {
        public AdventOfCode201716(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var programs = "abcdefghijklmnop".ToCharArray().ToList();
            var commands = Input[0].Split(',');

            programs = DoDaDance(programs, commands);
            return string.Concat(programs);
        }

        protected override object SolvePart2()
        {
            var programs = "abcdefghijklmnop".ToCharArray().ToList();
            var commandsArray = Input[0].Split(',');

            // Fake-Memoize so that we can detect cycles, if any.
            var lookup = new List<string> { string.Concat(programs) };

            for (var i = 0L; i < 1000000000; i++)
            {
                programs = AdventOfCode201716.DoDaDance(programs, commandsArray);

                // If we find a cycle then get the period and return the programs order in the state (1000000000 % periodLength).
                var programString = string.Concat(programs);
                if (lookup.Contains(programString))
                {
                    return lookup[1000000000 % lookup.Count];
                }
                lookup.Add(programString);
            }

            return null;
        }

        public static List<char> DoDaDance(List<char> programs, string[] commands)
        {
            foreach (var command in GetCommands(commands))
            {
                if (command.Item1 == 's')
                {
                    // Rotate queue
                    var count = (int)command.Item2;
                    programs = programs.Skip(16 - count).Take(count).Concat(programs.Take(16 - count)).ToList();

                }
                else if (command.Item1 == 'p')
                {
                    // Swap partner A nd B
                    var indexA = programs.IndexOf((char)command.Item2);
                    var indexB = programs.IndexOf((char)command.Item3);

                    var tmp = programs[indexA];
                    programs[indexA] = programs[indexB];
                    programs[indexB] = tmp;
                }
                else if (command.Item1 == 'x')
                {
                    // Exchange position A and B
                    var posA = (int)command.Item2;
                    var posB = (int)command.Item3;

                    var tmp = programs[posA];
                    programs[posA] = programs[posB];
                    programs[posB] = tmp;
                }
            }

            return programs;
        }

        internal static IEnumerable<Tuple<char, object, object>> GetCommands(string[] commands)
        {
            foreach (var command in commands)
            {
                if (command[0] == 's')
                {
                    var count = string.Join("", command.Split('/')[0].Skip(1));
                    yield return new Tuple<char, object, object>(command[0], int.Parse(count, CultureInfo.InvariantCulture), null);

                }
                else if (command[0] == 'x')
                {
                    var p1 = string.Join("", command.Split('/')[0].Skip(1));
                    var p2 = string.Join("", command.Split('/')[1]);
                    yield return new Tuple<char, object, object>(command[0], int.Parse(p1, CultureInfo.InvariantCulture), int.Parse(p2, CultureInfo.InvariantCulture));
                }
                else if (command[0] == 'p')
                {
                    yield return new Tuple<char, object, object>(command[0], command[1], command[3]);
                }
            }
        }
    }
}
