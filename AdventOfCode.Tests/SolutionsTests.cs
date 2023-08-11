using System.Reflection;
using AdventOfCode.Core;

namespace AdventOfCode.Tests
{
	public class SolutionsTests
	{
		[Fact]
		public void AoC()
		{
			System.Attribute[] attrs = System.Attribute.GetCustomAttributes(typeof(_2018.AdventOfCode201801));
			var assem = Assembly.GetAssembly(typeof(AdventOfCodeBase));

			var t = assem?.GetTypes().Where(t => t.IsSubclassOf(typeof(AdventOfCodeBase)));
		}
	}
}