using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2021, 3, "Binary Diagnostic", 1092896, 4672151)]
    public class AdventOfCode202103 : AdventOfCodeBase
    {
        public AdventOfCode202103(string sessionCookie) : base(sessionCookie) { }

        protected override async Task<object> SolvePart1(CancellationToken cancellationToken)
        {
            var gamma = string.Join("", Enumerable.Range(0, Input[0].Length).Select(x => Input.Select(s => s[x]).Count(c => c == '1')).Select(x => x > 500 ? '1' : '0'));
            var epsilon = string.Join("", Enumerable.Range(0, Input[0].Length).Select(x => Input.Select(s => s[x]).Count(c => c == '1')).Select(x => x < 500 ? '1' : '0'));

            return Convert.ToInt64(gamma, 2) * Convert.ToInt64(epsilon, 2);
        }

        protected override async Task<object> SolvePart2(CancellationToken cancellationToken)
        {
            var oxygenNumbers = new List<string>(Input);
            var co2Numbers = new List<string>(Input);

            for (var n = 0; n < oxygenNumbers[0].Length; n++)
            {
                var ones = oxygenNumbers.Select(x => x[n]).Count(x => x == '1');
                var mostCommon = ones >= oxygenNumbers.Count - ones ? '1' : '0';
                oxygenNumbers = oxygenNumbers.Where(x => x[n] == mostCommon).ToList();
                if (oxygenNumbers.Count == 1) break;
            }

            for (var n = 0; n < co2Numbers[0].Length; n++)
            {
                var ones = co2Numbers.Select(x => x[n]).Count(x => x == '1');
                var leastCommon = co2Numbers.Count - ones <= ones ? '0' : '1';
                co2Numbers = co2Numbers.Where(x => x[n] == leastCommon).ToList();
                if (co2Numbers.Count == 1) break;
            }

            return Convert.ToInt64(oxygenNumbers.Single(), 2) * Convert.ToInt64(co2Numbers.Single(), 2);
        }
    }
}
