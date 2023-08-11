using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Mathematics.PathFinding.Core;

namespace AdventOfCode.Mathematics.PathFinding
{
	public static class BFS
	{
		public static HashSet<T> Search<T>(Graph<T> graph, T start, Action<T> preVisit = null)
		{
			var visited = new HashSet<T>();

			if (!graph.Neighbors.ContainsKey(start))
				return visited;

			var queue = new Queue<T>();
			queue.Enqueue(start);

			while (queue.Count > 0)
			{
				var node = queue.Dequeue();

				if (visited.Contains(node))
					continue;

				if (preVisit != null)
					preVisit(node);

				visited.Add(node);
				queue.EnqueueRange(graph.Neighbors[node].Where(neighbor => !visited.Contains(neighbor)));
			}

			return visited;
		}

		public static Func<T, IEnumerable<T>> PathsFrom<T>(Graph<T> graph, T start)
		{
			var visited = new Dictionary<T, T>();

			var queue = new Queue<T>();
			queue.Enqueue(start);

			while (queue.Count > 0)
			{
				var node = queue.Dequeue();
				foreach (T neighbor in graph.Neighbors[node].Where(neighbor => !visited.ContainsKey(neighbor)))
				{
					visited[neighbor] = node;
					queue.Enqueue(neighbor);
				}
			}

			IEnumerable<T> ShortestPathTo(T v)
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

			return ShortestPathTo;
		}
	}
}
