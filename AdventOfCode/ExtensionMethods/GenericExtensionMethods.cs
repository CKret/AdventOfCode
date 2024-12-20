﻿using SuperLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.ExtensionMethods
{
	public static class GenericExtensionMethods
	{
		public static TSource AggregateWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func, Func<TSource, bool> predicate)
		{
			using var e = source.GetEnumerator();
			var result = e.Current;
			TSource tmp;
			while (e.MoveNext() && predicate(tmp = func(result, e.Current)))
				result = tmp;
			return result;
		}

		public static IEnumerable<IEnumerable<T>> SplitAndParse<T>(this IEnumerable<string> input, string separator = "")
		{
			return input.Split(separator).Select(x => x.Select(y => (T)Convert.ChangeType(y, typeof(T))));
		}

		public static (IEnumerable<T> Head, IEnumerable<T> Tail) SplitInHalf<T>(this IEnumerable<T> e)
		{
			return (Head: e.Take(e.Count() / 2), Tail: e.Skip(e.Count() / 2));
		}

		public static (IEnumerable<T> Head, IEnumerable<T> Tail) SplitAt<T>(this IEnumerable<T> e, int pos)
		{
			return (Head: e.Take(pos), Tail: e.Skip(pos));
		}

		public static Queue<T> ToQueue<T>(this IEnumerable<T> items)
		{
			var q = new Queue<T>();
			q.EnqueueRange(items);
			return q;
		}
	}
}
