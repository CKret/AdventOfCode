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
			for (var x = 0; x < trees.Length; x++)
			{
				for (var y = 0; y < trees[x].Length; y++)
				{
					var xMin = 0;
					var yMin = 0;
					var xMax = trees.Length;
					var yMax = trees[x].Length;
					var tree = trees[x][y];

					var leftTrees = new List<int>();
					var rightTrees = new List<int>();
					var topTrees = new List<int>();
					var bottomTrees = new List<int>();


					for (var i = xMin; i < xMax; i++)
					{
						if (i == x) continue;
						if (i < x) leftTrees.Add(trees[i][y]);
						if (i > x) rightTrees.Add(trees[i][y]);
					}
					for (var i = yMin; i < yMax; i++)
					{
						if (i == y) continue;
						if (i < y) topTrees.Add(trees[x][i]);
						if (i > y) bottomTrees.Add(trees[x][i]);
					}

					if (leftTrees.All(t => t < tree) || rightTrees.All(t => t < tree) || topTrees.All(t => t < tree) || bottomTrees.All(t => t < tree)) count++;
				}
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
					var xMin = 0;
					var yMin = 0;
					var xMax = trees.Length;
					var yMax = trees[y].Length;

					var leftTrees = new List<int>();
					var rightTrees = new List<int>();
					var topTrees = new List<int>();
					var bottomTrees = new List<int>();

					for (var i = xMin; i < xMax; i++)
					{
						if (i == x) continue;
						if (i < x) leftTrees.Add(trees[y][i]);
						if (i > x) rightTrees.Add(trees[y][i]);
					}
					for (var i = yMin; i < yMax; i++)
					{
						if (i == y) continue;
						if (i < y) topTrees.Add(trees[i][x]);
						if (i > y) bottomTrees.Add(trees[i][x]);
					}

					var leftScore = 0;
					while (leftTrees.Count > 0)
					{
						leftScore++;
						if (leftTrees.Last() < trees[y][x])
							leftTrees.RemoveLast();
						else break;
					}
					var rightScore = 0;
					while (rightTrees.Count > 0)
					{
						rightScore++;
						if (rightTrees.First() < trees[y][x])
							rightTrees.RemoveAt(0);
						else break;
					}
					var topScore = 0;
					while (topTrees.Count > 0)
					{
						topScore++;
						if (topTrees.Last() < trees[y][x])
							topTrees.RemoveLast();
						else break;
					}
					var bottomScore = 0;
					while (bottomTrees.Count > 0)
					{
						bottomScore++;
						if (bottomTrees.First() < trees[y][x])
							bottomTrees.RemoveAt(0);
						else break;
					}

					scores.Add(leftScore * rightScore * topScore * bottomScore);
				}
			}

			return scores.Max();
		}
	}
}
