using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2023
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2023, 7, "Camel Cards", 252656917, 253499763)]
	public class AdventOfCode202307 : AdventOfCodeBase
	{
		public AdventOfCode202307(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			return Input
				.Select(l => (Hand: l.Split(" ")[0], Bid: l.Split(" ")[1].ToInt()))
				.OrderBy(h => h.Hand, new CardComparer(new() { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' }))
				.Select((l, i) => (l.Hand, l.Bid, Index: i + 1))
				.Aggregate(0, (p, c) => p + c.Bid * c.Index);
		}

		protected override object SolvePart2()
		{
			return Input
				.Select(l => (Hand: l.Split(" ")[0], Bid: l.Split(" ")[1].ToInt()))
				.OrderBy(h => h.Hand, new CardComparer(new() { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' }, true))
				.Select((l, i) => (l.Hand, l.Bid, Index: i + 1))
				.Aggregate(0, (p, c) => p + c.Bid * c.Index);
		}
	}

	public class CardComparer : IComparer<string>
	{
		private readonly List<char> cards;
		private bool checkJoker;

		public CardComparer(List<char> cards, bool checkJoker = false)
		{
			this.cards = cards;
			this.checkJoker = checkJoker;
		}

		public int Compare(string left, string right)
		{
			// Same hand
			if (string.Equals(left, right)) return 0;

			var leftValue = HandValue(left, checkJoker);
			var rightValue = HandValue(right, checkJoker);

			if (leftValue > rightValue) return 1;
			if (leftValue < rightValue) return -1;

			if (leftValue == rightValue)
			{
				for (var i = 0; i < left.Length; i++)
				{
					if (cards.IndexOf(left[i]) > cards.IndexOf(right[i])) return 1;
					if (cards.IndexOf(left[i]) < cards.IndexOf(right[i])) return -1;
				}
			}

			return 0;
		}

		private int HandValue(string card, bool checkJoker = false)
		{
			// Five of a kind
			if (card.All(c => c == card[0])) return 6;

			// Four of a kind
			if (card.GroupBy(c => c).Any(g => g.Count() == 4))
			{
				if (checkJoker && card.Contains('J')) return 6;
				return 5;
			}

			// Full house
			if (card.GroupBy(c => c).Any(g => g.Count() == 3) && card.GroupBy(c => c).Any(g => g.Count() == 2))
			{
				if (checkJoker && card.Contains('J')) return 6;
				return 4;
			}

			// Three of a kind
			if (card.GroupBy(c => c).Any(g => g.Count() == 3))
			{
				if (checkJoker)
				{
					if (card.GroupBy(c => c).Any(g => g.Count() == 3 && g.Key == 'J')) return 5;
					else if (card.Contains('J')) return 5;
				}

				return 3;
			}

			// Two pair
			if (card.GroupBy(c => c).Count(g => g.Count() == 2) == 2)
			{
				if (checkJoker)
				{
					if (card.GroupBy(c => c).Any(g => g.Count() == 2 && g.Key == 'J')) return 5;
					else if (card.Contains('J')) return 4;
				}

				return 2;
			}

			// One pair
			if (card.GroupBy(c => c).Any(g => g.Count() == 2))
			{
				if (checkJoker && card.Contains('J')) return 3;
				return 1;
			}

			// High card
			if (checkJoker && card.Contains('J')) return 1;
			return 0;
		}
	}
}
