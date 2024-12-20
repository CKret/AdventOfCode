using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using SuperLinq;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 5, "Supply Stacks", "ZSQVCCJLL", "QZFJRWHGS")]
	public class AdventOfCode202205 : AdventOfCodeBase
	{
		public AdventOfCode202205(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var drawing = Input
				.TakeWhile(x => x != string.Empty)
				.Reverse()
				.Skip(1);

			var procedures = Input
				.SkipUntil(x => x == string.Empty)
				.Select(x => x
					.SplitAndParse<int>(new[] { "move", "from", "to" })
					.ToArray()
				)
				.Select(x => new { Move = x[0], From = x[1] - 1, To = x[2] - 1 });

			var stacks = Enumerable
				.Range(0, 9)
				.Select(x => new Stack<char>())
				.ToArray();

			drawing.ForEach(line => stacks.ForEach((s, i) => { if (line[i * 4 + 1] != ' ') s.Push(line[i * 4 + 1]); }));

			procedures.ForEach(p => Enumerable.Range(0, p.Move).ForEach(x => stacks[p.To].Push(stacks[p.From].Pop())));

			return string.Join("", stacks.Select(x => x.Peek()));
		}

		protected override object SolvePart2()
		{
			var drawing = Input
				.TakeWhile(x => x != string.Empty)
				.Reverse()
				.Skip(1);

			var procedures = Input
				.SkipUntil(x => x == "")
				.Select(x => x
                    .SplitAndParse<int>(new[] { "move", "from", "to" })
                    .ToArray()
				)
				.Select(x => new { Move = x[0], From = x[1] - 1, To = x[2] - 1 });

			var stacks = Enumerable
				.Range(0, 9)
				.Select(x => new Stack<char>())
				.ToArray();

			drawing.ForEach(line => stacks.ForEach((s, i) => { if (line[i * 4 + 1] != ' ') s.Push(line[i * 4 + 1]); }));

			var tmp = new Stack<char>();
			procedures.ForEach(p =>
			{
				Enumerable.Range(0, p.Move).ForEach(x => tmp.Push(stacks[p.From].Pop()));
				Enumerable.Range(0, p.Move).ForEach(x => stacks[p.To].Push(tmp.Pop()));
			});

			return string.Join("", stacks.Select(x => x.Peek()));
		}
	}
}
