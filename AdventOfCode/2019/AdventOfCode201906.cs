using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2019
{
    /// <summary>
    /// --- Day 6: Universal Orbit Map ---
    /// 
    /// You've landed at the Universal Orbit Map facility on Mercury. Because
    /// navigation in space often involves transferring between orbits, the orbit
    /// maps here are useful for finding efficient routes between, for example, you
    /// and Santa. You download a map of the local orbits (your puzzle input).
    /// 
    /// Except for the universal Center of Mass (COM), every object in space is in
    /// orbit around exactly one other object. An orbit looks roughly like this:
    /// 
    ///                    \
    ///                     \
    ///                      |
    ///                      |
    ///    AAA  o            o  BBB
    ///                      |
    ///                      |
    ///                     /
    ///                    /
    /// In this diagram, the object BBB is in orbit around AAA. The path that BBB
    /// takes around AAA (drawn with lines) is only partly shown. In the map data,
    /// this orbital relationship is written AAA)BBB, which means "BBB is in orbit
    /// around AAA".
    /// 
    /// Before you use your map data to plot a course, you need to make sure it
    /// wasn't corrupted during the download. To verify maps, the Universal Orbit
    /// Map facility uses orbit count checksums - the total number of direct orbits
    /// (like the one shown above) and indirect orbits.
    /// 
    /// Whenever A orbits B and B orbits C, then A indirectly orbits C. This chain
    /// can be any number of objects long: if A orbits B, B orbits C, and C orbits
    /// D, then A indirectly orbits D.
    /// 
    /// For example, suppose you have the following map:
    ///
    /// COM)B
    /// B)C
    /// C)D
    /// D)E
    /// E)F
    /// B)G
    /// G)H
    /// D)I
    /// E)J
    /// J)K
    /// K)L
    /// 
    /// Visually, the above map of orbits looks like this:
    /// 
    ///         G - H       J - K - L
    ///        /           /
    /// COM - B - C - D - E - F
    ///                \
    ///                 I
    /// 
    /// In this visual representation, when two objects are connected by a line,
    /// the one on the right directly orbits the one on the left.
    /// 
    /// Here, we can count the total number of orbits as follows:
    /// 
    ///  - D directly orbits C and indirectly orbits B and COM, a total of 3
    ///    orbits.
    ///  - L directly orbits K and indirectly orbits J, E, D, C, B, and COM, a
    ///    total of 7 orbits.
    ///  - COM orbits nothing.
    /// 
    /// The total number of direct and indirect orbits in this example is 42.
    /// 
    /// What is the total number of direct and indirect orbits in your map data?
    ///
    /// --- Part Two ---
    /// 
    /// Now, you just need to figure out how many orbital transfers you (YOU) need
    /// to take to get to Santa (SAN).
    /// 
    /// You start at the object YOU are orbiting; your destination is the object
    /// SAN is orbiting. An orbital transfer lets you move from any object to an
    /// object orbiting or orbited by that object.
    /// 
    /// For example, suppose you have the following map:
    /// 
    /// COM)B
    /// B)C
    /// C)D
    /// D)E
    /// E)F
    /// B)G
    /// G)H
    /// D)I
    /// E)J
    /// J)K
    /// K)L
    /// K)YOU
    /// I)SAN
    /// 
    /// Visually, the above map of orbits looks like this:
    /// 
    ///                   YOU
    ///                  /
    ///          G - H       J - K - L
    ///         /           /
    /// COM - B - C - D - E - F
    ///                \
    ///                 I - SAN
    ///
    /// In this example, YOU are in orbit around K, and SAN is in orbit around I.
    /// To move from K to I, a minimum of 4 orbital transfers are required:
    /// 
    ///  - K to J
    ///  - J to E
    ///  - E to D
    ///  - D to I
    /// 
    /// Afterward, the map of orbits looks like this:
    /// 
    ///         G - H       J - K - L
    ///        /           /
    /// COM - B - C - D - E - F
    ///                \
    ///                 I - SAN
    ///                  \
    ///                   YOU
    /// 
    /// What is the minimum number of orbital transfers required to move from the
    /// object YOU are orbiting to the object SAN is orbiting? (Between the objects
    /// they are orbiting - not between YOU and SAN.)
    /// </summary>
    [AdventOfCode(2019, 6, "Universal Orbit Map", 273985, 460)]
    public class AdventOfCode201906 : AdventOfCodeBase
    {
        private List<SpaceObject> objects;

        public AdventOfCode201906(string sessionCookie) : base(sessionCookie) { }

        public override void Solve()
        {
            var objects = new List<SpaceObject>();

            foreach (var obj in Input)
            {
                var o = obj.Split(')');

                var p1 = objects.SingleOrDefault(x => x.Id == o[0]);
                var p2 = objects.SingleOrDefault(x => x.Id == o[1]);

                if (p1 == null)
                {
                    p1 = new SpaceObject { Id = o[0] };
                    objects.Add(p1);
                }

                if (p2 == null)
                {
                    p2 = new SpaceObject { Id = o[1], Orbits = p1 };
                    objects.Add(p2);
                }

                p2.Orbits = p1;
                p1.OrbitedBy.Add(p2);
            }

            base.Solve();
        }

        protected override object SolvePart1()
        {
            return objects.Sum(o => CountOrbits(o));
        }

        protected override object SolvePart2()
        {
            var you = objects.Single(x => x.Id == "YOU");
            return FindSanta(you);
        }

        public int CountOrbits(SpaceObject o, int count = 0)
        {
            return o.Orbits == null ? count : CountOrbits(o.Orbits, count + 1);
        }

        public int FindSanta(SpaceObject o, int count = 0)
        {
            o.Visited = true;
            if (o.Orbits == null) return 0;
            if (o.OrbitedBy.SingleOrDefault(x => x.Id == "SAN") != null) return count - 1;

            foreach (var p in o.OrbitedBy.Where(x => x.Visited == false))
            {
                var sub = FindSanta(p, count + 1);
                if (sub > 0) return sub;
            }

            return o.Orbits.Visited ? 0 : FindSanta(o.Orbits, count + 1);
        }
    }

    public class SpaceObject
    {
        public string Id { get; set; }
        public SpaceObject Orbits { get; set; }
        public List<SpaceObject> OrbitedBy = new List<SpaceObject>();
        public bool Visited { get; set; }
    }
}
