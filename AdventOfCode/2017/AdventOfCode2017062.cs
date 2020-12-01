using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 6: Memory Reallocation ---
    /// 
    /// --- Part Two ---
    /// 
    /// Out of curiosity, the debugger would also like to know the size of the
    /// loop: starting from a state that has already been seen, how many block
    /// redistribution cycles must be performed before that same state is seen
    /// again?
    /// 
    /// In the example above, 2 4 1 2 is seen again after four cycles, and so the
    /// answer in that example would be 4.
    /// 
    /// How many cycles are in the infinite loop that arises from the configuration
    /// in your puzzle input?
    /// 
    /// </summary>
    [AdventOfCode(2017, 6, 2, "Memory Reallocation - Part Two", 2392)]
    public class AdventOfCode2017062 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var mem = new List<int> { 4, 1, 15, 12, 0, 9, 9, 5, 5, 8, 7, 3, 14, 5, 12, 3 };
            var prev = new List<string> { string.Join(",", mem) };

            string memString;
            while (true)
            {
                var index = mem.IndexOf(mem.Max());
                var banks = mem[index];
                mem[index] = 0;
                while (banks > 0)
                {
                    index++;
                    mem[index % mem.Count] += 1;
                    banks--;
                }

                memString = string.Join(",", mem);
                if (prev.Contains(memString))
                    break;

                prev.Add(memString);
            }

            Result = prev.Count - prev.IndexOf(memString);
        }

        public AdventOfCode2017062(string sessionCookie) : base(sessionCookie) { }
    }
}
