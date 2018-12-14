using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Core;

namespace AdventOfCode._2018
{
    /// <summary>
    /// --- Day 10: The Stars Align ---
    ///
    /// --- Part Two ---
    /// 
    /// Good thing you didn't have to wait, because that would have taken a long
    /// time - much longer than the 3 seconds in the example above.
    /// 
    /// Impressed by your sub-hour communication capabilities, the Elves are
    /// curious: exactly how many seconds would they have needed to wait for that
    /// message to appear?
    /// </summary>
    [AdventOfCode(2018, 10, 2, "The Stars Align - Part 2", 10946)]
    public class AdventOfCode2018102 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var lights = File.ReadAllLines(@"2018\AdventOfCode201810.txt")
                             .Select(line => Regex.Matches(line, @"-?\d+"))
                             .Select(matches => new LightSignal
                             {
                                 PositionX = int.Parse(matches[0].Value, CultureInfo.InvariantCulture),
                                 PositionY = int.Parse(matches[1].Value, CultureInfo.InvariantCulture),
                                 VelocityX = int.Parse(matches[2].Value, CultureInfo.InvariantCulture),
                                 VelocityY = int.Parse(matches[3].Value, CultureInfo.InvariantCulture)
                             })
                             .ToList();

            var previousMinY = long.MaxValue;
            var messageTime = 0;
            
            while(true)
            {
                long currentY = (lights.Max(x => x.PositionY) - lights.Min(x => x.PositionY));
                if (currentY > previousMinY) break;

                previousMinY = currentY;

                foreach (var l in lights)
                {
                    l.PositionX += l.VelocityX;
                    l.PositionY += l.VelocityY;
                }

                messageTime++;
            }

            Result = --messageTime ;
        }
    }
}
