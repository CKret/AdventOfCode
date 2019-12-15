using System;

namespace AdventOfCode.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var aoc1 = new AdventOfCode._2019.AdventOfCode2019151();
            aoc1.Solve();

            var aoc2 = new AdventOfCode._2019.AdventOfCode2019152();
            aoc2.Solve();

            Console.WriteLine($"Part 1: {aoc1.Result}");
            Console.WriteLine($"Part 2: {aoc2.Result}");

            //var a = new AdventOfCode.Solutions.Year2019.Day15();

            //var p1 = a.SolvePartOne();
            //var p2 = a.SolvePartTwo();
        }
    }
}
