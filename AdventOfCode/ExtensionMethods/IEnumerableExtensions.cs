using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
            action(item);
    }

    public static bool IsEmpty<T>(this IEnumerable<T> source)
    {
        return !source.Any();
    }

    public static IEnumerable<TResult> Scan<TSource, TResult>(
        this IEnumerable<TSource> source,
        TResult seed,
        Func<TResult, TSource, TResult> accumulator)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (accumulator == null) throw new ArgumentNullException(nameof(accumulator));

        var result = seed;
        yield return result;

        foreach (var item in source)
        {
            result = accumulator(result, item);
            yield return result;
        }
    }

    public static IEnumerable<TSource> Scan<TSource>(
        this IEnumerable<TSource> source,
        Func<TSource, TSource, TSource> accumulator)
    {
        using var e = source.GetEnumerator();
        if (!e.MoveNext()) yield break;

        var result = e.Current;
        yield return result;

        while (e.MoveNext())
        {
            result = accumulator(result, e.Current);
            yield return result;
        }
    }
}
