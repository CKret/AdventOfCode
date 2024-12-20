﻿using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2018
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2018, 18, "", 466312, 176782)]
	public class AdventOfCode201818 : AdventOfCodeBase
	{
		public AdventOfCode201818(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var data = Input.SelectMany(x => x).ToArray();

			for (var m = 0; m < 10; m++)
			{
				var newData = new List<char>();
				for (var y = 0; y < 50; y++)
				{
					for (var x = 0; x < 50; x++)
					{
						var current = data[y * 50 + x];

						// adjacent
						var adjacent = new List<char>();

						if (y > 0 && x > 0) adjacent.Add(data[(y - 1) * 50 + x - 1]);
						if (y > 0) adjacent.Add(data[(y - 1) * 50 + x]);
						if (y > 0 && x < 49) adjacent.Add(data[(y - 1) * 50 + x + 1]);

						if (x > 0) adjacent.Add(data[y * 50 + x - 1]);
						if (x < 49) adjacent.Add(data[y * 50 + x + 1]);

						if (y < 49 && x > 0) adjacent.Add(data[(y + 1) * 50 + x - 1]);
						if (y < 49) adjacent.Add(data[(y + 1) * 50 + x]);
						if (y < 49 && x < 49) adjacent.Add(data[(y + 1) * 50 + x + 1]);

						if (current == '.') // Open
						{
							if (adjacent.Count(a => a == '|') >= 3)
								current = '|';
						}
						else if (current == '|') // Trees
						{
							if (adjacent.Count(a => a == '#') >= 3)
								current = '#';
						}
						else if (current == '#') // Lumberyard
						{
							if (adjacent.All(a => a != '#') || adjacent.All(a => a != '|'))
								current = '.';
						}

						newData.Add(current);
					}
				}

				data = newData.ToArray();
			}

			var trees = data.Count(c => c == '|');
			var lumberyards = data.Count(c => c == '#');

			return trees * lumberyards;
		}

		protected override object SolvePart2()
		{
			var data = Input.SelectMany(x => x).ToArray();

			var history = new List<char[]>();

			for (var m = 0; m < 1000000000; m++)
			{
				var newData = new List<char>();
				for (var y = 0; y < 50; y++)
				{
					for (var x = 0; x < 50; x++)
					{
						var current = data[y * 50 + x];

						// adjacent
						var adjacent = new List<char>();

						if (y > 0 && x > 0) adjacent.Add(data[(y - 1) * 50 + x - 1]);
						if (y > 0) adjacent.Add(data[(y - 1) * 50 + x]);
						if (y > 0 && x < 49) adjacent.Add(data[(y - 1) * 50 + x + 1]);

						if (x > 0) adjacent.Add(data[y * 50 + x - 1]);
						if (x < 49) adjacent.Add(data[y * 50 + x + 1]);

						if (y < 49 && x > 0) adjacent.Add(data[(y + 1) * 50 + x - 1]);
						if (y < 49) adjacent.Add(data[(y + 1) * 50 + x]);
						if (y < 49 && x < 49) adjacent.Add(data[(y + 1) * 50 + x + 1]);

						if (current == '.') // Open
						{
							if (adjacent.Count(a => a == '|') >= 3)
								current = '|';
						}
						else if (current == '|') // Trees
						{
							if (adjacent.Count(a => a == '#') >= 3)
								current = '#';
						}
						else if (current == '#') // Lumberyard
						{
							if (adjacent.All(a => a != '#') || adjacent.All(a => a != '|'))
								current = '.';
						}

						newData.Add(current);
					}
				}

				history.Add(data);
				data = newData.ToArray();
				if (history.Any(h => h.SequenceEqual(data))) break;
			}

			var prev = history.IndexOf(history.Single(h => h.SequenceEqual(data)));
			var last = history.Count;
			var repeat = last - prev;

			var index = prev + (1000000000 - last) % repeat;

			var trees = history[index].Count(c => c == '|');
			var lumberyards = history[index].Count(c => c == '#');

			return trees * lumberyards;
		}
	}
}
