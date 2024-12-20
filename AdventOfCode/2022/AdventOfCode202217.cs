using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AdventOfCode._2018;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using SuperLinq;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 17, "Pyroclastic Flow", 3106, 1541449275365)]
	public class AdventOfCode202217 : AdventOfCodeBase
	{
		public AdventOfCode202217(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var rocks = new[] {new[] {"####"}, new[] {".#.", "###", ".#."}, new[] {"..#", "..#", "###"}, new []{ "#", "#", "#", "#"}, new[] {"##", "##"} };
			var jet = Input.First();
			var map = new Dictionary<int, bool[]>();

			var towerHeight = 0;
			var jetCounter = 0;
			for (var i = 0; i < 2022; i++)
			{
				var currentRock = rocks[i % rocks.Length];
				var x = 2;
				var y = towerHeight + 3;

				while (true)
				{
					var currentJet = jet[jetCounter++ % jet.Length];
					if (currentJet == '<' && x > 0)
					{
						if (MoveRock(currentRock, x - 1, y, map)) x--;
					}
					else if (currentJet == '>' && x < 7 - currentRock[0].Length)
					{
						if (MoveRock(currentRock, x + 1, y, map)) x++;
					}

					if (MoveRock(currentRock, x, y - 1, map) && y > 0)
					{
						y--;
					}
					else
					{
						for (var sY = 0; sY < currentRock.Length; sY++)
						{
							var actualY = y + (currentRock.Length - sY);
							var shapeLayer = currentRock[sY];
							if (!map.ContainsKey(actualY)) map.Add(actualY, new bool[7]);
							var layer = map[actualY];
							for (var sX = 0; sX < shapeLayer.Length; sX++)
							{
								layer[x + sX] |= shapeLayer[sX] == '#';
							}
						}
						break;
					}
				}


				towerHeight = map.Keys.Max();
			}
			
			return towerHeight;
		}

		protected override object SolvePart2()
		{
			var map = new Dictionary<(int X, int Y), int>();

			string[][] blocks = {
				new[] {
					"@@@@"
				},
				new[] {
					" @ ",
					"@@@",
					" @ "
				},
				new[] {
					"@@@",
					"  @",
					"  @"
				},
				new[] {
					"@",
					"@",
					"@",
					"@"
				},
				new[] {
					"@@",
					"@@"
				}
			};

			string tape = Input.First();
			int index = 0;
			long repeats = 0;

			int maxY = 0;
			int dispMax = 0;

			bool inPlay = false;

			var (blockX, blockY) = (0, 0);

			long nth = 0;
			int block = 0;
			string[] current = blocks[block];

			var revisit = new Dictionary<(int Tape, int Shape), (long Rocks, long Height)>();

			bool checkCollision(int bx, int by)
			{
				bool ok = true;
				for (int y = by; y < by + current.Length; y++)
				{
					for (int x = bx; x < bx + current[y - by].Length; x++)
					{
						int read = map.Read((x, y), -1);
						if (by <= y && y <= by + current.Length - 1 && bx <= x && x <= bx + current[y - by].Length - 1)
						{
							if (current[y - by][x - bx] == '@' && read != -1)
							{
								ok = false;
							}
						}
					}
				}
				return ok;
			}

			long found1 = 0;
			long found2 = 0;

			while (found1 == 0 || found2 == 0)
			{
				if (!inPlay)
				{
					blockX = 2;
					blockY = (map.Count > 1 ? maxY : -1) + 4;
					dispMax = blockY + blocks[block].Length;
					current = blocks[block];
					inPlay = true;
					nth++;
					if (nth == 2023)
					{
						found1 = maxY + 1;
					}
				}

				int next = blockX;
				if (tape[index] == '<')
				{
					next--;
				}
				else
				{
					next++;
				}
				if (0 <= next && next + current[0].Length <= 7 && checkCollision(next, blockY))
				{
					blockX = next;
				}
				index = (index + 1) % tape.Length;
				if (index == 0) repeats++;
				next = blockY - 1;
				if (0 <= next && checkCollision(blockX, next))
				{
					blockY = next;
				}
				else
				{
					for (int y = 0; y < blocks[block].Length; y++)
					{
						for (int x = 0; x < blocks[block][y].Length; x++)
						{
							bool place = blocks[block][y][x] == '@';
							if (place) map[(blockX + x, blockY + y)] = block;
							maxY = Math.Max(maxY, blockY + y);
						}
					}
					block = (block + 1) % blocks.Length;
					inPlay = false;

					if (revisit.ContainsKey((index, block)) && found2 == 0)
					{
						var last = revisit[(index, block)];
						long cycle = nth - last.Rocks;
						long adds = maxY + 1 - last.Height;
						long remaining = 1000000000000 - nth - 1;
						long combo = (remaining / (cycle) + 1);
						if (nth + combo * cycle == 1000000000000)
							found2 = maxY + 1 + combo * adds;
					}
					else
					{
						revisit[(index, block)] = (nth, maxY + 1);
					}
				}
			}
			Console.WriteLine($"Part 1: {found1}");
			Console.WriteLine($"Part 2: {found2}");

			return found2;
		}

		private bool MoveRock(IReadOnlyList<string> shape, int x, int y, Dictionary<int, bool[]> layers)
		{
			for (var sY = 0; sY < shape.Count; sY++)
			{
				var actualY = y + (shape.Count - sY);
				if (!layers.ContainsKey(actualY)) continue;
				var shapeLayer = shape[sY];
				var layer = layers[actualY];
				for (var sX = 0; sX < shapeLayer.Length; sX++)
				{
					if (layer[x + sX] && shapeLayer[sX] == '#' ) return false;
				}
			}

			return true;
		}

	}

	public record SeenKey
	{
		public int Ins;
		public int Shape;
		public List<int> View;
	}

	public record SeenValue
	{
		public long Rock;
		public int Height;
	}
}
