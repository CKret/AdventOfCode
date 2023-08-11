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
        public static void Set<T>(this Dictionary<T, KeyValuePair<int, LinkedList<Vertex<T>>>> dictionary, T destination, int cost, params Vertex<T>[] paths)
        {
            var completePath = paths == null ? new LinkedList<Vertex<T>>() : new LinkedList<Vertex<T>>(paths);
            dictionary[destination] = new KeyValuePair<int, LinkedList<Vertex<T>>>(cost, completePath);
        }
    }
}
