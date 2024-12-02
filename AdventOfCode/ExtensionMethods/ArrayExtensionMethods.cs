using AdventOfCode.Mathematics;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.ExtensionMethods
{
	public static class ArrayExtensionMethods
	{
		public static T[] RotateLeft<T>(this T[] arr)
		{
			return arr.Skip(1).Concat(arr.Take(1)).ToArray();
		}

		public static T[] RotateRight<T>(this T[] arr)
		{
			return arr.Skip(arr.Length - 1).Concat(arr.Take(arr.Length - 1)).ToArray();
		}

		public static T LeastCommonMultiple<T>(this T[] arr) where T : IBinaryInteger<T>
		{
			return arr.Aggregate(T.One, (c, p) => c.LeastCommonMultiple(p));
		}
	}
}
