using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using MoreLinq;

namespace AdventOfCode._2018
{
    /// <summary>
    /// --- Day 8: Memory Maneuver ---
    /// 
    /// The sleigh is much easier to pull than you'd expect for something its
    /// weight. Unfortunately, neither you nor the Elves know which way the North
    /// Pole is from here.
    /// 
    /// You check your wrist device for anything that might help. It seems to have
    /// some kind of navigation system! Activating the navigation system produces
    /// more bad news: "Failed to start navigation system. Could not read software
    /// license file."
    /// 
    /// The navigation system's license file consists of a list of numbers (your
    /// puzzle input). The numbers define a data structure which, when processed,
    /// produces some kind of tree that can be used to calculate the license
    /// number.
    /// 
    /// The tree is made up of nodes; a single, outermost node forms the tree's
    /// root, and it contains all other nodes in the tree (or contains nodes that
    /// contain nodes, and so on).
    /// 
    /// Specifically, a node consists of:
    /// 
    /// - A header, which is always exactly two numbers:
    ///   - The quantity of child nodes.
    ///   - The quantity of metadata entries.
    /// - Zero or more child nodes (as specified in the header).
    /// - One or more metadata entries (as specified in the header).
    /// 
    /// Each child node is itself a node that has its own header, child nodes, and
    /// metadata. For example:
    /// 
    /// 2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2
    /// A----------------------------------
    ///     B----------- C-----------
    ///                      D-----
    /// 
    /// In this example, each node of the tree is also marked with an underline
    /// starting with a letter for easier identification. In it, there are four
    /// nodes:
    /// 
    /// - A, which has 2 child nodes (B, C) and 3 metadata entries (1, 1, 2).
    /// - B, which has 0 child nodes and 3 metadata entries (10, 11, 12).
    /// - C, which has 1 child node (D) and 1 metadata entry (2).
    /// - D, which has 0 child nodes and 1 metadata entry (99).
    /// 
    /// The first check done on the license file is to simply add up all of the
    /// metadata entries. In this example, that sum is 1+1+2+10+11+12+2+99=138.
    /// 
    /// What is the sum of all metadata entries?
    /// </summary>
    [AdventOfCode(2018, 8, 1, "Memory Maneuver - Part 1", 41926)]
    public class AdventOfCode2018081 : AdventOfCodeBase
    {
        public static int Index;
        public static LicenseNode RootNode;

        public override void Solve()
        {
            var data = File.ReadAllText(@"2018\AdventOfCode201808.txt").Split(' ').Select(int.Parse).ToList();
            Index = 0;
            RootNode = ParseNodes(data);
            Result = RootNode.SumMetaEntries();
        }

        public static LicenseNode ParseNodes(List<int> data)
        {
            var node = new LicenseNode();

            Enumerable.Range(0, data[Index++]).ForEach(n => node.ChildNodes.Add(ParseNodes(data)));
            Enumerable.Range(0, data[Index++]).ForEach(n => node.MetaEntries.Add(data[Index++]));

            return node;
        }
    }

    public class LicenseNode
    {
        public readonly List<LicenseNode> ChildNodes = new List<LicenseNode>();
        public readonly List<int> MetaEntries = new List<int>();

        public int SumMetaEntries()
        {
            var sum = MetaEntries.Sum();
            foreach (var node in ChildNodes)
            {
                sum += node.SumMetaEntries();
            }

            return sum;
        }

        public int SumNodeValues()
        {
            if (!ChildNodes.Any())
            {
                return MetaEntries.Sum();
            }

            var sum = 0;
            foreach (var m in MetaEntries)
            {
                if (m <= ChildNodes.Count)
                {
                    sum += ChildNodes[m - 1].SumNodeValues();
                }
            }

            return sum;
        }
    }
}
