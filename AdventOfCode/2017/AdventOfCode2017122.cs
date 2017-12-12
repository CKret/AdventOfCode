using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2017, 12, 2, "", 193)]
    public class AdventOfCode2017122 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var nodes = new List<Node>();
            foreach (var line in File.ReadAllLines("2017\\AdventOfCode201712.txt"))
            {
                var splits = line.Split(new[] { " <-> " }, StringSplitOptions.None);

                var node = new Node
                {
                    Name = splits[0]
                };

                if (splits.Length == 2)
                {
                    node.ChildNodes.AddRange(splits[1].Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Where(x => x != node.Name));
                }

                nodes.Add(node);
            }

            // Build tree
            foreach (var node in nodes)
            {
                node.Children.AddRange(nodes.Where(n => node.ChildNodes.Contains(n.Name)));
            }

            var groups = new List<List<Node>>();
            foreach (var n in nodes)
            {
                if (groups.Any(x => x.Contains(n))) continue;
                groups.Add(Node.GetGroup(new List<Node>(), n));
            }
            Result = groups.Count;
        }

        public class Node
        {
            public string Name { get; set; }
            public List<string> ChildNodes { get; } = new List<string>();
            public List<Node> Children { get; } = new List<Node>();

            public static List<Node> GetGroup(List<Node> group, Node current)
            {
                group.Add(current);
                foreach (var child in current.Children)
                {
                    if (!group.Contains(child))
                    {
                        group = GetGroup(group, child);
                    }
                }

                return group;
            }
        }
    }
}
