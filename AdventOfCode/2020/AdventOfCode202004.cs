using System.Collections.Generic;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using MoreLinq;

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
            var timer = new Stopwatch();
            timer.Start();

            var passports = new List<Passport>();
            var passport = new Dictionary<string, string>();
            for (var i = 0; i < Input.Length; i++)
            {
                var line = Input[i];
                if (!string.IsNullOrWhiteSpace(line))
                {
                    line.Split(' ')
                        .Select(f => f.Split(':'))
                        .ForEach(p => passport.Add(p[0], p[1]));
                }

                if (string.IsNullOrWhiteSpace(line) || i == Input.Length - 1)
                {
                    passports.Add(new Passport
                    {
                        BirthYear = passport.ContainsKey("byr") ? passport["byr"] : string.Empty,
                        IssueYear = passport.ContainsKey("iyr") ? passport["iyr"] : string.Empty,
                        ExpirationYear = passport.ContainsKey("eyr") ? passport["eyr"] : string.Empty,
                        Height = passport.ContainsKey("hgt") ? passport["hgt"] : string.Empty,
                        HairColor = passport.ContainsKey("hcl") ? passport["hcl"] : string.Empty,
                        EyeColor = passport.ContainsKey("ecl") ? passport["ecl"] : string.Empty,
                        PassportId = passport.ContainsKey("pid") ? passport["pid"] : string.Empty,
                        CountryId = passport.ContainsKey("cid") ? passport["cid"] : string.Empty
                    });

                    passport = new Dictionary<string, string>();

                    continue;
                }
            }

            ResultPart1 = passports.Count(p => p.IsValid());

            timer.Stop();
            TimePart1 = timer.ElapsedTicks.ToMilliseconds();

            timer.Start();
            ResultPart2 = passports.Count(p => p.IsValid(validateFields: true));
            timer.Stop();
            TimePart2 = timer.ElapsedTicks.ToMilliseconds();
        }
        
        
        private static readonly List<string> EyeColors = new() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        private struct Passport
        {
            public string BirthYear { get; init; }
            public string IssueYear { get; init; }
            public string ExpirationYear { get; init; }
            public string Height { get; init; }
            public string HairColor { get; init; }
            public string EyeColor { get; init; }
            public string PassportId { get; init; }
            public string CountryId { get; init; }

            public bool IsValid(bool validateFields = false)
            {
                if (string.IsNullOrWhiteSpace(BirthYear) ||
                    string.IsNullOrWhiteSpace(IssueYear) ||
                    string.IsNullOrWhiteSpace(ExpirationYear) ||
                    string.IsNullOrWhiteSpace(Height) ||
                    string.IsNullOrWhiteSpace(HairColor) ||
                    string.IsNullOrWhiteSpace(EyeColor) ||
                    string.IsNullOrWhiteSpace(PassportId)) return false;

                if (validateFields == false) return true;

                // Birth year: Number [1920, 2002]
                if (!int.TryParse(BirthYear, out var bYear)) return false;
                if (bYear < 1920 || bYear > 2002) return false;

                // Issue year: Number [2010, 2020]
                if (!int.TryParse(IssueYear, out var iYear)) return false;
                if (iYear < 2010 || iYear > 2020) return false;

                // Expiration year: Number [2020, 2030]
                if (!int.TryParse(ExpirationYear, out var eYear)) return false;
                if (eYear < 2020 || eYear > 2030) return false;

                // Height: If cm then number [150, 193]. If in then number [59, 76]
                if (Height.Length < 4) return false;
                var unit = Height.Substring(Height.Length - 2, 2);
                var value = Height.Substring(0, Height.Length - 2);
                if (!int.TryParse(value, out var height)) return false;
                if (unit != "cm" && unit != "in") return false;
                if (unit == "cm" && (height < 150 || height > 193)) return false;
                if (unit == "in" && (height < 59 || height > 76)) return false;

                // Hair color: # + 6 hexadecimal values.
                if (HairColor.Length != 7 || HairColor[0] != '#') return false;
                value = HairColor.Substring(1);
                if (!int.TryParse(value, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out var hair)) return false;

                // Eye color: Exactly one of [amb blu brn gry grn hzl oth]
                if (!EyeColors.Contains(EyeColor)) return false;

                // Passport Id: 9 digit number including leading zeroes.
                if (PassportId.Length != 9) return false;
                if (!int.TryParse(PassportId, out var passportid)) return false;

                return true;

            }
        }
    }
}
