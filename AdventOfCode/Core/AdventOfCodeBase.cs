using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Core
{
    public abstract class AdventOfCodeBase
    {
        public object Result { get; protected set; }

        protected string DataFileName
        {
            get
            {
                var name = this.GetType().Name;
                if (string.IsNullOrEmpty(name)) return null;

                var filename = name.Remove(name.Length - 1);

                return @$"{Problem.Year}\{filename}.txt";
            }
        }

        protected string[] Input => File.ReadAllLines(DataFileName);

        public AdventOfCodeAttribute Problem => (AdventOfCodeAttribute)Attribute.GetCustomAttribute(GetType(), typeof(AdventOfCodeAttribute));

        public abstract void Solve();
    }
}
