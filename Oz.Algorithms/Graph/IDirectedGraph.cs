namespace Oz.Algorithms.Graph
{
    public interface IDirectedGraph<T> : IGraph<T>
    {
        IDirectedGraph<T> Transposed { get; }
    }
}