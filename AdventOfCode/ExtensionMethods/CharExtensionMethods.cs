namespace AdventOfCode.ExtensionMethods
{
	public static class CharExtensionMethods
	{
		public static bool IsInt(this char c)
		{
			return int.TryParse(c.ToString(), out _);
		}
	}
}
