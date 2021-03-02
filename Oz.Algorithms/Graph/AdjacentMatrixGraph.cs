using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oz.Algorithms.Graph
{
    public abstract class AdjacentMatrixGraph<T> : IGraph<T>
    {
        protected byte[,] AdjacencyMatrix;

        protected AdjacentMatrixGraph(T[] vertices, IEdge<T>[] edges, Func<GraphVertex<T>, GraphVertex<T>, int> weightFunc)
        {
            WeightFunc = weightFunc;
            Edges = edges;
            SetupVertices(vertices);
        }

        public IEdge<T>[] Edges { get; }

        public int EdgeCount => Edges.Length;

        public abstract IEdge<T> GetEdge(int fromIndex, int toIndex);

        public GraphVertex<T> GetVertex(int index)
        {
            return Vertices[index];
        }

        public int VertexCount => Vertices.Length;

        public IEnumerable<GraphVertex<T>> AdjacentVertices(GraphVertex<T> vertex)
        {
            return GetAdjacentVertices(vertex.Index);
        }

        public Func<GraphVertex<T>, GraphVertex<T>, int> WeightFunc { get; protected set; }

        public GraphVertex<T>[] Vertices { get; private set; }

        public IEnumerator<GraphVertex<T>> GetEnumerator()
        {
            foreach (var vertex in Vertices)
            {
                yield return vertex;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void SetupVertices(T[] vertices)
        {
            Vertices = new GraphVertex<T>[vertices.Length];
            for (var i = 0; i < vertices.Length; i++)
            {
                Vertices[i] = new GraphVertex<T>(i, vertices[i]);
            }

            foreach (var edge in Edges)
            {
                edge.FromVertex = GetVertex(edge.FromIndex);
                edge.ToVertex = GetVertex(edge.ToIndex);
            }
        }

        private IEnumerable<GraphVertex<T>> GetAdjacentVertices(int vertexIndex)
        {
            return Vertices.Where((_, i) => AdjacencyMatrix[vertexIndex, i] > 0);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < Vertices.Length; i++)
            {
                var adjacentVertices = GetAdjacentVertices(i).ToArray();
                stringBuilder.Append(adjacentVertices.Length > 0 ? $"{i} -> " : $"{i}:");

                for (var k = 0; k < adjacentVertices.Length; k++)
                {
                    stringBuilder.Append(k != adjacentVertices.Length - 1
                        ? $"{adjacentVertices[k].Index} -> "
                        : $"{adjacentVertices[k].Index}");
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}