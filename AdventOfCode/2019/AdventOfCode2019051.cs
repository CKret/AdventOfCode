using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.VMs;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 5, 1, "", 11933517L)]
    public class AdventOfCode2019051 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201905.txt");

            var vm = new IntcodeVM(data);
            vm.Execute(new long[] { 1 });
            Result = vm.Output.Last();
        }
    }
}
