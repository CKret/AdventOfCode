using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 9, "Rope Bridge", 6642, 2765)]
	public class AdventOfCode202209 : AdventOfCodeBase
	{
		public AdventOfCode202209(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var motions = Input.Select(row => row.Split(" ")).Select(row => (Direction: row[0], Steps: int.Parse(row[1])));
			return MoveRope(motions, 2);
		}
		protected override object SolvePart2()
		{
			var motions = Input.Select(row => row.Split(" ")).Select(row => (Direction: row[0], Steps: int.Parse(row[1])));
			return MoveRope(motions, 10);
		}

		private int MoveRope(IEnumerable<(string Direction, int Steps)> motions, int nKnots)
		{
			var visited = new Dictionary<(int X, int Y), bool>();
			var rope = Enumerable.Range(0, nKnots).Select(_ => (X: 0, Y: 0)).ToArray();

			foreach (var row in motions)
			{
				for (var i = 0; i < row.Steps; i++)
				{
					rope[0] = MoveHead(rope[0], row.Direction);

					for (var r = 1; r < rope.Length; r++)
					{
						if (Math.Abs(rope[r - 1].X - rope[r].X) > 1 || Math.Abs(rope[r - 1].Y - rope[r].Y) > 1)
						{
							rope[r].X += Math.Min(Math.Max(-1, rope[r - 1].X - rope[r].X), 1);
							rope[r].Y += Math.Min(Math.Max(-1, rope[r - 1].Y - rope[r].Y), 1);
						}
					}

					visited[rope[^1]] = true;
				}
			}

			return visited.Count();
		}
		private (int X, int Y) MoveHead((int X, int Y) head, string direction)
		{
			return direction switch
			{
				"L" => (head.X - 1, head.Y),
				"R" => (head.X + 1, head.Y),
				"U" => (head.X, head.Y - 1),
				"D" => (head.X, head.Y + 1),
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}
	}
}
