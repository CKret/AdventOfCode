using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2023
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2023, 10, "", 6640, null)]
	public class AdventOfCode202310 : AdventOfCodeBase
	{
		public AdventOfCode202310(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var tiles = new[] { '|', '-', 'L', 'J', '7', 'F', '.', 'S' };
			var map = Input.Select(l => l.ToCharArray()).ToArray();


			// Find the starting point S in map
			var start = FindStartingPoint(map);
			var startTile = FindTileType(map, start);
			map[start.Y][start.X] = startTile;

			var step = 0;
			var path = new List<(int X, int Y, int Step)> { (start.X, start.Y, step) };
			var queue = new Queue<(int X, int Y)>();
			queue.Enqueue(start);
			do
			{
				step++;
				var nextTiles1 = FindNextTiles(map, queue.Dequeue());
				if (!path.Any(p => p.X == nextTiles1.Item1.X && p.Y == nextTiles1.Item1.Y) && nextTiles1.Item1 != nextTiles1.Item2)
				{
					path.Add((nextTiles1.Item1.X, nextTiles1.Item1.Y, step));
					queue.Enqueue(nextTiles1.Item1);
				}
				if (!path.Any(p => p.X == nextTiles1.Item2.X && p.Y == nextTiles1.Item2.Y) && nextTiles1.Item2 != nextTiles1.Item1)
				{
					path.Add((nextTiles1.Item2.X, nextTiles1.Item2.Y, step));
					queue.Enqueue(nextTiles1.Item2);
				}

				if (!queue.IsEmpty())
				{
					var nextTiles2 = FindNextTiles(map, queue.Dequeue());
					if (!path.Any(p => p.X == nextTiles2.Item1.X && p.Y == nextTiles2.Item1.Y) && nextTiles2.Item1 != nextTiles2.Item2)
					{
						path.Add((nextTiles2.Item1.X, nextTiles2.Item1.Y, step));
						queue.Enqueue(nextTiles2.Item1);
					}
					if (!path.Any(p => p.X == nextTiles2.Item2.X && p.Y == nextTiles2.Item2.Y) && nextTiles2.Item2 != nextTiles2.Item1)
					{
						path.Add((nextTiles2.Item2.X, nextTiles2.Item2.Y, step));
						queue.Enqueue(nextTiles2.Item2);
					}
				}

			} while (!queue.IsEmpty());



			for (var y = 0; y < map.Length; y++)
			{
				for (var x = 0; x < map[y].Length; x++)
				{
					if (path.Any(p => p.X == x && p.Y == y))
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.Write(map[y][x]);
						Console.ForegroundColor = ConsoleColor.White;
					}
					else
						Console.Write(map[y][x]);
				}
				Console.WriteLine();
			}	

			return step;
		}

		private static (int X, int Y) FindStartingPoint(char[][] map)
		{
			var start = (X: -1, Y: -1);

			for (var y = 0; y < map.Length && start == (-1, -1); y++)
			{
				for (var x = 0; x < map[y].Length; x++)
				{
					if (map[y][x] == 'S')
					{
						start = (x, y);
						break;
					}
				}
			}

			return start;
		}

		protected override object SolvePart2()
		{
			return null;
		}

		private char FindTileType(char[][] map, (int X, int Y) current)
		{
			// Possible tiles
			var tiles = new List<char> { '|', '-', 'L', 'J', '7', 'F', '.', 'S' };

			var northTiles = new List<char> { '|', '7', 'F' };
			var southTiles = new List<char> { '|', 'L', 'J' };
			var eastTiles = new List<char> { '-', 'J', '7' };
			var westTiles = new List<char> { '-', 'L', 'F' };

			var northTile = map[current.Y - 1][current.X];
			var southTile = map[current.Y + 1][current.X];
			var eastTile = map[current.Y][current.X + 1];
			var westTile = map[current.Y][current.X - 1];

			if (northTiles.Contains(northTile) && southTiles.Contains(southTile)) return '|';
			if (eastTiles.Contains(eastTile) && westTiles.Contains(westTile)) return '-';
			if (northTiles.Contains(northTile) && eastTiles.Contains(eastTile)) return 'L';
			if (northTiles.Contains(northTile) && westTiles.Contains(westTile)) return 'J';
			if (southTiles.Contains(southTile) && eastTiles.Contains(eastTile)) return 'F';
			if (southTiles.Contains(southTile) && westTiles.Contains(westTile)) return '7';
			
			throw new Exception("Invalid map");
		}


		private ((int X, int Y), (int X, int Y)) FindNextTiles(char[][] map, (int X, int Y) current)
		{
			var next = ((X: -1, Y: -1), (X: -1, Y: -1));
			var tiles = new List<char> { '|', '-', 'L', 'J', '7', 'F', '.', 'S' };

			if (map[current.Y][current.X] == '|') return ((current.X, current.Y - 1), (current.X, current.Y + 1));
			if (map[current.Y][current.X] == '-') return ((current.X - 1, current.Y), (current.X + 1, current.Y));
			if (map[current.Y][current.X] == 'L') return ((current.X, current.Y - 1), (current.X + 1, current.Y));
			if (map[current.Y][current.X] == 'J') return ((current.X, current.Y - 1), (current.X - 1, current.Y));
			if (map[current.Y][current.X] == '7') return ((current.X - 1, current.Y), (current.X, current.Y + 1));
			if (map[current.Y][current.X] == 'F') return ((current.X + 1, current.Y), (current.X, current.Y + 1));
			if (map[current.Y][current.X] == '.') throw new Exception();
			if (map[current.Y][current.X] == 'S') throw new Exception();

			return next;
		}
	}
}
