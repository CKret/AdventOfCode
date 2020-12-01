using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.VMs;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 13, 1, "", 324)]
    public class AdventOfCode2019131 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201913.txt");
            var vm = new IntcodeVM(data);
            var result = vm.Execute();

            Result = vm.Output.Where((t, i) => i % 3 == 2).Count(x => x == 2);  
        }

        public AdventOfCode2019131(string sessionCookie) : base(sessionCookie) { }
    }
}
