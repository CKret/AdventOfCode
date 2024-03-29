using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;
using SuperLinq;

namespace AdventOfCode._2021
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2021, 4, "Giant Squid", 69579, 14877)]
    public class AdventOfCode202104 : AdventOfCodeBase
    {
        public AdventOfCode202104(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var numbers = Input.First().Split(',').Select(int.Parse);

            var boards = Input
                .Where(l => l.Length > 0)
                .Skip(1)
                .Batch(5)
                .Select(b => b
                    .Select(r => r
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(n => new BingoNumber { Number = int.Parse(n), Marked = false })
                        .ToList())
                    .ToList())
                .Select(b => new BingoBoard { Numbers = b })
                .ToList();


            foreach (var num in numbers)
            foreach (var result in boards.Select(b => b.CheckNumber(num)).Where(result => result > -1))
            {
                return result;
            }

            return null;
        }

        protected override object SolvePart2()
        {
            var numbers = Input.First().Split(',').Select(int.Parse);

            var boards = Input
                .Where(l => l.Length > 0)
                .Skip(1)
                .Batch(5)
                .Select(b => b
                    .Select(r => r
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(n => new BingoNumber { Number = int.Parse(n), Marked = false })
                        .ToList())
                    .ToList())
                .Select(b => new BingoBoard { Numbers = b })
                .ToList();

            foreach (var num in numbers)
            {
                for (var b = boards.Count - 1; b >= 0; b--)
                {
                    var result = boards[b].CheckNumber(num);
                    if (result <= -1) continue;

                    if (boards.Count > 1)
                    {
                        boards.RemoveAt(b);
                    }
                    else
                    {
                        return result;
                    }
                }
            }

            return null;
        }
    }

    public record BingoNumber
    {
        public int Number { get; set; }
        public bool Marked { get; set; }
    }

    public class BingoBoard
    {
        public List<List<BingoNumber>> Numbers { get; set; }
        
        public long UnmarkedSum => Numbers.Sum(r => r.Where(n => !n.Marked).Sum(n => n.Number));

        public long CheckNumber(int num)
        {
            var w = Numbers
                .Select(r => r.SingleOrDefault(n => n.Number == num && n.Marked == false))
                .Where(r => r != null)
                .SingleOrDefault(r => r.Number == num);

            if (w == null) return -1;

            w.Marked = true;
            
            if (IsBingo()) return num * UnmarkedSum;

            return -1;
        }

        private bool IsBingo()
        {
            // Rows
            if (Numbers.Any(r => r.All(n => n.Marked)))
            {
                return true;
            }

            // Columns
            for (var x = 0; x < 5; x++)
            {
                if (Numbers.Select(r => r.Skip(x).First()).All(n => n.Marked))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
