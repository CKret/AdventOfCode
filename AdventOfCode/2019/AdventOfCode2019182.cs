using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 18, "", null)]
    public class AdventOfCode2019182 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllLines(@"2019\AdventOfCode201918.txt");
        }

        public AdventOfCode2019182(string sessionCookie) : base(sessionCookie) { }
    }
}
