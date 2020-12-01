using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 21: Fractal Art ---
    /// 
    /// You find a program trying to generate some art. It uses a strange process
    /// that involves repeatedly enhancing the detail of an image through a set of
    /// rules.
    /// 
    /// The image consists of a two-dimensional square grid of pixels that are
    /// either on (#) or off (.). The program always begins with this pattern:
    /// 
    /// .#.
    /// ..#
    /// ###
    /// 
    /// Because the pattern is both 3 pixels wide and 3 pixels tall, it is said to
    /// have a size of 3.
    /// 
    /// Then, the program repeats the following process:
    /// 
    ///     - If the size is evenly divisible by 2, break the pixels up into 2x2
    ///       squares, and convert each 2x2 square into a 3x3 square by following
    ///       the corresponding enhancement rule.
    ///     - Otherwise, the size is evenly divisible by 3; break the pixels up into
    ///       3x3 squares, and convert each 3x3 square into a 4x4 square by
    ///       following the corresponding enhancement rule.
    /// 
    /// Because each square of pixels is replaced by a larger one, the image gains
    /// pixels and so its size increases.
    /// 
    /// The artist's book of enhancement rules is nearby (your puzzle input);
    /// however, it seems to be missing rules. The artist explains that sometimes,
    /// one must rotate or flip the input pattern to find a match. (Never rotate or
    /// flip the output pattern, though.) Each pattern is written concisely: rows
    /// are listed as single units, ordered top-down, and separated by slashes. For
    /// example, the following rules correspond to the adjacent patterns:
    /// 
    /// ../.#  =  ..
    ///           .#
    /// 
    ///                 .#.
    /// .#./..#/###  =  ..#
    ///                 ###
    /// 
    ///                         #..#
    /// #..#/..../#..#/.##.  =  ....
    ///                         #..#
    ///                         .##.
    /// 
    /// When searching for a rule to use, rotate and flip the pattern as necessary.
    /// For example, all of the following patterns match the same rule:
    /// 
    /// .#.   .#.   #..   ###
    /// ..#   #..   #.#   ..#
    /// ###   ###   ##.   .#.
    /// 
    /// Suppose the book contained the following two rules:
    /// 
    /// ../.# =&gt; ##./#../...
    /// .#./..#/### =&gt; #..#/..../..../#..#
    /// 
    /// As before, the program begins with this pattern:
    /// 
    /// .#.
    /// ..#
    /// ###
    /// 
    /// The size of the grid (3) is not divisible by 2, but it is divisible by 3.
    /// It divides evenly into a single square; the square matches the second rule,
    /// which produces:
    /// 
    /// #..#
    /// ....
    /// ....
    /// #..#
    /// 
    /// The size of this enhanced grid (4) is evenly divisible by 2, so that rule
    /// is used. It divides evenly into four squares:
    /// 
    /// #.|.#
    /// ..|..
    /// --+--
    /// ..|..
    /// #.|.#
    /// 
    /// Each of these squares matches the same rule (../.# =&gt; ##./#../...), three
    /// of which require some flipping and rotation to line up with the rule. The
    /// output for the rule is the same in all four cases:
    /// 
    /// ##.|##.
    /// #..|#..
    /// ...|...
    /// ---+---
    /// ##.|##.
    /// #..|#..
    /// ...|...
    /// 
    /// Finally, the squares are joined into a new grid:
    /// 
    /// ##.##.
    /// #..#..
    /// ......
    /// ##.##.
    /// #..#..
    /// ......
    /// 
    /// Thus, after 2 iterations, the grid contains 12 pixels that are on.
    /// 
    /// How many pixels stay on after 5 iterations?
    /// 
    /// Your puzzle answer was 184.
    /// </summary>
    [AdventOfCode(2017, 21, 1, "Fractal Art - Part One", 184)]
    public class AdventOfCode2017211 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var inputRules = File.ReadAllLines("2017\\AdventOfCode201721.txt");
            var rulesMap = new Dictionary<string, string>();

            foreach (var rule in inputRules)
            {
                var expansion = rule.Split(new []{" => "}, StringSplitOptions.RemoveEmptyEntries);

                var from = expansion[0];
                var to = expansion[1];


                rulesMap.TryAdd(from, to);
                rulesMap.TryAdd(FlipHorizontal(from), to);
                rulesMap.TryAdd(FlipVertical(from), to);

                for (var i = 0; i < 3; i++)
                {
                    var newFrom = Rotate(from);
                    rulesMap.TryAdd(newFrom, to);
                    rulesMap.TryAdd(FlipHorizontal(newFrom), to);
                    rulesMap.TryAdd(FlipVertical(newFrom), to);

                    from = newFrom;
                }
            }

            var grid = new[]
            {
                ".#.",
                "..#",
                "###",
            };

            grid = CreateArt(5, grid, rulesMap);

            Result = grid.Sum(p => p.Sum(q => q == '#' ? 1 : 0));
        }

        public static string FlipHorizontal(string grid)
        {
            var rows = grid.Split('/');
            var newRows = rows.Select(r => string.Join("/", r.Reverse()));
            return string.Join("/", newRows);
        }

        public static string FlipVertical(string grid)
        {
            var rows = grid.Split('/');
            var newRows = new string[rows.Length];

            for (var i = 0; i < rows.Length; i++)
                newRows[rows.Length - i - 1] = rows[i];

            return string.Join("/", newRows);
        }

        [SuppressMessage("Microsoft.Performance", "CA1814")]
        public static string Rotate(string grid)
        {
            var rows = grid.Split('/');
            var newRows = new char[rows.Length, rows.Length];

            for (var i = 0; i < rows.Length; i++)
                for (var j = 0; j < rows.Length; j++)
                    newRows[rows.Length - j - 1, i] = rows[i][j];

            var sNewRows = new string[rows.Length];
            for (var i = 0; i < rows.Length; i++)
                for (var j = 0; j < rows.Length; j++)
                    sNewRows[i] += newRows[i, j];

            var result = string.Join("/", sNewRows);
            return result;
        }

        public static string CopyFrom(string[] grid, int startRow, int startColumn, int num)
        {
            var section = new string[num];
            for (var i = 0; i < num; i++)
                for (var j = 0; j < num; j++)
                    section[i] += grid[i + startRow][j + startColumn];

            return string.Join("/", section);
        }

        public static void CopyTo(string[] grid, string section, int size, int startRow, int startColumn)
        {
            var rows = section.Split('/');
            for (var i = 0; i < size; i++)
                for (var j = 0; j < size; j++)
                    grid[startRow + i] += rows[i][j];
        }

        public static string[] ExpandGrid(string[] grid, Dictionary<string, string> rules, int size)
        {
            var newSize = size + 1;

            var newGrid = new string[grid.Length / size * newSize];

            for (var j = 0; j * size < grid.Length; j++)
            {
                for (var k = 0; k * size < grid.Length; k++)
                {
                    var section = CopyFrom(grid, j * size, k * size, size);
                    CopyTo(newGrid, rules[section], newSize, j * newSize, k * newSize);
                }
            }

            return newGrid;
        }

        public static string[] CreateArt(int iterations, string[] grid, Dictionary<string, string> rules)
        {
            for (var i = 0; i < iterations; i++)
            {
                grid = ExpandGrid(grid, rules, grid.Length % 2 == 0 ? 2 : 3);
            }

            return grid;
        }

        public AdventOfCode2017211(string sessionCookie) : base(sessionCookie) { }
    }
}
