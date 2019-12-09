﻿using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.VMs;

namespace AdventOfCode._2019
{
    /// <summary>
    /// --- Day 9: Sensor Boost ---
    ///
    /// --- Part Two ---
    /// 
    /// You now have a complete Intcode computer.
    /// 
    /// Finally, you can lock on to the Ceres distress signal! You just need to
    /// boost your sensors using the BOOST program.
    /// 
    /// The program runs in sensor boost mode by providing the input instruction
    /// the value 2. Once run, it will boost the sensors automatically, but it
    /// might take a few seconds to complete the operation on slower hardware. In
    /// sensor boost mode, the program will output a single value: the coordinates
    /// of the distress signal.
    /// 
    /// Run the BOOST program in sensor boost mode. What are the coordinates of
    /// the distress signal?
    /// </summary>
    [AdventOfCode(2019, 9, 2, "Sensor Boost - Part 2", 73439L)]
    public class AdventOfCode2019092 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201909.txt");

            var vm = new IntcodeVM(data);
            vm.Execute(new long[] { 2 });

            Result = vm.Output.First();
        }
    }
}
