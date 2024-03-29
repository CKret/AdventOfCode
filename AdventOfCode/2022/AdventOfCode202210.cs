using System;
using System.Drawing;
using System.Linq;
using AdventOfCode.Core;
using AdventOfCode.ExtensionMethods;
using Tesseract;
// ReSharper disable StringLiteralTypo

namespace AdventOfCode._2022
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2022, 10, "Cathode-Ray Tube", 14040, "ZGCJZJFL")]
	public class AdventOfCode202210 : AdventOfCodeBase
	{
		public AdventOfCode202210(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			return Input
				.SelectMany(r => r.Split(" "))
				.Select(r => r[^1].IsNumber() ? int.Parse(r) : 0)
				.Scan(1, ((a, b) => a + b))
				.Select((register, cycle) => (cycle + 2) % 40 == 20 ? (cycle + 2) * register : 0)
				.Sum();
		}

		protected override object SolvePart2()
		{
#pragma warning disable CA1416 // Validate platform compatibility
			//Input
			//	.SelectMany(r => r.Split(" "))
			//	.Select(r => r[^1].IsNumber() ? int.Parse(r) : 0)
			//	.Scan(1, ((a, b) => a + b))
			//	.Prepend(1)
			//	.Select((register, cycle) => Math.Abs((cycle) % 40 - register) < 2 ? '#' : ' ')
			//	.Chunk(40)
			//	.Select(s => string.Join("", s))
			//	.ForEach(s => Console.WriteLine(s));

			var result = Input
				.SelectMany(r => r.Split(" "))
				.Select(r => r[^1].IsNumber() ? int.Parse(r) : 0)
				.Scan(1, ((a, b) => a + b))
				.Prepend(1)
				.Select((register, cycle) => Math.Abs((cycle) % 40 - register) < 2 ? '#' : ' ')
				.Chunk(40)
				.Select(s => string.Join("", s))
				.ToArray();

			using var image = new Bitmap(result[0].Length + 20, result.Length + 20);

			for (var y = 0; y < image.Height; y++)
				for (var x = 0; x < image.Width; x++)
					image.SetPixel(x, y, Color.White);

			for (var y = 0; y < result.Length; y++)
				for (var x = 0; x < result[y].Length; x++)
				{
					if (result[y][x] != '#') continue;
					image.SetPixel(x + 10, y + 10, Color.Black);
				}

			var largerImage = new Bitmap(image, image.Width * 2, image.Height * 2);

			// ReSharper disable once StringLiteralTypo
			using var engine = new TesseractEngine(@".\_ExternalDependencies\tessdata_legacy", "eng", EngineMode.TesseractOnly);    // Uses legacy OCR. To use LTSM neural network use \tessdata_ltsm.
			using var pix = PixConverter.ToPix(largerImage);
			using var page = engine.Process(pix);

			return page.GetText().Trim('\n');
#pragma warning restore CA1416 // Validate platform compatibility
		}
	}
}
