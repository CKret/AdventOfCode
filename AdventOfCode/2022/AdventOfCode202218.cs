using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 18, "Boiling Boulders", 3500, 2048)]
	public class AdventOfCode202218 : AdventOfCodeBase
	{
		public AdventOfCode202218(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var a = "2,2,2\r\n1,2,2\r\n3,2,2\r\n2,1,2\r\n2,3,2\r\n2,2,1\r\n2,2,3\r\n2,2,4\r\n2,2,6\r\n1,2,5\r\n3,2,5\r\n2,1,5\r\n2,3,5".Split("\r\n");


			var cubes = Input.Select(l => l.Split(",").Select(int.Parse).ToArray()).ToArray();
			var con = cubes.ToDictionary(cube => cube, cube => new List<int>());

			for (var i = 0; i < cubes.Length - 1; i++)
			{
				for (var j = i + 1; j < cubes.Length; j++)
				{
					var isCon = IsConnected(cubes[i], cubes[j]);
					if (isCon.Item1 > 0 && isCon.Item2 > 0)
					{
						if (!con[cubes[i]].Contains(isCon.Item1))
							con[cubes[i]].Add(isCon.Item1);
						if (!con[cubes[j]].Contains(isCon.Item2))
							con[cubes[j]].Add(isCon.Item2);
					}
				}
			}


			var count = con.Sum(x => 6 - x.Value.Count);

			return count;
		}

		protected override object SolvePart2()
		{
			var a = "2,2,2\r\n1,2,2\r\n3,2,2\r\n2,1,2\r\n2,3,2\r\n2,2,1\r\n2,2,3\r\n2,2,4\r\n2,2,6\r\n1,2,5\r\n3,2,5\r\n2,1,5\r\n2,3,5".Split("\r\n");


			var cubes = Input.Select(l => l.Split(",").Select(int.Parse).ToArray()).ToArray();
			var con = cubes.ToDictionary(cube => cube, cube => new List<int>());

			var minX = cubes.Min(x => x[0]);
			var maxX = cubes.Max(x => x[0]);
			var minY = cubes.Min(x => x[1]);
			var maxY = cubes.Max(x => x[1]);
			var minZ = cubes.Min(x => x[2]);
			var maxZ = cubes.Max(x => x[2]);

			var outside = new List<int[]>();

			var trapped = new List<int[]>();
			for (var i = 0; i < cubes.Length - 1; i++)
			{
				for (var j = i + 1; j < cubes.Length; j++)
				{
					var isCon = IsConnected(cubes[i], cubes[j]);
					//if (AreTrappingAir(cubes[i], cubes)) trapped++;
					if (isCon.Item1 > 0 && isCon.Item2 > 0)
					{
						if (!con[cubes[i]].Contains(isCon.Item1))
							con[cubes[i]].Add(isCon.Item1);
						if (!con[cubes[j]].Contains(isCon.Item2))
							con[cubes[j]].Add(isCon.Item2);
					}
				}
			}
			var count = con.Sum(x => 6 - x.Value.Count);

			var air = 0;
			for (var z = minZ; z < maxZ; z++)
			{
				for (var y = minY; y < maxY; y++)
				{
					for (var x = minX; x < maxX; x++)
					{
						var c = new[] { x, y, z };
						if (!Contains(c, cubes))
						{
							air += IsSurrounded(c, cubes);
							outside.Add(c);
							if (air == 6)
								trapped.Add(c);
						}
					}
				}
			}

			


			var trappedArray = trapped.ToArray();
			var trappedCon = trapped.ToDictionary(cube => cube, cube => new List<int>());
			for (var i = 0; i < trappedArray.Length - 1; i++)
			{
				for (var j = i + 1; j < trappedArray.Length; j++)
				{
					var isCon = IsConnected(trappedArray[i], trappedArray[j]);
					//if (AreTrappingAir(cubes[i], cubes)) trapped++;
					if (isCon.Item1 > 0 && isCon.Item2 > 0)
					{
						trappedCon[trappedArray[i]].Add(isCon.Item1);
						trappedCon[trappedArray[j]].Add(isCon.Item2);
					}
				}
			}
			var trappedCount = trappedCon.Sum(x => 6 - x.Value.Count);




			return air;
		}

		private (int, int) IsConnected(int[] cube1, int[] cube2)
		{
			if (cube1[0] == cube2[0] + 1 && cube1[1] == cube2[1] && cube1[2] == cube2[2]) return (1, 2);
			if (cube1[0] == cube2[0] - 1 && cube1[1] == cube2[1] && cube1[2] == cube2[2]) return (2, 1);

			if (cube1[1] == cube2[1] + 1 && cube1[0] == cube2[0] && cube1[2] == cube2[2]) return (3, 4);
			if (cube1[1] == cube2[1] - 1 && cube1[0] == cube2[0] && cube1[2] == cube2[2]) return (4, 3);

			if (cube1[2] == cube2[2] + 1 && cube1[0] == cube2[0] && cube1[1] == cube2[1]) return (5, 6);
			if (cube1[2] == cube2[2] - 1 && cube1[0] == cube2[0] && cube1[1] == cube2[1]) return (6, 5);

			return (0, 0);
		}

		private int IsSurrounded(int[] coordinate, int[][] cubes)
		{
			var left = new [] {coordinate[0] + 1, coordinate[1], coordinate[2]};
			var right= new [] { coordinate[0] - 1, coordinate[1], coordinate[2] };
			var front = new [] { coordinate[0], coordinate[1] + 1, coordinate[2] };
			var back = new [] { coordinate[0], coordinate[1] - 1, coordinate[2] };
			var up = new [] { coordinate[0], coordinate[1], coordinate[2] + 1 };
			var down = new [] { coordinate[0], coordinate[1], coordinate[2] - 1 };

			var count = 0;

			if (Contains(left, cubes)) count++;
			if (Contains(right, cubes)) count++;
			if (Contains(front, cubes)) count++;
			if (Contains(back, cubes)) count++;
			if (Contains(up, cubes)) count++;
			if (Contains(down, cubes)) count++;

			return count;
		}


		private bool Contains(int[] coordinate, int[][] cubes)
		{
			return cubes.Any(c => c[0] == coordinate[0] && c[1] == coordinate[1] && c[2] == coordinate[2]);
		}
	}
}
