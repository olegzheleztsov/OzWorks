using Oz.Algorithms.DataStructures;

namespace Oz.Algorithms.Graph
{
    public class DijkstraShortestPath<T> : DirectedGraphShortestPath<T>
    {
        public DijkstraShortestPath(DirectedAdjacentListGraph<BellmanFordNodeData<T>> graph)
            : base(graph)
        {
            
        }

        public void Run(GraphVertex<BellmanFordNodeData<T>> sourceNode)
        {
            static bool CompareVertices(GraphVertex<BellmanFordNodeData<T>> firstVertex,
                GraphVertex<BellmanFordNodeData<T>> secondVertex)
            {
                return (firstVertex.Index == secondVertex.Index);
            }
            
            InitializeSingleSource(sourceNode);
            var verifiedSet =
                DisjointSet<GraphVertex<BellmanFordNodeData<T>>>.Empty(CompareVertices);
                               
            var unverifiedQueue =
                new MinPriorityQueue<GraphVertex<BellmanFordNodeData<T>>>();

            foreach (var vertex in Graph.Vertices)
            {
                unverifiedQueue.Insert(vertex, vertex.Data.Distance);
            }

            while (!unverifiedQueue.IsEmpty)
            {
                var uVertex = unverifiedQueue.ExtractMinimum();
                verifiedSet = DisjointSet<GraphVertex<BellmanFordNodeData<T>>>.Union(verifiedSet,
                    DisjointSet<GraphVertex<BellmanFordNodeData<T>>>.MakeSet(uVertex, CompareVertices));
                foreach (var vVertex in Graph.AdjacentVertices(uVertex))
                {
                    var modifiedVertex = vVertex;
                    var edge = Graph.GetEdge(uVertex.Index, vVertex.Index);
                    Relax(uVertex, modifiedVertex, edge);
                    var relaxIndex = unverifiedQueue.GetIndex(vertex => vertex.Index == modifiedVertex.Index);
                    if (relaxIndex != null)
                    {
                        unverifiedQueue.DecreasePriority(relaxIndex.Value, modifiedVertex.Data.Distance);
                    }
                }
            }
        }
    }
}