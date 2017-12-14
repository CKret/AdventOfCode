using System;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 13: Packet Scanners ---
    /// 
    /// --- Part Two ---
    /// 
    /// Now, you need to pass through the firewall without being caught - easier
    /// said than done.
    /// 
    /// You can't control the speed of the packet, but you can delay it any number
    /// of picoseconds. For each picosecond you delay the packet before beginning
    /// your trip, all security scanners move one step. You're not in the firewall
    /// during this time; you don't enter layer 0 until you stop delaying the
    /// packet.
    /// 
    /// In the example above, if you delay 10 picoseconds (picoseconds 0 - 9), you
    /// won't get caught:
    /// 
    /// State after delaying:
    ///  0   1   2   3   4   5   6
    /// [ ] [S] ... ... [ ] ... [ ]
    /// [ ] [ ]         [ ]     [ ]
    /// [S]             [S]     [S]
    ///                 [ ]     [ ]
    /// 
    /// Picosecond 10:
    ///  0   1   2   3   4   5   6
    /// ( ) [S] ... ... [ ] ... [ ]
    /// [ ] [ ]         [ ]     [ ]
    /// [S]             [S]     [S]
    ///                 [ ]     [ ]
    /// 
    ///   0   1   2   3   4   5   6
    /// ( ) [ ] ... ... [ ] ... [ ]
    /// [S] [S]         [S]     [S]
    /// [ ]             [ ]     [ ]
    ///                 [ ]     [ ]
    /// 
    /// 
    /// Picosecond 11:
    ///  0   1   2   3   4   5   6
    /// [ ] ( ) ... ... [ ] ... [ ]
    /// [S] [S]         [S]     [S]
    /// [ ]             [ ]     [ ]
    ///                 [ ]     [ ]
    /// 
    ///  0   1   2   3   4   5   6
    /// [S] (S) ... ... [S] ... [S]
    /// [ ] [ ]         [ ]     [ ]
    /// [ ]             [ ]     [ ]
    ///                 [ ]     [ ]
    /// 
    /// 
    /// Picosecond 12:
    ///  0   1   2   3   4   5   6
    /// [S] [S] (.) ... [S] ... [S]
    /// [ ] [ ]         [ ]     [ ]
    /// [ ]             [ ]     [ ]
    ///                 [ ]     [ ]
    /// 
    ///  0   1   2   3   4   5   6
    /// [ ] [ ] (.) ... [ ] ... [ ]
    /// [S] [S]         [S]     [S]
    /// [ ]             [ ]     [ ]
    ///                 [ ]     [ ]
    /// 
    /// 
    /// Picosecond 13:
    ///  0   1   2   3   4   5   6
    /// [ ] [ ] ... (.) [ ] ... [ ]
    /// [S] [S]         [S]     [S]
    /// [ ]             [ ]     [ ]
    ///                 [ ]     [ ]
    /// 
    ///  0   1   2   3   4   5   6
    /// [ ] [S] ... (.) [ ] ... [ ]
    /// [ ] [ ]         [ ]     [ ]
    /// [S]             [S]     [S]
    ///                 [ ]     [ ]
    /// 
    /// 
    /// Picosecond 14:
    ///  0   1   2   3   4   5   6
    /// [ ] [S] ... ... ( ) ... [ ]
    /// [ ] [ ]         [ ]     [ ]
    /// [S]             [S]     [S]
    ///                 [ ]     [ ]
    /// 
    ///  0   1   2   3   4   5   6
    /// [ ] [ ] ... ... ( ) ... [ ]
    /// [S] [S]         [ ]     [ ]
    /// [ ]             [ ]     [ ]
    ///                 [S]     [S]
    /// 
    /// 
    /// Picosecond 15:
    ///  0   1   2   3   4   5   6
    /// [ ] [ ] ... ... [ ] (.) [ ]
    /// [S] [S]         [ ]     [ ]
    /// [ ]             [ ]     [ ]
    ///                 [S]     [S]
    /// 
    ///  0   1   2   3   4   5   6
    /// [S] [S] ... ... [ ] (.) [ ]
    /// [ ] [ ]         [ ]     [ ]
    /// [ ]             [S]     [S]
    ///                 [ ]     [ ]
    /// 
    /// 
    /// Picosecond 16:
    ///  0   1   2   3   4   5   6
    /// [S] [S] ... ... [ ] ... ( )
    /// [ ] [ ]         [ ]     [ ]
    /// [ ]             [S]     [S]
    ///                 [ ]     [ ]
    /// 
    ///  0   1   2   3   4   5   6
    /// [ ] [ ] ... ... [ ] ... ( )
    /// [S] [S]         [S]     [S]
    /// [ ]             [ ]     [ ]
    ///                 [ ]     [ ]
    /// 
    /// Because all smaller delays would get you caught, the fewest number of
    /// picoseconds you would need to delay to get through safely is 10.
    /// 
    /// What is the fewest number of picoseconds that you need to delay the packet
    /// to pass through the firewall without being caught?
    /// 
    /// </summary>
    [AdventOfCode(2017, 13, 2, "Packet Scanners - Part Two", 3943252)]
    public class AdventOfCode2017132 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var layers = File.ReadAllLines("2017\\AdventOfCode201713.txt").Select(line => line.Split(new[] { ": " }, StringSplitOptions.None).Select(int.Parse).ToList()).ToList();
            Result = Enumerable.Range(0, int.MaxValue).First(delay => layers.All(layer => (layer[0] + delay) % (2 * layer[1] - 2) != 0));
        }
    }
}
