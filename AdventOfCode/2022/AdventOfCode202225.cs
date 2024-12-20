using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 25, "Full of Hot Air", "2-0-01==0-1=2212=100", null)]
	public class AdventOfCode202225 : AdventOfCodeBase
	{
		private Dictionary<long, string> DigitToSnafu = new Dictionary<long, string>
		{
			{ -2, "=" },
			{ -1, "-" },
			{ 0, "0" },
			{ 1, "1" },
			{ 2, "2" }
		};

		public AdventOfCode202225(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{

			return DecimalToSNAFU(Input.Sum(n => SNAFUToDecimal(n)));
		}

		protected override object SolvePart2()
		{
			return null;
		}


		private string DecimalToSNAFU(long n)
		{
			var snafu = "";
			var q = n;

			while (q > 0)
			{
				var r = ((q + 2) % 5) - 2;
				q = (q + 2) / 5;
				snafu = DigitToSnafu[r] + snafu;
			}

			return snafu;
		}

		private long SNAFUToDecimal(string snafu)
		{
			var multiplier = (long) Math.Pow(5, snafu.Length - 1);

			var decimalValue = 0L;
			for (var i = 0; i < snafu.Length; i++)
			{
				decimalValue = snafu[i] switch
				{
					'1' => decimalValue + multiplier,
					'2' => decimalValue + 2 * multiplier,
					'=' => decimalValue - 2 * multiplier,
					'-' => decimalValue - multiplier,
					_ => decimalValue
				};

				multiplier /= 5;
			}

			return decimalValue;
		}
	}
}
