using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Core;
using MoreLinq;

namespace AdventOfCode._2015
{
    /// <summary>
    /// Every year, Santa manages to deliver all of his presents in a single night.
    /// 
    /// This year, however, he has some new locations to visit; his elves have provided
    /// him the distances between every pair of locations. He can start and end at any
    /// two (different) locations he wants, but he must visit each location exactly once.
    /// What is the shortest distance he can travel to achieve this?
    /// </summary>
    [AdventOfCode(2015, 9, 1, "What is the distance of the shortest route?", 251)]
    public class AdventOfCode2015091 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var distances = File.ReadAllLines("2015/AdventOfCode201509.txt")
                .Select(s => Regex.Match(s, @"^(\w+) to (\w+) = (\d+)").Groups)
                .Select(g => new { From = g[1].Value, To = g[2].Value, Distance = int.Parse(g[3].Value, CultureInfo.InvariantCulture) })
                .ToList();

            var places = distances.SelectMany(d => new[] { d.From, d.To }).Distinct().ToList();

            int GetDistance(string a, string b) => distances.FirstOrDefault(d => d.From == a && d.To == b || d.To == a && d.From == b).Distance;

            Result = places.Permutations().Select(route => route.Pairwise(GetDistance).Sum()).Min();
        }

        public AdventOfCode2015091(string sessionCookie) : base(sessionCookie) { }
    }
}
