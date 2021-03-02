namespace Oz.Algorithms.Graph
{
    public interface IEdge<T>
    {
        int FromIndex { get; }
        int ToIndex { get; }

        IEdge<T> CreateInverted();

        public GraphVertex<T> FromVertex
        {
            get;
            set;
        }

        public GraphVertex<T> ToVertex
        {
            get;
            set;
        }
        
        object UserData { get; }
        
        public void Deconstruct(out GraphVertex<T> fromVertex, out GraphVertex<T> toVertex);
    }
}