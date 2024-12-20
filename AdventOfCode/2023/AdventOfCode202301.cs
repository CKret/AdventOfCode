using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2023
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2023, 1, "Trebuchet?!", 54990, 54473)]
	public class AdventOfCode202301 : AdventOfCodeBase
	{

		public AdventOfCode202301(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			return Input
				.Select(x => x.Where(c => char.IsDigit(c)))
				.Sum(x => (x.First().ToString() + x.Last())
				.ToInt());
		}

		protected override object SolvePart2()
		{
			string[] digits = [ "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" ];

			var values = new List<int>();
			foreach (var line in Input)
			{
				var currentVal = 0;
				var numbers = line.Where(char.IsDigit);

				var firstIndices = digits.Select((n, i) => (Number: i + 1, Index: line.IndexOf(n))).Where(n => n.Index > -1);
				var lastIndices = digits.Select((n, i) => (Number: i + 1, Index: line.LastIndexOf(n))).Where(n => n.Index > -1);

				var firstString = firstIndices.IsEmpty() ? (-1, int.MaxValue) : firstIndices.MinBy(n => n.Index);
				var lastString = lastIndices.IsEmpty() ? (-1, int.MinValue) : lastIndices.MaxBy(n => n.Index);

				var firstNumber = line.IndexOf(numbers.First());
				var lastNumber = line.LastIndexOf(numbers.Last());

				if (firstString.Item2 < firstNumber) currentVal = firstString.Item1 * 10;
				else currentVal = numbers.First().ToInt() * 10;

				if (lastString.Item2 > lastNumber) currentVal += lastString.Item1;
				else currentVal += numbers.Last().ToInt();

				values.Add(currentVal);
			}

			return values.Sum();
		}
	}
}
