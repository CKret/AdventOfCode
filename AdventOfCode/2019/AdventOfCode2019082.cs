using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using AdventOfCode.Core;
using Tesseract;

namespace AdventOfCode._2019
{
    /// <summary>
    /// --- Day 8: Space Image Format ---
    ///
    /// --- Part Two ---
    /// 
    /// Now you're ready to decode the image. The image is rendered by stacking the
    /// layers and aligning the pixels with the same positions in each layer. The
    /// digits indicate the color of the corresponding pixel: 0 is black, 1 is
    /// white, and 2 is transparent.
    /// 
    /// The layers are rendered with the first layer in front and the last layer in
    /// back. So, if a given position has a transparent pixel in the first and
    /// second layers, a black pixel in the third layer, and a white pixel in the
    /// fourth layer, the final image would have a black pixel at that position.
    /// 
    /// For example, given an image 2 pixels wide and 2 pixels tall, the image data
    /// 0222112222120000 corresponds to the following image layers:
    /// 
    /// Layer 1: 02
    ///          22
    /// 
    /// Layer 2: 11
    ///          22
    /// 
    /// Layer 3: 22
    ///          12
    /// 
    /// Layer 4: 00
    ///          00
    /// 
    /// Then, the full image can be found by determining the top visible pixel in
    /// each position:
    /// 
    ///  - The top-left pixel is black because the top layer is 0.
    ///  - The top-right pixel is white because the top layer is 2 (transparent),
    ///    but the second layer is 1.
    ///  - The bottom-left pixel is white because the top two layers are 2, but
    ///    the third layer is 1.
    ///  - The bottom-right pixel is black because the only visible pixel in that
    ///    position is 0 (from layer 4).
    /// 
    /// So, the final image looks like this:
    /// 
    /// 01
    /// 10
    /// 
    /// What message is produced after decoding your image?
    /// </summary>
    [AdventOfCode(2019, 8, "Space Image Format - Part 2", "RCYKR")]
    public class AdventOfCode2019082 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2019\AdventOfCode201908.txt");

            const int imageWidth = 25;
            const int imageHeight = 6;

            const int layerSize = imageWidth * imageHeight;

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

            using (var imagePass = new Bitmap(imageWidth + 20, imageHeight + 20))
            {
                for (var y = 0; y < imageHeight + 20; y++)
                {
                    for (var x = 0; x < imageWidth + 20; x++)
                        imagePass.SetPixel(x, y, Color.Black);
                }

                for (var y = 0; y < imageHeight; y++)
                {
                    for (var x = 0; x < imageWidth; x++)
                    {
                        imagePass.SetPixel(x + 10, y + 10, nLayer[y][x] == '0' ? Color.Black : Color.White);
                    }
                }

                var bigImage = new Bitmap(imagePass, new Size(imagePass.Width * 4, imagePass.Height * 4));
                bigImage.Save(@".\2019\AdventOfCode2019082.png");

                using (var engine = new TesseractEngine(@".\_ExternalDependencies\tessdata", "eng", EngineMode.Default))
                using (var pix = PixConverter.ToPix(bigImage))
                using (var page = engine.Process(pix))
                {
                    Result = page.GetText().Trim('\n');
                }

                bigImage.Dispose();
            }
        }

        public AdventOfCode2019082(string sessionCookie) : base(sessionCookie) { }
    }
}
