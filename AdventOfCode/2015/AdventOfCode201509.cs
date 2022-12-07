using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Core;
using SuperLinq;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 9, "", 251, 898)]
    public class AdventOfCode201509 : AdventOfCodeBase
    {
        public AdventOfCode201509(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var distances = Input
                .Select(s => Regex.Match(s, @"^(\w+) to (\w+) = (\d+)").Groups)
                .Select(g => new { From = g[1].Value, To = g[2].Value, Distance = int.Parse(g[3].Value, CultureInfo.InvariantCulture) });

            var places = distances.SelectMany(d => new[] { d.From, d.To }).Distinct();

            int GetDistance(string a, string b) => distances.FirstOrDefault(d => d.From == a && d.To == b || d.To == a && d.From == b).Distance;

            return places.Permutations().Select(route => route.Window(2).Select(x => GetDistance(x.First(), x.Last())).Sum()).Min();
        }

        protected override object SolvePart2()
        {
            var distances = Input
                .Select(s => Regex.Match(s, @"^(\w+) to (\w+) = (\d+)").Groups)
                .Select(g => new { From = g[1].Value, To = g[2].Value, Distance = int.Parse(g[3].Value, CultureInfo.InvariantCulture) })
                .ToList();

            var places = distances.SelectMany(d => new[] { d.From, d.To }).Distinct().ToList();

            int GetDistance(string a, string b) => distances.FirstOrDefault(d => d.From == a && d.To == b || d.To == a && d.From == b).Distance;

            return places.Permutations().Select(route => route.Window(2).Select(x => GetDistance(x.First(), x.Last())).Sum()).Max();
        }
    }
}
