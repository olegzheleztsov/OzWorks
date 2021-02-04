using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oz.Algorithms.Matrices;

namespace Oz.Algorithms.Graph
{
    public class AdjacentMatrixGraph<T> : IGraph<T>
    {
        private byte[,] _adjacencyMatrix;

        private GraphVertex<T>[] _vertices;

        public AdjacentMatrixGraph(bool directed) :
            this(new T[] { }, new (int from, int to)[] { }, directed)
        {
        }

        public AdjacentMatrixGraph(T[] vertices, IEnumerable<(int from, int to)> edges, bool directed)
        {
            IsDirected = directed;
            Setup(vertices, edges);
        }

        private AdjacentMatrixGraph(T[] vertices, byte[,] adjacencyMatrix, bool directed)
        {
            IsDirected = directed;
            SetupVertices(vertices);
            _adjacencyMatrix = adjacencyMatrix;
        }

        public AdjacentMatrixGraph<T> Transposed
        {
            get
            {
                if (!IsDirected)
                {
                    throw new InvalidOperationException("Impossible transpose non direct graph");
                }

                var transposedMatrix = new MatrixBase<byte>(_adjacencyMatrix).Transposed;
                return new AdjacentMatrixGraph<T>(_vertices.Select(v => v.Data).ToArray(), transposedMatrix,
                    true);
            }
        }

        public IGraph<T> TransposedGraph => Transposed;

        public GraphVertex<T> GetVertex(int index)
        {
            return _vertices[index];
        }

        public int VertexCount => _vertices.Length;

        public IEnumerable<GraphVertex<T>> AdjacentVertices(GraphVertex<T> vertex)
        {
            return GetAdjacentVertices(vertex.Index);
        }

        public void Setup(T[] vertices, IEnumerable<(int from, int to)> edges)
        {
            SetupVertices(vertices);
            SetupEdges(edges);
        }

        public bool IsDirected { get; }
        public IEnumerable<GraphVertex<T>> GraphVertices => _vertices;

        private void SetupVertices(T[] vertices)
        {
            _vertices = new GraphVertex<T>[vertices.Length];
            for (var i = 0; i < vertices.Length; i++)
            {
                _vertices[i] = new GraphVertex<T>(i, vertices[i]);
            }
        }

        private void SetupEdges(IEnumerable<(int from, int to)> edges)
        {
            _adjacencyMatrix = new byte[_vertices.Length, _vertices.Length];
            foreach (var (from, to) in edges)
            {
                _adjacencyMatrix[from, to] = 1;
                if (!IsDirected)
                {
                    _adjacencyMatrix[to, from] = 1;
                }
            }
        }

        private IEnumerable<GraphVertex<T>> GetAdjacentVertices(int vertexIndex)
        {
            return _vertices.Where((t, i) => _adjacencyMatrix[vertexIndex, i] > 0);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < _vertices.Length; i++)
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