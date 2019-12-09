using System.IO;
using AdventOfCode.Core;
using AdventOfCode.Mathematics;
using AdventOfCode.VMs;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 7, 1, "", 18812)]
    public class AdventOfCode2019071 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201907.txt");

            var phaseSettings = new[] { 0, 1, 2, 3, 4 };

            var vm = new IntcodeVM(data);

            var maxOutput = 0;
            do
            {
                // Amp A
                vm.ResetVM();
                var input = new[] { phaseSettings[0], 0 };
                vm.Execute(input);
                var output = vm.Output.ToArray();

                // Amp B
                vm.ResetVM();
                input = new[] { phaseSettings[1], output[0] };
                vm.Execute(input);
                output = vm.Output.ToArray();

                // Amp C
                vm.ResetVM();
                input = new[] { phaseSettings[2], output[0] };
                vm.Execute(input);
                output = vm.Output.ToArray();

                // Amp D
                vm.ResetVM();
                input = new[] { phaseSettings[3], output[0] };
                vm.Execute(input);
                output = vm.Output.ToArray();

                // Amp E
                vm.ResetVM();
                input = new[] { phaseSettings[4], output[0] };
                vm.Execute(input);
                output = vm.Output.ToArray();

                if (output[0] > maxOutput)
                {
                    maxOutput = output[0];
                }
            } while (phaseSettings.NextPermutation());

            Result = maxOutput;
        }
    }
}
