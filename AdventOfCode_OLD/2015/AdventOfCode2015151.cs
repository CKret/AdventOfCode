using System.Globalization;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 15, 1, "", 13882464L)]
    public class AdventOfCode2015151 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var ingredients = File.ReadAllLines("2015\\AdventOfCode201515.txt").Select(line => line.Split(':'))
                .Select(ingredient => new
                {
                    Ingredient = ingredient[0],
                    Properties = ingredient[1].Split(',').Select(p => p.Trim().Split(' ')).ToDictionary(q => q[0], q => int.Parse(q[1], CultureInfo.InvariantCulture))
                }).ToList();

            var max = 0L;

            for (var i = 1; i < 100 - ingredients.Count; i++)
            {
                for (var j = 1; j < 100 - ingredients.Count; j++)
                {
                    for (var k = 1; k < 100 - ingredients.Count; k++)
                    {
                        var capacity = i * ingredients[0].Properties["capacity"] + j * ingredients[1].Properties["capacity"] + k * ingredients[2].Properties["capacity"] + (100 - i - j - k) * ingredients[3].Properties["capacity"];
                        var durability = i * ingredients[0].Properties["durability"] + j * ingredients[1].Properties["durability"] + k * ingredients[2].Properties["durability"] + (100 - i - j - k) * ingredients[3].Properties["durability"];
                        var flavor = i * ingredients[0].Properties["flavor"] + j * ingredients[1].Properties["flavor"] + k * ingredients[2].Properties["flavor"] + (100 - i - j - k) * ingredients[3].Properties["flavor"];
                        var texture = i * ingredients[0].Properties["texture"] + j * ingredients[1].Properties["texture"] + k * ingredients[2].Properties["texture"] + (100 - i - j - k) * ingredients[3].Properties["texture"];

                        if (capacity < 0) capacity = 0;
                        if (durability < 0) durability = 0;
                        if (flavor < 0) flavor = 0;
                        if (texture < 0) texture = 0;

                        long sum = capacity * durability * flavor * texture;

                        if (sum > max) max = sum;
                    }

                }
            }

            Result = max;
        }
    }
}
