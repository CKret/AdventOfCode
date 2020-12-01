using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2018
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2018, 19, 1, "", null)]
    public class AdventOfCode2018191 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllLines(@"2018\AdventOfCode201817.txt");
        }

        public AdventOfCode2018191(string sessionCookie) : base(sessionCookie) { }
    }
}
