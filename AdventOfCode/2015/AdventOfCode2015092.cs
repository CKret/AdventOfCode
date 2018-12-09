using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Core;
using MoreLinq;

namespace AdventOfCode._2015
{
    /// <summary>
    /// The next year, just to show off, Santa decides to take the route with the longest distance instead.
    /// 
    /// He can still start and end at any two (different) locations he wants, and he still must visit each location exactly once.
    /// 
    /// For example, given the distances above, the longest route would be 982 via (for example) Dublin -&gt; London -&gt; Belfast.
    /// </summary>
    [AdventOfCode(2015, 9, 2, "What is the distance of the longest route?", 898)]
    public class AdventOfCode2015092 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var distances = File.ReadAllLines("2015/AdventOfCode201509.txt")
                .Select(s => Regex.Match(s, @"^(\w+) to (\w+) = (\d+)").Groups)
                .Select(g => new { From = g[1].Value, To = g[2].Value, Distance = int.Parse(g[3].Value, CultureInfo.InvariantCulture) })
                .ToList();

            var places = distances.SelectMany(d => new[] { d.From, d.To }).Distinct().ToList();

            Func<string, string, int> getDistance = (a, b) => distances.FirstOrDefault(d => d.From == a && d.To == b || d.To == a && d.From == b).Distance;

            Result = places.Permutations().Select(route => route.Pairwise((from, to) => getDistance(from, to)).Sum()).Max();
        }
    }
}
