﻿using SuperLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.ExtensionMethods
{
	public static class StringExtensionMethods
	{
		public static int LevenshteinDistance(this string source, string target)
		{
			var n = source.Length;
			var m = target.Length;
			var d = new int[n + 1, m + 1];

			if (n == 0) { return m; }
			if (m == 0) { return n; }

			for (var i = 0; i <= n; d[i, 0] = i++) { }
			for (var j = 0; j <= m; d[0, j] = j++) { }

			for (var i = 1; i <= n; i++)
			{
				for (var j = 1; j <= m; j++)
				{
					var cost = (target[j - 1] == source[i - 1]) ? 0 : 1;
					d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
				}
			}

			return d[n, m];
		}

		public static string ReplaceAll(this string source, string[] tokens, string value)
		{
			return tokens.Aggregate(source, (current, token) => current.Replace(token, value));
		}

		public static string ReplaceAll(this string source, char[] tokens, char value)
		{
			return tokens.Aggregate(source, (current, token) => current.Replace(token, value));
		}

		public static string[] Split(this string s, string splitWith, StringSplitOptions options = StringSplitOptions.None)
		{
			return s.Split(new[] { splitWith }, options);
		}

        public static IEnumerable<T> SplitAndParse<T>(this string input, IEnumerable<string> separators)
        {
            return input.Split(separators.ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(x => (T)Convert.ChangeType(x, typeof(T)));
        }

        public static string TrimEnd(this string input, string suffixToRemove, StringComparison comparisonType = StringComparison.CurrentCulture)
		{
			if (suffixToRemove != null && input.EndsWith(suffixToRemove, comparisonType))
			{
				return input.Substring(0, input.Length - suffixToRemove.Length);
			}

			return input;
		}

		public static (string Head, string Tail) SplitInHalf(this string s)
		{
			return (Head: s[..(s.Length / 2)], Tail: s[(s.Length / 2)..]);
		}

		public static (string Head, string Tail) SplitAt(this string s, int pos)
		{
			return (Head: s[..pos], Tail: s[pos..]);
		}

		public static int ToInt(this string s)
		{
			_ = int.TryParse(s, out int val);
			return val;
		}

		public static long ToLong(this string s)
		{
			_ = long.TryParse(s, out long val);
			return val;
		}
	}
}
