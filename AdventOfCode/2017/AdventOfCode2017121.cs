using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2017, 12, 1, "", 134)]
    public class AdventOfCode2017121 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var nodes = new List<Node>();
            foreach (var line in File.ReadAllLines("2017\\AdventOfCode201712.txt"))
            {
                var splits = line.Split(new[] { " <-> " }, StringSplitOptions.None);

                var node = new Node { Name = splits[0] };

                if (splits.Length == 2)
                    node.ChildNodes.AddRange(splits[1].Split(new [] {", "}, StringSplitOptions.RemoveEmptyEntries).Where(x => x != node.Name));

                nodes.Add(node);
            }

            // Build tree
            foreach (var node in nodes)
            {
                node.Children.AddRange(nodes.Where(n => node.ChildNodes.Contains(n.Name)));
            }

            var total = 0;
            foreach (var node in nodes)
            {
                var past = new List<Node>();
                total += Node.CountReferences(past, node, "0");
            }
            Result = total;
        }

        public class Node
        {
            public string Name { get; set; }
            public List<string> ChildNodes { get; } = new List<string>();
            public List<Node> Children { get; } = new List<Node>();

            public static int CountReferences(List<Node> past, Node current, string nodeName)
            {
                var count = 0;
                if (current.Name == nodeName)
                    count++;
                else
                {
                    foreach (var child in current.Children)
                    {
                        if (!past.Contains(child))
                        {
                            past.Add(child);
                            count += CountReferences(past, child, nodeName);
                        }
                    }

                }

                return count;
            }
        }
    }
}
