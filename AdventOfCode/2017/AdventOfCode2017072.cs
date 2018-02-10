using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// --- Part Two ---
    /// 
    /// The programs explain the situation: they can't get down. Rather, they could
    /// get down, if they weren't expending all of their energy trying to keep the
    /// tower balanced. Apparently, one program has the wrong weight, and until
    /// it's fixed, they're stuck here.
    /// 
    /// For any program holding a disc, each program standing on that disc forms a
    /// sub-tower. Each of those sub-towers are supposed to be the same weight, or
    /// the disc itself isn't balanced. The weight of a tower is the sum of the
    /// weights of the programs in that tower.
    /// 
    /// In the example above, this means that for ugml's disc to be balanced, gyxo,
    /// ebii, and jptl must all have the same weight, and they do: 61.
    /// 
    /// However, for tknk to be balanced, each of the programs standing on its disc
    /// and all programs above it must each match. This means that the following
    /// sums must all be the same:
    /// 
    ///     ugml + (gyxo + ebii + jptl) = 68 + (61 + 61 + 61) = 251
    ///     padx + (pbga + havc + qoyq) = 45 + (66 + 66 + 66) = 243
    ///     fwft + (ktlj + cntj + xhth) = 72 + (57 + 57 + 57) = 243
    /// 
    /// As you can see, tknk's disc is unbalanced: ugml's stack is heavier than the
    /// other two. Even though the nodes above ugml are balanced, ugml itself is
    /// too heavy: it needs to be 8 units lighter for its stack to weigh 243 and
    /// keep the towers balanced. If this change were made, its weight would be 60.
    /// 
    /// Given that exactly one program is the wrong weight, what would its weight
    /// need to be to balance the entire tower?
    /// 
    /// </summary>
    [AdventOfCode(2017, 7, 2, "Recursive Circus - Part Two", 2310)]
    public class AdventOfCode2017072 : AdventOfCodeBase
    {
        public override void Solve()
        {
            // Create all nodes.
            var nodes = new List<Node>();
            foreach (var line in File.ReadAllLines("2017\\AdventOfCode201707.txt"))
            {
                var splits = line.Split(new[] { "->" }, StringSplitOptions.None);

                var node = new Node
                {
                    Name = splits[0].Split()[0],
                    Weight = int.Parse(splits[0].Split()[1].Trim().Trim('(', ')'))
                };

                if (splits.Length == 2)
                {
                    node.ChildNodes.AddRange(splits[1].Trim().Split().Select(c => c.Trim(',')).ToList());
                }

                nodes.Add(node);
            }

            // Build tree
            foreach (var node in nodes)
            {
                foreach (var child in node.ChildNodes)
                {
                    node.Children.Add(nodes.Single(n => n.Name == child));
                }
                node.Children.ForEach(n => n.Parent = node);
            }

            // Get root node (the only one that isn't a child to another node.
            var children = nodes.Where(n => n.ChildNodes.Count > 0).SelectMany(n => n.ChildNodes);
            var rootNode = nodes.Single(n => !children.Contains(n.Name));

            // Traverse tree to find the faulty weight.
            var currentNode = rootNode;
            var lastDiff = 0;
            while (currentNode != null)
            {
                var childWeights = currentNode.GetChildWeights();

                var diff = childWeights.Distinct().Max() - childWeights.Distinct().Min();

                if (diff == 0)
                {
                    Result = currentNode.Weight - lastDiff;
                    break;
                }

                lastDiff = diff;
                var unique = childWeights.Single(n => childWeights.Count(m => m == n) == 1);
                currentNode = currentNode.Children.Single(n => n.GetTowerWeight() == unique);
            }
        }

        public class Node
        {
            public string Name { get; set; }
            public int Weight { get; set; }
            public int TowerWeight { get; set; }
            public Node Parent { get; set; }
            public List<string> ChildNodes { get; } = new List<string>();
            public List<Node> Children { get; } = new List<Node>();

            public int GetTowerWeight()
            {
                return Weight + Children.Select(c => c.GetTowerWeight()).Sum();
            }

            public List<int> GetChildWeights()
            {
                return Children.Select(c => c.GetTowerWeight()).ToList();
            }
        }
    }
}
