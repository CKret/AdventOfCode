using SuperLinq;
using System;
using System.Linq;

namespace AdventOfCode._2023
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2023, 4, "Scratchcards", 26218, null)]
	public class AdventOfCode202304 : AdventOfCodeBase
	{
		public AdventOfCode202304(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			return Input.Select(l => l
			.Split(": ")[1]
				.Split(" | ")
				.Select(r => r.Split(" ", StringSplitOptions.RemoveEmptyEntries)
					.Select(int.Parse)
				)
			)
			.Where(c => c.First().Intersect(c.Last()).Count() > 0)
			.Select(c => Math.Pow(2, c.First().Intersect(c.Last()).Count() - 1))
			.Sum();
		}

		protected override object SolvePart2()
		{
			var cards = Input
				.Select(l => (
					Number: l.Split(": ")[0].Replace("Card ", "").Trim().ToInt(),
					Matches: l.Split(": ")[1].Split(" | ").Select(r => r
						.Split(" ", StringSplitOptions.RemoveEmptyEntries)
						.Select(int.Parse)).First().Intersect(l.Split(": ")[1].Split(" | ").Select(r => r
							.Split(" ", StringSplitOptions.RemoveEmptyEntries)
							.Select(int.Parse))
					.Last()).Count(),
					Count: 1))
				.ToList();

			for (var i = 0; i < cards.Count; i++)
			{
				var matches = cards[i].Matches;
				var count = cards[i].Count;

				for (var c = 0; c < count; c++)
					for (var j = 1; j <= matches && j < cards.Count; j++)
					{
						var next = cards[i + j];
						next.Count++;
						cards[i + j] = next;
					}
			}

			return cards.Sum(c => c.Count);
		}
	}
}
