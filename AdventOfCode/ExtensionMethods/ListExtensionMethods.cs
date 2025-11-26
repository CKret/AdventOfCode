using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.ExtensionMethods
{
    public static class ListExtensionMethods
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(i => (T)i.Clone()).ToList();
        }

        public static void RemoveLast<T>(this IList<T> e, int count = 1)
        {
            while (count-- > 0) e.RemoveAt(e.Count - 1);
        }

        public static IEnumerable<List<T>> ChunkBy<T>(this IEnumerable<T> source, Func<T, bool> isSeparator)
        {
            var chunk = new List<T>();
            foreach (var item in source)
            {
                if (isSeparator(item))
                {
                    if (chunk.Count > 0)
                    {
                        yield return chunk;
                        chunk = new List<T>();
                    }
                }
                else
                {
                    chunk.Add(item);
                }
            }
            if (chunk.Count > 0)
            {
                yield return chunk;
            }
        }
    }
}
