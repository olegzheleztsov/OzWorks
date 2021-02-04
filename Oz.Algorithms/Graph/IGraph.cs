using System.Collections.Generic;

namespace Oz.Algorithms.Graph
{
    public interface IGraph<T>
    {
        void Setup(T[] vertices, IEnumerable<(int @from, int to)> edges);
        bool IsDirected { get; }
        IEnumerable<GraphVertex<T>> GraphVertices { get; }

        GraphVertex<T> GetVertex(int index);

        int VertexCount { get; }

        IEnumerable<GraphVertex<T>> AdjacentVertices(GraphVertex<T> vertex);
        
        IGraph<T> TransposedGraph { get; }
    }
}