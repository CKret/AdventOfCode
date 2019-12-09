using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.VMs;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 5, 2, "", 10428568)]
    public class AdventOfCode2019052 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201905.txt");

            var vm = new IntcodeVM(data);
            vm.Execute(new[] { 5 });
            Result = vm.Output.Last();
        }
    }
}
