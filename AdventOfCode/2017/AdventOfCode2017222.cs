using System.Collections.Generic;
using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 22: Sporifica Virus ---
    /// 
    /// --- Part Two ---
    /// 
    /// As you go to remove the virus from the infected nodes, it evolves to resist
    /// your attempt.
    /// 
    /// Now, before it infects a clean node, it will weaken it to disable your
    /// defenses. If it encounters an infected node, it will instead flag the node
    /// to be cleaned in the future. So:
    /// 
    ///     Clean nodes become weakened.
    ///     Weakened nodes become infected.
    ///     Infected nodes become flagged.
    ///     Flagged nodes become clean.
    /// 
    /// Every node is always in exactly one of the above states.
    /// 
    /// The virus carrier still functions in a similar way, but now uses the
    /// following logic during its bursts of action:
    /// 
    ///     - Decide which way to turn based on the current node:
    ///         - If it is clean, it turns left.
    ///         - If it is weakened, it does not turn, and will continue moving in
    ///           the same direction.
    ///         - If it is infected, it turns right.
    ///         - If it is flagged, it reverses direction, and will go back the way
    ///           it came.
    ///     - Modify the state of the current node, as described above.
    ///     - The virus carrier moves forward one node in the direction it is
    ///       facing.
    /// 
    /// Start with the same map (still using . for clean and # for infected) and
    /// still with the virus carrier starting in the middle and facing up.
    /// 
    /// Using the same initial state as the previous example, and drawing weakened
    /// as W and flagged as F, the middle of the infinite grid looks like this,
    /// with the virus carrier's position again marked with [ ]:
    /// 
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . # . . .
    /// . . . #[.]. . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// 
    /// This is the same as before, since no initial nodes are weakened or flagged.
    /// The virus carrier is on a clean node, so it still turns left, instead
    /// weakens the node, and moves left:
    /// 
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . # . . .
    /// . . .[#]W . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// 
    /// The virus carrier is on an infected node, so it still turns right, instead
    /// flags the node, and moves up:
    /// 
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . .[.]. # . . .
    /// . . . F W . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// 
    /// This process repeats three more times, ending on the previously-flagged
    /// node and facing right:
    /// 
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . W W . # . . .
    /// . . W[F]W . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// 
    /// Finding a flagged node, it reverses direction and cleans the node:
    /// 
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . W W . # . . .
    /// . .[W]. W . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// 
    /// The weakened node becomes infected, and it continues in the same direction:
    /// 
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . W W . # . . .
    /// .[.]# . W . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// 
    /// Of the first 100 bursts, 26 will result in infection. Unfortunately,
    /// another feature of this evolved virus is speed; of the first 10000000
    /// bursts, 2511944 will result in infection.
    /// 
    /// Given your actual map, after 10000000 bursts of activity, how many bursts
    /// cause a node to become infected? (Do not count nodes that begin infected.)
    /// 
    /// </summary>
    [AdventOfCode(2017, 22, 2, "Sporifica Virus - Part Two", 2511927)]
    public class AdventOfCode2017222 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var input = File.ReadAllLines("2017\\AdventOfCode201722.txt");

            var nodes = new Dictionary<(int, int), char>();

            int x, y;
            for (y = 0; y < input.Length; y++)
                for (x = 0; x < input.Length; x++)
                    if (input[y][x] == '#') nodes.Add((y, x), input[y][x]);

            x = input.Length / 2;
            y = input.Length / 2;

            var direction = 0;
            var count = 0;
            for (var i = 0; i < 10000000; i++)
            {
                if (!nodes.ContainsKey((y, x))) nodes.Add((y, x), '.');

                direction = Turn(nodes[(y, x)], direction);
                nodes[(y, x)] = ChangeState(nodes[(y, x)]);
                if (nodes[(y, x)] == '#') count++;
                (x, y) = Move(x, y, direction);
            }

            Result = count;
        }

        internal static int Turn(char node, int direction)
        {
            var turns = new Dictionary<char, int> { { '.', 3 }, { '#', 1 }, { 'F', 2 }, { 'W', 0 } };
            return (direction + turns[node]) % 4;
        }

        internal static char ChangeState(char currentState)
        {
            const string states = ".W#F.";
            return states[states.IndexOf(currentState) + 1];
        }

        internal static (int, int) Move(int x, int y, int direction)
        {
            switch (direction)
            {
                case 0: return (x, --y);    // Up
                case 1: return (++x, y);    // Right
                case 2: return (x, ++y);    // Down
                default: return (--x, y);   // Left
            }
        }
    }
}
