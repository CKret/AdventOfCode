using SuperLinq;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2023
{
	/// <summary>
	/// </summary>
	[AdventOfCode(2023, 5, "If You Give A Seed A Fertilizer", 26273516, 34039469)]
	public class AdventOfCode202305 : AdventOfCodeBase
	{
		public AdventOfCode202305(string sessionCookie) : base(sessionCookie) { }

		protected override object SolvePart1()
		{
			var seeds = Input.First().Split(": ")[1].Split(" ").Select(long.Parse).ToList();

			var skip = 3;
			var seedToSoilMap = Input.Skip(skip).TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => l.Split(" ").Select(long.Parse).ToList()).Select(c => (Seed: (RangeStart: c[1], RangeEnd: c[1] + c[2] - 1), Soil: (RangeStart: c[0], RangeEnd: c[0] + c[2] - 1), Length: c[2])).OrderBy(c => c.Seed.RangeStart);
			var soilToFertilizerMap = Input.Skip(skip += seedToSoilMap.Count() + 2).TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => l.Split(" ").Select(long.Parse).ToList()).Select(c => (Soil: (RangeStart: c[1], RangeEnd: c[1] + c[2] - 1), Fertilizer: (RangeStart: c[0], RangeEnd: c[0] + c[2] - 1), Length: c[2])).OrderBy(c => c.Fertilizer.RangeStart);
			var fertilizerToWaterMap = Input.Skip(skip += soilToFertilizerMap.Count() + 2).TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => l.Split(" ").Select(long.Parse).ToList()).Select(c => (Fertilizer: (RangeStart: c[1], RangeEnd: c[1] + c[2] - 1), Water: (RangeStart: c[0], RangeEnd: c[0] + c[2] - 1), Length: c[2])).OrderBy(c => c.Water.RangeStart);
			var waterToLightMap = Input.Skip(skip += fertilizerToWaterMap.Count() + 2).TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => l.Split(" ").Select(long.Parse).ToList()).Select(c => (Water: (RangeStart: c[1], RangeEnd: c[1] + c[2] - 1), Light: (RangeStart: c[0], RangeEnd: c[0] + c[2] - 1), Length: c[2])).OrderBy(c => c.Light.RangeStart);
			var lightToTemperatureMap = Input.Skip(skip += waterToLightMap.Count() + 2).TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => l.Split(" ").Select(long.Parse).ToList()).Select(c => (Light: (RangeStart: c[1], RangeEnd: c[1] + c[2] - 1), Temperature: (RangeStart: c[0], RangeEnd: c[0] + c[2] - 1), Length: c[2])).OrderBy(c => c.Temperature.RangeStart);
			var temperatureToHumidityMap = Input.Skip(skip += lightToTemperatureMap.Count() + 2).TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => l.Split(" ").Select(long.Parse).ToList()).Select(c => (Temperature: (RangeStart: c[1], RangeEnd: c[1] + c[2] - 1), Humidity: (RangeStart: c[0], RangeEnd: c[0] + c[2] - 1), Length: c[2])).OrderBy(c => c.Humidity.RangeStart);
			var humidityToLocationMap = Input.Skip(skip += temperatureToHumidityMap.Count() + 2).TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => l.Split(" ").Select(long.Parse).ToList()).Select(c => (Humidity: (RangeStart: c[1], RangeEnd: c[1] + c[2] - 1), Location: (RangeStart: c[0], RangeEnd: c[0] + c[2] - 1), Length: c[2])).OrderBy(c => c.Location.RangeStart);

			var closest = long.MaxValue;

			foreach (var seed in seeds)
			{
				var ssm = seedToSoilMap.SingleOrDefault(c => c.Seed.RangeStart <= seed && c.Seed.RangeEnd >= seed);
				var soil = ssm.Soil.RangeStart + seed - ssm.Seed.RangeStart;

				var sfm = soilToFertilizerMap.SingleOrDefault(c => c.Soil.RangeStart <= soil && c.Soil.RangeEnd >= soil);
				var fertilizer = sfm.Fertilizer.RangeStart + soil - sfm.Soil.RangeStart;
				
				var fwm = fertilizerToWaterMap.SingleOrDefault(c => c.Fertilizer.RangeStart <= fertilizer && c.Fertilizer.RangeEnd >= fertilizer);
				var water = fwm.Water.RangeStart + fertilizer - fwm.Fertilizer.RangeStart;

				var wlm = waterToLightMap.SingleOrDefault(c => c.Water.RangeStart <= water && c.Water.RangeEnd >= water);
				var light = wlm.Light.RangeStart + water - wlm.Water.RangeStart;

				var ltm = lightToTemperatureMap.SingleOrDefault(c => c.Light.RangeStart <= light && c.Light.RangeEnd >= light);
				var temperatur = ltm.Temperature.RangeStart + light - ltm.Light.RangeStart;

				var thm = temperatureToHumidityMap.SingleOrDefault(c => c.Temperature.RangeStart <= temperatur && c.Temperature.RangeEnd >= temperatur);
				var humidity  = thm.Humidity.RangeStart + temperatur - thm.Temperature.RangeStart;

				var hlm = humidityToLocationMap.SingleOrDefault(c => c.Humidity.RangeStart <= humidity && c.Humidity.RangeEnd >= humidity);
				var location = hlm.Location.RangeStart + humidity - hlm.Humidity.RangeStart;

				if (location < closest) closest = location;
			}

			return closest;
		}

		protected override object SolvePart2()
		{
			var seeds = Input.First().Split(": ")[1].Split(" ").Select(long.Parse).ToList().Batch(2).Select(i => (RangeStart: i[0], RangeEnd: i[0] + i[1] - 1)).Order();

			var skip = 3;
			var seedToSoilMap = Input.Skip(skip).TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => l.Split(" ").Select(long.Parse).ToList()).Select(c => (Seed: (RangeStart: c[1], RangeEnd: c[1] + c[2] - 1), Soil: (RangeStart: c[0], RangeEnd: c[0] + c[2] - 1), Length: c[2])).OrderBy(c => c.Seed.RangeStart).ToArray();
			var soilToFertilizerMap = Input.Skip(skip += seedToSoilMap.Count() + 2).TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => l.Split(" ").Select(long.Parse).ToList()).Select(c => (Soil: (RangeStart: c[1], RangeEnd: c[1] + c[2] - 1), Fertilizer: (RangeStart: c[0], RangeEnd: c[0] + c[2] - 1), Length: c[2])).OrderBy(c => c.Soil.RangeStart).ToArray();
			var fertilizerToWaterMap = Input.Skip(skip += soilToFertilizerMap.Count() + 2).TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => l.Split(" ").Select(long.Parse).ToList()).Select(c => (Fertilizer: (RangeStart: c[1], RangeEnd: c[1] + c[2] - 1), Water: (RangeStart: c[0], RangeEnd: c[0] + c[2] - 1), Length: c[2])).OrderBy(c => c.Fertilizer.RangeStart).ToArray();
			var waterToLightMap = Input.Skip(skip += fertilizerToWaterMap.Count() + 2).TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => l.Split(" ").Select(long.Parse).ToList()).Select(c => (Water: (RangeStart: c[1], RangeEnd: c[1] + c[2] - 1), Light: (RangeStart: c[0], RangeEnd: c[0] + c[2] - 1), Length: c[2])).OrderBy(c => c.Water.RangeStart).ToArray();
			var lightToTemperatureMap = Input.Skip(skip += waterToLightMap.Count() + 2).TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => l.Split(" ").Select(long.Parse).ToList()).Select(c => (Light: (RangeStart: c[1], RangeEnd: c[1] + c[2] - 1), Temperature: (RangeStart: c[0], RangeEnd: c[0] + c[2] - 1), Length: c[2])).OrderBy(c => c.Light.RangeStart).ToArray();
			var temperatureToHumidityMap = Input.Skip(skip += lightToTemperatureMap.Count() + 2).TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => l.Split(" ").Select(long.Parse).ToList()).Select(c => (Temperature: (RangeStart: c[1], RangeEnd: c[1] + c[2] - 1), Humidity: (RangeStart: c[0], RangeEnd: c[0] + c[2] - 1), Length: c[2])).OrderBy(c => c.Temperature.RangeStart).ToArray();
			var humidityToLocationMap = Input.Skip(skip += temperatureToHumidityMap.Count() + 2).TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => l.Split(" ").Select(long.Parse).ToList()).Select(c => (Humidity: (RangeStart: c[1], RangeEnd: c[1] + c[2] - 1), Location: (RangeStart: c[0], RangeEnd: c[0] + c[2] - 1), Length: c[2])).OrderBy(c => c.Humidity.RangeStart).ToArray();

			var seedToLocationMap = new List<((long RangeStart, long RangeEnd) Seed, (long RangeStart, long RangeEnd) LocationRange, long LocationStart)>();
			foreach (var ssm in seedToSoilMap)
			{
				var soil = ssm.Soil.RangeStart + ssm.Seed.RangeStart - ssm.Seed.RangeStart;

				var sfm = soilToFertilizerMap.SingleOrDefault(c => c.Soil.RangeStart <= soil && c.Soil.RangeEnd >= soil);
				var fertilizer = sfm.Fertilizer.RangeStart + soil - sfm.Soil.RangeStart;

				var fwm = fertilizerToWaterMap.SingleOrDefault(c => c.Fertilizer.RangeStart <= fertilizer && c.Fertilizer.RangeEnd >= fertilizer);
				var water = fwm.Water.RangeStart + fertilizer - fwm.Fertilizer.RangeStart;

				var wlm = waterToLightMap.SingleOrDefault(c => c.Water.RangeStart <= water && c.Water.RangeEnd >= water);
				var light = wlm.Light.RangeStart + water - wlm.Water.RangeStart;

				var ltm = lightToTemperatureMap.SingleOrDefault(c => c.Light.RangeStart <= light && c.Light.RangeEnd >= light);
				var temperatur = ltm.Temperature.RangeStart + light - ltm.Light.RangeStart;

				var thm = temperatureToHumidityMap.SingleOrDefault(c => c.Temperature.RangeStart <= temperatur && c.Temperature.RangeEnd >= temperatur);
				var humidity = thm.Humidity.RangeStart + temperatur - thm.Temperature.RangeStart;

				var hlm = humidityToLocationMap.SingleOrDefault(c => c.Humidity.RangeStart <= humidity && c.Humidity.RangeEnd >= humidity);
				var location = hlm.Location.RangeStart + humidity - hlm.Humidity.RangeStart;

				seedToLocationMap.Add((ssm.Seed, hlm.Location, location));
			}

			return seedToLocationMap.OrderBy(l => l.LocationStart).First().LocationStart;

			// David Part 2: 1493866

			// (4290924725, 4294967295)

			// Bruteforce version:
			//foreach (var seedRange in seeds)
			//{
			//	for (var seed = seedRange.RangeStart; seed < seedRange.RangeEnd; seed++)
			//	{
			//		var ssm = seedToSoilMap.SingleOrDefault(c => c.Seed.RangeStart <= seed && c.Seed.RangeEnd >= seed);
			//		var soil = ssm.Soil.RangeStart + seed - ssm.Seed.RangeStart;

			//		var sfm = soilToFertilizerMap.SingleOrDefault(c => c.Soil.RangeStart <= soil && c.Soil.RangeEnd >= soil);
			//		var fertilizer = sfm.Fertilizer.RangeStart + soil - sfm.Soil.RangeStart;

			//		var fwm = fertilizerToWaterMap.SingleOrDefault(c => c.Fertilizer.RangeStart <= fertilizer && c.Fertilizer.RangeEnd >= fertilizer);
			//		var water = fwm.Water.RangeStart + fertilizer - fwm.Fertilizer.RangeStart;

			//		var wlm = waterToLightMap.SingleOrDefault(c => c.Water.RangeStart <= water && c.Water.RangeEnd >= water);
			//		var light = wlm.Light.RangeStart + water - wlm.Water.RangeStart;

			//		var ltm = lightToTemperatureMap.SingleOrDefault(c => c.Light.RangeStart <= light && c.Light.RangeEnd >= light);
			//		var temperatur = ltm.Temperature.RangeStart + light - ltm.Light.RangeStart;

			//		var thm = temperatureToHumidityMap.SingleOrDefault(c => c.Temperature.RangeStart <= temperatur && c.Temperature.RangeEnd >= temperatur);
			//		var humidity = thm.Humidity.RangeStart + temperatur - thm.Temperature.RangeStart;

			//		var hlm = humidityToLocationMap.SingleOrDefault(c => c.Humidity.RangeStart <= humidity && c.Humidity.RangeEnd >= humidity);
			//		var location = hlm.Location.RangeStart + humidity - hlm.Humidity.RangeStart;

			//		if (location < closest) closest = location;
			//	}
			//}
		}
	}
}
