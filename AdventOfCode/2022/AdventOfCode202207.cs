using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 7, "No Space Left On Device", 919137, 2877389)]
	public class AdventOfCode202207 : AdventOfCodeBase
	{
		public AdventOfCode202207(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			return ParseFolderStructure().Where(x => x.Value <= 100000).Sum(x => x.Value);
		}

		protected override object SolvePart2()
		{
			var folderStructure = ParseFolderStructure();
			return folderStructure.Where(x => x.Value >= 30000000 - (70000000 - folderStructure.Max(x => x.Value))).MinBy(x => x.Value).Value;
		}

		private ConcurrentDictionary<string, int> ParseFolderStructure()
		{
			var folderStructure = new ConcurrentDictionary<string, int>();
			var folders = new List<string>();
			Input.ForEach(line =>
			{
				var cmd = line.Split(' ');
				if (cmd[0] == "$")
				{
					if (cmd[1] == "cd")
						if (cmd[2] == "..") folders.RemoveLast();
						else folders.Add(cmd[2]);
				}
				else if (cmd[0] != "dir")
					for (var i = 0; i <= folders.Count; i++)
						folderStructure.AddOrUpdate(string.Join("/", folders.ToArray()[..i]), int.Parse(cmd[0]), (_, v) => v + int.Parse(cmd[0]));
			});

			return folderStructure;
		}
	}
}
