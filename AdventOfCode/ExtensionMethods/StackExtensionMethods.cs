using System.Collections.Generic;

namespace AdventOfCode.ExtensionMethods
{
	public static class StackExtensionMethods
	{
		public static void PushRange<T>(this Stack<T> q, IEnumerable<T> items)
		{
			foreach (var item in items)
			{
				q.Push(item);
			}
		}
	}
}
