using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2023
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2023, 9, "", 1584748274, 1026)]
	public class AdventOfCode202309 : AdventOfCodeBase
	{
		public AdventOfCode202309(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var histories = Input.Select(l => l.Split(" ").Select(int.Parse).ToList()).ToList();

			var sum = 0;
			foreach (var history in histories)
			{
				var predictions = new List<List<int>> { history };

				var prediction = history;
				do
				{
					prediction = GetPredictionPart1(prediction);
					predictions.Add(prediction);
				}
				while (!prediction.All(p => p == 0));

				var current = 0;

				predictions.Last().Add(current);
				for (var i = predictions.Count - 2; i >= 0; i--)
				{
					current += predictions[i].Last();
					predictions[i].Add(current);

					if (i == 0) sum += current;
				}
			}

			return sum;
		}

		protected override object SolvePart2()
		{
			var c = "0 3 6 9 12 15\r\n1 3 6 10 15 21\r\n10 13 16 21 30 45".Split("\r\n");
			var histories = Input.Select(l => l.Split(" ").Select(int.Parse).ToList()).ToList();

			var sum = 0;
			foreach (var history in histories)
			{
				var predictions = new List<List<int>> { history };

				var prediction = history;
				do
				{
					prediction = GetPredictionPart2(prediction);
					predictions.Add(prediction);
				}
				while (!prediction.All(p => p == 0));

				var current = 0;

				predictions.Last().Insert(0, current);
				for (var i = predictions.Count - 2; i >= 0; i--)
				{
					current += predictions[i].First();
					predictions[i].Insert(0, current);

					if (i == 0) sum += current;
				}
			}

			return sum;
		}

		protected List<int> GetPredictionPart1(List<int> history)
		{
			var prediction = new List<int>();

			for (var i = 0; i < history.Count() - 1; i++)
			{
				prediction.Add(history[i + 1] - history[i]);
			}

			return prediction;
		}

		protected List<int> GetPredictionPart2(List<int> history)
		{
			var prediction = new List<int>();

			for (var i = 0; i < history.Count() - 1; i++)
			{
				prediction.Add(history[i] - history[i + 1]);
			}

			return prediction;
		}
	}
}
