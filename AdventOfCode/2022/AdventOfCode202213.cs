using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using AdventOfCode.Core;
using SuperLinq;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 13, "Distress Signal", 5625, 23111)]
	public class AdventOfCode202213 : AdventOfCodeBase
	{
		public AdventOfCode202213(string sessionCookie) : base(sessionCookie)
		{
		}

		protected override object SolvePart1()
		{
			return Input
				.Split(string.Empty)
				.Select(p => p.Select(x => JsonNode.Parse(x)).ToArray())
				.Select((p, i) => new SignalPacketComparer().Compare(p[0], p[1]) < 0 ? i + 1 : 0)
				.Sum();
		}

		protected override object SolvePart2()
		{
			var div1 = JsonNode.Parse("[[2]]");
			var div2 = JsonNode.Parse("[[6]]");

			return Input
				.Where(x => !x.IsEmpty())
				.Select(x => JsonNode.Parse(x))
				.Concat(new[] { div1, div2 })
				.OrderBy(x => x, new SignalPacketComparer())
				.Select((x, i) => (Packet: x, Index: i + 1))
				.Where(x => x.Packet == div1 || x.Packet == div2)
				.Aggregate(1, (p, c) => p * c.Index);
		}

		internal class SignalPacketComparer : IComparer<JsonNode>
		{
			public int Compare(JsonNode left, JsonNode right)
			{
				if (left is JsonValue && right is JsonValue) return (int)left - (int)right;

				var arrayLeft = left as JsonArray ?? new JsonArray((int)left);
				var arrayRight = right as JsonArray ?? new JsonArray((int)right);

				return arrayLeft.Zip(arrayRight)
					.Select(p => Compare(p.First, p.Second))
					.FirstOrDefault(c => c != 0, arrayLeft.Count - arrayRight.Count);
			}
		}
	}
}