﻿using System;
using System.Globalization;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2018
{
	/// <summary>
	/// --- Day 3: No Matter How You Slice It ---
	/// 
	/// The Elves managed to locate the chimney-squeeze prototype fabric for
	/// Santa's suit (thanks to someone who helpfully wrote its box IDs on the wall
	/// of the warehouse in the middle of the night). Unfortunately, anomalies are
	/// still affecting them - nobody can even agree on how to cut the fabric.
	/// 
	/// The whole piece of fabric they're working on is a very large square - at
	/// least 1000 inches on each side.
	/// 
	/// Each Elf has made a claim about which area of fabric would be ideal for
	/// Santa's suit. All claims have an ID and consist of a single rectangle with
	/// edges parallel to the edges of the fabric. Each claim's rectangle is
	/// defined as follows:
	/// 
	/// - The number of inches between the left edge of the fabric and the left
	///   edge of the rectangle.
	/// - The number of inches between the top edge of the fabric and the top
	///   edge of the rectangle.
	/// - The width of the rectangle in inches.
	/// - The height of the rectangle in inches.
	/// 
	/// A claim like #123 @ 3,2: 5x4 means that claim ID 123 specifies a rectangle
	/// 3 inches from the left edge, 2 inches from the top edge, 5 inches wide, and
	/// 4 inches tall. Visually, it claims the square inches of fabric represented
	/// by # (and ignores the square inches of fabric represented by .) in the
	/// diagram below:
	/// 
	/// ...........
	/// ...........
	/// ...#####...
	/// ...#####...
	/// ...#####...
	/// ...#####...
	/// ...........
	/// ...........
	/// ...........
	/// The problem is that many of the claims overlap, causing two or more claims
	/// to cover part of the same areas. For example, consider the following
	/// claims:
	/// 
	/// #1 @ 1,3: 4x4
	/// #2 @ 3,1: 4x4
	/// #3 @ 5,5: 2x2
	/// Visually, these claim the following areas:
	/// 
	/// ........
	/// ...2222.
	/// ...2222.
	/// .11XX22.
	/// .11XX22.
	/// .111133.
	/// .111133.
	/// ........
	/// The four square inches marked with X are claimed by both 1 and 2. (Claim 3,
	/// while adjacent to the others, does not overlap either of them.)
	/// 
	/// If the Elves all proceed with their own plans, none of them will have
	/// enough fabric. How many square inches of fabric are within two or more
	/// claims?
	///
	/// --- Part Two ---
	/// 
	/// Amidst the chaos, you notice that exactly one claim doesn't overlap by even
	/// a single square inch of fabric with any other claim. If you can somehow
	/// draw attention to it, maybe the Elves will be able to make Santa's suit
	/// after all!
	/// 
	/// For example, in the claims above, only claim 3 is intact after all claims
	/// are made.
	/// 
	/// What is the ID of the only claim that doesn't overlap?
	/// </summary>
	[AdventOfCode(2018, 3, "No Matter How You Slice It", 110389, 552)]
	public class AdventOfCode201803 : AdventOfCodeBase
	{
		public AdventOfCode201803(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var claims = Input
											 .Select(c => c.ReplaceAll(new[] { "#", "@", ":" }, "").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
											 .Select(c => new Claim
											 {
												 Id = int.Parse(c[0], CultureInfo.InvariantCulture),
												 Left = int.Parse(c[1].Split(',')[0], CultureInfo.InvariantCulture),
												 Top = int.Parse(c[1].Split(',')[1], CultureInfo.InvariantCulture),
												 Width = int.Parse(c[2].Split('x')[0], CultureInfo.InvariantCulture),
												 Height = int.Parse(c[2].Split('x')[1], CultureInfo.InvariantCulture)
											 })
											 .ToList();

			var fabric = new int[1000, 1000];

			foreach (var claim in claims)
			{
				for (var x = claim.Left; x < claim.Left + claim.Width; x++)
				{
					for (var y = claim.Top; y < claim.Top + claim.Height; y++)
					{
						fabric[x, y]++;
					}
				}
			}

			var count = 0;
			for (var x = 0; x < 1000; x++)
			{
				for (var y = 0; y < 1000; y++)
				{
					if (fabric[x, y] > 1) count++;
				}
			}

			return count;
		}

		protected override object SolvePart2()
		{
			var claims = Input
											 .Select(c => c.ReplaceAll(new[] { "#", "@", ":" }, "").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
											 .Select(c => new Claim
											 {
												 Id = int.Parse(c[0], CultureInfo.InvariantCulture),
												 Left = int.Parse(c[1].Split(',')[0], CultureInfo.InvariantCulture),
												 Top = int.Parse(c[1].Split(',')[1], CultureInfo.InvariantCulture),
												 Width = int.Parse(c[2].Split('x')[0], CultureInfo.InvariantCulture),
												 Height = int.Parse(c[2].Split('x')[1], CultureInfo.InvariantCulture)
											 })
											 .ToList();

			var fabric = new int[1000, 1000];

			foreach (var claim in claims)
			{
				for (var x = claim.Left; x < claim.Left + claim.Width; x++)
				{
					for (var y = claim.Top; y < claim.Top + claim.Height; y++)
					{
						fabric[x, y]++;
					}
				}
			}

			foreach (var claim in claims)
			{
				var isSingle = true;
				for (var x = claim.Left; x < claim.Left + claim.Width && isSingle; x++)
				{
					for (var y = claim.Top; y < claim.Top + claim.Height; y++)
					{
						if (fabric[x, y] == 1) continue;

						isSingle = false;
						break;
					}
				}

				if (isSingle)
				{
					return claim.Id;
				}
			}

			return null;
		}
	}

	public class Claim
	{
		public int Id { get; set; }
		public int Left { get; set; }
		public int Top { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
	}
}
