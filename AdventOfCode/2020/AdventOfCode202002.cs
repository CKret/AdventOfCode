using System.Diagnostics;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 2, "Day 2: Password Philosophy", 582L, 729L)]
    public class AdventOfCode202002 : AdventOfCodeBase
    {
        public AdventOfCode202002(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            var timer = new Stopwatch();
            timer.Start();

            var validPasswords1 = 0;
            var validPasswords2 = 0;
            foreach (var line in Input)
            {
                var data = line.Split(' ', '-');
                var min = int.Parse(data[0]);
                var max = int.Parse(data[1]);
                var chr = data[2][0];
                var pass = data[3];

                var count = pass.ToCharArray().Count(x => x == chr);
                if (count >= min && count <= max)
                    validPasswords1++;
                if (pass[min - 1] != pass[max - 1] && (pass[min - 1] == chr || pass[max - 1] == chr))
                    validPasswords2++;

            }
            timer.Stop();
            TimePart1 = timer.ElapsedTicks.ToMilliseconds();

            ResultPart1 = validPasswords1;
            ResultPart2 = validPasswords2;
        }
    }
}
