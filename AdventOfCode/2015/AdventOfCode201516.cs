﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 16, "", 40, 241)]
    public class AdventOfCode201516 : AdventOfCodeBase
    {
        public AdventOfCode201516(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var sues = Input
                .Select(s => s.Split())
                .Select(i => new
                {
                    Id = int.Parse(i[1].Trim(':'), CultureInfo.InvariantCulture),
                    Items = new Dictionary<string, int>
                    {
                        { i[2].Trim(':'), int.Parse(i[3].Trim(','), CultureInfo.InvariantCulture) },
                        { i[4].Trim(':'), int.Parse(i[5].Trim(','), CultureInfo.InvariantCulture) },
                        { i[6].Trim(':'), int.Parse(i[7].Trim(','), CultureInfo.InvariantCulture) }
                    }
                });

            var notSues = sues.Where(s => s.Items.ContainsKey("children") && s.Items["children"] != 3 ||
                                          s.Items.ContainsKey("cats") && s.Items["cats"] != 7 ||
                                          s.Items.ContainsKey("samoyeds") && s.Items["samoyeds"] != 2 ||
                                          s.Items.ContainsKey("pomeranians") && s.Items["pomeranians"] != 3 ||
                                          s.Items.ContainsKey("akitas") && s.Items["akitas"] != 0 ||
                                          s.Items.ContainsKey("vizslas") && s.Items["vizslas"] != 0 ||
                                          s.Items.ContainsKey("goldfish") && s.Items["goldfish"] != 5 ||
                                          s.Items.ContainsKey("trees") && s.Items["trees"] != 3 ||
                                          s.Items.ContainsKey("cars") && s.Items["cars"] != 2 ||
                                          s.Items.ContainsKey("perfumes") && s.Items["perfumes"] != 1)
                .Select(s => s.Id);

            var sue = sues.Single(s => !notSues.Contains(s.Id));

            return sue.Id;
        }

        protected override object SolvePart2()
        {
            var sues = Input
                .Select(s => s.Split())
                .Select(i => new
                {
                    Id = int.Parse(i[1].Trim(':'), CultureInfo.InvariantCulture),
                    Items = new Dictionary<string, int>
                    {
                        { i[2].Trim(':'), int.Parse(i[3].Trim(','), CultureInfo.InvariantCulture) },
                        { i[4].Trim(':'), int.Parse(i[5].Trim(','), CultureInfo.InvariantCulture) },
                        { i[6].Trim(':'), int.Parse(i[7].Trim(','), CultureInfo.InvariantCulture) }
                    }
                });

            var notSues = sues.Where(s => s.Items.ContainsKey("children") && s.Items["children"] != 3 ||
                                          s.Items.ContainsKey("cats") && s.Items["cats"] <= 7 ||
                                          s.Items.ContainsKey("samoyeds") && s.Items["samoyeds"] != 2 ||
                                          s.Items.ContainsKey("pomeranians") && s.Items["pomeranians"] >= 3 ||
                                          s.Items.ContainsKey("akitas") && s.Items["akitas"] != 0 ||
                                          s.Items.ContainsKey("vizslas") && s.Items["vizslas"] != 0 ||
                                          s.Items.ContainsKey("goldfish") && s.Items["goldfish"] >= 5 ||
                                          s.Items.ContainsKey("trees") && s.Items["trees"] <= 3 ||
                                          s.Items.ContainsKey("cars") && s.Items["cars"] != 2 ||
                                          s.Items.ContainsKey("perfumes") && s.Items["perfumes"] != 1)
                .Select(s => s.Id);

            var sue = sues.Single(s => !notSues.Contains(s.Id));

            return sue.Id;
        }
    }
}
