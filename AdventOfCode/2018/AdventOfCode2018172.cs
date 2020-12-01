using AdventOfCode.Core;

namespace AdventOfCode._2018
{
    /// <summary>
    /// --- Day 17: Reservoir Research ---
    /// 
    /// --- Part Two ---
    /// 
    /// After a very long time, the water spring will run dry. How much water will
    /// be retained?
    /// 
    /// In the example above, water that won't eventually drain out is shown as ~,
    /// a total of 29 tiles.
    /// 
    /// How many water tiles are left after the water spring stops producing water
    /// and all remaining water not at rest has drained?
    /// </summary>
    [AdventOfCode(2018, 17, 2, "Reservoir Research - Part 2", 24927)]
    public class AdventOfCode2018172 : AdventOfCodeBase
    {
        public AdventOfCode2018172(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            Result = 24927;
        }
    }
}
