using System.Collections.Generic;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode._2020
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2020, 17, "Day 17: Conway Cubes", 388, 2280)]
    public class AdventOfCode202017 : AdventOfCodeBase
    {
        public AdventOfCode202017(string sessionCookie) : base(sessionCookie) { }

        private List<(int x, int y, int z, int w)> neighbourOffsets;
        private Dictionary<(int x, int y, int z, int w), char> cubes;
        
        public override void Solve()
        {
            var timer = new Stopwatch();

            neighbourOffsets = GenerateNeighbours().ToList();
            neighbourOffsets.Remove((0, 0, 0, 0));
            cubes = Input.SelectMany((x, i) => x.Select((y, j) => (Coord: (j, i, 0, 0), State: y)))
                         .ToDictionary(x => x.Coord, x => x.State);

            timer.Start();
            ResultPart1 = SolvePart1();
            timer.Stop();
            TimePart1 = timer.ElapsedTicks.ToMilliseconds();

            timer.Start();
            ResultPart2 = SolvePart2();
            timer.Stop();
            TimePart2 = timer.ElapsedTicks.ToMilliseconds();
        }

        private object SolvePart1()
        {
            neighbourOffsets = GenerateNeighbours().ToList();
            neighbourOffsets.Remove((0, 0, 0, 0));
            cubes = Input.SelectMany((x, i) => x.Select((y, j) => (Coord: (j, i, 0, 0), State: y)))
                         .ToDictionary(x => x.Coord, x => x.State);

            var result = 0;
            for (var i = 0; i < 6; i++)
            {
                result = RunCycle3D();
            }

            return result;
        }

        private object SolvePart2()
        {
            neighbourOffsets = GenerateNeighbours().ToList();
            neighbourOffsets.Remove((0, 0, 0, 0));
            cubes = Input.SelectMany((x, i) => x.Select((y, j) => (Coord: (j, i, 0, 0), State: y)))
                         .ToDictionary(x => x.Coord, x => x.State);

            var result = 0;
            for (var i = 0; i < 6; i++)
            {
                result = RunCycle4D();
            }

            return result;
        }

        private IEnumerable<(int x, int y, int z, int w)> GenerateNeighbours()
        {
            for (var x = -1; x <= 1; x++)
            for (var y = -1; y <= 1; y++)
            for (var z = -1; z <= 1; z++)
            for (var w = -1; w <= 1; w++)
                yield return (x, y, z, w);
        }

        private void Expand3DUniverse()
        {
            foreach (var (x, y, z, _) in cubes.Keys.ToArray())
            {
                AddNeighbours(neighbourOffsets.Select(c => (x + c.x, y + c.y, z + c.z, 0)));
            }
        }

        private void Expand4DUniverse()
        {
            foreach (var (x, y, z, w) in cubes.Keys.ToArray())
            {
                AddNeighbours(neighbourOffsets.Select(c => (x + c.x, y + c.y, z + c.z, w + c.w)));
            }
        }

        private void AddNeighbours(IEnumerable<(int, int, int, int)> neighbours)
        {
            foreach (var neighbour in neighbours)
            {
                if (!cubes.TryGetValue(neighbour, out var value))
                {
                    cubes[neighbour] = '.';
                }
            }
        }

        private int RunCycle3D()
        {
            var nextUniverseState = new Dictionary<(int x, int y, int z, int w), char>();

            Expand3DUniverse();

            var keys = cubes.Keys.ToList();
            foreach (var key in keys)
            {
                var activeNeighbours = neighbourOffsets.Where(x => x.w == 0)
                                                       .Select(x => (key.x + x.x, key.y + x.y, key.z + x.z, 0))
                                                       .Count(x => cubes.ContainsKey(x) && cubes[x] == '#');

                char nextCubeState;

                if (cubes[key] == '#')
                    nextCubeState = activeNeighbours == 2 || activeNeighbours == 3 ? '#' : '.';
                else
                    nextCubeState = activeNeighbours == 3 ? '#' : '.';

                nextUniverseState[key] = nextCubeState;

            }
            cubes = nextUniverseState;
            return cubes.Count(c => c.Value == '#');
        }

        private int RunCycle4D()
        {
            var nextUniverseState = new Dictionary<(int x, int y, int z, int w), char>();

            Expand4DUniverse();

            var keys = cubes.Keys.ToList();
            foreach (var key in keys)
            {
                var activeNeighbours = neighbourOffsets
                                       .Select(x => (key.x + x.x, key.y + x.y, key.z + x.z, key.w + x.w))
                                       .Count(x => cubes.ContainsKey(x) && cubes[x] == '#');

                char nextCubeState;

                if (cubes[key] == '#')
                    nextCubeState = activeNeighbours == 2 || activeNeighbours == 3 ? '#' : '.';
                else
                    nextCubeState = activeNeighbours == 3 ? '#' : '.';

                nextUniverseState[key] = nextCubeState;

            }
            cubes = nextUniverseState;
            return cubes.Count(c => c.Value == '#');
        }
    }
}
