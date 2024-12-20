using AdventOfCode.Core;
using SuperLinq;
using System.Linq;

namespace AdventOfCode._2022
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2022, 6, "Tuning Trouble", 1235, 3051)]
    public class AdventOfCode202206 : AdventOfCodeBase
    {
        public AdventOfCode202206(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            return Solve(4);
        }

        protected override object SolvePart2()
        {
            return Solve(14);
        }

        private int Solve(int ws)
        {
            return Input.First().IndexOf(string.Join("", Input.First().Window(ws).First(x => x.Distinct().Count() == ws))) + ws;
        }
    }
}
