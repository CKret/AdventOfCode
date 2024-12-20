using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2024
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2024, 5, "Print Queue", 3608, 4922)]
    public class AdventOfCode202405 : AdventOfCodeBase
    {
        public AdventOfCode202405(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var pageOrders = Input
                .Where(x => x.Contains('|'))
                .Select(x => x.Split('|').Select(int.Parse))
                .GroupBy(x => x.First())
                .ToDictionary(x => x.Key, x => x.Select(y => y.Last()).ToList());

            var printQueues = Input
                .Where(x => x.Contains(','))
                .Select(x => x.Split(',').Select(int.Parse).ToList());

            return printQueues
                .Where(pq => pq
                    .SelectMany((x, i) => pq
                        .Where((y, j) => i != j && ((j < i && pageOrders[x].Contains(y)) || (j > i && !pageOrders[x].Contains(y)))))
                    .Any() == false)
                .Sum(pq => pq[pq.Count / 2]);
        }

        protected override object SolvePart2()
        {
            var pageOrders = Input
                .Where(x => x.Contains('|'))
                .Select(x => x.Split('|').Select(int.Parse))
                .GroupBy(x => x.First())
                .ToDictionary(x => x.Key, x => x.Select(y => y.Last()).ToList());

            var printQueues = Input
                .Where(x => x.Contains(','))
                .Select(x => x.Split(',').Select(int.Parse).ToList());

            var sum = 0;
            foreach (var pq in printQueues)
            {
                var isCorrect = !pq
                    .SelectMany((x, i) => pq
                        .Where((y, j) => i != j && ((j < i && pageOrders[pq[i]].Contains(pq[j])) || (j > i && !pageOrders[pq[i]].Contains(pq[j])))))
                    .Any();

                if (!isCorrect)
                {
                    var newOrder = new List<int>(pq);
                    for (var i = 0; i < pq.Count; i++)
                    {
                        if (!pageOrders.ContainsKey(newOrder[i])) continue;

                        for (var j = 0; j < newOrder.Count; j++)
                        {
                            if (i == j) continue;

                            if (j < i)
                            {
                                if (pageOrders[newOrder[i]].Contains(newOrder[j]))
                                {
                                    (newOrder[i], newOrder[j]) = (newOrder[j], newOrder[i]);
                                    continue;
                                }
                            }
                            else if (pageOrders.ContainsKey(newOrder[i]) && !pageOrders[newOrder[i]].Contains(newOrder[j]))
                            {
                                (newOrder[i], newOrder[j]) = (newOrder[j], newOrder[i]);
                                continue;
                            }
                        }
                    }

                    sum += newOrder[newOrder.Count / 2];
                }
            }

            return sum;
        }
    }
}
