using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using SuperLinq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 7, "", 919137, 2877389)]
	public class AdventOfCode202207 : AdventOfCodeBase
	{
		public AdventOfCode202207(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
            ConcurrentDictionary<string, int> folderStructure = ParseFolderStructure();
            return folderStructure.Where(x => x.Value <= 100000).Sum(x => x.Value);
		}

		protected override object SolvePart2()
		{
			ConcurrentDictionary<string, int> structure = ParseFolderStructure();

			var total = structure.Max(x => x.Value);
			return structure.Where(x => x.Value >= 30000000 - (70000000 - total)).OrderBy(x => x.Value).First().Value;
		}

		private ConcurrentDictionary<string, int> ParseFolderStructure()
		{
			var folderStructure = new ConcurrentDictionary<string, int>();
			var folders = new List<string>();
			Input.ForEach(line =>
			{
				if (line[0] == '$')
				{
					var cmd = line.Split(" ");
					if (cmd[1] == "cd")
					{
						if (cmd[2] == "..") folders.RemoveLast();
						else folders.Add(cmd[2]);
					}
				}
				else
				{
					var entry = line.Split(" ");
					if (entry[0] != "dir")
					{
						for (var i = 0; i <= folders.Count(); i++)
						{
							var path = string.Join("/", folders.ToArray()[..i]);
                            folderStructure.AddOrUpdate(path, int.Parse(entry[0]), (k, v) => v + int.Parse(entry[0]));
						}
					}
				}
			});

			return folderStructure;
        }
	}
}
