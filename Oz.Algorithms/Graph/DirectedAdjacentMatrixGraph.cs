using System;
using System.Collections.Generic;
using System.Linq;
using Oz.Algorithms.Matrices;

namespace Oz.Algorithms.Graph
{
    public class DirectedAdjacentMatrixGraph<T> : AdjacentMatrixGraph<T>, IDirectedGraph<T>
    {
        public DirectedAdjacentMatrixGraph(T[] vertices, IEdge<T>[] edges,
            Func<GraphVertex<T>, GraphVertex<T>, int> weightFunc)
            : base(vertices, edges, weightFunc)
        {
            ConfigureEdges();
            Weights = new IntegerMatrix(VertexCount, VertexCount);
            for (var i = 0; i < VertexCount; i++)
            {
                for (var j = 0; j < VertexCount; j++)
                {
                    if (i == j)
                    {
                        Weights[i, j] = 0;
                    }
                    else
                    {
                        Weights[i, j] = Util.IntegerPositiveInfinity;
                    }
                }
            }

            foreach (var edge1 in Edges)
            {
                var edge = edge1;
                Weights[edge.FromIndex, edge.ToIndex] = WeightFunc(edge.FromVertex, edge.ToVertex);
            }
        }

        public IntegerMatrix Weights { get; }

        public override IEdge<T> GetEdge(int fromIndex, int toIndex)
        {
            return Edges.FirstOrDefault(edge => edge.FromIndex == fromIndex && edge.ToIndex == toIndex);
        }

        public IDirectedGraph<T> Transposed
        {
            get
            {
                return new DirectedAdjacentMatrixGraph<T>(Vertices.Select(v => v.Data).ToArray(),
                    Edges.Select(e => e.CreateInverted()).ToArray(), (a, b) => WeightFunc(b, a));
            }
        }

        private void ConfigureEdges()
        {
            AdjacencyMatrix = new byte[Vertices.Length, Vertices.Length];
            foreach (var edge in Edges)
            {
                AdjacencyMatrix[edge.FromIndex, edge.ToIndex] = 1;
            }
        }

        public IntegerMatrix SlowAllPairsShortestPaths()
        {
            return SlowAllPairsShortestPaths(Weights);
        }

        public IntegerMatrix FasterAllPairsShortestPaths()
        {
            return FasterAllPairsShortestPaths(Weights);
        }

        private IntegerMatrix FasterAllPairsShortestPaths(IntegerMatrix weights)
        {
            var size = weights.Rows;
            var lengthM = weights.Clone() as IntegerMatrix;
            var m = 1;
            while (m < size - 1)
            {
                lengthM = ExtendShortestPaths(lengthM, lengthM);
                m *= 2;
            }

            return lengthM;
        }

        private IntegerMatrix SlowAllPairsShortestPaths(IntegerMatrix weights)
        {
            var size = weights.Rows;
            var lengths = weights.Clone() as IntegerMatrix;
            for (var m = 1; m < size - 1; m++)
            {
                lengths = ExtendShortestPaths(lengths, weights);
            }

            return lengths;
        }

        private IntegerMatrix ExtendShortestPaths(IntegerMatrix lengths, IntegerMatrix weights)
        {
            var size = lengths.Rows;
            var result = new IntegerMatrix(size, size);
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    result[i, j] = Util.IntegerPositiveInfinity;
                    for (var k = 0; k < size; k++)
                    {
                        result[i, j] = Math.Min(result[i, j], GraphUtil.AddGraphWeights(lengths[i, k], weights[k, j]));
                    }
                }
            }

            return result;
        }

        public List<int> GetPath(MatrixBase<int?> predecessors, int fromIndex, int toIndex)
        {
            try
            {
                var path = new List<int>();
                var guards = new List<int>();
                ComputePath(predecessors, fromIndex, toIndex, path, guards);
                if (!path.Contains(fromIndex))
                {
                    path.Insert(0, fromIndex);
                }

                return path;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        private void ComputePath(MatrixBase<int?> predecessors, int fromIndex, int toIndex, List<int> path,
            List<int> guards)
        {
            guards.Add(toIndex);
            if (fromIndex == toIndex)
            {
                path.Add(fromIndex);
            }
            else if (predecessors[fromIndex, toIndex] == null)
            {
                throw new InvalidOperationException("No path");
            }
            else
            {
                if (!guards.Contains(predecessors[fromIndex, toIndex].Value))
                {
                    ComputePath(predecessors, fromIndex, predecessors[fromIndex, toIndex].Value, path, guards);
                }

                path.Add(toIndex);
            }
        }

        public IntegerMatrix FloydWarshall()
        {
            return FloydWarshall(Weights);
        }

        private IntegerMatrix FloydWarshall(IntegerMatrix weights)
        {
            var size = weights.Rows;
            var distances = weights.Clone() as IntegerMatrix;
            for (var k = 0; k < size; k++)
            {
                var dNext = new IntegerMatrix(size, size);
                for (var i = 0; i < size; i++)
                {
                    for (var j = 0; j < size; j++)
                    {
                        dNext[i, j] = Math.Min(distances[i, j],
                            GraphUtil.AddGraphWeights(distances[i, k], distances[k, j]));
                    }
                }

                distances = dNext;
            }

            return distances;
        }

        public (MatrixBase<int?>, IntegerMatrix) FloydWarshallMod()
        {
            return FloydWarshallMod(Weights);
        }

        private (MatrixBase<int?>, IntegerMatrix) FloydWarshallMod(IntegerMatrix weights)
        {
            var size = weights.Rows;
            var distances = weights.Clone() as IntegerMatrix;
            var predecessors = new MatrixBase<int?>(size, size, (int?) null);
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    if (i != j && !Util.IsInfinity(distances[i, j]))
                    {
                        predecessors[i, j] = i;
                    }
                }
            }

            for (var k = 0; k < size; k++)
            {
                var nextDistances = new IntegerMatrix(size, size);
                var nextPredecessors = new MatrixBase<int?>(size, size);
                for (var i = 0; i < size; i++)
                {
                    for (var j = 0; j < size; j++)
                    {
                        if (distances[i, j] <= GraphUtil.AddGraphWeights(distances[i, k], distances[k, j]))
                        {
                            nextDistances[i, j] = distances[i, j];
                            nextPredecessors[i, j] = predecessors[i, j];
                        }
                        else
                        {
                            nextDistances[i, j] = GraphUtil.AddGraphWeights(distances[i, k], distances[k, j]);
                            nextPredecessors[i, j] = predecessors[k, j];
                        }
                    }
                }

                distances = nextDistances;
                predecessors = nextPredecessors;
            }

            return (predecessors, distances);
        }

        public MatrixBase<bool> TransitiveClosure()
        {
            var size = VertexCount;
            var transitions = new MatrixBase<bool>(size, size);
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    if (i == j || !Util.IsInfinity(Weights[i, j]))
                    {
                        transitions[i, j] = true;
                    }
                    else
                    {
                        transitions[i, j] = false;
                    }
                }
            }

            for (var k = 0; k < size; k++)
            {
                var nextTransitions = new MatrixBase<bool>(size, size);
                for (var i = 0; i < size; i++)
                {
                    for (var j = 0; j < size; j++)
                    {
                        nextTransitions[i, j] = transitions[i, j] || transitions[i, k] && transitions[k, j];
                    }
                }

                transitions = nextTransitions;
            }

            return transitions;
        }
    }
}