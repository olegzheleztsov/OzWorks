using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oz.Algorithms.Graph
{
    public abstract class AdjacentListGraph<T> : IGraph<T>
    {
        protected AdjacentListGraph(IReadOnlyList<T> vertices, IEdge<T>[] edges, Func<GraphVertex<T>, GraphVertex<T>, int> weightFunc)
        {
            WeightFunc = weightFunc;
            Edges = edges;
            SetupVertices(vertices);
        }

        protected AdjacentListGraph(IReadOnlyList<GraphVertex<T>> vertices, IEdge<T>[] edges,
            Func<GraphVertex<T>, GraphVertex<T>, int> weightFunc)
        {
            WeightFunc = weightFunc;
            
            foreach (var v in vertices)
            {
                (v as LinkedGraphVertex<T>)?.LinkedVertices.Clear();
            }

            Vertices = vertices.ToArray();
            Edges = edges;
            UpdateEdges();
        }
        
        public GraphVertex<T>[] Vertices { get; private set; }

        public int VertexCount => Vertices.Length;
        public int EdgeCount => Edges.Count();

        public IEdge<T>[] Edges { get; }

        public abstract IEdge<T> GetEdge(int fromIndex, int toIndex);
        
        public Func<GraphVertex<T>, GraphVertex<T>, int> WeightFunc { get; protected set; }

        public IEnumerable<GraphVertex<T>> AdjacentVertices(GraphVertex<T> vertex)
        {
            var linkedVertex = vertex as LinkedGraphVertex<T>;
            if (linkedVertex == null)
            {
                throw new ArgumentException(
                    $"Vertex should be of type: {typeof(LinkedGraphVertex<T>).Name}. Real type: {vertex.GetType().Name}");
            }

            return linkedVertex.LinkedVertices;
        }

        public GraphVertex<T> GetVertex(int index)
        {
            return Vertices[index];
        }


        public IEnumerator<GraphVertex<T>> GetEnumerator()
        {
            return ((IEnumerable<GraphVertex<T>>) Vertices).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < Vertices.Length; i++)
            {
                stringBuilder.Append(
                    !((LinkedGraphVertex<T>) Vertices[i]).LinkedVertices.IsEmpty ? $"{i} -> " : $"{i}:");

                foreach (var linkedVertex in ((LinkedGraphVertex<T>) Vertices[i]).LinkedVertices.EnumerateNodes())
                {
                    stringBuilder.Append(!((LinkedGraphVertex<T>) Vertices[i]).LinkedVertices.IsLast(linkedVertex)
                        ? $"{linkedVertex.Data.Index} -> "
                        : $"{linkedVertex.Data.Index}");
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        private void SetupVertices(IReadOnlyList<T> vertices)
        {
            Vertices = new GraphVertex<T>[vertices.Count];

            for (var i = 0; i < vertices.Count; i++)
            {
                Vertices[i] = new LinkedGraphVertex<T>(i, vertices[i]);
            }

            UpdateEdges();
        }

        private void UpdateEdges()
        {
            foreach (var edge in Edges)
            {
                edge.FromVertex = GetVertex(edge.FromIndex);
                edge.ToVertex = GetVertex(edge.ToIndex);
            }
        }
    }
}