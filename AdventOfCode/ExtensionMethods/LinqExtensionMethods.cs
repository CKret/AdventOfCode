using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.ExtensionMethods
{
    public static class LinqExtensionMethods
    {
        public static TSource AggregateWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func, Func<TSource, bool> predicate)
        {
            using (var e = source.GetEnumerator())
            {
                var result = e.Current;
                var tmp = default(TSource);
                while (e.MoveNext() && predicate(tmp = func(result, e.Current)))
                    result = tmp;
                return result;
            }
        }
    }
}
