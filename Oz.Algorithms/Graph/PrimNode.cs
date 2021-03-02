namespace Oz.Algorithms.Graph
{
    public class PrimNode<T>
    {
        public T Data { get; set; }
        public int Key { get; set; }
        public GraphVertex<PrimNode<T>> Parent { get; set; }
    }
}