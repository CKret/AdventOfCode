using System;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var builder = new ConfigurationBuilder()
              .AddJsonFile($"appsettings.json", true, true)
              .AddJsonFile($"appsettings.{env}.json", true, true)
              .AddEnvironmentVariables();

var config = builder.Build();
var sessionCookie = config["AdventOfCodeSessionCookie"];

var timer = new Stopwatch();

var aoc1 = new AdventOfCode._2020.AdventOfCode2020031(sessionCookie);

timer.Start();
aoc1.Solve();
timer.Stop();

var part1Time = timer.ElapsedMilliseconds;

var aoc2 = new AdventOfCode._2020.AdventOfCode2020032(sessionCookie);

timer.Start();
aoc2.Solve();
timer.Stop();

var part2Time = timer.ElapsedMilliseconds;

Console.WriteLine($"Part 1: {aoc1.Result} in {part1Time}ms.");
Console.WriteLine($"Part 2: {aoc2.Result} in {part2Time}ms.");
