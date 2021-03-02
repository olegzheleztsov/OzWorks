namespace Oz.Algorithms.Graph
{
    public sealed class Edge<T> : IEdge<T>
    {
        public Edge(int fromIndex, int toIndex, object userData = null)
        {
            FromIndex = fromIndex;
            ToIndex = toIndex;
            UserData = userData;
        }

        public int FromIndex { get; }
        
        public int ToIndex { get; }
        
        public object UserData { get; }
        
        public GraphVertex<T> FromVertex { get; set; }
        
        public GraphVertex<T> ToVertex { get; set; }
        
        public IEdge<T> CreateInverted()
        {
            return new Edge<T>(ToIndex, FromIndex, UserData)
            {
                FromVertex = ToVertex,
                ToVertex = FromVertex
            };
        }
        
        public void Deconstruct(out GraphVertex<T> fromVertex, out GraphVertex<T> toVertex)
        {
            fromVertex = FromVertex;
            toVertex = ToVertex;
        }
    }
}