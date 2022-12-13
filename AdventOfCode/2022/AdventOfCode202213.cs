using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using AdventOfCode.Mathematics.PathFinding;
using AdventOfCode.Mathematics.PathFinding.Core;
using SuperLinq;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 13, "", null, null)]
	public class AdventOfCode202213 : AdventOfCodeBase
	{
		public AdventOfCode202213(string sessionCookie) : base(sessionCookie)
		{
		}

		protected override object SolvePart1()
		{
			var pairs = Input.Split(string.Empty);
			var f = pairs.First().First();

			var root = Parse(f);
			

			return null;
		}

		protected override object SolvePart2()
		{
			return null;
		}

		private bool CompareList(int[] left, int[] right)
		{
			for (var i = 0; i < left.Length; i++)
			{
				if (i >= right.Length) return false;
				if (left[i] > right[i]) return false;
			}
			return left.Length == right.Length;
		}

		private List<Node> Parse(string packet)
		{
			var nodes = new List<Node>();
			for (var i = 0; i < packet.Length; i++)
			{
				switch (packet[i])
				{
					case '[':
						if (i == 0) continue;
						var c = 0;
						var j = 0;
						for (j = i;  j < packet.Length; j++)
						{
							if (packet[j] == '[') c++;
							if (packet[j] == ']') c--;
							if (c == 0) break;
						}
                        nodes.Add(new Node { Children = Parse(packet[(i + 1)..j]), StringValue = packet[(i + 1)..j] });
                        i = j;
                        break;
					case ']':
					case ',':
						continue;
					default:
						var tmp = string.Join("", packet.Skip(i).TakeWhile(char.IsDigit));
						i += tmp.Length;
						nodes.Add(new Node { IsList = false, Value = int.Parse(tmp), StringValue = tmp });
						break;
				}

			}

			return nodes;
		}
	}

	public class Node : IComparable<Node>
	{
		public bool IsList = true;
		public string StringValue;
		public int Value;
		public List<Node> Children = new();

        public int CompareTo(Node other)
        {
			for (var i = 0; i < int.Max(this.Children.Count, other.Children.Count); i++)
			{
				if (i >= this.Children) return true;
                if (i >= other.Children) return false;
            }
        }
    }
}
