using System.Collections.Generic;
using AdventOfCode.Core;
using System.Linq;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 11, "Seating System", 2346, 3111)]
    public class AdventOfCode202011 : AdventOfCodeBase
    {
        public AdventOfCode202011(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var seats = Input.Select(l => l.ToCharArray()).ToArray();

            while (NextRound(seats)) { }

            return seats.SelectMany(x => x).Count(c => c == '#');
        }

        protected override object SolvePart2()
        {
            var seats = Input.Select(l => l.ToCharArray()).ToArray();

            while (NextRound2(seats)) { }

            return seats.SelectMany(x => x).Count(c => c == '#');
        }

        private bool NextRound(char[][] seats)
        {
            var newSeats = new Dictionary<(int, int), char>();

            for (var y = 0; y < seats.Length; y++)
            {
                for (var x = 0; x < seats[y].Length; x++)
                {
                    if (seats[y][x] == '.') continue;

                    var occupied = 0;
                    // Top Left
                    if (y > 0 && x > 0)
                    {
                        if (seats[y-1][x-1] == '#') occupied++;
                    }
                    // Top Middle
                    if (y > 0)
                    {
                        if (seats[y-1][x] == '#') occupied++;
                    }
                    // Top Right
                    if (y > 0 && x < seats[y].Length - 1)
                    {
                        if (seats[y-1][x+1] == '#') occupied++;
                    }
                    // Middle Left
                    if (x > 0)
                    {
                        if (seats[y][x-1] == '#') occupied++;
                    }
                    // Middle Right
                    if (x < seats[y].Length - 1)
                    {
                        if (seats[y][x+1] == '#') occupied++;
                    }
                    // Bottom Left
                    if (y < seats.Length - 1 && x > 0)
                    {
                        if (seats[y+1][x-1] == '#') occupied++;
                    }
                    // Bottom Middle
                    if (y < seats.Length - 1)
                    {
                        if (seats[y+1][x] == '#') occupied++;
                    }
                    // Bottom Right
                    if (y < seats.Length - 1 && x < seats[y].Length - 1)
                    {
                        if (seats[y+1][x+1] == '#') occupied++;
                    }
                    
                    if (seats[y][x] == 'L' && occupied == 0) newSeats.Add((x, y), '#');
                    else if (seats[y][x] == '#' && occupied >= 4) newSeats.Add((x, y), 'L');
                }
            }

            foreach (var seat in newSeats)
            {
                seats[seat.Key.Item2][seat.Key.Item1] = seat.Value;
            }

            return newSeats.Count != 0;
        }

        private bool NextRound2(char[][] seats)
        {
            var newSeats = new Dictionary<(int, int), char>();

            for (var y = 0; y < seats.Length; y++)
            {
                for (var x = 0; x < seats[y].Length; x++)
                {
                    if (seats[y][x] == '.') continue;

                    var occupied = 0;

                    // Top Left
                    if (x > 0 && y > 0)
                    {
                        var (cx, cy) = (x - 1, y - 1);
                        while (cx >= 0 && cy >= 0 && seats[cy][cx] == '.') (cx, cy) = (cx - 1, cy - 1);
                        if (cx >= 0 && cy >= 0 && seats[cy][cx] == '#') occupied++;
                    }
                    // Top Middle
                    if (y > 0)
                    {
                        var (cx, cy) = (x, y - 1);
                        while (cy >= 0 && seats[cy][cx] == '.') cy--;
                        if (cy >= 0 && seats[cy][cx] == '#') occupied++;
                    }
                    // Top Right
                    if (x < seats[y].Length - 1 && y > 0)
                    {
                        var (cx, cy) = (x + 1, y - 1);
                        while (cx < seats[y].Length && cy >= 0 && seats[cy][cx] == '.') (cx, cy) = (cx + 1, cy - 1);
                        if (cx < seats[y].Length && cy >= 0 && seats[cy][cx] == '#') occupied++;
                    }
                    // Middle Left
                    if (x > 0)
                    {
                        var (cx, cy) = (x - 1, y);
                        while (cx >= 0 && seats[cy][cx] == '.') cx--;
                        if (cx >= 0 && seats[cy][cx] == '#') occupied++;
                    }
                    // Middle Right
                    if (x < seats[y].Length - 1)
                    {
                        var (cx, cy) = (x + 1, y);
                        while (cx < seats[y].Length && seats[cy][cx] == '.') cx++;
                        if (cx < seats[y].Length && seats[cy][cx] == '#') occupied++;
                    }
                    // Bottom Left
                    if (x > 0 && y < seats.Length - 1)
                    {
                        var (cx, cy) = (x - 1, y + 1);
                        while (cx >= 0 && cy < seats.Length && seats[cy][cx] == '.') (cx, cy) = (cx - 1, cy + 1);
                        if (cx >= 0 && cy < seats.Length && seats[cy][cx] == '#') occupied++;
                    }
                    // Bottom Middle
                    if (y < seats.Length - 1)
                    {
                        var (cx, cy) = (x, y + 1);
                        while (cy < seats.Length && seats[cy][cx] == '.') cy++;
                        if (cy < seats.Length && seats[cy][cx] == '#') occupied++;
                    }
                    // Bottom Right
                    if (x < seats[y].Length - 1 && y < seats.Length - 1)
                    {
                        var (cx, cy) = (x + 1, y + 1);
                        while (cx < seats[y].Length && cy < seats.Length && seats[cy][cx] == '.') (cx, cy) = (cx + 1, cy + 1);
                        if (cx < seats[y].Length && cy < seats.Length && seats[cy][cx] == '#') occupied++;
                    }

                    if (seats[y][x] == 'L' && occupied == 0) newSeats.Add((x, y), '#');
                    else if (seats[y][x] == '#' && occupied >= 5) newSeats.Add((x, y), 'L');
                }
            }

            foreach (var ((x, y), value) in newSeats)
            {
                seats[y][x] = value;
            }

            return newSeats.Count != 0;
        }
    }
}
