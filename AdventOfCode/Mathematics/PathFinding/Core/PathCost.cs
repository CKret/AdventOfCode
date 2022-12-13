namespace AdventOfCode.Mathematics.PathFinding.Core
{
    public class PathCost<T>
    {
        public T Source { get; set; }
        public T Destination { get; set; }
        public int Cost { get; set; }
    }
}
