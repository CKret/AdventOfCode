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
    /// Walking along the memory banks of the stream, you find a small village that
    /// is experiencing a little confusion: some programs can't communicate with
    /// each other.
    /// 
    /// Programs in this village communicate using a fixed system of pipes.
    /// Messages are passed between programs using these pipes, but most programs
    /// aren't connected to each other directly. Instead, programs pass messages
    /// between each other until the message reaches the intended recipient.
    /// 
    /// For some reason, though, some of these messages aren't ever reaching their
    /// intended recipient, and the programs suspect that some pipes are missing.
    /// They would like you to investigate.
    /// 
    /// You walk through the village and record the ID of each program and the IDs
    /// with which it can communicate directly (your puzzle input). Each program
    /// has one or more programs with which it can communicate, and these pipes are
    /// bidirectional; if 8 says it can communicate with 11, then 11 will say it
    /// can communicate with 8.
    /// 
    /// You need to figure out how many programs are in the group that contains
    /// program ID 0.
    /// 
    /// For example, suppose you go door-to-door like a travelling salesman and
    /// record the following list:
    /// 
    /// 0 &lt;-&gt; 2
    /// 1 &lt;-&gt; 1
    /// 2 &lt;-&gt; 0, 3, 4
    /// 3 &lt;-&gt; 2, 4
    /// 4 &lt;-&gt; 2, 3, 6
    /// 5 &lt;-&gt; 6
    /// 6 &lt;-&gt; 4, 5
    /// 
    /// In this example, the following programs are in the group that contains
    /// program ID 0:
    /// 
    ///     - Program 0 by definition.
    ///     - Program 2, directly connected to program 0.
    ///     - Program 3 via program 2.
    ///     - Program 4 via program 2.
    ///     - Program 5 via programs 6, then 4, then 2.
    ///     - Program 6 via programs 4, then 2.
    /// 
    /// Therefore, a total of 6 programs are in this group; all but program 1,
    /// which has a pipe that connects it to itself.
    /// 
    /// How many programs are in the group that contains program ID 0?
    /// 
    /// </summary>
    [AdventOfCode(2017, 12, 1, "Digital Plumber - Part One", 134)]
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
