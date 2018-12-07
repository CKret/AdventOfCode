using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2018
{
    /// <summary>
    /// --- Day 2: Inventory Management System ---
    ///
    /// --- Part Two ---
    /// 
    /// Confident that your list of box IDs is complete, you're ready to find the
    /// boxes full of prototype fabric.
    /// 
    /// The boxes will have IDs which differ by exactly one character at the same
    /// position in both strings. For example, given the following box IDs:
    /// 
    /// - abcde
    /// - fghij
    /// - klmno
    /// - pqrst
    /// - fguij
    /// - axcye
    /// - wvxyz
    /// 
    /// The IDs abcde and axcye are close, but they differ by two characters (the
    /// second and fourth). However, the IDs fghij and fguij differ by exactly one
    /// character, the third (h and u). Those must be the correct boxes.
    /// 
    /// What letters are common between the two correct box IDs? (In the example
    /// above, this is found by removing the differing character from either ID,
    /// producing fgij.)
    /// </summary>
    [AdventOfCode(2018, 2, 2, "Inventory Management System - Part 2", "wrziyfdmlumeqvaatbiosngkc")]
    public class AdventOfCode2018022 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var boxIds = File.ReadAllLines("2018\\AdventOfCode201802.txt");

            for (var i = 0; i < boxIds.Length - 1; i++)
            {
                for (var j = i + 1; j < boxIds.Length; j++)
                {
                    var id1 = boxIds[i];
                    var id2 = boxIds[j];

                    var count = 0;
                    var pos = -1;
                    for (var k = 0; k < id1.Length && count < 2; k++)
                    {
                        if (id1[k] != id2[k])
                        {
                            count++;
                            pos = k;
                        }
                    }

                    if (count == 1)
                    {
                        Result = id1.Remove(pos, 1);
                        return;
                    }
                }
            }
        }
    }
}
