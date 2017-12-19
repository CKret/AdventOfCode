using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2017, 19, 1, "", null)]
    public class AdventOfCode2017191 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var input = File.ReadAllLines("2017\\AdventOfCode201719.txt");

            var startPos = input[0].IndexOf('|');
            var grid = input.Select(a => a.ToCharArray().ToList()).ToList();

            int x = startPos, y = 0;
            var direction = 0;
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToList();
            var encounters = new List<char>();
            while (true)
            {
                if (direction == 0) // down
                {
                    if (grid[x][y + 1] == '|' || grid[x][y + 1] == '-')
                    {
                        y++;
                    }
                    else if (grid[x][y + 1] == '+')
                    {
                        y++;
                        direction = 1;
                    }
                    else if (letters.Contains(grid[x][y + 1]))
                    {
                        encounters.Add(grid[x][y + 1]);
                        y++;
                    }
                    else
                    {
                        if (grid[x - 1][y + 1] == '-')
                            direction = 3;
                        if (grid[x + 1][y + 1] == '-')
                            direction = 1;
                        y++;
                    }
                }
                else if (direction == 1) // left
                {
                    if (grid[x-1][y] == '-' || grid[x - 1][y] == '|')
                    {
                        x--;
                    }
                    else if (grid[x - 1][y] == '+')
                    {
                        x--;
                        direction = 2;
                    }
                    else if (letters.Contains(grid[x - 1][y]))
                    {
                        encounters.Add(grid[x - 1][y]);
                        x--;
                    }
                    else
                    {
                        if (grid[x - 1][y - 1] == '-')
                            direction = 0;
                        if (grid[x - 1][y + 1] == '-')
                            direction = 2;
                        x--;
                    }
                }
                else if (direction == 2) // up
                {
                    if (grid[x][y - 1] == '|' || grid[x][y - 1] == '-')
                    {
                        y--;
                    }
                    else if (grid[x][y - 1] == '+')
                    {
                        y--;
                        direction = 3;
                    }
                    else if (letters.Contains(grid[x][y - 1]))
                    {
                        encounters.Add(grid[x][y - 1]);
                        y++;
                    }
                    else
                    {
                        if (grid[x - 1][y + 1] == '-')
                            direction = 1;
                        if (grid[x + 1][y + 1] == '-')
                            direction = 3;
                        y--;
                    }
                }
                else if (direction == 3) // right
                {
                    if (grid[x + 1][y] == '-' || grid[x + 1][y] == '|')
                    {
                        x++;
                    }
                    else if (grid[x + 1][y] == '+')
                    {
                        x++;
                        direction = 1;
                    }
                    else if (letters.Contains(grid[x + 1][y]))
                    {
                        encounters.Add(grid[x + 1][y]);
                        x++;
                    }
                    else
                    {
                        if (grid[x - 1][y - 1] == '-')
                            direction = 2;
                        if (grid[x - 1][y + 1] == '-')
                            direction = 0;
                        x++;
                    }
                }
                else break;
            }

        }
    }
}
