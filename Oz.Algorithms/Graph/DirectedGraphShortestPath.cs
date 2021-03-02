using System;

namespace Oz.Algorithms.Graph
{
    public abstract class DirectedGraphShortestPath<T>
    {
        protected readonly DirectedAdjacentListGraph<BellmanFordNodeData<T>> Graph;

        public DirectedGraphShortestPath(DirectedAdjacentListGraph<BellmanFordNodeData<T>> graph)
        {
            Graph = graph;
        }
        
        protected void InitializeSingleSource(GraphVertex<BellmanFordNodeData<T>> source)
        {
            foreach (var vertex in Graph.Vertices)
            {
                vertex.Data.Distance = Util.IntegerPositiveInfinity;
                vertex.Data.Parent = null;
            }

            source.Data.Distance = 0;
        }

        protected  void Relax(GraphVertex<BellmanFordNodeData<T>> u, 
            GraphVertex<BellmanFordNodeData<T>> v, 
            IEdge<BellmanFordNodeData<T>> edge)
        {
            var weight = Graph.WeightFunc(edge.FromVertex, edge.ToVertex);
            if (v.Data.Distance > AddWeight(u.Data.Distance, weight))
            {
                v.Data.Distance = AddWeight(u.Data.Distance, weight);
                v.Data.Parent = u;
            }
        }

        protected static int AddWeight(int first, int second)
        {
            if (Util.IsInfinity(first) && Util.IsInfinity(second))
            {
                throw new ArgumentException($"Both values can't be infinity");
            }

            return GraphUtil.AddGraphWeights(first, second);
        }
    }
}