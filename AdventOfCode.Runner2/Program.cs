using System;

namespace AdventOfCode.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var aoc1 = new AdventOfCode._2019.AdventOfCode2019181();
            aoc1.Solve();

            var aoc2 = new AdventOfCode._2019.AdventOfCode2019182();
            aoc2.Solve();

            Console.WriteLine($"Part 1: {aoc1.Result}");
            Console.WriteLine($"Part 2: {aoc2.Result}");
        }
    }
}
