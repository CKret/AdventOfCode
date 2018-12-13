using System.IO;
using System.Linq;
using System.Text;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2018
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2018, 12, 2, "", 1250000000991L)]
    public class AdventOfCode2018122 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var maxGenerations = 50000000000;
            var data = File.ReadAllLines(@"2018\AdventOfCode201812.txt");
            var initialState = data[0].Substring(15);
            var transformations = data.Skip(2).ToArray().Select(t => (Pattern: t.Split(" => ")[0], Result: t.Split(" => ")[1][0])).ToArray();
            var prependedPots = 0;
            var currentGen = initialState;
            var previousGenSumDiff = 0;
            var previousGenSum = 0;

            for (var i = 0L; i < maxGenerations; i++)
            {
                var nextGen = new StringBuilder(currentGen);

                var prependCount = 4 - currentGen.IndexOf('#');
                if (prependCount > 0)
                {
                    nextGen.Insert(0, ".", prependCount);
                    prependedPots += prependCount;
                }

                var appendCount = 5 - (currentGen.Length - currentGen.LastIndexOf('#'));
                if (appendCount > 0) nextGen.Append('.', appendCount);


                currentGen = nextGen.ToString();
                for (var p = 0; p < currentGen.Length - 5; p++)
                {
                    nextGen[p + 2] = transformations.Single(t => t.Pattern == currentGen.Substring(p, 5)).Result;
                }

                currentGen = nextGen.ToString();
                var currentGenSum = currentGen.Select((c, j) => new { Pot = c, Index = j - prependedPots }).Where(c => c.Pot == '#').Sum(c => c.Index);
                var currentGenSumDiff = currentGenSum - previousGenSum;
                if (currentGenSumDiff == previousGenSumDiff)
                {
                    Result = currentGenSum + currentGen.Count(c => c == '#') * (maxGenerations - i - 1);
                    break;
                }
                previousGenSum = currentGenSum;
                previousGenSumDiff = currentGenSumDiff;
            }
        }
    }
}
