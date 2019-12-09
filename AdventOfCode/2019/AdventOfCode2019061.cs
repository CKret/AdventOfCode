using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode._2018;
using AdventOfCode.Core;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 6, 1, "", 273985)]
    public class AdventOfCode2019061 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllLines(@"2019\AdventOfCode201906.txt");

            var objects = new List<SpaceObject>();

            foreach (var obj in data)
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
                    p2 = new SpaceObject {Id = o[1], Orbits = p1};
                    objects.Add(p2);
                }

                p2.Orbits = p1;
                p1.OrbitedBy.Add(p2);
            }

            Result = objects.Sum(o => CountOrbits(o));

            var san = objects.Single(x => x.Id == "SAN");
            var you = objects.Single(x => x.Id == "YOU");

            var s = FindSanta(you);

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

    public class Orbit
    {
        public SpaceObject CenterOfMass { get; set; }
        public SpaceObject OrbitingObject { get; set; }
    }
}
