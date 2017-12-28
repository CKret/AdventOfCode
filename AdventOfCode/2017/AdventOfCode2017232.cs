using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.Mathematics;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 23: Coprocessor Conflagration ---
    /// 
    /// --- Part Two ---
    /// 
    /// Now, it's time to fix the problem.
    /// 
    /// The debug mode switch is wired directly to register a. You flip the switch,
    /// which makes register a now start at 1 when the program is executed.
    /// 
    /// Immediately, the coprocessor begins to overheat. Whoever wrote this program
    /// obviously didn't choose a very efficient implementation. You'll need to
    /// optimize the program if it has any hope of completing before Santa needs
    /// that printer working.
    /// 
    /// The coprocessor's ultimate goal is to determine the final value left in
    /// register h once the program completes. Technically, if it had that... it
    /// wouldn't even need to run the program.
    /// 
    /// After setting register a to 1, if the program were to run to completion,
    /// what value would be left in register h?
    /// 
    /// </summary>
    [AdventOfCode(2017, 23, 2, "Coprocessor Conflagration - Part Two", 909)]
    public class AdventOfCode2017232 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var count = 0;
            // 81 * 100 + 100000 = 108100
            // 108100 + 17000 = 125100
            Result = Enumerable.Range(108100, 17000).Where(x => (x + 1) % 17 == 0).Sum(x => x.IsPrime() ? 0 : 1);
        }
    }
}
