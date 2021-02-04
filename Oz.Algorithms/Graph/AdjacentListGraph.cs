using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oz.Algorithms.Graph
{
    public class AdjacentListGraph<T> : IGraph<T>
    {
        public AdjacentListGraph(bool directed) :
            this(new T[] { }, new (int from, int to)[] { }, directed)
        {
        }

        public AdjacentListGraph(T[] vertices, IEnumerable<(int from, int to)> edges, bool directed)
        {
            IsDirected = directed;
            Setup(vertices, edges);
        }

        private AdjacentListGraph(LinkedGraphVertex<T>[] vertices, bool directed)
        {
            Vertices = vertices;
            IsDirected = directed;
        }

        public LinkedGraphVertex<T>[] Vertices { get; private set; }

        public int VertexCount => Vertices.Length;

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

        public AdjacentListGraph<T> Transposed
        {
            get
            {
                if (!IsDirected)
                {
                    throw new InvalidOperationException("Impossible transpose non direct graph");
                }

                var transposedEdges = new List<(int from, int to)>();
                for (var i = 0; i < Vertices.Length; i++)
                {
                    transposedEdges.AddRange(Vertices[i].LinkedVertices.Select(vertex => (vertex.Index, i)));
                }

                return new AdjacentListGraph<T>(Vertices.Select(v => v.Data).ToArray(),
                    transposedEdges, IsDirected);
            }
        }

        public IGraph<T> TransposedGraph => Transposed;

        public void Setup(T[] vertices, IEnumerable<(int from, int to)> edges)
        {
            Vertices = new LinkedGraphVertex<T>[vertices.Length];

            for (var i = 0; i < vertices.Length; i++)
            {
                Vertices[i] = new LinkedGraphVertex<T>(i, vertices[i]);
            }
            
            foreach (var (fromIndex, toIndex) in edges)
            {
                Vertices[fromIndex].AddLinkedVertex(Vertices[toIndex]);
                if (!IsDirected)
                {
                    Vertices[toIndex].AddLinkedVertex(Vertices[fromIndex]);
                }
            }
        }

        public bool IsDirected { get; }
        
        public IEnumerable<GraphVertex<T>> GraphVertices => Vertices;

        public GraphVertex<T> GetVertex(int index) => Vertices[index];
        
        
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < Vertices.Length; i++)
            {
                stringBuilder.Append(!Vertices[i].LinkedVertices.IsEmpty ? $"{i} -> " : $"{i}:");

                foreach (var linkedVertex in Vertices[i].LinkedVertices.EnumerateNodes())
                {
                    stringBuilder.Append(!Vertices[i].LinkedVertices.IsLast(linkedVertex)
                        ? $"{linkedVertex.Data.Index} -> "
                        : $"{linkedVertex.Data.Index}");
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}