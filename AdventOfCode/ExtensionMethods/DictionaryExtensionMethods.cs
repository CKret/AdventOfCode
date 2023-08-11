using System.Collections.Generic;

namespace AdventOfCode.ExtensionMethods
{
	public static class DictionaryExtensionMethods
	{
		public static void TryAdd<T1, T2>(this Dictionary<T1, T2> source, T1 key, T2 value)
		{
			if (!source.ContainsKey(key))
				source.Add(key, value);
		}

		public static void TrySet<T1, T2>(this Dictionary<T1, T2> source, T1 key, T2 value)
		{
			if (!source.ContainsKey(key))
				source.Add(key, value);
			else
				source[key] = value;
		}

		public static V Read<K, V>(this Dictionary<K, V> dict, K key, V def = default(V))
		{
			if (dict.ContainsKey(key)) return dict[key];
			return def;
		}
	}
}
