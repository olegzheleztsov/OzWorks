using System;
using System.Collections.Generic;
using System.Linq;
using Oz.Algorithms.Matrices;

namespace Oz.Algorithms.Graph
{
    public class JohnsonSearchPath<T> : DirectedGraphShortestPath<T>
    {
        public JohnsonSearchPath(DirectedAdjacentListGraph<BellmanFordNodeData<T>> graph) : base(graph)
        {
            
        }

        public IntegerMatrix Run()
        {
            var (auxGraph, s) = ConstructAuxiliaryGraph(Graph);
            Console.WriteLine(auxGraph.ToString());
            BellmanFord<T> bellmanFord = new BellmanFord<T>(auxGraph as DirectedAdjacentListGraph<BellmanFordNodeData<T>>);
            if (!bellmanFord.Run(s))
            {
                return null;
            }

            var distances = new Dictionary<int, int>();
            foreach (var v in auxGraph.Vertices)
            {
                distances[v.Index] = v.Data.Distance;
            }
                
            List<Edge<BellmanFordNodeData<T>>> tempEdges = auxGraph
                .Edges
                .Select(edge 
                    => new Edge<BellmanFordNodeData<T>>(
                        edge.FromIndex, 
                        edge.ToIndex, 
                        auxGraph.WeightFunc(edge.FromVertex, edge.ToVertex) + distances[edge.FromIndex] - distances[edge.ToIndex])).ToList();

            int ModifiedWeightFunc(
                GraphVertex<BellmanFordNodeData<T>> firstVertex,
                GraphVertex<BellmanFordNodeData<T>> secondVertex)
            {
                var edge = tempEdges.FirstOrDefault(e 
                    => e.FromIndex == firstVertex.Index && e.ToIndex == secondVertex.Index);
                return (int) edge.UserData;
            }

            var resultDistances = new IntegerMatrix(Graph.VertexCount, Graph.VertexCount);
                
            Console.WriteLine(Graph.ToString());
            foreach (var u in Graph.Vertices)
            {
                var oldFunc = Graph.ReplaceWeightFunc(ModifiedWeightFunc);
                var dijkstraShortestPath = new DijkstraShortestPath<T>(Graph);
                dijkstraShortestPath.Run(u);
                foreach (var v in Graph)
                {
                    resultDistances[u.Index, v.Index] = v.Data.Distance + distances[v.Index] - distances[u.Index];
                }

                Graph.ReplaceWeightFunc(oldFunc);
            }

            return resultDistances;
        }

        private (IGraph<BellmanFordNodeData<T>>, GraphVertex<BellmanFordNodeData<T>>) ConstructAuxiliaryGraph(IGraph<BellmanFordNodeData<T>> sourceGraph)
        {
            var sGraph = sourceGraph as DirectedAdjacentListGraph<BellmanFordNodeData<T>>;
            var s = new LinkedGraphVertex<BellmanFordNodeData<T>>(sourceGraph.VertexCount, new BellmanFordNodeData<T>(default));
            var vertices = new List<GraphVertex<BellmanFordNodeData<T>>>();
            vertices.AddRange(sourceGraph.Vertices);
            vertices.Add(s);

            var edges = new List<IEdge<BellmanFordNodeData<T>>>();
            edges.AddRange(sourceGraph.Edges);
            edges.AddRange(sGraph
                .Vertices
                .Select(v 
                    => new Edge<BellmanFordNodeData<T>>(
                        sourceGraph.VertexCount, 
                        v.Index, 0))
                .Cast<IEdge<BellmanFordNodeData<T>>>());

            int NewWeightFunc(GraphVertex<BellmanFordNodeData<T>> firstVertex,
                GraphVertex<BellmanFordNodeData<T>> secondVertex)
            {
                return firstVertex.Index == sGraph.VertexCount 
                    ? 0 
                    : sGraph.WeightFunc(firstVertex, secondVertex);
            }

            var auxGraph = new DirectedAdjacentListGraph<BellmanFordNodeData<T>>(
                vertices.ToArray(), 
                edges.ToArray(),
                sGraph.EdgeFactory, 
                NewWeightFunc);
            return (auxGraph, s);
        }
    }
}