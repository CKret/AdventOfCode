using System.Collections.Generic;
using AdventOfCode.Core;
using System.Linq;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 19, "", null, null)]
    public class AdventOfCode202019 : AdventOfCodeBase
    {
        public AdventOfCode202019(string sessionCookie) : base(sessionCookie) { }
 
        protected override object SolvePart1()
        {
            var data = Input;

            var rules = data.TakeWhile(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            var messages = data.SkipWhile(x => !string.IsNullOrWhiteSpace(x)).Skip(1).ToArray();

            var re = new RulesEngine(rules);

            foreach (var message in messages)
            {
                var a = RulesEngine.ValidateMessage(message);
            }



            return null;
        }

        protected override object SolvePart2()
        {
            return null;
        }
    }

    public class RulesEngine
    {
        private readonly Dictionary<int, string> atomicRules = new();
        private readonly Dictionary<int, List<List<int>>> rules = new();

        public RulesEngine(string[] data)
        {
            foreach (var r in data)
            {
                if (string.IsNullOrWhiteSpace(r)) break;

                var ruleNr = int.Parse(r.Split(": ")[0]);
                var rule = r.Split(": ")[1];

                if (rule.StartsWith("\""))
                {
                    atomicRules.Add(ruleNr, rule.Replace("\"", ""));
                }
                else
                {
                    rules.Add(ruleNr, new List<List<int>>());
                    foreach (var rp in rule.Split(" | "))
                    {
                        var ruleList = new List<int>();
                        foreach (var a in rp.Split(" "))
                        {
                            ruleList.Add(int.Parse(a));
                        }
                        rules[ruleNr].Add(ruleList);
                    }
                }
            }
        }

        public static bool ValidateMessage(string message)
        {
            return false;
        }

    }
}
