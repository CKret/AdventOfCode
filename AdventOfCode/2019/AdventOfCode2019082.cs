using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using AdventOfCode.Core;
using AdventOfCode.Mathematics;
using AdventOfCode.VMs;
using Tesseract;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 8, 2, "", "RCYKR")]
    public class AdventOfCode2019082 : AdventOfCodeBase
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

            var image = new StringBuilder();

            for (var p = 0; p < layerSize; p++)
            {
                var curLayer = 0;
                while (curLayer < layers.Count && layers[curLayer][p] == '2') curLayer++;
                image.Append(curLayer < layers.Count ? layers[curLayer][p] : '2');
            }

            var img = image.ToString();
            var nLayer = new List<string>();
            for (var i = 0; i < img.Length; i += imageWidth)
            {
                nLayer.Add(string.Join("", img.Skip(i).Take(imageWidth).ToArray()));
            }

            using (var pass = new Bitmap(imageWidth + 100, imageHeight + 100))
            {
                for (var y = 0; y < imageHeight + 100; y++)
                {
                    for (var x = 0; x < imageWidth + 100; x++)
                        pass.SetPixel(x, y, Color.Black);
                }

                for (var y = 0; y < imageHeight; y++)
                {
                    for (var x = 0; x < imageWidth; x++)
                    {
                        pass.SetPixel(x + 50, y + 50, nLayer[y][x] == '0' ? Color.Black : Color.White);
                    }
                }

                pass.Save(@".\2019\AdventOfCode2019082.png");
            }

            // ReSharper disable once StringLiteralTypo
            Result = "RCYKR";       // As seen in the image.
        }
    }
}
