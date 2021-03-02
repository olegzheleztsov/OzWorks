using System;
using System.Collections.Generic;
using System.Linq;

namespace Oz.Algorithms.Graph
{
    public class DirectedAdjacentListGraph<T> : AdjacentListGraph<T>, IDirectedGraph<T>
    {
        public DirectedAdjacentListGraph(IReadOnlyList<T> vertices, IEdge<T>[] edges,
            Func<int, int, IEdge<T>> edgeFactory, Func<GraphVertex<T>, GraphVertex<T>, int> weightFunc) :
            base(vertices, edges, weightFunc)
        {
            EdgeFactory = edgeFactory;
            ConfigureEdges();
        }

        public DirectedAdjacentListGraph(IReadOnlyList<GraphVertex<T>> vertices, IEdge<T>[] edges,
            Func<int, int, IEdge<T>> edgeFactory,
            Func<GraphVertex<T>, GraphVertex<T>, int> weightFunc)
            : base(vertices, edges, weightFunc)
        {
            EdgeFactory = edgeFactory;
            ConfigureEdges();
        }

        public Func<int, int, IEdge<T>> EdgeFactory { get; }

        public Func<GraphVertex<T>, GraphVertex<T>, int> ReplaceWeightFunc(
            Func<GraphVertex<T>, GraphVertex<T>, int> newWeightFunc)
        {
            var oldFunc = WeightFunc;
            WeightFunc = newWeightFunc;
            return oldFunc;
        }

        private void ConfigureEdges()
        {
            foreach (var edge in Edges)
            {
                ((LinkedGraphVertex<T>) Vertices[edge.FromIndex]).AddLinkedVertex((LinkedGraphVertex<T>) Vertices[edge.ToIndex]);
            }
        }

        public override IEdge<T> GetEdge(int fromIndex, int toIndex)
        {
            return Edges.FirstOrDefault(edge => edge.FromIndex == fromIndex && edge.ToIndex == toIndex);
        }

        public IDirectedGraph<T> Transposed
        {
            get
            {
                var transposedEdges = new List<IEdge<T>>();
                for (var i = 0; i < Vertices.Length; i++)
                {
                    transposedEdges.AddRange(((LinkedGraphVertex<T>) Vertices[i]).LinkedVertices.Select(vertex => EdgeFactory(vertex.Index, i)));
                }

                return new DirectedAdjacentListGraph<T>(
                    Vertices.Select(v => v.Data).ToArray(), 
                    transposedEdges.ToArray(), 
                    EdgeFactory, (a, b) => WeightFunc(b, a));
            }
        }
    }
}