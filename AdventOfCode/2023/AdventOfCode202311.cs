using AdventOfCode.Mathematics;
using SuperLinq;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2023
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2023, 11, "", 9445168, null)]
	public class AdventOfCode202311 : AdventOfCodeBase
	{
		public AdventOfCode202311(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var spaceMap = Input.Select(l => l.ToCharArray().ToList()).ToList();

			// Expande space
			var emptyRows = spaceMap.Select((l, i) => l.All(c => c == '.') ? i : -1).Where(r => r >= 0).ToList();
			var empty = spaceMap[emptyRows.First()];

			foreach (var row in emptyRows.OrderByDescending(r => r))
				spaceMap.Insert(row + 1, empty);

			var transposedMap = spaceMap
				.SelectMany(inner => inner.Select((item, index) => new { item, index }))
				.GroupBy(x => x.index, x => x.item)
				.Select(g => g.ToList())
				.ToList();

			var emptyCols = transposedMap.Select((l, i) => l.All(c => c == '.') ? i : -1).Where(r => r >= 0).ToList();
			empty = transposedMap[emptyCols.First()];

			foreach (var row in emptyCols.OrderByDescending(c => c))
				transposedMap.Insert(row + 1, empty);

			spaceMap = transposedMap
				.SelectMany(inner => inner.Select((item, index) => new { item, index }))
				.GroupBy(x => x.index, x => x.item)
				.Select(g => g.ToList())
				.ToList();

			var galaxies = new List<(int X, int Y)>();

			// Find all galaxies
			for (var y = 0; y < spaceMap.Count; y++)
			{
				for (var x = 0; x < spaceMap[y].Count; x++)
				{
					if (spaceMap[y][x] == '#')
					{
						galaxies.Add((x, y));
					}
				}
			}

			// Calculate the distancies between all galaxies
			var distancies = new Dictionary<(int X, int Y), Dictionary<(int X, int Y), int>>();
			for (var galaxy1 = 0; galaxy1 < galaxies.Count - 1; galaxy1++)
			{
				var dist = new Dictionary<(int X, int Y), int>();
				for (var galaxy2 = galaxy1 + 1; galaxy2 < galaxies.Count; galaxy2++)
				{
					var distance = galaxies[galaxy1].ManhattanDistance(galaxies[galaxy2]);
					dist.Add(galaxies[galaxy2], distance);

				}
				distancies.Add(galaxies[galaxy1], dist);
			}

			// Sum all distancies
			var sumDistancies = new Dictionary<(int X, int Y), int>();
			foreach (var galaxy in distancies)
			{
				var sum = galaxy.Value.Sum(d => d.Value);
				sumDistancies.Add(galaxy.Key, sum);
			}

			var totalSum = sumDistancies.Sum(s => s.Value);

			return totalSum;
		}

		protected override object SolvePart2()
		{
			var spaceMap = Input.Select(l => l.ToCharArray().ToList()).ToList();

			// Expande space
			var emptyRows = spaceMap.Select((l, i) => l.All(c => c == '.') ? i : -1).Where(r => r >= 0).ToList();
			var empty = spaceMap[emptyRows.First()];

			foreach (var row in emptyRows.OrderByDescending(r => r))
				spaceMap.InsertRange(row + 1, Enumerable.Repeat(empty, 1000000));

			var transposedMap = spaceMap
				.SelectMany(inner => inner.Select((item, index) => new { item, index }))
				.GroupBy(x => x.index, x => x.item)
				.Select(g => g.ToList())
				.ToList();

			var emptyCols = transposedMap.Select((l, i) => l.All(c => c == '.') ? i : -1).Where(r => r >= 0).ToList();
			empty = transposedMap[emptyCols.First()];

			foreach (var row in emptyCols.OrderByDescending(c => c))
				transposedMap.InsertRange(row + 1, Enumerable.Repeat(empty, 1000000));

			spaceMap = transposedMap
				.SelectMany(inner => inner.Select((item, index) => new { item, index }))
				.GroupBy(x => x.index, x => x.item)
				.Select(g => g.ToList())
				.ToList();

			var galaxies = new List<(int X, int Y)>();

			// Find all galaxies
			for (var y = 0; y < spaceMap.Count; y++)
			{
				for (var x = 0; x < spaceMap[y].Count; x++)
				{
					if (spaceMap[y][x] == '#')
					{
						galaxies.Add((x, y));
					}
				}
			}

			// Calculate the distancies between all galaxies
			var distancies = new Dictionary<(int X, int Y), Dictionary<(int X, int Y), int>>();
			for (var galaxy1 = 0; galaxy1 < galaxies.Count - 1; galaxy1++)
			{
				var dist = new Dictionary<(int X, int Y), int>();
				for (var galaxy2 = galaxy1 + 1; galaxy2 < galaxies.Count; galaxy2++)
				{
					var distance = galaxies[galaxy1].ManhattanDistance(galaxies[galaxy2]);
					dist.Add(galaxies[galaxy2], distance);

				}
				distancies.Add(galaxies[galaxy1], dist);
			}

			// Sum all distancies
			var sumDistancies = new Dictionary<(int X, int Y), int>();
			foreach (var galaxy in distancies)
			{
				var sum = galaxy.Value.Sum(d => d.Value);
				sumDistancies.Add(galaxy.Key, sum);
			}

			var totalSum = sumDistancies.Sum(s => s.Value);

			return totalSum;

		}
	}
}
