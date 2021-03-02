using System;
using System.Collections.Generic;

namespace Oz.Algorithms.Graph
{
    public interface IGraph<T> : IEnumerable<GraphVertex<T>>
    {
        GraphVertex<T>[] Vertices { get; }
        
        IEdge<T>[] Edges { get; }

        int VertexCount { get; }

        int EdgeCount { get; }
        
        GraphVertex<T> GetVertex(int index);

        IEdge<T> GetEdge(int fromIndex, int toIndex);

        IEnumerable<GraphVertex<T>> AdjacentVertices(GraphVertex<T> vertex);
        
        Func<GraphVertex<T>, GraphVertex<T>, int> WeightFunc { get; }
    }
}