﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 6, 2, "", 460)]
    public class AdventOfCode2019062 : AdventOfCodeBase
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
                    p2 = new SpaceObject { Id = o[1], Orbits = p1 };
                    objects.Add(p2);
                }

                p2.Orbits = p1;
                p1.OrbitedBy.Add(p2);
            }

            var you = objects.Single(x => x.Id == "YOU");
            Result = FindSanta(you);
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
}