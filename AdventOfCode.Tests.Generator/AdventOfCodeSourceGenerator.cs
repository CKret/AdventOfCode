using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace AdventOfCode.Tests.Generator;

[Generator]
public sealed class AdventOfCodeSourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context) { }

    public void Execute(GeneratorExecutionContext context)
    {
        var compilation = context.Compilation;

        var aocBase = compilation.GetTypeByMetadataName("AdventOfCode.Core.AdventOfCodeBase");
        var aocAttr = compilation.GetTypeByMetadataName("AdventOfCode.Core.AdventOfCodeAttribute");

        if (aocBase is null || aocAttr is null)
            return;

        // ONLY scan AdventOfCode.dll
        IAssemblySymbol aocAssembly = aocBase.ContainingAssembly;

        var solverTypes = aocAssembly
            .GlobalNamespace
            .GetNamespaceTypes()
            .Where(t =>
                t.TypeKind == TypeKind.Class &&
                !t.IsAbstract &&
                t.GetAttributes().Any(a =>
                    SymbolEqualityComparer.Default.Equals(a.AttributeClass, aocAttr)))
            .Distinct(SymbolEqualityComparer.Default)
            .ToList();

        // Dedupe by (year, day)
        var unique = new Dictionary<(int year, int day), (INamedTypeSymbol type, string desc)>();

        foreach (var type in solverTypes)
        {
            var attr = type.GetAttributes()
                .FirstOrDefault(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, aocAttr));

            if (attr is null)
                continue;

            var year = (int)attr.ConstructorArguments[0].Value!;
            var day = (int)attr.ConstructorArguments[1].Value!;
            var desc = (string)attr.ConstructorArguments[2].Value!;

            var key = (year, day);
            if (!unique.ContainsKey(key))
                unique[key] = ((INamedTypeSymbol) type, desc);
        }

        // Generate one .g.cs file per solver
        foreach (var ((year, day), (type, desc)) in unique)
        {
            string safeDesc = new([.. desc.Where(c => char.IsLetterOrDigit(c) || c == '_')]);

            string className = $"Day_{day:00}_{safeDesc}";
            string fullTypeName = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

            var sb = new StringBuilder();

            sb.AppendLine("using Xunit;");
            sb.AppendLine("using AdventOfCode.Core;");
            sb.AppendLine("using System.Threading;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine();
            sb.AppendLine($"namespace AdventOfCode.Tests.Generated._{year};");
            sb.AppendLine();
            sb.AppendLine($"public sealed class {className} : AdventOfCodeTestBase");
            sb.AppendLine("{");

            // Part 1 (async + timeout)
            sb.AppendLine($"    [Fact(DisplayName=\"Part 1\")]");
            sb.AppendLine($"    public async Task Test_{year}_{day:00}_Part1()");
            sb.AppendLine("    {");
            sb.AppendLine("        using var cts = new CancellationTokenSource(System.TimeSpan.FromMinutes(1));");
            sb.AppendLine("        try");
            sb.AppendLine("        {");
            sb.AppendLine($"            await RunTestAsync(typeof({fullTypeName}), 1, cts.Token).ConfigureAwait(false);");
            sb.AppendLine("        }");
            sb.AppendLine("        catch (OperationCanceledException)"); // thrown when token cancels
            sb.AppendLine("        {");
            sb.AppendLine("            throw new Xunit.Sdk.XunitException(\"Test timed out after 1 minute\");");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine();

            // Part 2 (async + timeout)
            sb.AppendLine($"    [Fact(DisplayName=\"Part 2\")]");
            sb.AppendLine($"    public async Task Test_{year}_{day:00}_Part2()");
            sb.AppendLine("    {");
            sb.AppendLine("        using var cts = new CancellationTokenSource(System.TimeSpan.FromMinutes(1));");
            sb.AppendLine("        try");
            sb.AppendLine("        {");
            sb.AppendLine($"            await RunTestAsync(typeof({fullTypeName}), 2, cts.Token).ConfigureAwait(false);");
            sb.AppendLine("        }");
            sb.AppendLine("        catch (OperationCanceledException)");
            sb.AppendLine("        {");
            sb.AppendLine("            throw new Xunit.Sdk.XunitException(\"Test timed out after 1 minute\");");
            sb.AppendLine("        }");
            sb.AppendLine("    }");

            sb.AppendLine("}");

            context.AddSource($"{className}.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
        }



        // Test collection for parallelization
        context.AddSource("AdventOfCodeTestCollection.g.cs", SourceText.From("""using Xunit; [CollectionDefinition("AdventOfCodeTests", DisableParallelization = false)] public class AdventOfCodeTestCollection { }""", Encoding.UTF8));
    }
}

// EXTENSION =============================================

public static class NamespaceSymbolExtensions
{
    public static IEnumerable<INamedTypeSymbol> GetNamespaceTypes(this INamespaceSymbol ns)
    {
        foreach (var t in ns.GetTypeMembers())
            yield return t;

        foreach (var sub in ns.GetNamespaceMembers())
            foreach (var child in sub.GetNamespaceTypes())
                yield return child;
    }
}
