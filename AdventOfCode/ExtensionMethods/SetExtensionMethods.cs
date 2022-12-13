using System.Collections.Generic;
using AdventOfCode.Mathematics.PathFinding.Core;

namespace AdventOfCode.ExtensionMethods
{
    public static class SetExtensionMethods
    {
        /// <summary>
        /// Adds or Updates the dictionary to include the destination and its associated cost 
        /// and complete path (and param arrays make paths easier to work with)
        /// </summary>
        public static void Set<T>(this Dictionary<T, KeyValuePair<int, LinkedList<PathCost<T>>>> dictionary, T destination, int cost, params PathCost<T>[] paths)
        {
            var completePath = paths == null ? new LinkedList<PathCost<T>>() : new LinkedList<PathCost<T>>(paths);
            dictionary[destination] = new KeyValuePair<int, LinkedList<PathCost<T>>>(cost, completePath);
        }
    }
}
