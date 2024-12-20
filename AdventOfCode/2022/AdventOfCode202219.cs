using System;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 19, "", null, null)]
	public class AdventOfCode202219 : AdventOfCodeBase
	{
		public AdventOfCode202219(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var blueprints = Input.Select(bp => Regex.Matches(bp, "\\d+")).Select(bp =>
			(
				BlueprintId: bp[0].Value.ToInt(),
				OreRobotCost: bp[1].Value.ToInt(),
				ClayRobotCost: bp[2].Value.ToInt(),
				ObsidianRobotCost: (Ore: bp[3].Value.ToInt(), Clay: bp[4].Value.ToInt()),
				GeodeRobotCost: (Ore: bp[5].Value.ToInt(), Obsidian: bp[6].Value.ToInt())
			));


			foreach (var bp in blueprints)
			{
				Console.WriteLine(bp);
			}

			return null;
		}

		protected override object SolvePart2()
		{
			return null;
		}
	}
}
