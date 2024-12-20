using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text.RegularExpressions;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using AdventOfCode.Mathematics;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 15, "", null, null)]
	public class AdventOfCode202215 : AdventOfCodeBase
	{
		public AdventOfCode202215(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			//var r = new[]
			//{
			//	"Sensor at x=",
			//	" y=",
			//	": closest beacon is at x="
			//};
			//var a = Input.Select(l => l.Replace(r[0], "").Replace(r[1], "").Replace(r[2], ",").Split(",")).Select(x => x.Select(int.Parse).ToArray()).ToArray();
			//var minX = a.Select(l =>  (l.Take(1), l.Skip(2).Take(1))).Select(x => int.Min(x.Item1.First(), x.Item2.First())).Min();
			//var maxX = a.Select(l => (l.Take(1), l.Skip(2).Take(1))).Select(x => int.Max(x.Item1.First(), x.Item2.First())).Max();
			//var minY = a.Select(l => (l.Skip(1).Take(1), l.Skip(3).Take(1))).Select(x => int.Min(x.Item1.First(), x.Item2.First())).Min();
			//var maxY = a.Select(l => (l.Skip(1).Take(1), l.Skip(3).Take(1))).Select(x => int.Max(x.Item1.First(), x.Item2.First())).Max();

			//var spanX = maxX - minX;
			//var spanY = maxY - minY;

			//var notThere = new List<(int X, int Y)>();

			//foreach (var i in a)
			//{
			//	var sensor = (x: i[0], y: i[1]);
			//	var beacon = (x: i[2], y: i[3]);

			//	var dist = sensor.ManhattanDistance(beacon);

			//}
			//return null;
				const int row = 2_000_000;

				var sensorReport = new Dictionary<Point, Point>();

				// Parsing the input.
				foreach (var line in Input)
				{
					var splitData = line.Split(": ");
					var sensorLine = splitData[0].Split(" ");
					var beaconLine = splitData[1].Split(" ");
					var sensorX = int.Parse(sensorLine[2].Split("x=")[1].Split(",")[0]);
					var sensorY = int.Parse(sensorLine[3].Split("y=")[1]);

					var beaconX = int.Parse(beaconLine[4].Split("x=")[1].Split(",")[0]);
					var beaconY = int.Parse(beaconLine[5].Split("y=")[1]);

					sensorReport.Add(new Point(sensorX, sensorY), new Point(beaconX, beaconY));
				}

				// Finding the number of coordinates covered by sensor signals.
				var signals = new HashSet<Point>();
				foreach (var (sensor, beacon) in sensorReport)
				{
					// Find the distance between the beacons and sensors x coordinates.
					var xDistance = sensor.X > beacon.X ?
							sensor.X - beacon.X :
							beacon.X - sensor.X;

					// Find the distance between the beacons and sensors y coordinates.
					var yDistance = sensor.Y > beacon.Y ?
							sensor.Y - beacon.Y :
							beacon.Y - sensor.Y;

					// Find the maximum radius of the sensor's signal area for its closest beacon.
					var signalRadius = xDistance + yDistance;

					// If y = 2_000_000 is not inside the signal area, continue.
					if (row > signalRadius + sensor.Y &&
							row < sensor.Y - signalRadius)
						continue;

					// Find the distance between y = 2_000_000 and the signal area radius.
					var rowDistance = row > sensor.Y ?
							(sensor.Y + signalRadius) - row :
							row - (sensor.Y - signalRadius);

					// Loop through every x coordinate for point that is inside the signal area, and has y = 2_000_000.
					for (var x = sensor.X - rowDistance; x < sensor.X + rowDistance; x++)
						signals.Add(new Point(x, row));
				}

				Day15(Input.ToList());

				return signals.Count;
			}
		struct BeaconLine
		{
			public int X1;
			public int X2;

			public BeaconLine(int x1, int x2)
			{
				X1 = x1;
				X2 = x2;
			}

			public bool Valid
			{
				get
				{
					return (X1 <= X2);
				}
			}

			public int Width
			{
				get
				{
					return X2 - X1 + 1;
				}
			}

			public override string ToString()
			{
				return $"{X1}..{X2}";
			}

			public static BeaconLine Intersection(BeaconLine a, BeaconLine b)
			{
				return new BeaconLine(Math.Max(a.X1, b.X1), Math.Min(a.X2, b.X2));
			}

			public static void Abjunction(List<BeaconLine> lines, BeaconLine a, BeaconLine b)
			{
				BeaconLine intersection = Intersection(a, b);
				if (intersection.Valid)
				{
					//List<BeaconLine> lines = new List<BeaconLine>();
					if (intersection.X1 > a.X1)
					{
						lines.Add(new BeaconLine(a.X1, intersection.X1 - 1));
					}
					if (intersection.X2 < a.X2)
					{
						lines.Add(new BeaconLine(intersection.X2 + 1, a.X2));
					}
					//return lines.ToArray();
				}
				else
				{
					lines.Add(a);
					//return new BeaconLine[] { a };
				}
			}
		}

		// [UseSRL] // Uncomment if you wanna use SuperReadLine
		//[NoTrailingNewLine] // Uncomment to not include an extra blank line in the input at the end
		static void Day15(List<string> input)
		{
			var beacons = new List<(int X, int Y, int X2, int Y2, int Size)>();
			var beaconMap = new Dictionary<(int X, int Y), bool>();

			Regex coords = new Regex(@"x=(-?\d+), y=(-?\d+)", RegexOptions.Compiled);

			int maxX = 0;

			foreach (var line in input)
			{
				var match = coords.Matches(line);

				(int x, int y) = (int.Parse(match[0].Groups[1].Value), int.Parse(match[0].Groups[2].Value));
				(int x2, int y2) = (int.Parse(match[1].Groups[1].Value), int.Parse(match[1].Groups[2].Value));

				int size = Math.Abs(x - x2) + Math.Abs(y - y2);
				maxX = Math.Max(Math.Max(maxX, x + size), x2);

				beacons.Add((x, y, x2, y2, size));
				beaconMap[(x2, y2)] = true;
			}

			bool test = maxX < 100;
			int onRow = test ? 10 : 2000000;
			int scanMax = test ? 20 : 4000000;

			int rowBlocked = 0;
			{
				int y = onRow;
				var lines = new List<BeaconLine>();
				var lines2 = new List<BeaconLine>();
				for (int i = 0; i < beacons.Count; i++)
				{
					var beacon = beacons[i];
					int distance = Math.Abs(y - beacon.Y);
					if (distance <= beacon.Size)
					{
						int size = beacon.Size - distance;
						BeaconLine newLine = new BeaconLine(beacon.X - size, beacon.X + size);
						for (int j = 0; j < lines.Count; j++)
						{
							BeaconLine.Abjunction(lines2, lines[j], newLine);
						}
						lines2.Add(newLine);
						(lines, lines2) = (lines2, lines);
						lines2.Clear();
					}
				}
				rowBlocked = lines.Sum(line => line.Width) - beaconMap.Count(line => line.Key.Y == y);
			}
			Console.WriteLine($"Part 1: {rowBlocked}");

			long tuning = 0;
			for (int y = 0; y <= scanMax; y++)
			{
				var lines = new List<BeaconLine>();
				for (int i = 0; i < beacons.Count; i++)
				{
					var beacon = beacons[i];
					int distance = Math.Abs(y - beacon.Y);
					if (distance <= beacon.Size)
					{
						int size = beacon.Size - distance;
						BeaconLine newLine = new BeaconLine(beacon.X - size, beacon.X + size);
						lines.Add(newLine);
					}
				}

				bool first = false;
				int prevX = int.MinValue;
				foreach (var line in lines.OrderBy(line => line.X1))
				{
					if (first && line.X1 > prevX && line.X1 - prevX != 1)
					{
						int x = prevX + 1;
						if (0 <= x && x <= scanMax && 0 <= y && y <= scanMax)
						{
							tuning = x * 4000000L + y;
						}
					}
					first = true;
					prevX = Math.Max(prevX, line.X2);
				}

				if (tuning != 0) break;
			}
			Console.WriteLine($"Part 2: {tuning}");
		}
		protected override object SolvePart2()
		{
			return null;
		}
	}
}
