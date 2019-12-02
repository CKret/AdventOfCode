using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.ExtensionMethods;
using AdventOfCode.Mathematics.PathFinding.Core;

namespace AdventOfCode.Mathematics.PathFinding
{
    public static class Dijkstras
    {
        public static LinkedList<PathFindingBase<T>> CalculateShortestPathBetween<T>(T source, T destination, IEnumerable<PathFindingBase<T>> paths)
        {
            return CalculateFrom(source, paths)[destination];
        }

        public static Dictionary<T, LinkedList<PathFindingBase<T>>> CalculateShortestFrom<T>(T source, IEnumerable<PathFindingBase<T>> paths)
        {
            return CalculateFrom(source, paths);
        }

        private static Dictionary<T, LinkedList<PathFindingBase<T>>> CalculateFrom<T>(T source, IEnumerable<PathFindingBase<T>> paths)
        {
            // validate the paths
            if (paths.Any(p => p.Source.Equals(p.Destination)))
                throw new ArgumentException("No path can have the same source and destination");

            // keep track of the shortest paths identified thus far
            Dictionary<T, KeyValuePair<int, LinkedList<PathFindingBase<T>>>> shortestPaths = new Dictionary<T, KeyValuePair<int, LinkedList<PathFindingBase<T>>>>();

            // keep track of the locations which have been completely processed
            List<T> locationsProcessed = new List<T>();

            // include all possible steps, with Int.MaxValue cost
            paths.SelectMany(p => new T[] { p.Source, p.Destination })           // union source and destinations
                    .Distinct()                                                  // remove duplicates
                    .ToList()                                                    // ToList exposes ForEach
                    .ForEach(s => shortestPaths.Set(s, Int32.MaxValue, null));   // add to ShortestPaths with MaxValue cost

            // update cost for self-to-self as 0; no path
            shortestPaths.Set(source, 0, null);

            // keep this cached
            var locationCount = shortestPaths.Keys.Count;

            while (locationsProcessed.Count < locationCount)
            {
                T _locationToProcess = default(T);

                //Search for the nearest location that isn't handled
                foreach (T _location in shortestPaths.OrderBy(p => p.Value.Key).Select(p => p.Key).ToList())
                {
                    if (!locationsProcessed.Contains(_location))
                    {
                        if (shortestPaths[_location].Key == Int32.MaxValue)
                            return shortestPaths.ToDictionary(k => k.Key, v => v.Value.Value); //ShortestPaths[destination].Value;

                        _locationToProcess = _location;
                        break;
                    }
                } // foreach

                var selectedPaths = paths.Where(p => p.Source.Equals(_locationToProcess));

                foreach (PathFindingBase<T> path in selectedPaths)
                {
                    if (shortestPaths[path.Destination].Key > path.Cost + shortestPaths[path.Source].Key)
                    {
                        shortestPaths.Set(
                            path.Destination,
                            path.Cost + shortestPaths[path.Source].Key,
                            shortestPaths[path.Source].Value.Union(new PathFindingBase<T>[] { path }).ToArray());
                    }
                }

                //Add the location to the list of processed locations
                locationsProcessed.Add(_locationToProcess);
            } // while

            return shortestPaths.ToDictionary(k => k.Key, v => v.Value.Value);
            //return ShortestPaths[destination].Value;
        }
    }
}
