using System;
using System.Collections.Generic;

namespace AdventOfCode.ExtensionMethods
{
    public static class FuncExtensionMethods
    {
        public static Func<T> Memoize<T>(this Func<T> f)
        {
            var value = default(T);
            var hasValue = false;
            return () =>
            {
                if (!hasValue)
                {
                    hasValue = true;
                    value = f();
                }
                return value;
            };
        }

        public static Func<TA, TB> Memoize<TA, TB>(this Func<TA, TB> f)
        {
            var map = new Dictionary<TA, TB>();
            return a =>
            {
                if (map.TryGetValue(a, out var value)) return value;
                value = f(a);
                map.Add(a, value);

                return value;
            };
        }
    }
}
