using System.Diagnostics;

namespace AdventOfCode.ExtensionMethods
{
    public static class LongExtensionMethods
    {
        public static decimal ToMilliseconds(this long ticks)
        {
            return (decimal) ticks / Stopwatch.Frequency * 1000;
        }
    }
}
