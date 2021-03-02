using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Oz.Algorithms.DataStructures;

namespace Oz.Algorithms.Graph
{
    public static class GraphExtensions
    {
        private const int Infinity = int.MaxValue;


        /// <summary>
        ///     Divides directed graph on connected components
        /// </summary>
        /// <param name="graph">Input graph</param>
        /// <typeparam name="T">Type of the graph's node data</typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Throws if input graph is not directed</exception>
        public static ConnectedComponentsData<T> ConnectedComponents<T>(
            this IDirectedGraph<DfsSearchNodeData<T>> graph)
        {
            var connectedComponents = new ConnectedComponentsData<T>();
            graph.Dfs();
            var graphTransposed = graph.Transposed;
            graphTransposed.Dfs(forwardNode => { connectedComponents.Aggregate(forwardNode); }, null, graphTr =>
            {
                var sourceVertices = graph.Vertices.OrderByDescending(vert => vert.Data.FinalDistance)
                    .ToImmutableArray();

                return sourceVertices.Select(sVertex => graphTr.GetVertex(sVertex.Index)).ToList();
            }, _ => { connectedComponents.StartNewComponent(); });
            connectedComponents.StartNewComponent();
            return connectedComponents;
        }

        /// <summary>
        ///     Completes the topological sorting of the directed graph
        /// </summary>
        /// <param name="graph">Input graph</param>
        /// <typeparam name="T">Type of the graph's node data</typeparam>
        /// <returns>Returns vertices of the graph ordered topologically</returns>
        public static IEnumerable<GraphVertex<DfsSearchNodeData<T>>> TopologicalSort<T>(
            this IGraph<DfsSearchNodeData<T>> graph)
        {
            var result = new OzSingleLinkedList<GraphVertex<DfsSearchNodeData<T>>>();
            graph.Dfs(null, backwardNode => { result.InsertFirst(backwardNode); });
            return result;
        }
        
        public static IEnumerable<GraphVertex<BellmanFordNodeData<T>>> TopologicalSort<T>(
            this IGraph<BellmanFordNodeData<T>> graph)
        {
            var result = new OzSingleLinkedList<GraphVertex<BellmanFordNodeData<T>>>();
            graph.Dfs(null, backwardNode => { result.InsertFirst(backwardNode); });
            return result;
        }

        /// <summary>
        ///     To Do: refactor input action parameters
        ///     Computes depth first search and executes different procedure during search
        /// </summary>
        /// <param name="graph">Source graph</param>
        /// <param name="forwardVisitor">Invokes when visit node in forward search direction</param>
        /// <param name="backwardVisitor">Invokes when visit node in  backward search direction</param>
        /// <param name="customMainLevelNodeEnumerator">Allows customize order of enumeration of top level graph nodes</param>
        /// <param name="startNewTopLevelNode">Invokes when search starts new top level graph node</param>
        /// <typeparam name="T">Type of the graph node data</typeparam>
        public static void Dfs<T>(this IGraph<DfsSearchNodeData<T>> graph,
            Action<GraphVertex<DfsSearchNodeData<T>>> forwardVisitor = null,
            Action<GraphVertex<DfsSearchNodeData<T>>> backwardVisitor = null,
            Func<IGraph<DfsSearchNodeData<T>>, IEnumerable<GraphVertex<DfsSearchNodeData<T>>>>
                customMainLevelNodeEnumerator = null,
            Action<GraphVertex<DfsSearchNodeData<T>>> startNewTopLevelNode = null)
        {
            foreach (var vertex in graph.Vertices)
            {
                vertex.Data.Color = GraphNodeColor.White;
                vertex.Data.Previous = null;
            }

            var time = 0;

            IEnumerable<GraphVertex<DfsSearchNodeData<T>>> EnumerateNodes(IGraph<DfsSearchNodeData<T>> graphInner,
                Func<IGraph<DfsSearchNodeData<T>>, IEnumerable<GraphVertex<DfsSearchNodeData<T>>>>
                    customMainLevelNodeEnumeratorInner)
            {
                return customMainLevelNodeEnumeratorInner == null
                    ? graphInner.Vertices
                    : customMainLevelNodeEnumeratorInner(graph);
            }

            foreach (var vertex in EnumerateNodes(graph, customMainLevelNodeEnumerator))
            {
                if (vertex.Data.Color == GraphNodeColor.White)
                {
                    startNewTopLevelNode?.Invoke(vertex);
                    DfsVisiting(graph, vertex, ref time, forwardVisitor, backwardVisitor);
                }
            }
        }
        
