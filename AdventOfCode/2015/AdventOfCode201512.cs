using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Core;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 1, "", 156366, 96852)]
    public class AdventOfCode201512 : AdventOfCodeBase
    {

        protected override object SolvePart1()
        {
            var input = Input[0];
            return Regex.Matches(input, @"[+-]?\d+").Cast<Match>().Select(m => int.Parse(m.Value, CultureInfo.InvariantCulture)).ToArray().Sum();
        }

        protected override object SolvePart2()
        {
            dynamic input = Input[0];
            var json = JsonConvert.DeserializeObject(input);
            return GetSum(json, "red");

        }

        internal long GetSum(JObject json, string avoid = null)
        {
            var shouldAvoid = json.Properties()
                .Select(a => a.Value).OfType<JValue>()
                .Select(v => v.Value).Contains(avoid);

            return shouldAvoid ? 0 : json.Properties().Sum((dynamic a) => (long)GetSum(a.Value, avoid));
        }

        internal long GetSum(JArray arr, string avoid) => arr.Sum((dynamic a) => (long)GetSum(a, avoid));

        internal static long GetSum(JValue val, string avoid) => val.Type == JTokenType.Integer ? (long)val.Value : 0;

        public AdventOfCode201512(string sessionCookie) : base(sessionCookie) { }
    }
}
