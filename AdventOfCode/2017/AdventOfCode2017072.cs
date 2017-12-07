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

            var children = nodes.Where(n => n.ChildNodes.Count > 0).SelectMany(n => n.ChildNodes);
            var rootNode = nodes.Single(n => !children.Contains(n.Name));

            var lastDiff = 0;
            while (true)
            {
                var weights = new List<int>();
                foreach (var childName in rootNode.ChildNodes)
                {
                    var childNode = nodes.Single(n => n.Name == childName);
                    weights.Add(GetTowerWeight(nodes, childNode));
                }

                var diff = weights.Distinct().Max() - weights.Distinct().Min();

                if (diff == 0)
                {
                    Result = rootNode.Weight - lastDiff;
                    break;
                }

                lastDiff = diff;
                var unique = weights.Single(n => weights.Count(m => m == n) == 1);
                rootNode = nodes.Single(n => n.TowerWeight == unique);
            }
        }

        public int GetTowerWeight(List<Node> root, Node node)
        {
            var towerWight = node.Weight;
            var children = root.Where(n => node.ChildNodes.Contains(n.Name));
            towerWight += children.Sum(n => GetTowerWeight(root, n));

            node.TowerWeight = towerWight;
            return towerWight;
        }

        public class Node
        {
            public string Name { get; set; }
            public int Weight { get; set; }
            public int TowerWeight { get; set; }
            public Node Parent { get; set; }
            public List<string> ChildNodes { get; } = new List<string>();
        }
    }
}
