using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2023
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2023, 3, "Gear Ratios", 517021, 81296995)]
	public class AdventOfCode202303 : AdventOfCodeBase
	{
		public AdventOfCode202303(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var schematic = Input.Select(c => c.ToCharArray()).ToArray();
			var engineParts = new List<int>();

			for (var y = 0; y < schematic.Count(); y++)
			{
				var num = "";
				for (var x = 0; x < schematic[y].Count(); x++)
				{
					if (schematic[y][x].IsNumber())
					{
						num += schematic[y][x];
					}
					else if (num.Length > 0)
					{
						if (IsEnginePart(x - num.Length - 1 < 0 ? 0 : x - num.Length - 1, y == 0 ? 0 : y - 1, x, y + 1, schematic[y].Length, schematic.Length, schematic)) engineParts.Add(num.ToInt());
						num = "";
					}

					if (num.Length > 0 && x == schematic[y].Count() - 1)
					{
						if (IsEnginePart(x - num.Length - 1 < 0 ? 0 : x - num.Length - 1, y == 0 ? 0 : y - 1, x, y + 1, schematic[y].Length, schematic.Length, schematic)) engineParts.Add(num.ToInt());
					}
				}
			}

			return engineParts.Sum();
		}

		protected override object SolvePart2()
		{
			var schematic = Input.Select(c => c.ToCharArray()).ToArray();
			var gearParts = new List<((int, int), int)>();

			for (var y = 0; y < schematic.Count(); y++)
			{
				var num = "";
				for (var x = 0; x < schematic[y].Count(); x++)
				{
					if (schematic[y][x].IsNumber())
					{
						num += schematic[y][x];
					}
					else if (num.Length > 0)
					{
						var gearPos = IsGear(x - num.Length - 1 < 0 ? 0 : x - num.Length - 1, y == 0 ? 0 : y - 1, x, y + 1, schematic[y].Length, schematic.Length, schematic);
						if (gearPos != (-1, -1)) gearParts.Add((gearPos, num.ToInt()));
						num = "";
					}

					if (num.Length > 0 && x == schematic[y].Count() - 1)
					{
						var gearPos = IsGear(x - num.Length - 1 < 0 ? 0 : x - num.Length - 1, y == 0 ? 0 : y - 1, x, y + 1, schematic[y].Length, schematic.Length, schematic);
						if (gearPos != (-1, -1)) gearParts.Add((gearPos, num.ToInt()));
					}
				}
			}

			return gearParts.GroupBy(x => x.Item1).Where(g => g.Count() > 1).Select(g => g.Aggregate(1, (a, b) => a * b.Item2)).Sum();
		}

		protected bool IsEnginePart(int xs, int ys, int xe, int ye, int maxX, int maxY, char[][] schematic)
		{
			for (var y = ys; y <= ye && y < maxY; y++)
			{
				if (y < 0) continue;
				for (var x = xs; x <= xe && x < maxX; x++)
				{
					if (x < 0) continue;
					if (schematic[y][x] != '.' && !schematic[y][x].IsNumber()) return true;
				}
			}

			return false;
		}

		protected (int x, int y) IsGear(int xs, int ys, int xe, int ye, int maxX, int maxY, char[][] schematic)
		{
			for (var y = ys; y <= ye && y < maxY; y++)
			{
				if (y < 0) continue;
				for (var x = xs; x <= xe && x < maxX; x++)
				{
					if (x < 0) continue;
					if (schematic[y][x] == '*' && !schematic[y][x].IsNumber()) return (x, y);
				}
			}

			return (-1, -1);
		}
	}
}
