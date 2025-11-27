using AdventOfCode.Core;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;
using System.Text;

namespace AdventOfCode.Tests;

public abstract class AdventOfCodeTestBase
{
    protected AdventOfCodeAttribute GetAdventOfCodeAttribute(Type solverType) => (AdventOfCodeAttribute)Attribute.GetCustomAttribute(solverType, typeof(AdventOfCodeAttribute))!;

    // Backwards-compatible synchronous wrapper
    public static void RunTest(Type solverType, int part) => RunTestAsync(solverType, part, CancellationToken.None).GetAwaiter().GetResult();

    // New async RunTest that accepts a CancellationToken
    public static async Task RunTestAsync(Type solverType, int part, CancellationToken cancellationToken)
        => await RunTestAsync(solverType, part, cancellationToken, null).ConfigureAwait(false);

    // Overload that accepts a TextWriter (used by generated tests to attach xUnit output)
    public static async Task RunTestAsync(Type solverType, int part, CancellationToken cancellationToken, TextWriter? outputWriter)
    {
        var cookie = AdventOfCode.Runner.AocConfig.SessionCookie;
        var solver = (AdventOfCodeBase) Activator.CreateInstance(solverType, cookie)!;

        // Determine writer to use for reporting
        TextWriter writer = outputWriter ?? solver.OutputWriter ?? Console.Out;

        var attr = solverType.GetCustomAttribute<AdventOfCodeAttribute>();

        // Try to get a human-friendly summary: prefer XML doc <summary>, then attribute.Description, then empty
        var xmlSummary = TryGetXmlSummaryForType(solverType);
        var summary = !string.IsNullOrWhiteSpace(xmlSummary) ? xmlSummary : (attr?.Description ?? string.Empty);

        // Normalize whitespace safely
        if (string.IsNullOrEmpty(summary)) summary = string.Empty;
        else summary = System.Text.RegularExpressions.Regex.Replace(summary, "\\s+", " ").Trim();

        // Prepare body by removing any leading title fragment from the xml summary when attribute description exists
        string body = summary;
        var title = attr?.Description ?? string.Empty;
        if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(body))
        {
            var idx = body.IndexOf(title, StringComparison.OrdinalIgnoreCase);
            if (idx >= 0)
            {
                // remove the title and any surrounding separators
                body = body[(idx + title.Length)..].TrimStart(' ', '\r', '\n', '-', ':');
            }
        }

        // Print header once for this test (attached to the test's output)
        writer.WriteLine();
        if (attr is not null)
        {
            writer.WriteLine($"{attr.Year} Day {attr.Day:00}");
            writer.WriteLine("");
            writer.WriteLine($"--- {title} ---");

            if (!string.IsNullOrWhiteSpace(body))
            {
                foreach (var line in WrapTextPreservingParagraphs(body, 80))
                    writer.WriteLine(line);
                writer.WriteLine();
            }
        }
        else
        {
            writer.WriteLine($"--- {solverType.FullName} ---");
            if (!string.IsNullOrEmpty(summary)) writer.WriteLine(summary);
        }

        // Suppress solver's own reporting while we run its async entry so we can control what gets printed
        var originalWriter = solver.OutputWriter;
        solver.OutputWriter = TextWriter.Null;

        try
        {
            // Run the solver via its async entry so any initialization it performs is executed.
            await solver.SolveAsync(cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            // restore original writer
            solver.OutputWriter = originalWriter;
        }

        // Now print only the requested part's result/time from the populated properties.
        if (part == 1)
        {
            var result = solver.ResultPart1;
            var ms = solver.TimePart1;

            writer.WriteLine($"Part 1: {FormatObject(result)} (Time: {FormatTime(ms)})");

            string actual1 = Convert.ToString(result) ?? "";
            string expected1 = Convert.ToString(attr?.SolutionPart1) ?? "";
            Assert.Equal(expected1, actual1);
        }
        else if (part == 2)
        {
            var result = solver.ResultPart2;
            var ms = solver.TimePart2;

            writer.WriteLine($"Part 2: {FormatObject(result)} (Time: {FormatTime(ms)})");

            string actual2 = Convert.ToString(result) ?? "";
            string expected2 = Convert.ToString(attr?.SolutionPart2) ?? "";
            Assert.Equal(expected2, actual2);
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(part));
        }

        writer.WriteLine();
    }

    private static string TryGetXmlSummaryForType(Type t)
    {
        try
        {
            var asm = t.Assembly;
            if (asm is null) return string.Empty;
            var xmlPath = Path.ChangeExtension(asm.Location, ".xml");
            if (string.IsNullOrEmpty(xmlPath) || !File.Exists(xmlPath)) return string.Empty;

            var doc = XDocument.Load(xmlPath);
            var members = doc.Root?.Element("members");
            if (members is null) return string.Empty;

            var memberName = "T:" + t.FullName;
            var member = members.Elements("member").FirstOrDefault(m => string.Equals((string?)m.Attribute("name"), memberName, StringComparison.Ordinal));
            if (member is null) return string.Empty;

            var summaryElem = member.Element("summary");
            if (summaryElem is null) return string.Empty;

            return summaryElem.Value ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    private static IEnumerable<string> WrapTextPreservingParagraphs(string text, int width)
    {
        if (string.IsNullOrEmpty(text)) yield break;

        // Split paragraphs by two or more newlines
        var paragraphs = text.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
        const string partTwoSeparator = "--- Part Two ---";

        foreach (var para in paragraphs)
        {
            var trimmed = para.Trim();
            if (string.IsNullOrEmpty(trimmed))
            {
                yield return string.Empty;
                continue;
            }

            // If the paragraph contains the Part Two separator, split and emit extra spacing
            int start = 0;
            while (true)
            {
                int idx = trimmed.IndexOf(partTwoSeparator, start, StringComparison.Ordinal);
                if (idx == -1)
                {
                    // process remainder from 'start' to end
                    var remainder = trimmed[start..].Trim();
                    if (!string.IsNullOrEmpty(remainder))
                    {
                        foreach (var line in WrapSingleParagraph(remainder, width))
                            yield return line;
                    }
                    break;
                }

                // process segment before separator
                var seg = trimmed[start..idx].Trim();
                if (!string.IsNullOrEmpty(seg))
                {
                    foreach (var line in WrapSingleParagraph(seg, width))
                        yield return line;
                }

                // emit spacing and separator line
                // ensure single blank line before separator, and then emit separator only
                yield return string.Empty;
                yield return partTwoSeparator;

                start = idx + partTwoSeparator.Length;
            }
        }

        // blank line between paragraphs
        yield return string.Empty;
    }

    // Helper to wrap a single paragraph (no paragraph delimiters inside)
    private static IEnumerable<string> WrapSingleParagraph(string paragraph, int width)
    {
        if (string.IsNullOrEmpty(paragraph)) yield break;

        var words = paragraph.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var line = new StringBuilder();

        foreach (var w in words)
        {
            if (line.Length == 0)
            {
                line.Append(w);
            }
            else if (line.Length + 1 + w.Length <= width)
            {
                line.Append(' ').Append(w);
            }
            else
            {
                yield return line.ToString();
                line.Clear();
                line.Append(w);
            }
        }

        if (line.Length > 0) yield return line.ToString();
    }

    private static string FormatObject(object? o) => o is null ? "null" : o.ToString()!;
    private static string FormatTime(decimal ms) => $"{Math.Round(ms, 2)} ms";

}
