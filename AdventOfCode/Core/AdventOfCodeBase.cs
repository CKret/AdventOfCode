using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
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

		public AdventOfCodeAttribute Problem => (AdventOfCodeAttribute) Attribute.GetCustomAttribute(GetType(), typeof(AdventOfCodeAttribute));

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

		/// <summary>
		/// Async entry point for running solutions that supports a CancellationToken.
		/// Default implementation runs the synchronous <see cref="Solve"/> on the thread-pool.
		/// Override in long-running solvers to observe <paramref name="cancellationToken"/>.
		/// </summary>
		public virtual async Task SolveAsync(CancellationToken cancellationToken = default)
		{
			// Note: Task.Run with a token does not forcibly abort a running delegate.
			// To support cooperative cancellation the solver should override this method
			// and observe the token inside SolvePart1/SolvePart2 or provide their own async implementation.
			//return Task.Run(() => Solve(), cancellationToken);
			await Task.Run(async () =>
			{
				var timer = new Stopwatch();

				timer.Start();
				// Compute part 1 with cancellation support
				cancellationToken.ThrowIfCancellationRequested();
				ResultPart1 = await SolvePart1(cancellationToken).ConfigureAwait(false);
				timer.Stop();
				TimePart1 = timer.ElapsedTicks.ToMilliseconds();

				timer.Restart();
				// Part2 remains synchronous in this class - still observe cancellation token at the start.
				cancellationToken.ThrowIfCancellationRequested();
				ResultPart2 = await SolvePart2(cancellationToken).ConfigureAwait(false);
				timer.Stop();
				TimePart2 = timer.ElapsedTicks.ToMilliseconds();
			}, cancellationToken).ConfigureAwait(false);

		}

		#endregion

		#region Abstract methods

		protected abstract Task<object> SolvePart1(CancellationToken cancellationToken);

		protected abstract Task<object> SolvePart2(CancellationToken cancellationToken);

		#endregion
	}
}
