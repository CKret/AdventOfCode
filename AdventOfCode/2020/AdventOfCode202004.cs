using System.Collections.Generic;
using AdventOfCode.Core;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 4, "Passport Processing", 204, 179)]
    public class AdventOfCode202004 : AdventOfCodeBase
    {
        private string[][] passports;

        public AdventOfCode202004(string sessionCookie) : base(sessionCookie) { }

        public override void Solve()
        {
            passports = string.Join(' ', Input).Split("  ").Select(p => p.Split(' ')).ToArray();

            base.Solve();
        }

        protected override object SolvePart1()
        {
            return passports.Count(ValidFields);
        }

        protected override object SolvePart2()
        {
            return passports.Where(ValidFields).Count(ValidValues);
        }
        
        private static bool ValidFields(IEnumerable<string> fields) => new[] { "byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:" }.All(field => fields.Any(p => p.StartsWith(field)));

        private static bool ValidValue(string value) =>
            new Regex(value.Substring(0, 4) switch
            {
                "byr:" => "^byr:(19[2-9][0-9]|200[0-2])$",
                "iyr:" => "^iyr:(201[0-9]|2020)$",
                "eyr:" => "^eyr:(202[0-9]|2030)$",
                "hgt:" => "^hgt:(1([5-8][0-9]|9[0-3])cm|(59|6[0-9]|7[0-6])in)$",
                "hcl:" => "^hcl:#[a-f0-9]{6}$",
                "ecl:" => "^ecl:(amb|blu|brn|gry|grn|hzl|oth)$",
                "pid:" => "^pid:[0-9]{9}$",
                "cid:" => "^cid:",
                _ => "$^",
            }).IsMatch(value);

        private static bool ValidValues(IEnumerable<string> values) => values.All(ValidValue);
    }
}
