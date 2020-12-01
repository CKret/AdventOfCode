using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Mathematics.PathFinding.Core
{
    public class PathFindingBase<T>
    {
        public T Source { get; set; }
        public T Destination { get; set; }
        public int Cost { get; set; }
    }
}
