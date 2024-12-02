using AdventOfCode.ExtensionMethods;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdventOfCode.Core
{
	public abstract class AdventOfCodeBase
	{
		#region Properties

		public object Result { get; protected set; }
		public object ResultPart1 { get; protected set; }
		public object ResultPart2 { get; protected set; }
		public decimal TimePart1 { get; protected set; }
		public decimal TimePart2 { get; protected set; }

		private bool IsInitialized => File.Exists(InputFileName) && new FileInfo(InputFileName).Length != 0;
		protected string[] Input => IsInitialized ? File.ReadAllLines(InputFileName) : null;

		private string InputFileName
		{
			get
			{
				var filename = this.GetType().Name;
				if (string.IsNullOrEmpty(filename)) return null;


				return @$"..\..\..\..\AdventOfCode\{Problem.Year}\Input\{filename}.txt";
			}
		}

		public AdventOfCodeAttribute Problem => (AdventOfCodeAttribute)Attribute.GetCustomAttribute(GetType(), typeof(AdventOfCodeAttribute));

		#endregion

		#region Constructors

		protected AdventOfCodeBase(string sessionCookie)
		{
			var success = GetInput(sessionCookie).Result;
		}

		#endregion

		#region Methods

		private async Task<bool> GetInput(string sessionCookie)
		{
			var targetDate = new DateTime(Problem.Year, 12, Problem.Day, 5, 0, 0, DateTimeKind.Utc);
			if (DateTime.UtcNow < targetDate) return false;

			if (!Directory.Exists(Path.GetDirectoryName(InputFileName)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(InputFileName));
			}


			if (!File.Exists(InputFileName) || new FileInfo(InputFileName).Length == 0)
			{
				var baseAddress = new Uri("https://adventofcode.com");
				var inputAddress = new Uri(baseAddress, $"{Problem.Year}/day/{Problem.Day}/input");
				var cookieContainer = new CookieContainer();
				using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
				using var client = new HttpClient(handler) { BaseAddress = baseAddress };
				cookieContainer.Add(baseAddress, new Cookie("session", sessionCookie));
				await client.DownloadFileTaskAsync(inputAddress, InputFileName);
			}

			return true;
		}

		#endregion

		#region Virtual methods

		public virtual void Solve()
		{
			var timer = new Stopwatch();

			timer.Start();
			ResultPart1 = SolvePart1();
			timer.Stop();
			TimePart1 = timer.ElapsedTicks.ToMilliseconds();

			timer.Start();
			ResultPart2 = SolvePart2();
			timer.Stop();
			TimePart2 = timer.ElapsedTicks.ToMilliseconds();
		}

		#endregion

		#region Abstract methods

		protected abstract object SolvePart1();

		protected abstract object SolvePart2();

		#endregion
	}
}
