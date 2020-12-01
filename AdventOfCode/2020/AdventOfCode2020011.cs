using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 1, 1, "", 1016964L)]
    public class AdventOfCode2020011 : AdventOfCodeBase
    {
        public AdventOfCode2020011(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            var data = Input.Select(int.Parse).ToArray();

            foreach (var a in data)
            {
                if (data.Contains(2020 - a))
                {
                    Result = a * (2020 - a);
                    return;
                }
            }
        }
    }
}
