using System.IO;
using System.Text;
using Xunit.Abstractions;

namespace AdventOfCode.Tests
{
    public sealed class XunitTextWriter : TextWriter
    {
        private readonly ITestOutputHelper _output;

        public XunitTextWriter(ITestOutputHelper output) => _output = output;

        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(string? value)
        {
            if (value is null) return;
            // ITestOutputHelper has only WriteLine; forward as a line to ensure it shows up.
            _output.WriteLine(value);
        }

        public override void WriteLine(string? value)
        {
            _output.WriteLine(value ?? string.Empty);
        }
    }
}