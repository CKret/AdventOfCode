using System;
using System.Linq;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 14, "Regolith Reservoir", 858, 26845)]
	public class AdventOfCode202214 : AdventOfCodeBase
	{
		public AdventOfCode202214(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var rocks = Input.Select(r => r.Split(" -> ").Select(s => s.Split(",").Select(int.Parse).ToArray())).Select(l => l.Select(m => (m[0], m[1])).ToArray());
			var minX = rocks.SelectMany(x => x).Min(r => r.Item1);
			var maxX = rocks.SelectMany(x => x).Max(r => r.Item1);
			var maxY = rocks.SelectMany(x => x).Max(r => r.Item2);

			var map = new char[maxY + 1][];
			Enumerable.Range(0, maxY + 1).ForEach(y => map[y] = Enumerable.Repeat('.', maxX - minX + 1).ToArray());

			foreach (var r in rocks)
			{
				for (var pos = 1; pos < r.Count(); pos++)
				{
					var curPos = r[pos - 1];
					// Vertical
					if (curPos.Item1 == r[pos].Item1)
					{
						for (var y = Math.Min(curPos.Item2, r[pos].Item2) ; y <= Math.Max(curPos.Item2, r[pos].Item2); y++)
						{
							map[y][curPos.Item1 - minX] = '#';
						}
					}
					// Horizontal
					if (curPos.Item2 == r[pos].Item2)
					{
						for (var x = Math.Min(curPos.Item1, r[pos].Item1); x <= Math.Max(curPos.Item1, r[pos].Item1); x++)
						{
							map[curPos.Item2][x - minX] = '#';
						}
					}
				}

			}

			var sand = (x: 500 - minX, y: 0);

			var count = 0;
			while (true)
			{
				var newPos = MoveSand(map, sand);
				if (newPos.Item1 >= maxX || newPos.Item2 >= maxY)
				{
					foreach (var l in map)
					{
						Console.WriteLine(string.Join(string.Empty, l));
					}
					return count;
				}
				if (newPos == sand)
				{
					if (count > 880)
					{
						foreach (var l in map)
						{
							Console.WriteLine(string.Join(string.Empty, l));
						}
					}

					map[sand.y][sand.x] = 'O';
					count++;
					sand = (x: 500 - minX, y: 0);
				}
				else sand = newPos;
			};
		}

		protected override object SolvePart2()
		{
			var rocks = Input.Select(r => r.Split(" -> ").Select(s => s.Split(",").Select(int.Parse).ToArray())).Select(l => l.Select(m => (m[0], m[1])).ToArray());
			var minX = -500;
			var maxX = 1000;
			var maxY = rocks.SelectMany(x => x).Max(r => r.Item2);

			var map = new char[maxY + 3][];
			Enumerable.Range(0, maxY + 3).ForEach(y => map[y] = Enumerable.Repeat('.', maxX - minX + 1).ToArray());

			foreach (var r in rocks)
			{
				for (var pos = 1; pos < r.Count(); pos++)
				{
					var curPos = r[pos - 1];
					// Vertical
					if (curPos.Item1 == r[pos].Item1)
					{
						for (var y = Math.Min(curPos.Item2, r[pos].Item2); y <= Math.Max(curPos.Item2, r[pos].Item2); y++)
						{
							map[y][curPos.Item1 - minX] = '#';
						}
					}
					// Horizontal
					if (curPos.Item2 == r[pos].Item2)
					{
						for (var x = Math.Min(curPos.Item1, r[pos].Item1); x <= Math.Max(curPos.Item1, r[pos].Item1); x++)
						{
							map[curPos.Item2][x - minX] = '#';
						}
					}
				}

			}

			var sand = (x: 500 - minX, y: 0);

			var count = 0;
			while (true)
			{
				var newPos = MoveSand(map, sand);
				if (newPos == (500 - minX, 0))
				{
					foreach (var l in map)
					{
						Console.WriteLine(string.Join(string.Empty, l));
					}
					return count + 1;
				}
				if (newPos == sand || newPos.Item2 >= maxY + 2)
				{
					map[sand.y][sand.x] = 'O';
					count++;
					sand = (x: 500 - minX, y: 0);
				}
				else sand = newPos;
			};
		}

		private (int, int) MoveSand(char[][] map, (int x, int y) sand)
		{
			if (sand.x + 1 >= map[0].Length || sand.y + 1 >= map.Length) return (sand.x + 1, sand.y + 1);
			if (map[sand.y + 1][sand.x] == '.') return (sand.x, sand.y + 1);

			if (map[sand.y + 1][sand.x] == '#' || map[sand.y + 1][sand.x] == 'O')
			{
				if (map[sand.y + 1][sand.x - 1] == '#' || map[sand.y + 1][sand.x - 1] == 'O')
				{
					if (map[sand.y + 1][sand.x + 1] == '#' || map[sand.y + 1][sand.x + 1] == 'O')
					{
						return sand;
					}
					else return (sand.x + 1, sand.y + 1);
				}
				else return (sand.x - 1, sand.y + 1);
			}

			return sand ;
		}
	}
}
