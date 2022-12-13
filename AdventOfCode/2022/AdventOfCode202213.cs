using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.Mathematics.PathFinding;
using AdventOfCode.Mathematics.PathFinding.Core;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 13, "", null, null)]
	public class AdventOfCode202213 : AdventOfCodeBase
	{
		public AdventOfCode202213(string sessionCookie) : base(sessionCookie)
		{
		}

		protected override object SolvePart1()
		{
			var nodes = Input.Select((line, y) => line.Select((c, x) => c != '#' ? (X: x, Y: y) : (X: -1, Y: -1))).SelectMany(c => c).Where(c => c.X != -1 && c.Y != -1).ToArray();
			var edges = FindEdges(Input.ToArray());
			var graph = new Graph<(int X, int Y)>(nodes, edges);

			var b = BFS.Search(graph, (51, 19));

			var dfs = BFS.PathsFrom<(int X, int Y)>(graph, (51, 19));

			var result = dfs((1, 1));

			var map = Input.ToArray();
			foreach (var p in result)
			{
				Console.Clear();
				for (var y = 0; y < map.Length; y++)
				{
					if (p.Y != y)
					{
						Console.WriteLine(map[y]);
						continue;
					}
					for (var x = 0; x < map[y].Length; x++)
					{
						Console.Write(p.X == x ? 'P' : map[y][x]);
					}
					Console.WriteLine();
				}

				Console.ReadKey();
			}
			return null;
		}

		protected override object SolvePart2()
		{
			return null;
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

					if (map[y][x] == '#') continue;

					if (map[up.Y][up.X] != '#') edges.Add((cur, up));
					if (map[down.Y][down.X] != '#') edges.Add((cur, down));
					if (map[left.Y][left.X] != '#') edges.Add((cur, left));
					if (map[right.Y][right.X] != '#') edges.Add((cur, right));
				}
			}

			return edges;
		}
	}
}
