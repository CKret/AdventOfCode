using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.Mathematics;
using AdventOfCode.VMs;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 8, 1, "", 1320)]
    public class AdventOfCode2019081 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201908.txt");

            var imageWidth = 25;
            var imageHeight = 6;

            var layerSize = imageWidth * imageHeight;

            var layers = new List<string>();
            for (var i = 0; i < data.Length; i += layerSize)
            {
                var layer = string.Empty;
                for (var j = 0; j < layerSize; j++)
                {
                    layer += data[i + j];
                }
                layers.Add(layer);
            }

            var minZeroes = layers.Min(x => x.Count(y => y == '0'));
            minZeroes = Int32.MaxValue;
            var minLayer = string.Empty;
            foreach (var l in layers)
            {
                var c = l.Count(x => x == '0');
                if (c < minZeroes)
                {
                    minZeroes = c;
                    minLayer = l;
                }
            }

            Result = minLayer.Count(x => x == '1') * minLayer.Count(x => x == '2');
        }
    }
}
