using System.Globalization;
using System.IO;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2018
{
    /// <summary>
    /// --- Day 5: Alchemical Reduction ---
    ///
    /// --- Part Two ---
    /// 
    /// Time to improve the polymer.
    /// 
    /// One of the unit types is causing problems; it's preventing the polymer from
    /// collapsing as much as it should. Your goal is to figure out which unit type
    /// is causing the most problems, remove all instances of it (regardless of
    /// polarity), fully react the remaining polymer, and measure its length.
    /// 
    /// For example, again using the polymer dabAcCaCBAcCcaDA from above:
    /// 
    /// - Removing all A/a units produces dbcCCBcCcD. Fully reacting this
    ///   polymer produces dbCBcD, which has length 6.
    /// - Removing all B/b units produces daAcCaCAcCcaDA. Fully reacting this
    ///   polymer produces daCAcaDA, which has length 8.
    /// - Removing all C/c units produces dabAaBAaDA. Fully reacting this
    ///   polymer produces daDA, which has length 4.
    /// - Removing all D/d units produces abAcCaCBAcCcaA. Fully reacting this
    ///   polymer produces abCBAc, which has length 6.
    /// 
    /// In this example, removing all C/c units was best, producing the answer 4.
    /// 
    /// What is the length of the shortest polymer you can produce by removing all
    /// units of exactly one type and fully reacting the result?
    /// </summary>
    [AdventOfCode(2018, 5, "Alchemical Reduction - Part 2", 6650)]
    public class AdventOfCode2018052 : AdventOfCodeBase
    {
        private readonly char[] letters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        public override void Solve()
        {
            var data = File.ReadAllText(@"2018\AdventOfCode201805.txt");
            var shortest = int.MaxValue;

            foreach (var c in letters)
            {
                var current = data.ReplaceAll(new [] {c.ToString(CultureInfo.InvariantCulture), c.ToString(CultureInfo.InvariantCulture).ToUpperInvariant() }, "");
                current = React(current);

                if (current.Length < shortest)
                {
                    shortest = current.Length;
                }
            }

            Result = shortest;
        }

        private string React(string data)
        {
            for (var i = 0; i < data.Length - 1; i++)
            {
                if (data[i] == data[i + 1] + 0x20 || data[i] + 0x20 == data[i + 1])
                {
                    data = data.Remove(i, 2);
                    i = i == 0 ? -1 : i - 2;
                }
            }

            return data;
        }

        public AdventOfCode2018052(string sessionCookie) : base(sessionCookie) { }
    }
}
