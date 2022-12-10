namespace AdventOfCode.ExtensionMethods
{
	public static class CharExtensionMethods
	{
		public static bool IsNumber(this char c)
		{
			return int.TryParse(c.ToString(), out _);
		}

		public static int ToInt(this char c)
		{
			int.TryParse(c.ToString(), out var value);
			return value;
		}
	}
}
