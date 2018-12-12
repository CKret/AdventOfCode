using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AdventOfCode.Core;
using AdventOfCode.Mathematics.Cryptography;
using MoreLinq;
using AdventOfCode.ExtensionMethods;

namespace AdventOfCode._2018
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2018, 12, 1, "", null)]
    public class AdventOfCode2018121 : AdventOfCodeBase
    {
        public override void Solve()
        {
            //var data = File.ReadAllLines(@"2018\AdventOfCode201812.txt");

            //var initialState = string.Empty;
            //for (var i = 0; i < 1000; i++)
            //{
            //    initialState += ".";
            //}
            //initialState += data[0].Substring(15);
            //for (var i = 0; i < 1000; i++)
            //{
            //    initialState += ".";
            //}

            //var transformations = data.Skip(2).ToArray().Select(t => (Pattern: t.Split(new[] { " => " }, StringSplitOptions.None)[0], Result: t.Split(new[] { " => " }, StringSplitOptions.None)[1][0])).ToArray();

            //var currentGen = initialState;
            //for (var i = 0; i < 20; i++)
            //{
            //    var nextGen = new StringBuilder(currentGen);
            //    for (var p = 0; p < currentGen.Length - 5; p++)
            //    {
            //        var c = p + 2;
            //        if (transformations.Any(t => t.Pattern == currentGen.Substring(p, 5) && currentGen[c] != t.Result))
            //        {
            //            var trans = transformations.Single(t => t.Pattern == currentGen.Substring(p, 5));
            //            nextGen[c] = trans.Result;
            //        }
            //        else
            //        {
            //            nextGen[p] = currentGen[p];
            //        }
            //    }

            //    currentGen = nextGen.ToString();
            //    //if (currentGen[2] == '#')
            //    //{
            //    //    currentGen = "....." + currentGen;
            //    //}
            //    //if (currentGen[currentGen.Length - 2] == '#')
            //    //{
            //    //    currentGen += ".....";
            //    //}

            //}

            //var sum = 0;
            //for (var i = -1000; i < initialState.Length - 1000; i++)
            //{
            //    if (currentGen[i + 1000] == '#') sum += i;
            //}

            //Result = sum;



            HashSet<int> currentPlants = new HashSet<int>();
            Dictionary<int, bool> plantRules = new Dictionary<int, bool>();
            StreamReader file = new StreamReader(@"2018\AdventOfCode201812.txt");

            string line = file.ReadLine();
            line.Skip(15).Select((x, i) => new { x, i }).Where(c => c.x == '#').Select(c => c.i).ToList().ForEach(x => currentPlants.Add(x));
            line = file.ReadLine();
            while (!file.EndOfStream)
            {
                line = file.ReadLine();
                int binary = line.Take(5).Select((x, i) => new { x, i }).Where(c => c.x == '#').Sum(c => (int)Math.Pow(2, c.i));
                plantRules.Add(binary, line[9] == '#' ? true : false);
            }

            long iterations = 20;
            long totalSum = 0;
            HashSet<int> newPlants = new HashSet<int>();

            for (int iter = 1; iter <= iterations; iter++)
            {
                newPlants = new HashSet<int>();
                int min = currentPlants.Min() - 3;
                int max = currentPlants.Max() + 3;

                for (int pot = min; pot <= max; pot++)
                {
                    int sum = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (currentPlants.Contains(pot + i - 2)) sum += (int)Math.Pow(2, i);
                    }
                    if (plantRules[sum]) newPlants.Add(pot);
                }
                //// the simulation converged to a stable point
                //if (currentPlants.Select(x => x + 1).Except(newPlants).Count() == 0)
                //{
                //    currentPlants = newPlants;
                //    totalSum = currentPlants.Sum();
                //    totalSum += currentPlants.Count() * (iterations - iter);
                //    break;
                //}

                currentPlants = newPlants;
            }

            totalSum = currentPlants.Sum();

            Result = totalSum;

        }
    }
}
