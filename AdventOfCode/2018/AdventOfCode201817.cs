using System.Text.RegularExpressions;
using AdventOfCode.Core;

namespace AdventOfCode._2018
{
	/// <summary>
	/// --- Day 17: Reservoir Research ---
	/// 
	/// You arrive in the year 18. If it weren't for the coat you got in 1018, you
	/// would be very cold: the North Pole base hasn't even been constructed.
	/// 
	/// Rather, it hasn't been constructed yet. The Elves are making a little
	/// progress, but there's not a lot of liquid water in this climate, so they're
	/// getting very dehydrated. Maybe there's more underground?
	/// 
	/// You scan a two-dimensional vertical slice of the ground nearby and discover
	/// that it is mostly sand with veins of clay. The scan only provides data with
	/// a granularity of square meters, but it should be good enough to determine
	/// how much water is trapped there. In the scan, x represents the distance to
	/// the right, and y represents the distance down. There is also a spring of
	/// water near the surface at x=500, y=0. The scan identifies which square
	/// meters are clay (your puzzle input).
	/// 
	/// For example, suppose your scan shows the following veins of clay:
	/// 
	/// x=495, y=2..7
	/// y=7, x=495..501
	/// x=501, y=3..7
	/// x=498, y=2..4
	/// x=506, y=1..2
	/// x=498, y=10..13
	/// x=504, y=10..13
	/// y=13, x=498..504
	/// 
	/// Rendering clay as #, sand as ., and the water spring as +, and with x
	/// increasing to the right and y increasing downward, this becomes:
	/// 
	///   44444455555555
	///   99999900000000
	///   45678901234567
	/// 0 ......+.......
	/// 1 ............#.
	/// 2 .#..#.......#.
	/// 3 .#..#..#......
	/// 4 .#..#..#......
	/// 5 .#.....#......
	/// 6 .#.....#......
	/// 7 .#######......
	/// 8 ..............
	/// 9 ..............
	/// 10 ....#.....#...
	/// 11 ....#.....#...
	/// 12 ....#.....#...
	/// 
	/// 13 ....#######...
	/// 
	/// The spring of water will produce water forever. Water can move through
	/// sand, but is blocked by clay. Water always moves down when possible, and
	/// spreads to the left and right otherwise, filling space that has clay on
	/// both sides and falling out otherwise.
	/// 
	/// For example, if five squares of water are created, they will flow downward
	/// until they reach the clay and settle there. Water that has come to rest is
	/// shown here as ~, while sand through which water has passed (but which is
	/// now dry again) is shown as |:
	/// 
	/// ......+.......
	/// ......|.....#.
	/// .#..#.|.....#.
	/// .#..#.|#......
	/// .#..#.|#......
	/// .#....|#......
	/// .#~~~~~#......
	/// .#######......
	/// ..............
	/// ..............
	/// ....#.....#...
	/// ....#.....#...
	/// ....#.....#...
	/// ....#######...
	/// 
	/// Two squares of water can't occupy the same location. If another five
	/// squares of water are created, they will settle on the first five, filling
	/// the clay reservoir a little more:
	/// 
	/// ......+.......
	/// ......|.....#.
	/// .#..#.|.....#.
	/// .#..#.|#......
	/// .#..#.|#......
	/// .#~~~~~#......
	/// .#~~~~~#......
	/// .#######......
	/// ..............
	/// ..............
	/// ....#.....#...
	/// ....#.....#...
	/// ....#.....#...
	/// ....#######...
	/// 
	/// Water pressure does not apply in this scenario. If another four squares of
	/// water are created, they will stay on the right side of the barrier, and no
	/// water will reach the left side:
	/// 
	/// ......+.......
	/// ......|.....#.
	/// .#..#.|.....#.
	/// .#..#~~#......
	/// .#..#~~#......
	/// .#~~~~~#......
	/// .#~~~~~#......
	/// .#######......
	/// ..............
	/// ..............
	/// ....#.....#...
	/// ....#.....#...
	/// ....#.....#...
	/// ....#######...
	/// 
	/// At this point, the top reservoir overflows. While water can reach the tiles
	/// above the surface of the water, it cannot settle there, and so the next
	/// five squares of water settle like this:
	/// 
	/// ......+.......
	/// ......|.....#.
	/// .#..#||||...#.
	/// .#..#~~#|.....
	/// .#..#~~#|.....
	/// .#~~~~~#|.....
	/// .#~~~~~#|.....
	/// .#######|.....
	/// ........|.....
	/// ........|.....
	/// ....#...|.#...
	/// ....#...|.#...
	/// ....#~~~~~#...
	/// ....#######...
	/// Note especially the leftmost |: the new squares of water can reach this
	/// tile, but cannot stop there. Instead, eventually, they all fall to the
	/// right and settle in the reservoir below.
	/// 
	/// After 10 more squares of water, the bottom reservoir is also full:
	/// 
	/// ......+.......
	/// ......|.....#.
	/// .#..#||||...#.
	/// .#..#~~#|.....
	/// .#..#~~#|.....
	/// .#~~~~~#|.....
	/// .#~~~~~#|.....
	/// .#######|.....
	/// ........|.....
	/// ........|.....
	/// ....#~~~~~#...
	/// ....#~~~~~#...
	/// ....#~~~~~#...
	/// ....#######...
	/// 
	/// Finally, while there is nowhere left for the water to settle, it can reach
	/// a few more tiles before overflowing beyond the bottom of the scanned data:
	/// 
	/// ......+.......    (line not counted: above minimum y value)
	/// ......|.....#.
	/// .#..#||||...#.
	/// .#..#~~#|.....
	/// .#..#~~#|.....
	/// .#~~~~~#|.....
	/// .#~~~~~#|.....
	/// .#######|.....
	/// ........|.....
	/// ...|||||||||..
	/// ...|#~~~~~#|..
	/// ...|#~~~~~#|..
	/// ...|#~~~~~#|..
	/// ...|#######|..
	/// ...|.......|..    (line not counted: below maximum y value)
	/// ...|.......|..    (line not counted: below maximum y value)
	/// ...|.......|..    (line not counted: below maximum y value)
	/// 
	/// How many tiles can be reached by the water? To prevent counting forever,
	/// ignore tiles with a y coordinate smaller than the smallest y coordinate in
	/// your scan data or larger than the largest one. Any x coordinate is valid.
	/// In this example, the lowest y coordinate given is 1, and the highest is 13,
	/// causing the water spring (in row 0) and the water falling off the bottom of
	/// the render (in rows 14 through infinity) to be ignored.
	/// 
	/// So, in the example above, counting both water at rest (~) and other sand
	/// tiles the water can hypothetically reach (|), the total number of tiles the
	/// water can reach is 57.
	/// 
	/// How many tiles can the water reach within the range of y values in your
	/// scan?
	/// 
	/// --- Part Two ---
	/// 
	/// After a very long time, the water spring will run dry. How much water will
	/// be retained?
	/// 
	/// In the example above, water that won't eventually drain out is shown as ~,
	/// a total of 29 tiles.
	/// 
	/// How many water tiles are left after the water spring stops producing water
	/// and all remaining water not at rest has drained?
	/// </summary>
	[AdventOfCode(2018, 17, "Reservoir Research", 31883, 24927)]
	public class AdventOfCode201817 : AdventOfCodeBase
	{
		private readonly char[][] scanSlice = new char[2000][];

