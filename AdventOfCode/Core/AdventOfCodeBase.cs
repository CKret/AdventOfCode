using System;
using System.IO;
using System.Net;

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


                return @$"{Problem.Year}\{filename}.txt";
            }
        }

        public AdventOfCodeAttribute Problem => (AdventOfCodeAttribute)Attribute.GetCustomAttribute(GetType(), typeof(AdventOfCodeAttribute));

        #endregion

        #region Constructors

        protected AdventOfCodeBase(string sessionCookie)
        {
            GetInput(sessionCookie);
        }

        #endregion

        #region Methods

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

        #endregion

        #region Abstract Methods

        public abstract void Solve();

        #endregion
    }
}
