using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 17: Spinlock ---
    /// 
    /// --- Part Two ---
    /// 
    /// The spinlock does not short-circuit. Instead, it gets more angry. At least,
    /// you assume that's what happened; it's spinning significantly faster than it
    /// was a moment ago.
    /// 
    /// You have good news and bad news.
    /// 
    /// The good news is that you have improved calculations for how to stop the
    /// spinlock. They indicate that you actually need to identify the value after
    /// 0 in the current state of the circular buffer.
    /// 
    /// The bad news is that while you were determining this, the spinlock has just
    /// finished inserting its fifty millionth value (50000000).
    /// 
    /// What is the value after 0 the moment 50000000 is inserted?
    /// 
    /// </summary>
    [AdventOfCode(2017, 17, 2, "Spinlock - Part Two", 39289581)]
    public class AdventOfCode2017172 : AdventOfCodeBase
    {
        public override void Solve()
        {
            const int input = 376;

            var pos = 0;
            for (var i = 1; i <= 50000000; i++)
            {
                pos = (pos + input) % i + 1;
                if (pos == 1) Result = i;
            }
        }
    }
}
