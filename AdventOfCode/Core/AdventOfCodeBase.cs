using System;

namespace AdventOfCode.Core
{
    public abstract class AdventOfCodeBase
    {
        public long Result { get; protected set; }

        public AdventOfCodeAttribute Problem => (AdventOfCodeAttribute)Attribute.GetCustomAttribute(GetType(), typeof(AdventOfCodeAttribute));

        public abstract void Solve();
    }
}
