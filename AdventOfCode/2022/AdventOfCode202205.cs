using AdventOfCode.Core;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 5, "", "ZSQVCCJLL", "QZFJRWHGS")]
	public class AdventOfCode202205 : AdventOfCodeBase
	{
		public AdventOfCode202205(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var drawing = Input
				.TakeWhile(x => x != string.Empty)
				.ToArray();

			var procedures = Input
				.SkipUntil(x => x == string.Empty)
				.Select(x => x
					.Split(new[] { "move", "from", "to" }, System.StringSplitOptions.TrimEntries | System.StringSplitOptions.RemoveEmptyEntries)
					.Select(int.Parse)
					.ToArray()
				)
				.Select(x => new { Move = x[0], From = x[1] - 1, To = x[2] - 1 })
				.ToArray();

			var stacks = Enumerable
				.Range(0, 9)
				.Select(x => new Stack<char>())
				.ToArray();


			drawing.Reverse().Skip(1).ForEach(x =>
			{
				for (var j = 0; j < stacks.Length; j++)
				{
					var c = x[j * 4 + 1];
					if (c != ' ')
						stacks[j].Push(c);
				}
			});

			procedures.ForEach(p =>
				Enumerable
					.Range(0, p.Move)
					.ForEach(x => stacks[p.To].Push(stacks[p.From].Pop())) 
				);

			return string.Join("", stacks.Select(x => x.Peek()));
		}

		protected override object SolvePart2()
		{
			var drawing = Input
				.TakeWhile(x => x != string.Empty)
				.ToArray();

			var procedures = Input
				.SkipUntil(x => x == "")
				.Select(x => x
					.Split(new[] { "move", "from", "to" }, System.StringSplitOptions.TrimEntries | System.StringSplitOptions.RemoveEmptyEntries)
					.Select(int.Parse)
					.ToArray()
				)
				.Select(x => new { Move = x[0], From = x[1], To = x[2] })
				.ToArray();

			var stacks = Enumerable
				.Range(0, 9)
				.Select(x => new Stack<char>())
				.ToArray();

			drawing.Reverse().Skip(1).ForEach(x =>
			{
				for (var j = 0; j < stacks.Length; j++)
				{
					var c = x[j * 4 + 1];
					if (c != ' ')
						stacks[j].Push(c);
				}
			});

			procedures.ForEach(p =>
			{
				var tmp = new Stack<char>();
				Enumerable
					.Range(0, p.Move)
					.ForEach(x =>
					{
						tmp.Push(stacks[p.From - 1].Pop());
					}
				);
				Enumerable
					.Range(0, p.Move)
					.ForEach(x =>
					{
						stacks[p.To - 1].Push(tmp.Pop());
					}
				);
			});

			return string.Join("", stacks.Select(x => x.Peek()));
		}
	}
}
