using System;
using System.Linq;

namespace Oz.Algorithms.Graph
{
    public class UndirectedAdjacentMatrixGraph<T> : AdjacentMatrixGraph<T>, IUndirectedGraph<T>
    {
        public UndirectedAdjacentMatrixGraph(T[] vertices, IEdge<T>[] edges, Func<GraphVertex<T>, GraphVertex<T>, int> weightFunc)
            : base(vertices, edges, weightFunc)
        {
            ConfigureEdges();
        }

        private void ConfigureEdges()
        {
            AdjacencyMatrix = new byte[Vertices.Length, Vertices.Length];
            foreach (var edge in Edges)
            {
                AdjacencyMatrix[edge.FromIndex, edge.ToIndex] = 1;
                AdjacencyMatrix[edge.ToIndex, edge.FromIndex] = 1;
            }
        }

        public override IEdge<T> GetEdge(int fromIndex, int toIndex)
        {
            return Edges.FirstOrDefault(edge => (edge.FromIndex == fromIndex && edge.ToIndex == toIndex) ||
                                                (edge.FromIndex == toIndex && edge.ToIndex == fromIndex));
        }
    }
}