namespace Oz.Algorithms.Graph
{
    public class GraphVertex<T> : IGraphVertex
    {
        public GraphVertex(int index, T data)
        {
            Index = index;
            Data = data;
        }
        
        public int Index { get; }
        public T Data { get; }
    }
}