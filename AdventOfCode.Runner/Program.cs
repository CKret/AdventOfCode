using System;
using Microsoft.Extensions.Configuration;

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var builder = new ConfigurationBuilder()
              .AddJsonFile($"appsettings.json", true, true)
              .AddJsonFile($"appsettings.{env}.json", true, true)
              .AddEnvironmentVariables();

var config = builder.Build();
var sessionCookie = config["AdventOfCodeSessionCookie"];

var aoc = new AdventOfCode._2020.AdventOfCode202003(sessionCookie);
aoc.Solve();

Console.WriteLine(aoc.Problem.Description);
Console.WriteLine();
Console.WriteLine($"Part 1:");
Console.WriteLine($"\t{aoc.ResultPart1}");
if (aoc.TimePart2 != 0)
    Console.WriteLine($"\t{aoc.TimePart1:N4}ms");
Console.WriteLine($"Part 2:");
Console.WriteLine($"\t{aoc.ResultPart2}");
if (aoc.TimePart2 != 0)
    Console.WriteLine($"\t{aoc.TimePart2:N4}ms");
Console.WriteLine();
Console.WriteLine($"\t{(aoc.TimePart1 + aoc.TimePart2):N4}ms total");
