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

        private bool GetInput()
        {
            var targetDate = new DateTime(Problem.Year, 12, Problem.Day, 6, 0, 0);
            if (DateTime.Now < targetDate) return false;

            if (!File.Exists(InputFileName) || new FileInfo(InputFileName).Length == 0)
            {
                using var client = new WebClient();
                client.Headers.Add(HttpRequestHeader.Cookie, "session=53616c7465645f5f9ad5172fd52a20994d97182152a663d5f1aa804eb410edb53b14b7f45eaa111241c6af59ffae914c");
                client.DownloadFile(new Uri($"https://adventofcode.com/{Problem.Year}/day/{Problem.Day}/input"), InputFileName);
            }

            return true;
        }

        protected AdventOfCodeBase()
        {
            GetInput();
        }

        public abstract void Solve();
    }
}
