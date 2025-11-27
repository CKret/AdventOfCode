using AdventOfCode.Runner;
using System;

string sessionCookie = AocConfig.SessionCookie;


var solver = new AdventOfCode._2024.AdventOfCode202406(sessionCookie);
solver.Solve();

Console.WriteLine($"{solver.Problem.Year} day {solver.Problem.Day} - {solver.Problem.Description}");
Console.WriteLine();
Console.WriteLine($"Part 1:");
Console.WriteLine($"\t{solver.ResultPart1}");
if (solver.TimePart2 != 0)
    Console.WriteLine($"\t{solver.TimePart1:N4}ms");
Console.WriteLine($"Part 2:");
Console.WriteLine($"\t{solver.ResultPart2}");
if (solver.TimePart2 != 0)
    Console.WriteLine($"\t{solver.TimePart2:N4}ms");
Console.WriteLine();
Console.WriteLine($"\t{(solver.TimePart1 + solver.TimePart2):N4}ms total");
