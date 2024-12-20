using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2024
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2024, 3, "Mull It Over", 188192787, 113965544)]
    public class AdventOfCode202403 : AdventOfCodeBase
    {
        public AdventOfCode202403(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            return Regex.Matches(string.Join("", Input), @"mul\(\d+,\d+\)")
                .Select(m => m.Value.Replace("mul(", "").Replace(")", "").Split(","))
                .Select(m => int.Parse(m[0]) * int.Parse(m[1]))
                .Sum();
        }

        protected override object SolvePart2()
        {
            var input = string.Join("", Input);

            var muls = Regex.Matches(input, @"mul\(\d+,\d+\)")
                .Select(m => new { m.Index, Values = m.Value.Replace("mul(", "").Replace(")", "").Split(",") })
                .Select(m => new { m.Index, Product = int.Parse(m.Values[0]) * int.Parse(m.Values[1]) })
                .ToList();

            var dos = Regex.Matches(input, @"do\(\)")
                .Select(m => m.Index)
                .ToList();

            var donts = Regex.Matches(input, @"don't\(\)")
                .Select(m => m.Index)
                .ToList();

            var sum = 0;
            var isEnabled = true;
            while (muls.Count > 0)
            {
                if (isEnabled)
                {
                    while (dos.Count > 0 && donts.Count > 0 && dos.First() < donts.First()) dos.RemoveAt(0);
                    while (muls.Count > 0 && (donts.Count == 0 || muls.First().Index < donts.First()))
                    {
                        sum += muls.First().Product;
                        muls.RemoveAt(0);
                    }

                    isEnabled = false;
                    if (donts.Count > 0) donts.RemoveAt(0);
                }
                else
                {
                    while (muls.Count > 0 && dos.Count > 0 && muls.First().Index < dos.First()) muls.RemoveAt(0);
                    while (donts.Count > 0 && dos.Count > 0 && donts.First() < dos.First()) donts.RemoveAt(0);

                    isEnabled = true;
                    dos.RemoveAt(0);
                }
            }

            return sum;
        }
    }
}
