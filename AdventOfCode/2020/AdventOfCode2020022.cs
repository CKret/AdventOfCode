﻿using AdventOfCode.Core;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 2, 2, "", 729)]
    public class AdventOfCode2020022 : AdventOfCodeBase
    {
        public AdventOfCode2020022(string sessionCookie) : base(sessionCookie) { }
        public override void Solve()
        {
            var validPasswords = 0;
            foreach (var line in Input)
            {
                var data = line.Split(' ', '-');
                var p1 = int.Parse(data[0]) - 1;
                var p2 = int.Parse(data[1]) - 1;
                var chr = data[2][0];
                var pass = data[3];

                if (pass[p1] != pass[p2] && (pass[p1] == chr || pass[p2] == chr))
                    validPasswords++;
            }

            Result = validPasswords;
        }
    }
}
