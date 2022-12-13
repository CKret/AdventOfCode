using System.Collections.Generic;

namespace AdventOfCode.Mathematics.PathFinding.Core
{
	public class Graph<T>
	{
		public Dictionary<T, HashSet<T>> Neighbors { get; } = new();

		public Graph(IEnumerable<T> vertices, IEnumerable<(T From, T To)> edges, bool isDirected = false)
		{
			foreach (var vertex in vertices)
				AddVertex(vertex);

			foreach (var edge in edges)
				AddEdge(edge, isDirected);
		}

		public void AddVertex(T node)
		{
			Neighbors[node] = new HashSet<T>();
		}

		public void AddEdge((T From, T To) edge, bool isDirected = false)
		{
			if (Neighbors.ContainsKey(edge.From)) Neighbors[edge.From].Add(edge.To);
			if (!isDirected && Neighbors.ContainsKey(edge.To)) Neighbors[edge.To].Add(edge.From);
		}
	}
}