        public static void Dfs<T>(this IGraph<BellmanFordNodeData<T>> graph,
            Action<GraphVertex<BellmanFordNodeData<T>>> forwardVisitor = null,
            Action<GraphVertex<BellmanFordNodeData<T>>> backwardVisitor = null,
            Func<IGraph<BellmanFordNodeData<T>>, IEnumerable<GraphVertex<BellmanFordNodeData<T>>>>
                customMainLevelNodeEnumerator = null,
            Action<GraphVertex<BellmanFordNodeData<T>>> startNewTopLevelNode = null)
        {
            foreach (var vertex in graph.Vertices)
            {
                vertex.Data.Color = GraphNodeColor.White;
                vertex.Data.Previous = null;
            }

            var time = 0;

            IEnumerable<GraphVertex<BellmanFordNodeData<T>>> EnumerateNodes(IGraph<BellmanFordNodeData<T>> graphInner,
                Func<IGraph<BellmanFordNodeData<T>>, IEnumerable<GraphVertex<BellmanFordNodeData<T>>>>
                    customMainLevelNodeEnumeratorInner)
            {
                return customMainLevelNodeEnumeratorInner == null
                    ? graphInner.Vertices
                    : customMainLevelNodeEnumeratorInner(graph);
            }

            foreach (var vertex in EnumerateNodes(graph, customMainLevelNodeEnumerator))
            {
                if (vertex.Data.Color == GraphNodeColor.White)
                {
                    startNewTopLevelNode?.Invoke(vertex);
                    DfsVisiting(graph, vertex, ref time, forwardVisitor, backwardVisitor);
                }
            }
        }

        /// <summary>
        ///     Inner recursive Dfs procedure
        /// </summary>
        /// <param name="graph">Input graph</param>
        /// <param name="node">Current node</param>
        /// <param name="time">Global time variable</param>
        /// <param name="forwardVisitor">Forward stage node visitor action</param>
        /// <param name="backwardVisitor">Backward stage node visitor action</param>
        /// <typeparam name="T">Type of the graph's node data</typeparam>
        private static void DfsVisiting<T>(IGraph<DfsSearchNodeData<T>> graph,
            GraphVertex<DfsSearchNodeData<T>> node,
            ref int time,
            Action<GraphVertex<DfsSearchNodeData<T>>> forwardVisitor = null,
            Action<GraphVertex<DfsSearchNodeData<T>>> backwardVisitor = null)
        {
            time++;
            node.Data.Distance = time;
            node.Data.Color = GraphNodeColor.Gray;
            forwardVisitor?.Invoke(node);

            foreach (var adjNode in graph.AdjacentVertices(node))
            {
                if (adjNode.Data.Color == GraphNodeColor.White)
                {
                    adjNode.Data.Previous = node;
                    DfsVisiting(graph, adjNode, ref time, forwardVisitor, backwardVisitor);
                }
            }

            node.Data.Color = GraphNodeColor.Black;
            time++;
            node.Data.FinalDistance = time;
            backwardVisitor?.Invoke(node);
        }
        
        private static void DfsVisiting<T>(IGraph<BellmanFordNodeData<T>> graph,
            GraphVertex<BellmanFordNodeData<T>> node,
            ref int time,
            Action<GraphVertex<BellmanFordNodeData<T>>> forwardVisitor = null,
            Action<GraphVertex<BellmanFordNodeData<T>>> backwardVisitor = null)
        {
            time++;
            node.Data.Distance = time;
            node.Data.Color = GraphNodeColor.Gray;
            forwardVisitor?.Invoke(node);

            foreach (var adjNode in graph.AdjacentVertices(node))
            {
                if (adjNode.Data.Color == GraphNodeColor.White)
                {
                    adjNode.Data.Previous = node;
                    DfsVisiting(graph, adjNode, ref time, forwardVisitor, backwardVisitor);
                }
            }

            node.Data.Color = GraphNodeColor.Black;
            time++;
            node.Data.FinalDistance = time;
            backwardVisitor?.Invoke(node);
        }

