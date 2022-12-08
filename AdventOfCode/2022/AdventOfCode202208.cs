using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 8, "Treetop Tree House", 1733, 284648)]
	public class AdventOfCode202208 : AdventOfCodeBase
	{
		public AdventOfCode202208(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var trees = Input.Select(row => row.Select(col => int.Parse(col.ToString())).ToArray()).ToArray();

			var count = 0;
			for (var y = 0; y < trees.Length; y++)
				for (var x = 0; x < trees[y].Length; x++)
				{
					var leftTrees = IsVisible(x, y, -1, 0, trees[y][x], trees);
					var rightTrees = IsVisible(x, y, 1, 0, trees[y][x], trees);
					var topTrees = IsVisible(x, y, 0, -1, trees[y][x], trees);
					var bottomTrees = IsVisible(x, y, 0, 1, trees[y][x], trees);

					if (leftTrees || rightTrees || topTrees || bottomTrees) count++;
				}

			return count;
		}

		protected override object SolvePart2()
		{
			var trees = Input.Select(row => row.Select(col => int.Parse(col.ToString())).ToArray()).ToArray();

			var scores = new List<int>();
			for (var y = 1; y < trees.Length - 1; y++)
			{
				for (var x = 1; x < trees[y].Length - 1; x++)
				{
					var leftScore = GetScore(x, y, -1, 0, trees[y][x], trees);
					var rightScore = GetScore(x, y, 1, 0, trees[y][x], trees);
					var topScore = GetScore(x, y, 0, -1, trees[y][x], trees);
					var bottomScore = GetScore(x, y, 0, 1, trees[y][x], trees);

					scores.Add(leftScore * rightScore * topScore * bottomScore);
				}
			}

			return scores.Max();
		}

		private bool IsVisible(int x, int y, int xd, int yd, int tree, int[][] trees)
		{
			return x == 0 || x == trees[y].Length - 1 || y == 0 || y == trees.Length - 1 ||
				trees[y + yd][x + xd] < tree && IsVisible(x + xd, y + yd, xd, yd, tree, trees);
		}

		private int GetScore(int x, int y, int xd, int yd, int tree, int[][] trees)
		{
			return x == 0 || x == trees[y].Length - 1 || y == 0 || y == trees.Length - 1 ? 0 :
				trees[y + yd][x + xd] >= tree ? 1 : 1 + GetScore(x + xd, y + yd, xd, yd, tree, trees);
		}
	}
}
