using System;
using System.Collections.Generic;

namespace AdventOfCode.ExtensionMethods
{
    public static class EnumerableExtensions
    {
        // IEnumerable<T> with index
        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (action == null) throw new ArgumentNullException(nameof(action));

            int index = 0;
            foreach (var item in source)
                action(item, index++);
        }

        // IEnumerable<T> without index
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (action == null) throw new ArgumentNullException(nameof(action));

            foreach (var item in source)
                action(item);
        }

        // Array version with index (int[])
        public static void ForEach<T>(this T[] array, Action<T, int> action)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (action == null) throw new ArgumentNullException(nameof(action));

            for (int i = 0; i < array.Length; i++)
                action(array[i], i);
        }

        // Array version without index
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (action == null) throw new ArgumentNullException(nameof(action));

            foreach (var item in array)
                action(item);
        }
    }
}
