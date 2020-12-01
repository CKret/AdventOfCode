using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 12: Digital Plumber ---
    /// 
    /// --- Part Two ---
    /// 
    /// There are more programs than just the ones in the group containing program
    /// ID 0. The rest of them have no way of reaching that group, and still might
    /// have no way of reaching each other.
    /// 
    /// A group is a collection of programs that can all communicate via pipes
    /// either directly or indirectly. The programs you identified just a moment
    /// ago are all part of the same group. Now, they would like you to determine
    /// the total number of groups.
    /// 
    /// In the example above, there were 2 groups: one consisting of programs
    /// 0,2,3,4,5,6, and the other consisting solely of program 1.
    /// 
    /// How many groups are there in total?
    /// 
    /// </summary>
    [AdventOfCode(2017, 12, 2, "Digital Plumber - Part Two", 193)]
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

        public AdventOfCode2017122(string sessionCookie) : base(sessionCookie) { }
    }
}
