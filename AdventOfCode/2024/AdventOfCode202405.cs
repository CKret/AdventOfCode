using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
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
            var pageOrders = new Dictionary<int, List<int>>();
            var printQueue = new List<List<int>>();
            var sum = 0;

            foreach (var line in Input)
            {
                if (line.Contains('|'))
                {
                    var pages = line.Split('|');
                    var x = int.Parse(pages[0]);
                    var y = int.Parse(pages[1]);

                    if (pageOrders.ContainsKey(x)) pageOrders[x].Add(y);
                    else pageOrders[x] = [y];
                }
                else if (line.Contains(','))
                {
                    printQueue.Add(line.Split(',').Select(int.Parse).ToList());
                }
            }

            foreach (var pq in printQueue)
            {
                var isCorrect = true;
                for (var i = 0; i < pq.Count; i++)
                {
                    if (!pageOrders.ContainsKey(pq[i])) continue;

                    for (var j = 0; j < pq.Count; j++)
                    {
                        if (i == j) continue;

                        if (j < i)
                        {
                            if (pageOrders[pq[i]].Contains(pq[j]))
                            {
                                isCorrect = false;
                                break;
                            }
                        }
                        else if (!pageOrders[pq[i]].Contains(pq[j]))
                        {
                            isCorrect = false;
                            break;
                        }
                    }
                    if (!isCorrect) break;
                }

                if (isCorrect) sum += pq[pq.Count / 2];
            }

            return sum;
        }

        protected override object SolvePart2()
        {
            var pageOrders = new Dictionary<int, List<int>>();
            var printQueue = new List<List<int>>();
            var sum = 0;

            foreach (var line in Input)
            {
                if (line.Contains('|'))
                {
                    var pages = line.Split('|');
                    var x = int.Parse(pages[0]);
                    var y = int.Parse(pages[1]);

                    if (pageOrders.ContainsKey(x)) pageOrders[x].Add(y);
                    else pageOrders[x] = [y];
                }
                else if (line.Contains(','))
                {
                    printQueue.Add(line.Split(',').Select(int.Parse).ToList());
                }
            }

            foreach (var pq in printQueue)
            {
                var isCorrect = true;
                for (var i = 0; i < pq.Count; i++)
                {
                    if (!pageOrders.ContainsKey(pq[i])) continue;

                    for (var j = 0; j < pq.Count; j++)
                    {
                        if (i == j) continue;

                        if (j < i)
                        {
                            if (pageOrders[pq[i]].Contains(pq[j]))
                            {
                                isCorrect = false;
                                break;
                            }
                        }
                        else if (!pageOrders[pq[i]].Contains(pq[j]))
                        {
                            isCorrect = false;
                            break;
                        }
                    }
                    if (!isCorrect) break;
                }

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
