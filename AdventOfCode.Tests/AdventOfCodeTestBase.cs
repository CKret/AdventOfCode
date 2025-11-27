using AdventOfCode.Core;
using System.Reflection;

namespace AdventOfCode.Tests;

public abstract class AdventOfCodeTestBase
{
    protected AdventOfCodeAttribute GetAdventOfCodeAttribute(Type solverType) => (AdventOfCodeAttribute)Attribute.GetCustomAttribute(solverType, typeof(AdventOfCodeAttribute))!;

    // Backwards-compatible synchronous wrapper
    public static void RunTest(Type solverType, int part) => RunTestAsync(solverType, part, CancellationToken.None).GetAwaiter().GetResult();

    // New async RunTest that accepts a CancellationToken
    public static async Task RunTestAsync(Type solverType, int part, CancellationToken cancellationToken)
    {
        var cookie = AdventOfCode.Runner.AocConfig.SessionCookie;
        var solver = (AdventOfCodeBase) Activator.CreateInstance(solverType, cookie)!;

        // Prefer cooperative async SolveAsync if overridden by a solver; otherwise the default will run Solve on the thread-pool.
        await solver.SolveAsync(cancellationToken).ConfigureAwait(false);

        var attr = solverType.GetCustomAttribute<AdventOfCodeAttribute>();

        string actual1 = Convert.ToString(solver.ResultPart1) ?? "";
        string actual2 = Convert.ToString(solver.ResultPart2) ?? "";
        string expected1 = Convert.ToString(attr.SolutionPart1) ?? "";
        string expected2 = Convert.ToString(attr.SolutionPart2) ?? "";

        if (part == 1)
            Assert.Equal(expected1, actual1);
        else
            Assert.Equal(expected2, actual2);
    }

}