        /// <summary>
        ///     Computes breadth first search on the graph
        /// </summary>
        /// <param name="graph">Input graph</param>
        /// <param name="sourceNode">Start search node</param>
        /// <param name="visitor">Visitor action</param>
        /// <typeparam name="T">Type of the graph's node data</typeparam>
        public static void Bfs<T>(this IGraph<BfsSearchNodeData<T>> graph,
            GraphVertex<BfsSearchNodeData<T>> sourceNode,
            Action<GraphVertex<BfsSearchNodeData<T>>> visitor = null)
        {
            foreach (var vertex in graph.Vertices)
            {
                if (sourceNode.Index != vertex.Index)
                {
                    vertex.Data.Color = GraphNodeColor.White;
                    vertex.Data.Distance = Infinity;
                    vertex.Data.Previous = null;
                }
            }

            sourceNode.Data.Color = GraphNodeColor.Gray;
            sourceNode.Data.Distance = 0;
            sourceNode.Data.Previous = null;
            var queue = new OzDequeue<GraphVertex<BfsSearchNodeData<T>>>(graph.VertexCount);
            queue.EnqueueRight(sourceNode);
            while (!queue.IsEmpty)
            {
                var node = queue.DequeueLeft();
                foreach (var adjVertex in graph.AdjacentVertices(node))
                {
                    if (adjVertex.Data.Color == GraphNodeColor.White)
                    {
                        adjVertex.Data.Color = GraphNodeColor.Gray;
                        adjVertex.Data.Distance = node.Data.Distance + 1;
                        adjVertex.Data.Previous = node;
                        queue.EnqueueRight(adjVertex);
                    }
                }

                node.Data.Color = GraphNodeColor.Black;
                visitor?.Invoke(node);
            }
        }

        /// <summary>
        ///     Find the shortest path between graph nodes
        /// </summary>
        /// <param name="graph">Input graph</param>
        /// <param name="fromNode">Start path node</param>
        /// <param name="toNode">End path node</param>
        /// <param name="rebuild">Should we call BFS on the graph to set node distance?</param>
        /// <typeparam name="T">Type of the graph's node data</typeparam>
        /// <returns>Collection of nodes which form the path between start and end nodes</returns>
        public static IEnumerable<GraphVertex<BfsSearchNodeData<T>>> GetPath<T>(
            this IGraph<BfsSearchNodeData<T>> graph,
            GraphVertex<BfsSearchNodeData<T>> fromNode, GraphVertex<BfsSearchNodeData<T>> toNode, bool rebuild = false)
        {
            if (rebuild)
            {
                graph.Bfs(fromNode);
            }

            var pathList = new OzSingleLinkedList<GraphVertex<BfsSearchNodeData<T>>>();
            ConstructPath(fromNode, toNode, pathList);
            return pathList;
        }

        /// <summary>
        ///     Inner procedure that construct path
        /// </summary>
        /// <param name="fromNode">Start path node</param>
        /// <param name="toNode">End path node</param>
        /// <param name="pathList">Current path list</param>
        /// <typeparam name="T">Type of the graph's node data</typeparam>
        private static void ConstructPath<T>(GraphVertex<BfsSearchNodeData<T>> fromNode,
            GraphVertex<BfsSearchNodeData<T>> toNode,
            OzSingleLinkedList<GraphVertex<BfsSearchNodeData<T>>> pathList)
        {
            if (toNode.Index == fromNode.Index)
            {
                pathList.InsertLast(fromNode);
            }
            else if (toNode.Data.Previous is null)
            {
                pathList.Clear();
            }
            else
            {
                ConstructPath(fromNode, toNode.Data.Previous as GraphVertex<BfsSearchNodeData<T>>, pathList);
                pathList.InsertLast(toNode);
            }
        }

        public static (List<int>, List<IEdge<T>>) ConstructMinimumSpanningTree<T>(this IUndirectedGraph<T> graph)
        {
            var mstBuilder = new MstBuilder<T>(graph);
            return mstBuilder.Build();
        }

