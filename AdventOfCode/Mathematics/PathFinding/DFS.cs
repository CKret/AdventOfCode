using AdventOfCode.ExtensionMethods;
using AdventOfCode.Mathematics.PathFinding.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Mathematics.PathFinding
{
	public static class DFS
	{
		public static HashSet<T> Search<T>(Graph<T> graph, T start, Action<T> preVisit = null)
		{
			var visited = new HashSet<T>();

			if (!graph.Neighbors.ContainsKey(start))
				return visited;

			var queue = new Stack<T>();
			queue.Push(start);

			while (queue.Count > 0)
			{
				var node = queue.Pop();

				if (visited.Contains(node))
					continue;

				if (preVisit != null)
					preVisit(node);

				visited.Add(node);
				queue.PushRange(graph.Neighbors[node].Where(neighbor => !visited.Contains(neighbor)));
			}

			return visited;
		}

		public static Func<T, IEnumerable<T>> ShortestPath<T>(Graph<T> graph, T start)
		{
			var visited = new Dictionary<T, T>();

			var queue = new Stack<T>();
			queue.Push(start);

			while (queue.Count > 0)
			{
				var node = queue.Pop();
				foreach (T neighbor in graph.Neighbors[node].Where(neighbor => !visited.ContainsKey(neighbor)))
				{
					visited[neighbor] = node;
					queue.Push(neighbor);
				}
			}

			IEnumerable<T> ShortestPath(T v)
			{
				var path = new List<T>();

				var current = v;
				while (!current.Equals(start))
				{
					path.Add(current);
					if (!visited.ContainsKey(current)) return new List<T>();
					current = visited[current];
				}

				path.Add(start);
				path.Reverse();

				return path;
			}

			return ShortestPath;
		}
	}
}
