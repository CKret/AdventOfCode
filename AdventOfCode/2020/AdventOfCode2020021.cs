using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 2, 1, "", 582)]
    public class AdventOfCode2020021 : AdventOfCodeBase
    {
        public AdventOfCode2020021(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            var validPasswords = 0;
            foreach (var line in Input)
            {
                var data = line.Split(' ', '-');
                var min = int.Parse(data[0]);
                var max = int.Parse(data[1]);
                var chr = data[2][0];
                var pass = data[3];

                var count = pass.ToCharArray().Count(x => x == chr);
                if (count >= min && count <= max)
                    validPasswords++;
            }

            Result = validPasswords;
        }
    }
}