        public static (List<int>, IEnumerable<IEdge<T>>) KruskalMinimumSpanningTree<T>(this IGraph<T> graph)
        {
            static bool EdgeComparer(IEdge<T> firstEdge, IEdge<T> secondEdge)
            {
                return firstEdge.FromIndex == secondEdge.FromIndex && firstEdge.ToIndex == secondEdge.ToIndex;
            }

            var result = DisjointSet<IEdge<T>>.Empty(EdgeComparer);
            var vertexSets = graph.Vertices.Select(vertex =>
                DisjointSet<GraphVertex<T>>.MakeSet(vertex,
                    (firstVertex, secondVertex) => firstVertex.Index == secondVertex.Index)).ToList();

            var sortedEdges = graph
                .Edges
                .OrderBy(edge => graph.WeightFunc(edge.FromVertex, edge.ToVertex))
                .ToList();

            foreach (var edge in sortedEdges)
            {
                var uVertex = graph.GetVertex(edge.FromIndex);
                var vVertex = graph.GetVertex(edge.ToIndex);
                var setU = vertexSets.FirstOrDefault(vertexSet => vertexSet.Find(uVertex) != null);
                var setV = vertexSets.FirstOrDefault(vertexSet => vertexSet.Find(vVertex) != null);
                if (setU == null)
                {
                    throw new InvalidOperationException($"Not found set for vertex: {uVertex.Index}");
                }

                if (setV == null)
                {
                    throw new InvalidOperationException($"Not found set for vertex: {vVertex.Index}");
                }

                if (setU != setV)
                {
                    var newEdgeSet = DisjointSet<IEdge<T>>.MakeSet(edge, EdgeComparer);
                    result = DisjointSet<IEdge<T>>.Union(result, newEdgeSet);
                    vertexSets.Remove(setU);
                    vertexSets.Remove(setV);
                    vertexSets.Add(DisjointSet<GraphVertex<T>>.Union(setU, setV));
                }
            }

            return (graph.Vertices.Select(v => v.Index).ToList(), result.Enumerate());
        }

        public static (List<int>, IEnumerable<IEdge<PrimNode<T>>>) PrimMinimumSpanningTree<T>(
            this IGraph<PrimNode<T>> graph, GraphVertex<PrimNode<T>> startVertex)
        {
            int Weight(int fromIndex, int toIndex)
            {
                var targetEdge = (from edge in graph.Edges
                    where edge.FromIndex == fromIndex && edge.ToIndex == toIndex ||
                          edge.ToIndex == fromIndex && edge.FromIndex == toIndex
                    select edge).FirstOrDefault();

                return targetEdge != null ? graph.WeightFunc(targetEdge.FromVertex, targetEdge.ToVertex) : Util.IntegerPositiveInfinity;
            }

            IEdge<PrimNode<T>> GetEdge(int fromIndex, int toIndex)
            {
                var targetEdge = (from edge in graph.Edges
                    where edge.FromIndex == fromIndex && edge.ToIndex == toIndex ||
                          edge.ToIndex == fromIndex && edge.FromIndex == toIndex
                    select edge).FirstOrDefault();
                if (targetEdge == null)
                {
                    throw new InvalidOperationException($"Not found edge {fromIndex}-{toIndex}");
                }

                return targetEdge;
            }


            foreach (var vertex in graph)
            {
                vertex.Data.Key = Util.IntegerPositiveInfinity;
                vertex.Data.Parent = null;
            }

            startVertex.Data.Key = 0;
            var queue = new MinPriorityQueue<GraphVertex<PrimNode<T>>>();
            foreach (var vertex in graph)
            {
                queue.Insert(vertex, vertex.Data.Key);
            }

            while (!queue.IsEmpty)
            {
                var u = queue.ExtractMinimum();
                foreach (var adjVertex in graph.AdjacentVertices(u))
                {
                    var weight = Weight(u.Index, adjVertex.Index);
                    var adjVertexIndex = queue.GetIndex(adjVertex);
                    if (adjVertexIndex >= 0 && weight < adjVertex.Data.Key)
                    {
                        adjVertex.Data.Parent = u;
                        adjVertex.Data.Key = weight;
                        queue.DecreasePriority(adjVertexIndex, weight);
                    }
                }
            }

            var vertices = graph.Vertices.Where(v => v != startVertex);
            var resultEdges = vertices.Select(v => GetEdge(v.Index, v.Data.Parent.Index)).ToList();
            return (graph.Vertices.Select(v => v.Index).OrderBy(i => i).ToList(), resultEdges);
        }
    }
}