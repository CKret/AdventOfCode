using System;
using System.Linq;

namespace AdventOfCode._2023
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2023, 6, "Wait For It", 625968, 43663323)]
	public class AdventOfCode202306 : AdventOfCodeBase
	{
		public AdventOfCode202306(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var raceTimes = Input.First().Split("Time: ", StringSplitOptions.RemoveEmptyEntries)[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
			var raceDistances = Input.Last().Split("Distance: ", StringSplitOptions.RemoveEmptyEntries)[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

			var wins = new int[raceTimes.Count];
			for (var race = 0; race < raceTimes.Count; race++)
			{
				var currentTime = raceTimes[race];
				var currentDistance = raceDistances[race];

				for (var ms = 1; ms <= currentTime; ms++)
				{
					var distance = ms * (currentTime - ms);
					if (distance > currentDistance) wins[race]++;
				}
			}

			return wins.Aggregate(1, (c, p) => p * c);
		}

		protected override object SolvePart2()
		{
			var raceTime = long.Parse(Input.First().Split("Time: ", StringSplitOptions.RemoveEmptyEntries)[0].Replace(" ", ""));
			var raceDistance = long.Parse(Input.Last().Split("Distance: ", StringSplitOptions.RemoveEmptyEntries)[0].Replace(" ", ""));

			var wins = 0;
			for (var ms = 1L; ms <= raceTime; ms++)
			{
				var distance = ms * (raceTime - ms);
				if (distance > raceDistance) wins++;
			}

			return wins;
		}
	}
}
