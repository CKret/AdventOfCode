namespace AdventOfCode.Mathematics.PathFinding.Core
{
    public class Vertex<T>
    {
        public T Source { get; set; }
        public T Destination { get; set; }
        public int Cost { get; set; }
    }
}
