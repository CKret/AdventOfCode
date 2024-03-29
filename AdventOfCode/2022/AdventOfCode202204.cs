using AdventOfCode.Core;
using System.Linq;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 4, "Camp Cleanup", 490, 921)]
	public class AdventOfCode202204 : AdventOfCodeBase
	{
		public AdventOfCode202204(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var a = Input.Select(x => x.Split(",").Select(y => y.Split("-").Select(int.Parse)).Select(a => Enumerable.Range(a.First(), a.Last() - a.First() + 1)));
			var b = a.Select(x => x.Skip(1).Aggregate(x.First(), (p, c) => c.Intersect(p)));

			var count = 0;
			for (var i = 0; i < a.Count(); i++)
			{
				var curA = a.Skip(i).First();
				var curB = b.Skip(i).First();

				for (var j = 0; j < curA.Count(); j++)
				{
					var curAA = curA.Skip(j).First();

					if (curAA.All(x => curB.Contains(x)))
					{
						count++;
						break;
					}
				}
			}

			return count;
		}

		protected override object SolvePart2()
		{
			var a = Input.Select(x => x.Split(",").Select(y => y.Split("-").Select(int.Parse)).Select(a => Enumerable.Range(a.First(), a.Last() - a.First() + 1)));

			var count = Input.Length;
			for (var i = 0; i < a.Count(); i++)
			{
				var curA = a.Skip(i).First();
				var curA1 = curA.First();
				var curA2 = curA.Skip(1).First();

				for (var j = 0; j < curA1.Count(); j++)
				{
					if (curA2.All(x => !curA1.Contains(x)))
					{
						count--;
						break;
					}
				}
			}

			return count;
		}
	}
}
