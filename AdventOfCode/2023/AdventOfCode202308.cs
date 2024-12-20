using AdventOfCode.Mathematics;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2023
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2023, 8, "Haunted Wasteland", 13207, 12324145107121)]
	public class AdventOfCode202308 : AdventOfCodeBase
	{
		public AdventOfCode202308(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var instructions = Input.First();
			var map = Input.Skip(2).Select(l => (Position: l.Split(" = (")[0], Element: (Left: l.Split(" = (")[1].Split(", ")[0], Right: l.Split(" = (")[1].Split(", ")[1].TrimEnd(")")))).ToDictionary(m => m.Position, m => m.Element);

			var currentPos = "AAA";

			var nSteps = 0;
			while (currentPos != "ZZZ")
			{
				currentPos = instructions[nSteps % instructions.Length] == 'L' ? map[currentPos].Left : map[currentPos].Right;
				nSteps++;
			}

			return nSteps;
		}

		protected override object SolvePart2()
		{
			var instructions = Input.First();
			var map = Input.Skip(2).Select(l => (Position: l.Split(" = (")[0], Element: (Left: l.Split(" = (")[1].Split(", ")[0], Right: l.Split(" = (")[1].Split(", ")[1].TrimEnd(")")))).ToDictionary(m => m.Position, m => m.Element);

			var startNode = map.Where(m => m.Key.EndsWith("A")).ToList();
			var currentNode = startNode;
			var cycles = new long[startNode.Count];

			var nSteps = 0;
			while (cycles.Any(c => c == 0))
			{
				for (var i = 0; i < currentNode.Count; i++)
				{
					if (cycles[i] > 0) continue;

					var currentPos = currentNode[i].Key;
					if (cycles[i] == 0 && currentPos.EndsWith("Z"))
					{
						cycles[i] = nSteps;
						continue;
					}

					var nextNode = instructions[nSteps % instructions.Length] == 'L' ? map[currentPos].Left : map[currentPos].Right;
					currentNode[i] = new KeyValuePair<string, (string Left, string Right)> (nextNode, map[nextNode]);
				}
				
				nSteps++;
			}
			

			return cycles.LeastCommonMultiple();
		}
	}
}
