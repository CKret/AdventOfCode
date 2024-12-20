﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Mathematics
{
	public struct PrimeFactor : IEquatable<PrimeFactor>
	{
		public int Base { get; set; }
		public int Exponent { get; set; }

		public override int GetHashCode() => Base ^ Exponent;

		public override bool Equals(object obj)
		{
			return obj is PrimeFactor pf && Equals(pf);
		}

		public bool Equals(PrimeFactor other)
		{
			if (Base != other.Base) return false;
			return Exponent == other.Exponent;
		}

		public static bool operator ==(PrimeFactor pf1, PrimeFactor pf2)
		{
			return pf1.Equals(pf2);
		}

		public static bool operator !=(PrimeFactor pf1, PrimeFactor pf2)
		{
			return !pf1.Equals(pf2);
		}
	}

	public static class NumberTheory
	{
		private static readonly Dictionary<long, long> sumOfDivisors = new Dictionary<long, long>();
		private static readonly Dictionary<long, long> sumOfProperDivisors = new Dictionary<long, long>();

		public static long SumOfDivisors(this int number) { return SumOfDivisors((long)number); }

		public static long SumOfDivisors(this long number)
		{
			if (number < 1 || number > Int32.MaxValue) throw new ArgumentException("Number must be greater than 0 and less or equal to Int32.MaxValue", nameof(number));
			if (sumOfDivisors.ContainsKey(number)) return sumOfDivisors[number];

			var sum = 1L;
			for (var i = 2; i <= number; i++)
				if (number % i == 0) sum += i;

			sumOfDivisors.Add(number, sum);

			return sum;
		}

		public static long SumOfProperDivisors(this int number) { return SumOfProperDivisors((long)number); }

		public static long SumOfProperDivisors(this long number)
		{
			if (number < 1 || number > Int32.MaxValue) throw new ArgumentException("Number must be greater than 0 and less or equal to Int32.MaxValue", nameof(number));
			if (sumOfProperDivisors.ContainsKey(number)) return sumOfProperDivisors[number];

			var sum = 1L;
			for (var i = 2; i <= number / 2; i++)
				if (number % i == 0) sum += i;

			sumOfProperDivisors.Add(number, sum);

			return sum;
		}

		public static BigInteger Factorial(BigInteger n)
		{
			BigInteger fac = 1;
			for (BigInteger i = 1; i <= n; i++)
				fac *= i;
			return fac;
		}

		public static bool IsPalindrome(this BigInteger number)
		{
			var num = number;
			BigInteger reverse = 0;
			while (num > 0)
			{
				var digit = num % 10;
				reverse = reverse * 10 + digit;
				num = num / 10;
			}

			return number == reverse;
		}

		public static bool IsPalindrome(this long number)
		{
			var num = number;
			var reverse = 0L;
			while (num > 0)
			{
				var digit = num % 10;
				reverse = reverse * 10 + digit;
				num = num / 10;
			}

			return number == reverse;
		}

		public static bool IsPalindrome(this string number)
		{
			for (var i = 0; i < number.Length / 2; i++)
				if (number[i] != number[number.Length - i - 1]) return false;

			return true;
		}

		public static IEnumerable<PrimeFactor> CountOccurances(this IEnumerable<int> items)
		{
			return items.GroupBy(n => n).Select(g => new PrimeFactor { Base = g.Key, Exponent = g.Count() });
		}

		public static long NextPermutation(this long num)
		{
			var s = num.ToString(CultureInfo.InvariantCulture);
			var numList = new int[s.Length];
			for (var i = 0; i < s.Length; i++)
			{
				numList[i] = int.Parse(s[i].ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
			}

			if (!NextPermutation(ref numList)) return 0;

			s = string.Empty;
			foreach (var n in numList)
			{
				s += n.ToString(CultureInfo.InvariantCulture);
			}


			return int.Parse(s, CultureInfo.InvariantCulture);
		}

		public static bool NextPermutation(ref int[] numList)
		{
			var tmpList = numList.Select(x => (long)x).ToArray();
			var result = NextPermutation(tmpList);
			numList = tmpList.Select(x => (int)x).ToArray();
			return result;
		}
		public static bool NextPermutation(this long[] numList)
		{
			/*
 Knuths
 1. Find the largest index j such that a[j] < a[j + 1]. If no such index exists, the permutation is the last permutation.
 2. Find the largest index l such that a[j] < a[l]. Since j + 1 is such an index, l is well defined and satisfies j < l.
 3. Swap a[j] with a[l].
 4. Reverse the sequence from a[j + 1] up to and including the final element a[n].

 */
			var largestIndex = -1;
			for (var i = numList.Length - 2; i >= 0; i--)
			{
				if (numList[i] < numList[i + 1])
				{
					largestIndex = i;
					break;
				}
			}

			if (largestIndex < 0) return false;

			var largestIndex2 = -1;
			for (var i = numList.Length - 1; i >= 0; i--)
			{
				if (numList[largestIndex] < numList[i])
				{
					largestIndex2 = i;
					break;
				}
			}

			var tmp = numList[largestIndex];
			numList[largestIndex] = numList[largestIndex2];
			numList[largestIndex2] = tmp;

			for (int i = largestIndex + 1, j = numList.Length - 1; i < j; i++, j--)
			{
				tmp = numList[i];
				numList[i] = numList[j];
				numList[j] = tmp;
			}

			return true;
		}

		public static bool IsPermutation(this int a, int b)
		{
			return IsPermutation((long)a, b);
		}

		public static bool IsPermutation(this long a, long b)
		{
			var str1 = a.ToString(CultureInfo.InvariantCulture).ToCharArray();
			var str2 = b.ToString(CultureInfo.InvariantCulture).ToCharArray();

			if (str1.Length != str2.Length)
				return false;

			Array.Sort(str1);
			Array.Sort(str2);

			for (var i = 0; i < str1.Length; i++)
				if (str1[i] != str2[i]) return false;

			return true;
		}

		public static long ToInt64(this int[] arr)
		{
			return arr.Select((n, i) => n * GetIntRank(arr.Length - i - 1)).Sum();
		}

		private static long GetIntRank(int rank)
		{
			var multiplier = 1;
			for (var k = 0; k < rank; k++) multiplier *= 10;

			return multiplier;
		}

		public static bool IsEven(this int num)
		{
			return (num & 1) == 0;
		}

		public static bool IsOdd(this int num)
		{
			return !IsEven(num);
		}

		public static long Pentagonal(this long n)
		{
			return n * (3 * n - 1) / 2;
		}

		public static bool IsPentagonal(this int n)
		{
			return IsPentagonal((long)n);
		}

		public static bool IsPentagonal(this long n)
		{
			var penTest = (Math.Sqrt(1 + 24 * n) + 1.0) / 6.0;
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			return penTest == (long)penTest;
		}

		public static bool IsHexagonal(this int n)
		{
			return IsHexagonal((long)n);
		}

		public static bool IsHexagonal(this long n)
		{
			var hexTest = (Math.Sqrt(8 * n + 1) + 1) / 4;
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			return hexTest == ((long)hexTest);
		}

		public static IEnumerable<long> GetPentagonalEnumerator()
		{
			const int maxN = 2168;
			for (var i = 1L; i <= maxN; i++)
				yield return i.Pentagonal();
		}

		public static bool IsTwiceASquare(this int n)
		{
			return IsTwiceASquare((long)n);
		}

		public static bool IsTwiceASquare(this long n)
		{
			var squareTest = Math.Sqrt(n / 2);
			return squareTest == ((long)squareTest);
		}

		public static T GreatestCommonDivider<T>(this T a, T b) where T : IBinaryInteger<T>
		{
			while (b != T.Zero)
			{
				var remainder = a % b;
				a = b;
				b = remainder;
			}

			return a;
		}

		public static T LeastCommonMultiple<T>(this T a, T b) where T : IBinaryInteger<T>
		{
			return a * b / a.GreatestCommonDivider(b);
		}

		public static string ConvertToRomanNumeral(int number)
		{
			var romanNumerals = new[]
			{
						new [] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" },	// ones
            new [] { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" },	// tens
            new [] { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" },	// hundreds
            new [] { "", "M", "MM", "MMM", "MMMM", "MMMMM", "MMMMMM", "MMMMMMM" }		// thousands
        };

			// split integer string into array and reverse array
			var intArr = number.ToString(CultureInfo.InvariantCulture).Reverse().ToArray();
			var romanNumeral = "";
			var i = intArr.Length;

			// starting with the highest place (for 3046, it would be the thousands
			// place, or 3), get the roman numeral representation for that place
			// and add it to the final roman numeral string
			while (i-- > 0)
			{
				romanNumeral += romanNumerals[i][int.Parse(intArr[i].ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)];
			}

			return romanNumeral;
		}

		public static int GetValueFromRomanNumeral(string numeral)
		{
			var romanNumbers = new Dictionary<string, int>
						{
								{"M", 1000}, {"CM", 900}, {"D", 500}, {"CD", 400},
								{"C", 100}, {"XC", 90}, {"L", 50}, {"XL", 40},
								{"X", 10}, {"IX", 9}, {"V", 5}, {"IV", 4}, {"I", 1}
						};

			var result = 0;

			foreach (var pair in romanNumbers)
			{
				while (numeral.IndexOf(pair.Key, StringComparison.Ordinal) == 0)
				{
					result += int.Parse(pair.Value.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					numeral = numeral.Substring(pair.Key.Length);
				}
			}

			return result;
		}

		public static T ManhattanDistance<T>(this (T X, T Y) coordinate1, (T X, T Y) coordinate2) where T : IBinaryInteger<T>
		{
			var dx = coordinate1.X - coordinate2.X;
			var dy = coordinate1.Y - coordinate2.Y;
			if (dx < T.Zero) dx = -dx;
			if (dy < T.Zero) dy = -dy;

			return dx + dy;
		}
	}
}
