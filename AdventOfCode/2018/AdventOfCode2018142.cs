using System.Collections.Generic;
using AdventOfCode.Core;

namespace AdventOfCode._2018
{
    /// <summary>
    /// --- Day 14: Chocolate Charts ---
    ///
    /// --- Part Two ---
    /// 
    /// As it turns out, you got the Elves' plan backwards. They actually want to
    /// know how many recipes appear on the scoreboard to the left of the first
    /// recipes whose scores are the digits from your puzzle input.
    /// 
    /// - 51589 first appears after 9 recipes.
    /// - 01245 first appears after 5 recipes.
    /// - 92510 first appears after 18 recipes.
    /// - 59414 first appears after 2018 recipes.
    /// 
    /// How many recipes appear on the scoreboard to the left of the score sequence
    /// in your puzzle input?
    /// </summary>
    [AdventOfCode(2018, 14, 2, "Chocolate Charts - Part 2", 20174745)]
    public class AdventOfCode2018142 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var nRecipes = new[] { 6, 4, 0, 4, 4, 1 };
            var elf1 = 0;
            var elf2 = 1;

            var recipes = new List<int> { 3, 7 };
            var index = 0;
            var pos = 0;

            while (true)
            {
                var elf1Score = recipes[elf1];
                var elf2Score = recipes[elf2];
                var sum = elf1Score + elf2Score;
                if (sum >= 10)
                    recipes.Add(1);
                recipes.Add(sum % 10);

                elf1 = (elf1 + 1 + elf1Score) % recipes.Count;
                elf2 = (elf2 + 1 + elf2Score) % recipes.Count;

                while (index + pos < recipes.Count)
                {
                    if (nRecipes[pos] == recipes[index + pos])
                    {
                        if (pos == nRecipes.Length - 1)
                        {
                            Result = index;
                            return;
                        }
                        pos++;
                    }
                    else
                    {
                        pos = 0;
                        index++;
                    }
                }
            }
        }

        public AdventOfCode2018142(string sessionCookie) : base(sessionCookie) { }
    }
}
