using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.Mathematics;
using AdventOfCode.VMs;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 9, 1, "", 3742852857L)]
    public class AdventOfCode2019091 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201909.txt");

            var vm = new IntcodeVM(data);

            var result = vm.Execute(new long[]{1});

            Result = vm.Output.First();
        }
    }
}
