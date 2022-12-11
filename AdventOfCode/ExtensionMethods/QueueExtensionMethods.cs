using System.Collections.Generic;

namespace AdventOfCode.ExtensionMethods
{
	public static class QueueExtensionMethods
	{
		public static void EnqueueRange<T>(this Queue<T> q, IEnumerable<T> items)
		{
			foreach (var item in items)
			{
				q.Enqueue(item);
			}
		}
	}
}
