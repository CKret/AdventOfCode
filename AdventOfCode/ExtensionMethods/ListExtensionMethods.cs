using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.ExtensionMethods
{
    public static class ListExtensionMethods
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(i => (T) i.Clone()).ToList();
        }

        public static void RemoveLast<T>(this IList<T> e, int count = 1)
        {
            while (count-- > 0) e.RemoveAt(e.Count - 1);
        }
    }
}
