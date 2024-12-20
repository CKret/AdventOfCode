using SuperLinq;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2023, 2, "Cube Conundrum", 2720, 71535)]
	public class AdventOfCode202302 : AdventOfCodeBase
	{
		public AdventOfCode202302(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var sum = 0;
			foreach (var game in Input)
			{
				var gameNr = Regex.Match(game.Split(": ")[0], @"\d+").Value.ToInt();
				var draws = game.Split(": ")[1].Split("; ");

				var c = draws
					.Select(d => d.Split(", "))
					.Select(c => c.Select(x => x.Split(" ")))
					.Select(v => v.ToDictionary(x => x[1], x => x[0].ToInt()))
					.ToList();

				for (var i = 0; i < c.Count(); i++ )
				{
					if (!c[i].ContainsKey("red")) c[i].Add("red", 0);
					if (!c[i].ContainsKey("green")) c[i].Add("green", 0);
					if (!c[i].ContainsKey("blue")) c[i].Add("blue", 0);
				}

				if (c.All(x => x["red"] <= 12 && x["green"] <= 13 && x["blue"] <= 14)) sum += gameNr;
			}

			return sum;
		}

		protected override object SolvePart2()
		{
			var sum = 0;
			foreach (var game in Input)
			{
				var gameNr = Regex.Match(game.Split(": ")[0], @"\d+").Value.ToInt();
				var draws = game.Split(": ")[1].Split("; ");

				var c = draws
					.Select(d => d.Split(", "))
					.Select(c => c.Select(x => x.Split(" ")))
					.Select(v => v.ToDictionary(x => x[1], x => x[0].ToInt()))
					.ToList();

				for (var i = 0; i < c.Count(); i++)
				{
					if (!c[i].ContainsKey("red")) c[i].Add("red", 0);
					if (!c[i].ContainsKey("green")) c[i].Add("green", 0);
					if (!c[i].ContainsKey("blue")) c[i].Add("blue", 0);
				}

				var maxRed = c.Max(x => x["red"]);
				var maxGreen = c.Max(x => x["green"]);
				var maxBlue = c.Max(x => x["blue"]);

				var power = maxRed * maxGreen * maxBlue;
				sum += power;
			}

			return sum;
		}
	}
}
