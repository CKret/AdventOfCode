using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 2, "Password Philosophy", 582L, 729L)]
    public class AdventOfCode202002 : AdventOfCodeBase
    {
        public AdventOfCode202002(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var validPasswords1 = 0;
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

            }

            return validPasswords1;
        }

        protected override object SolvePart2()
        {
            var validPasswords2 = 0;
            foreach (var line in Input)
            {
                var data = line.Split(' ', '-');
                var min = int.Parse(data[0]);
                var max = int.Parse(data[1]);
                var chr = data[2][0];
                var pass = data[3];

                var count = pass.ToCharArray().Count(x => x == chr);
                if (pass[min - 1] != pass[max - 1] && (pass[min - 1] == chr || pass[max - 1] == chr))
                    validPasswords2++;

            }

            return validPasswords2;
        }
    }
}
