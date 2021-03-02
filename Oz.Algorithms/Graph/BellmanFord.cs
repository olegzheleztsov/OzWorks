using System;
using System.Linq;

namespace Oz.Algorithms.Graph
{
    public sealed class BellmanFord<T> : DirectedGraphShortestPath<T>
    {
        public BellmanFord(DirectedAdjacentListGraph<BellmanFordNodeData<T>> graph) : base(graph)
        {
        }

        public bool Run(GraphVertex<BellmanFordNodeData<T>> source)
        {
            InitializeSingleSource(source);
            for (var i = 1; i < Graph.VertexCount; i++)
            {
                foreach (var edge in Graph.Edges)
                {
                     var (u, v) = edge;

                    Relax(u, v, edge);
                }
            }

            return !(from edge in Graph.Edges 
                let u = edge.FromVertex 
                let v = edge.ToVertex 
                let newDistance = AddWeight(u.Data.Distance, Graph.WeightFunc(edge.FromVertex, edge.ToVertex)) 
                where v.Data.Distance > newDistance select v).Any();
        }
    }
}