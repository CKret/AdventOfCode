using System.IO;
using System.Linq;
using AdventOfCode.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 12, 2, "", 96852)]
    public class AdventOfCode2015122 : AdventOfCodeBase
    {
        public override void Solve()
        {
            dynamic input = File.ReadAllText("2015/AdventOfCode201512.txt");
            var json = JsonConvert.DeserializeObject(input);
            Result = (int) GetSum(json, "red");

        }

        internal long GetSum(JObject json, string avoid = null)
        {
            var shouldAvoid = json.Properties()
                .Select(a => a.Value).OfType<JValue>()
                .Select(v => v.Value).Contains(avoid);

            return shouldAvoid ? 0 : json.Properties().Sum((dynamic a) => (long) GetSum(a.Value, avoid));
        }

        internal long GetSum(JArray arr, string avoid) => arr.Sum((dynamic a) => (long) GetSum(a, avoid));

        internal long GetSum(JValue val, string avoid) => val.Type == JTokenType.Integer ? (long) val.Value : 0;
    }
}
