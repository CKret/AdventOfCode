using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>

    /// </summary>
    [AdventOfCode(2017, 7, 2, "", 2310)]
    public class AdventOfCode2017072 : AdventOfCodeBase
    {
        public override void Solve()
        {
            // Create all nodes.
            var nodes = new List<Node>();
            foreach (var line in File.ReadAllLines("2017/AdventOfCode201707.txt"))
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
                    node.Children.ForEach(n => n.Parent = node);
                }
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
