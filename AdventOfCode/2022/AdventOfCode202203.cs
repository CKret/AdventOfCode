using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using MoreLinq;
using System.Linq;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 3, "Rucksack Reorganization", 8185, 2817)]
	public class AdventOfCode202203 : AdventOfCodeBase
	{
		public AdventOfCode202203(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			return Input
				.Select(x => x.SplitInHalf())
				.Select(x => x.Head.Intersect(x.Tail).SingleOrDefault())
				.Sum(x => x <= 'Z' ? x - 'A' + 27 : x - 'a' + 1);
		}

		protected override object SolvePart2()
		{
			return Input
				.Batch(3)
				.Select(x => x
					.Skip(1)
					.Aggregate(x.First(), (p, c) => string.Join("", p.Intersect(c)))
				)
				.Select(x => x[0])
				.Sum(x => x <= 'Z' ? x - 'A' + 27 : x - 'a' + 1);
		}
	}
}
