using AdventOfCode.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2017
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2017, 7, 1, "", "cqmvs")]
    public class AdventOfCode2017071 : AdventOfCodeBase
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
            Result = nodes.Single(n => !children.Contains(n.Name)).Name;
        }

        public class Node
        {
            public string Name { get; set; }
            public int Weight { get; set; }
            public Node Parent { get; set; }
            public List<string> ChildNodes { get; } = new List<string>();
        }
    }
}
