using System;
using System.IO;
using System.Net;

namespace AdventOfCode.Core
{
    public abstract class AdventOfCodeBase
    {
        public object Result { get; protected set; }

        private bool IsInitialized => File.Exists(InputFileName) && new FileInfo(InputFileName).Length != 0;
        protected string[] Input => IsInitialized ? File.ReadAllLines(InputFileName) : null;

        private string InputFileName
        {
            get
            {
                var name = this.GetType().Name;
                if (string.IsNullOrEmpty(name)) return null;

                var filename = name.Remove(name.Length - 1);

                return @$"{Problem.Year}\{filename}.txt";
            }
        }

        private AdventOfCodeAttribute Problem => (AdventOfCodeAttribute)Attribute.GetCustomAttribute(GetType(), typeof(AdventOfCodeAttribute));

        private bool GetInput(string sessionCookie)
        {
            var targetDate = new DateTime(Problem.Year, 12, Problem.Day, 6, 0, 0);
            if (DateTime.Now < targetDate) return false;

            if (!File.Exists(InputFileName) || new FileInfo(InputFileName).Length == 0)
            {
                using var client = new WebClient();
                client.Headers.Add(HttpRequestHeader.Cookie, $"session={sessionCookie}");
                client.DownloadFile(new Uri($"https://adventofcode.com/{Problem.Year}/day/{Problem.Day}/input"), InputFileName);
            }

            return true;
        }

        protected AdventOfCodeBase(string sessionCookie)
        {
            GetInput(sessionCookie);
        }

        public abstract void Solve();
    }
}
