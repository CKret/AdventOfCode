using System.Collections.Generic;
using AdventOfCode.Core;
using System.Linq;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 15, "Rambunctious Recitation", 929, 16671510)]
    public class AdventOfCode202015 : AdventOfCodeBase
    {
        public AdventOfCode202015(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var numbers = Input[0].Split(',').Select(int.Parse).ToArray();
            return GetNthSpokenNumber(2020, numbers);
        }

        protected override object SolvePart2()
        {
            var numbers = Input[0].Split(',').Select(int.Parse).ToArray();
            return GetNthSpokenNumber(30000000, numbers);
        }

        private int GetNthSpokenNumber(int n, IReadOnlyList<int> numbers)
        {
            var lastSpoken = new int[n];
            var turn = 0;

            foreach (var i in numbers)
                lastSpoken[i] = ++turn;

            var curr = numbers[^1];
            var next = 0;
            while (turn++ < n)
            {
                curr = next;
                if (lastSpoken[curr] == 0)
                    next = 0;
                else
                    next = turn - lastSpoken[curr];
                lastSpoken[curr] = turn;
            }

            return curr;
        }
    }
}
