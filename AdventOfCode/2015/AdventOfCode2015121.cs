﻿using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>

    /// </summary>
    [AdventOfCode(2015, 12, 1, "", 156366)]
    public class AdventOfCode2015121 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var input = File.ReadAllText("2015/AdventOfCode201512.txt");
            Result = Regex.Matches(input, @"[+-]?\d+").Cast<Match>().Select(m => int.Parse(m.Value)).ToArray().Sum();
        }
    }
}