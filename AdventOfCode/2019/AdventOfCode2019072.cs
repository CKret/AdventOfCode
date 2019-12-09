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
    [AdventOfCode(2019, 7, 2, "", 25534964L)]
    public class AdventOfCode2019072 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201907.txt");

            var phaseSettings = new long[] { 5, 6, 7, 8, 9 };

            var amps = new List<IntcodeVM>
            {
                new IntcodeVM(data),
                new IntcodeVM(data),
                new IntcodeVM(data),
                new IntcodeVM(data),
                new IntcodeVM(data)
            };


            var maxOutput = 0L;
            do
            {
                for (var i = 0; i < 5; i++)
                {
                    amps[i].ResetVM();
                    amps[i].Input.Enqueue(phaseSettings[i]);
                }

                var output = 0L;
                var vmLoop = new Queue<IntcodeVM>(amps);

                while (vmLoop.Count > 0)
                {
                    var currentAmp = vmLoop.Dequeue();
                    currentAmp.Input.Enqueue(output);

                    if (currentAmp.Execute() != IntcodeVM.HaltMode.Terminated)
                        vmLoop.Enqueue(currentAmp);

                    if (currentAmp.Output.Count > 0)
                        output = currentAmp.Output.Dequeue();

                    if (output > maxOutput)
                        maxOutput = output;
                }

            } while (phaseSettings.NextPermutation());

            Result = maxOutput;
        }
    }
}
