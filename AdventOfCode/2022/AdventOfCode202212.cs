using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.Mathematics.PathFinding;
using AdventOfCode.Mathematics.PathFinding.Core;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 12, "Hill Climbing Algorithm", 528, 522)]
	public class AdventOfCode202212 : AdventOfCodeBase
	{
		public AdventOfCode202212(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var map = Input.ToArray();

			var start = (X: -1, Y: -1);
			var end = (X: -1, Y: -1);
			for (var y = 0; y < map.Length; y++)
			{
				for (var x = 0; x < map[y].Length; x++)
				{
					if (map[y][x] == 'S') start = (X: x, Y: y);
					if (map[y][x] == 'E') end = (X: x, Y: y);
				}
			}

			var nodes = Enumerable.Range(0, map.Length).Select(y => Enumerable.Range(0, map[y].Length).Select(x => (X: x, Y: y))).SelectMany(x => x);
			var edges = FindEdges(map);

			var graph = new Graph<(int X, int Y)>(nodes, edges, true);
			var bfs = BFS.PathsFrom(graph, end);

			return bfs(start).Count() - 1;
		}

		protected override object SolvePart2()
		{
			var map = Input.ToArray();

			var startPos = new List<(int X, int Y)>();
			var end = (X: -1, Y: -1);
			for (var y = 0; y < map.Length; y++)
			{
				for (var x = 0; x < map[y].Length; x++)
				{
					if (map[y][x] == 'a') startPos.Add((X: x, Y: y));
					if (map[y][x] == 'E') end = (X: x, Y: y);
				}
			}

			var nodes = Enumerable.Range(0, map.Length).Select(y => Enumerable.Range(0, map[y].Length).Select(x => (X: x, Y: y))).SelectMany(x => x);
			var edges = FindEdges(map);

			var graph = new Graph<(int X, int Y)>(nodes, edges, true);
			var bfs = BFS.PathsFrom(graph, end);

			return startPos.Select(start => bfs(start).Count() - 1).Where(c => c > 0).Min();
		}

		private List<((int X, int Y), (int X, int Y))> FindEdges(string[] map)
		{
			var edges = new List<((int X, int Y), (int X, int Y))>();

			for (var y = 0; y < map.Length; y++)
			{
				for (var x = 0; x < map[y].Length; x++)
				{
					var cur = (X: x, Y: y);
					var up = (X: x, Y: y - 1);
					var down = (X: x, Y: y + 1);
					var left = (X: x - 1, Y: y);
					var right = (X: x + 1, Y: y);

					if (CanWalk(cur, up, map)) edges.Add((cur, up));
					if (CanWalk(cur, down, map)) edges.Add((cur, down));
					if (CanWalk(cur, left, map)) edges.Add((cur, left));
					if (CanWalk(cur, right, map)) edges.Add((cur, right));
				}
			}

			return edges;
		}

		private bool CanWalk((int X, int Y) from, (int X, int Y) to, string[] map)
		{
			return to.Y >= 0 && to.Y < map.Length && to.X >= 0 && to.X < map[0].Length && (map[to.Y][to.X] >= map[from.Y][from.X] - 1 || map[to.Y][to.X] == 'S' || map[from.Y][from.X] == 'E');
		}
	}
}
