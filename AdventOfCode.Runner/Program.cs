using System;
using System.Diagnostics;

var timer = new Stopwatch();
timer.Start();

var aoc1 = new AdventOfCode._2020.AdventOfCode2020011();
aoc1.Solve();
timer.Stop();
var part1Time = timer.ElapsedMilliseconds;

timer.Start();
var aoc2 = new AdventOfCode._2020.AdventOfCode2020012();
aoc2.Solve();
timer.Stop();

var part2Time = timer.ElapsedMilliseconds;

Console.WriteLine($"Part 1: {aoc1.Result} in {part1Time}ms.");
Console.WriteLine($"Part 2: {aoc2.Result} in {part2Time}ms.");
