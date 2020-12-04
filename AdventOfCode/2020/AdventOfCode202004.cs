using System.Collections.Generic;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 4, "Day 4: Passport Processing", 204, 179)]
    public class AdventOfCode202004 : AdventOfCodeBase
    {
        public AdventOfCode202004(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            var passports = string.Join(' ', Input).Split("  ").Select(ParseValues).ToArray();

            var timer = Stopwatch.StartNew();
            ResultPart1 = passports.Count(ValidField);
            timer.Stop();
            TimePart1 = timer.ElapsedTicks.ToMilliseconds();

            timer.Start();
            ResultPart2 = passports.Where(ValidField).Count(ValidValues);
            timer.Stop();
            TimePart2 = timer.ElapsedTicks.ToMilliseconds();
        }
        
        bool ValidField(IEnumerable<string> values) => new[] { "byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:" }.All(field => values.Any(x => x.StartsWith(field)));

        bool ValidValue(string value) =>
            new Regex(value.Substring(0, 3) switch
            {
                "byr" => "^byr:(19[2-9][0-9]|200[0-2])$",
                "iyr" => "^iyr:(201[0-9]|2020)$",
                "eyr" => "^eyr:(202[0-9]|2030)$",
                "hgt" => "^hgt:(1([5-8][0-9]|9[0-3])cm|(59|6[0-9]|7[0-6])in)$",
                "hcl" => "^hcl:#[a-f0-9]{6}$",
                "ecl" => "^ecl:(amb|blu|brn|gry|grn|hzl|oth)$",
                "pid" => "^pid:[0-9]{9}$",
                "cid" => "^cid:",
                _ => "$^",
            }).IsMatch(value);

        bool ValidValues(IEnumerable<string> values) => values.All(ValidValue);

        IEnumerable<string> ParseValues(string input) => input.Split(' ');
    }
}
