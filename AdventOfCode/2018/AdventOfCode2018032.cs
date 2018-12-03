using System;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2018
{
    /// <summary>
    /// --- Day 3: No Matter How You Slice It ---
    ///
    /// --- Part Two ---
    /// 
    /// Amidst the chaos, you notice that exactly one claim doesn't overlap by even
    /// a single square inch of fabric with any other claim. If you can somehow
    /// draw attention to it, maybe the Elves will be able to make Santa's suit
    /// after all!
    /// 
    /// For example, in the claims above, only claim 3 is intact after all claims
    /// are made.
    /// 
    /// What is the ID of the only claim that doesn't overlap?
    /// </summary>
    [AdventOfCode(2018, 3, 2, "No Matter How You Slice It - Part 2", 552)]
    public class AdventOfCode2018032 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var claims = File.ReadAllLines(@"2018\AdventOfCode201803.txt")
                             .Select(c => c.ReplaceAll(new[] {"#", "@", ":"}, "").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                             .Select(c => new Claim
                             {
                                 Id = int.Parse(c[0]),
                                 Left = int.Parse(c[1].Split(',')[0]),
                                 Top = int.Parse(c[1].Split(',')[1]),
                                 Width = int.Parse(c[2].Split('x')[0]),
                                 Height = int.Parse(c[2].Split('x')[1])
                             })
                             .ToList();

            var fabric = new int[1000, 1000];

            foreach (var claim in claims)
            {
                for (var x = claim.Left; x < claim.Left + claim.Width; x++)
                {
                    for (var y = claim.Top; y < claim.Top + claim.Height; y++)
                    {
                        fabric[x, y]++;
                    }
                }
            }

            foreach (var claim in claims)
            {
                var isSingle = true;
                for (var x = claim.Left; x < claim.Left + claim.Width && isSingle; x++)
                {
                    for (var y = claim.Top; y < claim.Top + claim.Height; y++)
                    {
                        if (fabric[x, y] == 1) continue;

                        isSingle = false;
                        break;
                    }
                }

                if (isSingle)
                {
                    Result = claim.Id;
                    break;
                }
            }
        }
    }
}
