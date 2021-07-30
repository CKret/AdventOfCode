using System.Collections.Immutable;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 24: Electromagnetic Moat ---
    /// 
    /// The CPU itself is a large, black building surrounded by a bottomless pit.
    /// Enormous metal tubes extend outward from the side of the building at
    /// regular intervals and descend down into the void. There's no way to cross,
    /// but you need to get inside.
    /// 
    /// No way, of course, other than building a bridge out of the magnetic
    /// components strewn about nearby.
    /// 
    /// Each component has two ports, one on each end. The ports come in all
    /// different types, and only matching types can be connected. You take an
    /// inventory of the components by their port types (your puzzle input). Each
    /// port is identified by the number of pins it uses; more pins mean a stronger
    /// connection for your bridge. A 3/7 component, for example, has a type-3 port
    /// on one side, and a type-7 port on the other.
    /// 
    /// Your side of the pit is metallic; a perfect surface to connect a magnetic,
    /// zero-pin port. Because of this, the first port you use must be of type 0.
    /// It doesn't matter what type of port you end with; your goal is just to make
    /// the bridge as strong as possible.
    /// 
    /// The strength of a bridge is the sum of the port types in each component.
    /// For example, if your bridge is made of components 0/3, 3/7, and 7/4, your
    /// bridge has a strength of 0+3 + 3+7 + 7+4 = 24.
    /// 
    /// For example, suppose you had the following components:
    /// 
    /// 0/2
    /// 2/2
    /// 2/3
    /// 3/4
    /// 3/5
    /// 0/1
    /// 10/1
    /// 9/10
    /// 
    /// With them, you could make the following valid bridges:
    /// 
    ///     - 0/1
    ///     - 0/1--10/1
    ///     - 0/1--10/1--9/10
    ///     - 0/2
    ///     - 0/2--2/3
    ///     - 0/2--2/3--3/4
    ///     - 0/2--2/3--3/5
    ///     - 0/2--2/2
    ///     - 0/2--2/2--2/3
    ///     - 0/2--2/2--2/3--3/4
    ///     - 0/2--2/2--2/3--3/5
    /// 
    /// (Note how, as shown by 10/1, order of ports within a component doesn't
    /// matter. However, you may only use each port on a component once.)
    /// 
    /// Of these bridges, the strongest one is 0/1--10/1--9/10; it has a strength
    /// of 0+1 + 1+10 + 10+9 = 31.
    /// 
    /// What is the strength of the strongest bridge you can make with the
    /// components you have available?
    /// 
    /// --- Part Two ---
    /// 
    /// The bridge you've built isn't long enough; you can't jump the rest of the
    /// way.
    /// 
    /// In the example above, there are two longest bridges:
    /// 
    ///     - 0/2--2/2--2/3--3/4
    ///     - 0/2--2/2--2/3--3/5
    /// 
    /// Of them, the one which uses the 3/5 component is stronger; its strength is
    /// 0+2 + 2+2 + 2+3 + 3+5 = 19.
    /// 
    /// What is the strength of the longest bridge you can make? If you can make
    /// multiple bridges of the longest length, pick the strongest one.
    /// </summary>
    [AdventOfCode(2017, 24, "Electromagnetic Moat", 1695, 1673)]
    public class AdventOfCode201724 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var input = File.ReadAllLines("2017\\AdventOfCode201724.txt").Select(l => l.Split('/').Select(int.Parse).ToArray()).Select(c => (c[0], c[1])).ToImmutableList();

            Result = BuildStrongBridge(input);
        }

        protected override object SolvePart1()
        {
            return BuildStrongBridge(Input.Select(l => l.Split('/').Select(int.Parse).ToArray()).Select(c => (c[0], c[1])).ToImmutableList());
        }

        protected override object SolvePart2()
        {
            return BuildLongBridge2(Input.Select(l => l.Split('/').Select(int.Parse).ToArray()).Select(c => (c[0], c[1])).ToImmutableList()).Item1;
        }

        public int BuildStrongBridge(IImmutableList<(int, int)> components, int cur = 0, int max = 0)
        {
            return components.Where(x => x.Item1 == cur || x.Item2 == cur)
                .Select(x => BuildStrongBridge(components.Remove(x), x.Item1 == cur ? x.Item2 : x.Item1, max + x.Item1 + x.Item2))
                .Concat(new[] { max }).Max();
        }

        public (int, int) BuildLongBridge2(IImmutableList<(int, int)> components, int cur = 0, int max = 0, int len = 0)
        {
            return components.Where(x => x.Item1 == cur || x.Item2 == cur)
                .Select(x => BuildLongBridge2(components.Remove(x), x.Item1 == cur ? x.Item2 : x.Item1, max + x.Item1 + x.Item2, len + 1))
                .Concat(new[] { (max, len) })
                .OrderByDescending(x => x.Item2)
                .ThenByDescending(x => x.Item1)
                .First();
        }

        public AdventOfCode201724(string sessionCookie) : base(sessionCookie) { }
    }
}