		private int minx = 2000;
		private int maxx = 0;

		private int miny = 2000;
		private int maxy = 0;

		public AdventOfCode201817(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var data = Input;

			var spring = new { X = 500, Y = 0 };

			for (var i = 0; i < 2000; i++)
			{
				scanSlice[i] = new char[2000];
			}


			for (var y = 0; y < 2000; y++)
			{
				for (var x = 0; x < 2000; x++)
				{
					scanSlice[y][x] = '.';
				}
			}
			scanSlice[spring.Y][spring.X] = '+';

			foreach (var line in data)
			{
				var coords = line.Split(", ");
				var xCoord = coords[0].StartsWith("x=") ? 0 : 1;

				var m = Regex.Matches(coords[xCoord], @"\d+");
				var x1 = int.Parse(m[0].Value);
				var x2 = m.Count == 2 ? int.Parse(m[1].Value) : x1;

				m = Regex.Matches(coords[++xCoord % 2], @"\d+");
				var y1 = int.Parse(m[0].Value);
				var y2 = m.Count == 2 ? int.Parse(m[1].Value) : y1;

				MinMax(x1, y1);
				MinMax(x2, y2);

				for (var y = y1; y <= y2; y++)
				{
					for (var x = x1; x <= x2; x++)
					{
						scanSlice[y][x] = '#';
					}
				}
			}

			Fill(spring.X, spring.Y);
			//var total = 0;
			var touched = 0;
			var water = 0;
			for (var y = miny; y <= maxy; y++)
			{
				for (var x = minx - 1; x <= maxx + 1; x++)
				{
					if (scanSlice[y][x] == '|')
					{
						touched++;
					}
					else if (scanSlice[y][x] == '~')
					{
						water++;
					}
				}
			}

			return touched + water;
		}

		protected override object SolvePart2()
		{
			return null;
		}

		private void MinMax(int x, int y)
		{
			if (x < minx) minx = x;
			if (x > maxx) maxx = x;
			if (y < miny) miny = y;
			if (y > maxy) maxy = y;
		}

		private bool IsOpen(int x, int y)
		{
			//return scanSlice[y][x] == '#';
			return scanSlice[y][x] == '+' || scanSlice[y][x] == '.' || scanSlice[y][x] == '|';
		}

		private void Fill(int x, int y)
		{
			if (y > maxy)
				return;

			if (!IsOpen(x, y))
				return;


			if (!IsOpen(x, y + 1))
			{
				var leftX = x;

				while (IsOpen(leftX, y) && !IsOpen(leftX, y + 1))
				{
					scanSlice[y][leftX] = '|';
					leftX--;

				}
				var rightX = x + 1;
				while (IsOpen(rightX, y) && !IsOpen(rightX, y + 1))
				{
					scanSlice[y][rightX] = '|';
					rightX++;

				}
				if (IsOpen(leftX, y + 1) || IsOpen(rightX, y + 1))
				{
					Fill(leftX, y);
					Fill(rightX, y);
				}
				else if (scanSlice[y][leftX] == '#' && scanSlice[y][rightX] == '#')
				{
					for (var x2 = leftX + 1; x2 < rightX; x2++)
					{
						scanSlice[y][x2] = '~';
					}
				}
			}
			else if (scanSlice[y][x] == '.' || scanSlice[y][x] == '+')
			{
				scanSlice[y][x] = '|';
				Fill(x, y + 1);

				if (scanSlice[y + 1][x] == '~')
				{
					Fill(x, y);
				}
			}
		}
	}
}
