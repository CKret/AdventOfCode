using System;
using System.Diagnostics;
using System.Threading;
using AdventOfCode.Core;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions;

namespace AdventOfCode.UnitTests
{
    public class AdventOfCodeTest
    {
        private readonly Stopwatch timer = new Stopwatch();
        private readonly ITestOutputHelper output;

        public AdventOfCodeTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InstanceData]
        public void AllAdventOfCodeTests(AdventOfCodeBase adventOfCode)
        {
            var memBefore = GC.GetTotalMemory(true) / 1024;
            Thread.MemoryBarrier();
            timer.Start();

            adventOfCode.Solve();

            timer.Stop();
            Thread.MemoryBarrier();
            var memAfter = GC.GetTotalMemory(true) / 1024;
            var totalMemoryUsage = memAfter - memBefore;

            output.WriteLine($"Advent Of Code {adventOfCode.Problem.Year} {adventOfCode.Problem.Day:D2} {adventOfCode.Problem.Number}" + Environment.NewLine);
            output.WriteLine($"Description : {adventOfCode.Problem.Description}");
            output.WriteLine($"Result      : {adventOfCode.Result}");
            output.WriteLine($"Time        : {timer.ElapsedMilliseconds} ms");
            output.WriteLine($"Memory Usage: {totalMemoryUsage} kB");

            Assert.Equal(adventOfCode.Problem.Solution, adventOfCode.Result);
        }
    }

}
