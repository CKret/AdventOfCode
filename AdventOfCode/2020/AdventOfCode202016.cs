using System;
using System.Collections.Generic;
using AdventOfCode.Core;
using System.Linq;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 16, "Ticket Translation", 19240L, 21095351239483L)]
    public class AdventOfCode202016 : AdventOfCodeBase
    {
        public AdventOfCode202016(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var (rules, myTicket, nearbyTickets) = ParseInput(Input);

            return (from ticket in nearbyTickets
                    from val in ticket
                    where rules.All(r => !r.Value.Contains(val))
                    select val).Sum();
        }

        protected override object SolvePart2()
        {
            var (rules, myTicket, nearbyTickets) = ParseInput(Input);

            var validTickets = nearbyTickets.Where(ticket => ticket.All(val => rules.Any(r => r.Value.Contains(val)))).ToList();
            validTickets.Add(myTicket);

            var possiblesRules = rules.Keys.ToDictionary(key => key, _ => new List<int>());

            foreach (var i in Enumerable.Range(0, rules.Count))
            {
                var column = validTickets.Select(t => t[i]);

                foreach (var rule in rules.Where(rule => column.All(n => rule.Value.Contains(n))))
                {
                    possiblesRules[rule.Key].Add(i);
                }
            }

            while (possiblesRules.Select(r => r.Value).Any(l => l.Count() > 1))
            {
                foreach (var rule in possiblesRules.Where(r => r.Value.Count() == 1))
                {
                    possiblesRules.Where(kv => kv.Value.Contains(rule.Value.Single()) && kv.Value.Count() > 1)
                                  .Select(r => r.Key)
                                  .ToList()
                                  .ForEach(key => possiblesRules.Where(r => Equals(key, r.Key))
                                                                .Select(r => r.Value)
                                                                .ToList()
                                                                .ForEach(l => l.Remove(rule.Value.Single())));
                }
            }

            var result = 1L;
            possiblesRules.Where(r => r.Key.StartsWith("departure"))
                          .SelectMany(r => r.Value.Select(n => n))
                          .ToList()
                          .ForEach(n => result *= myTicket[n]);

            return result;
        }

        private (Dictionary<string, int[]> rules, int[] yourTicket, int[][] nearbyTickets) ParseInput(string[] data)
        {
            var rules = new Dictionary<string, int[]>();

            var index = 0;
            while (!string.IsNullOrWhiteSpace(data[index]))
            {
                var ruleName = data[index].Split(": ")[0];
                var ruleValues = data[index].Split(": ")[1]
                                            .Split(new[] { "-", " or " }, StringSplitOptions.RemoveEmptyEntries)
                                            .Select(int.Parse)
                                            .ToArray();

                var ruleRanges = Enumerable.Range(ruleValues[0], ruleValues[1] - ruleValues[0] + 1)
                                           .Concat(Enumerable.Range(ruleValues[2], ruleValues[3] - ruleValues[2] + 1))
                                           .ToArray();

                rules.Add(ruleName, ruleRanges);
                index++;
            }

            index += 2;

            var yourTicket = data[index].Split(',').Select(int.Parse).ToArray();

            index += 3;

            var nearbyTickets = data.Skip(index)
                                    .Select(t => t.Split(',')
                                                  .Select(int.Parse)
                                                  .ToArray())
                                    .ToArray();
            return (rules, yourTicket, nearbyTickets);
        }
    }
}
