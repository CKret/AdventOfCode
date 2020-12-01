using System.Collections.Generic;
using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 22: Sporifica Virus ---
    /// 
    /// Diagnostics indicate that the local grid computing cluster has been
    /// contaminated with the Sporifica Virus. The grid computing cluster is a
    /// seemingly-infinite two-dimensional grid of compute nodes. Each node is
    /// either clean or infected by the virus.
    /// 
    /// To prevent overloading the nodes (which would render them useless to the
    /// virus) or detection by system administrators, exactly one virus carrier
    /// moves through the network, infecting or cleaning nodes as it moves. The
    /// virus carrier is always located on a single node in the network (the
    /// current node) and keeps track of the direction it is facing.
    /// 
    /// To avoid detection, the virus carrier works in bursts; in each burst, it
    /// wakes up, does some work, and goes back to sleep. The following steps are
    /// all executed in order one time each burst:
    /// 
    ///     - If the current node is infected, it turns to its right. Otherwise, it
    ///       turns to its left. (Turning is done in-place; the current node does
    ///       not change.)
    ///     - If the current node is clean, it becomes infected. Otherwise, it
    ///       becomes cleaned. (This is done after the node is considered for the
    ///       purposes of changing direction.)
    ///     - The virus carrier moves forward one node in the direction it is
    ///       facing.
    /// 
    /// Diagnostics have also provided a map of the node infection status (your
    /// puzzle input). Clean nodes are shown as .; infected nodes are shown as #.
    /// This map only shows the center of the grid; there are many more nodes
    /// beyond those shown, but none of them are currently infected.
    /// 
    /// The virus carrier begins in the middle of the map facing up.
    /// 
    /// For example, suppose you are given a map like this:
    /// 
    /// ..#
    /// #..
    /// ...
    /// 
    /// Then, the middle of the infinite grid looks like this, with the virus
    /// carrier's position marked with [ ]:
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
    /// The virus carrier is on a clean node, so it turns left, infects the node,
    /// and moves left:
    /// 
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . # . . .
    /// . . .[#]# . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// 
    /// The virus carrier is on an infected node, so it turns right, cleans the
    /// node, and moves up:
    /// 
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . .[.]. # . . .
    /// . . . . # . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// 
    /// Four times in a row, the virus carrier finds a clean, infects it, turns
    /// left, and moves forward, ending in the same place and still facing up:
    /// 
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . #[#]. # . . .
    /// . . # # # . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// 
    /// Now on the same node as before, it sees an infection, which causes it to
    /// turn right, clean the node, and move forward:
    /// 
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . # .[.]# . . .
    /// . . # # # . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// 
    /// After the above actions, a total of 7 bursts of activity had taken place.
    /// Of them, 5 bursts of activity caused an infection.
    /// 
    /// After a total of 70, the grid looks like this, with the virus carrier
    /// facing up:
    /// 
    /// . . . . . # # . .
    /// . . . . # . . # .
    /// . . . # . . . . #
    /// . . # . #[.]. . #
    /// . . # . # . . # .
    /// . . . . . # # . .
    /// . . . . . . . . .
    /// . . . . . . . . .
    /// 
    /// By this time, 41 bursts of activity caused an infection (though most of
    /// those nodes have since been cleaned).
    /// 
    /// After a total of 10000 bursts of activity, 5587 bursts will have caused an
    /// infection.
    /// 
    /// Given your actual map, after 10000 bursts of activity, how many bursts
    /// cause a node to become infected? (Do not count nodes that begin infected.)
    /// 
    /// </summary>
    [AdventOfCode(2017, 22, 1, "Sporifica Virus - Part One", 5261)]
    public class AdventOfCode2017221 : AdventOfCodeBase
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
            for (var i = 0; i < 10000; i++)
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
            switch (node)
            {
                case '.': return (direction + 3) % 4;
                case '#': return (direction + 1) % 4;
                case 'F': return (direction + 2) % 4;
                default: return direction;
            }
        }

        internal static char ChangeState(char currentState)
        {
            switch (currentState)
            {
                case '.': return '#';
                default: return '.';
            }
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

        public AdventOfCode2017221(string sessionCookie) : base(sessionCookie) { }
    }
}
