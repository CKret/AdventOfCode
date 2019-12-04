using System;
using System.ComponentModel;
using System.Diagnostics;
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
        [Trait("Category", "Advent of Code")]
        public void AllAdventOfCodeTests(AdventOfCodeBase adventOfCode)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var memBefore = Process.GetCurrentProcess().WorkingSet64 / 1024;
            timer.Start();

            adventOfCode.Solve();

            timer.Stop();
            var memAfter = Process.GetCurrentProcess().WorkingSet64 / 1024;
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
