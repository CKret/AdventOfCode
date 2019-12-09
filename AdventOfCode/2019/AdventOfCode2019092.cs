using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using AdventOfCode.Core;
using AdventOfCode.Mathematics;
using AdventOfCode.VMs;
using Tesseract;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 9, 2, "", 73439L)]
    public class AdventOfCode2019092 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201909.txt");

            var vm = new IntcodeVM(data);

            var result = vm.Execute(new long[] { 2 });

            Result = vm.Output.First();
        }
    }
}
