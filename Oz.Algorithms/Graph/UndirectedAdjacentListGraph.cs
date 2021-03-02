
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oz.Algorithms.Graph
{
    public class UndirectedAdjacentListGraph<T> : AdjacentListGraph<T>, IUndirectedGraph<T>
    {
        public UndirectedAdjacentListGraph(IReadOnlyList<T> vertices, IEdge<T>[] edges, Func<GraphVertex<T>, GraphVertex<T>, int> weightFunc)
            : base(vertices, edges, weightFunc)
        {
            ConfigureEdges();
        }
        private void ConfigureEdges()
        {
            foreach (var edge in Edges)
            {
                ((LinkedGraphVertex<T>) Vertices[edge.FromIndex]).AddLinkedVertex((LinkedGraphVertex<T>) Vertices[edge.ToIndex]);
                ((LinkedGraphVertex<T>) Vertices[edge.ToIndex]).AddLinkedVertex((LinkedGraphVertex<T>) Vertices[edge.FromIndex]);
            }
        }

        public override IEdge<T> GetEdge(int fromIndex, int toIndex)
        {
            return Edges.FirstOrDefault(edge => (edge.FromIndex == fromIndex && edge.ToIndex == toIndex) ||
                                                (edge.FromIndex == toIndex && edge.ToIndex == fromIndex));
        }
    }
}