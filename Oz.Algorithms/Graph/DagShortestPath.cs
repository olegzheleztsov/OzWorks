namespace Oz.Algorithms.Graph
{
    public class DagShortestPath<T> : DirectedGraphShortestPath<T>
    {
        public DagShortestPath(DirectedAdjacentListGraph<BellmanFordNodeData<T>> graph) : base(graph)
        {
        }

        public void Run(GraphVertex<BellmanFordNodeData<T>> source)
        {
            var sortedVertices = Graph.TopologicalSort();
            InitializeSingleSource(source);
            foreach (var u in sortedVertices)
            {
                foreach (var v in Graph.AdjacentVertices(u))
                {
                    var edge = Graph.GetEdge(u.Index, v.Index);
                    Relax(u, v, edge);
                }
            }
        }
    }
    
}