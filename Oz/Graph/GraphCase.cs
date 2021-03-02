using System;
using System.Collections.Generic;
using System.Linq;
using Oz.Algorithms;
using Oz.Algorithms.Graph;
using static System.Console;

namespace Oz.Graph
{
    public class GraphCase
    {

        public void SlowAllPairsShortestPaths()
        {
            GraphVertex<int>[] vertices = {
                new(0, 0),
                new(1, 1),
                new(2, 2),
                new(3, 3),
                new(4, 4),
                new(5, 5)
            };

            var edges = new Edge<int>[]
            {
                new(0, 1),
                new(0, 2),
                new(0, 4),
                new(1, 4),
                new(1, 3),
                new(2, 1),
                new(3, 2),
                new(3, 0),
                new(4, 3),
                new(4, 5)
            };

            var weigts = new (int FirstIndex, int SecondIndex, int Weight)[]
            {
                (0, 1, 3),
                (0, 2, 8),
                (0, 4, -4),
                (1, 4, 7),
                (1, 3, 1),
                (2, 1, 4),
                (3, 2, -5),
                (3, 0, 2),
                (4, 3, 6),
                (4, 5, 1)
            };

            int WeightFunc(GraphVertex<int> v1, GraphVertex<int> v2)
            {
                foreach (var w in weigts)
                {
                    if (w.FirstIndex == v1.Index && w.SecondIndex == v2.Index)
                    {
                        return w.Weight;
                    }
                }

                return Util.IntegerPositiveInfinity;
            }
            
            var graph = new DirectedAdjacentMatrixGraph<int>(
                new int[]{0, 1, 2, 3, 4, 5}, 
                edges.Cast<IEdge<int>>().ToArray(), WeightFunc);
            var result = graph.SlowAllPairsShortestPaths();
            Console.WriteLine(result);

            var fasterResult = graph.FasterAllPairsShortestPaths();
            Console.WriteLine(fasterResult);
            
            Console.WriteLine("DISTANCES");
            var distances = graph.FloydWarshall();
            Console.WriteLine(distances);
            
            Console.WriteLine("Transitive closure");
            var transitions = graph.TransitiveClosure();
            Console.WriteLine(transitions);
            
            Console.WriteLine("Floyd warshall mod");
            var (fPredecessors, fDistances) = graph.FloydWarshallMod();
            Console.WriteLine(fDistances);
            Console.WriteLine(fPredecessors);
            var fPath = graph.GetPath(fPredecessors, 0, 2);
            Console.WriteLine($"Path 0 -> 2: {(fPath == null ? "No" : string.Join(" ", fPath))}");
            var fPath20 = graph.GetPath(fPredecessors, 2, 0);
            Console.WriteLine($"Path 2 -> 0: {(fPath20 == null ? "No" : string.Join(" ", fPath20))}");

            BellmanFordNodeData<int>[] bfVertices = new BellmanFordNodeData<int>[]
            {
                new BellmanFordNodeData<int>(0),
                new BellmanFordNodeData<int>(1),
                new BellmanFordNodeData<int>(2),
                new BellmanFordNodeData<int>(3),
                new BellmanFordNodeData<int>(4),
                new BellmanFordNodeData<int>(5)
            };

            IEdge<BellmanFordNodeData<int>>[] bfEdges = new Edge<BellmanFordNodeData<int>>[]
            {
                new(0, 1, 3),
                new(0, 2, 8),
                new(0, 4, -4),
                new(1, 4, 7),
                new(1, 3, 1),
                new(2, 1, 4),
                new(3, 2, -5),
                new(3, 0, 2),
                new(4, 3, 6),
                new(4, 5, 1)
            };

            int BFWeightFunc(GraphVertex<BellmanFordNodeData<int>> v1, GraphVertex<BellmanFordNodeData<int>> v2)
            {
                var edge = bfEdges.FirstOrDefault(e => e.FromIndex == v1.Index
                                                       && e.ToIndex == v2.Index);
                return (int) edge.UserData;
            }

            var jonshonGraph =
                new DirectedAdjacentListGraph<BellmanFordNodeData<int>>(bfVertices, bfEdges, null, BFWeightFunc);
            var johnsonSearchPath = new JohnsonSearchPath<int>(jonshonGraph);
            var jResult = johnsonSearchPath.Run();
            if (jResult == null)
            {
                Console.WriteLine("Johnson failed");
            }
            else
            {
                Console.WriteLine("JOHNSON:");
                Console.WriteLine(jResult);
            }
        }
        
        
        public void DijkstraTestPath()
        {
            Edge<BellmanFordNodeData<char>>[] edges =
            {
                new(0, 1),
                new(0, 2),
                new(1, 2),
                new(1, 3),
                new(2, 3),
                new(2, 4),
                new(2, 5),
                new(3, 4),
                new(3, 5),
                new(4, 5)
            };
            
            var weigts = new (int FirstIndex, int SecondIndex, int Weight)[]
            {
                (0, 1, 5),
                (0, 2, 3),
                (1, 2, 2),
                (1, 3, 6),
                (2, 3, 7),
                (2, 4, 4),
                (2, 5, 2),
                (3, 4, -1),
                (3, 5, 1),
                (4, 5, 2)
            };

            int WeightFunc(GraphVertex<BellmanFordNodeData<char>> v1, GraphVertex<BellmanFordNodeData<char>> v2)
            {
                foreach (var w in weigts)
                {
                    if (w.FirstIndex == v1.Index && w.SecondIndex == v2.Index)
                    {
                        return w.Weight;
                    }
                }

                return Util.IntegerPositiveInfinity;
            }
            
            BellmanFordNodeData<char>[] vertices =
            {
                new('A'),
                new('B'),
                new('C'),
                new('D'),
                new('E'),
                new('F')
            };

            IEdge<BellmanFordNodeData<char>> EdgeGen(int fromIndex, int toIndex)
            {
                var oldEdge = edges.FirstOrDefault(e =>
                    (e.FromIndex == fromIndex && e.ToIndex == toIndex) ||
                    (e.ToIndex == fromIndex && e.FromIndex == toIndex));
                if (oldEdge == null)
                {
                    return null;
                }

                return new Edge<BellmanFordNodeData<char>>(oldEdge.FromIndex, oldEdge.ToIndex);
            }
            
            DirectedAdjacentListGraph<BellmanFordNodeData<char>>
                graph = new DirectedAdjacentListGraph<BellmanFordNodeData<char>>(vertices, edges,EdgeGen, WeightFunc);

            DijkstraShortestPath<char> dijkstraShortestPath = new DijkstraShortestPath<char>(graph);
            dijkstraShortestPath.Run(graph.Vertices.FirstOrDefault(v => v.Data.Data == 'B'));
            foreach (var v in graph)
            {
                Console.WriteLine(v.Data.ToString());
            }
        }
        
        
        public void DagShortestPath()
        {
            Edge<BellmanFordNodeData<char>>[] edges =
            {
                new(0, 1),
                new(0, 2),
                new(1, 2),
                new(1, 3),
                new(2, 3),
                new(2, 4),
                new(2, 5),
                new(3, 4),
                new(3, 5),
                new(4, 5)
            };
            
            var weigts = new (int FirstIndex, int SecondIndex, int Weight)[]
            {
                (0, 1, 5),
                (0, 2, 3),
                (1, 2, 2),
                (1, 3, 6),
                (2, 3, 7),
                (2, 4, 4),
                (2, 5, 2),
                (3, 4, -1),
                (3, 5, 1),
                (4, 5, -2)
            };

            int WeightFunc(GraphVertex<BellmanFordNodeData<char>> v1, GraphVertex<BellmanFordNodeData<char>> v2)
            {
                foreach (var w in weigts)
                {
                    if (w.FirstIndex == v1.Index && w.SecondIndex == v2.Index)
                    {
                        return w.Weight;
                    }
                }

                return Util.IntegerPositiveInfinity;
            }
            
            BellmanFordNodeData<char>[] vertices =
            {
                new('A'),
                new('B'),
                new('C'),
                new('D'),
                new('E'),
                new('F')
            };

            DirectedAdjacentListGraph<BellmanFordNodeData<char>>
                graph = new DirectedAdjacentListGraph<BellmanFordNodeData<char>>(vertices, edges,
                    (fromIndex, toIndex) =>
                    {
                        var oldEdge = edges.FirstOrDefault(e =>
                            (e.FromIndex == fromIndex && e.ToIndex == toIndex) ||
                            (e.ToIndex == fromIndex && e.FromIndex == toIndex));
                        if (oldEdge == null)
                        {
                            return null;
                        }

                        return new Edge<BellmanFordNodeData<char>>(oldEdge.FromIndex, oldEdge.ToIndex);
                    }, WeightFunc);

            DagShortestPath<char> dagShortestPath = new DagShortestPath<char>(graph);
            dagShortestPath.Run(graph.Vertices.FirstOrDefault(v => v.Data.Data == 'B'));
            foreach (var v in graph)
            {
                Console.WriteLine(v.Data.ToString());
            }
        }

        public void BellmanFordTest()
        {
            Edge<BellmanFordNodeData<char>>[] edges = {
                new(0, 1),
                new(0, 4),
                new(1, 4),
                new(4, 1),
                new(1, 2),
                new(2, 3),
                new(3, 2),
                new(3, 0),
                new(4, 3),
                new(4, 2)
            };
            
            var weigts = new (int FirstIndex, int SecondIndex, int Weight)[]
            {
                (0, 1, 3),
                (0, 4, 5),
                (1, 4, 2),
                (4, 1, 1),
                (1, 2, 6),
                (2, 3, 2),
                (3, 2, 7),
                (3, 0, 3),
                (4, 3, 6),
                (4, 2, 4) 
            };

            int WeightFunc(GraphVertex<BellmanFordNodeData<char>> v1, GraphVertex<BellmanFordNodeData<char>> v2)
            {
                foreach (var w in weigts)
                {
                    if (w.FirstIndex == v1.Index && w.SecondIndex == v2.Index)
                    {
                        return w.Weight;
                    }
                }

                return Util.IntegerPositiveInfinity;
            }
            
            var vertices = new[]
            {
                new BellmanFordNodeData<char>('A'),
                new BellmanFordNodeData<char>('B'),
                new BellmanFordNodeData<char>('C'),
                new BellmanFordNodeData<char>('D'),
                new BellmanFordNodeData<char>('E')
            };
            var graph =
                new DirectedAdjacentListGraph<BellmanFordNodeData<char>>(
                    vertices, edges, (fromIndex, toIndex) =>
                    {
                        var oldEdge = edges.FirstOrDefault(e =>
                            (e.FromIndex == fromIndex && e.ToIndex == toIndex) ||
                            (e.ToIndex == fromIndex && e.FromIndex == toIndex));
                        if (oldEdge == null)
                        {
                            return null;
                        }

                        return new Edge<BellmanFordNodeData<char>>(oldEdge.FromIndex, oldEdge.ToIndex);
                    }, WeightFunc);

            BellmanFord<char> bellmanFord = new BellmanFord<char>(graph);
            Console.WriteLine($"Result: {bellmanFord.Run(graph.GetVertex(0))}");

            foreach (var vertex in graph.Vertices)
            {
                Console.WriteLine(vertex.Data.ToString());
            }

        }
        /*
        public void ConstructMst()
        {
            var edges = new List<WeightedEdge>
            {
                new() {FromIndex = 0, ToIndex = 1, Weight = 4},
                new() {FromIndex = 1, ToIndex = 2, Weight = 8},
                new() {FromIndex = 1, ToIndex = 8, Weight = 11},
                new() {FromIndex = 2, ToIndex = 3, Weight = 7},
                new() {FromIndex = 3, ToIndex = 4, Weight = 9},
                new() {FromIndex = 3, ToIndex = 5, Weight = 14},
                new() {FromIndex = 4, ToIndex = 5, Weight = 10},
                new() {FromIndex = 5, ToIndex = 2, Weight = 4},
                new() {FromIndex = 5, ToIndex = 6, Weight = 2},
                new() {FromIndex = 6, ToIndex = 7, Weight = 6},
                new() {FromIndex = 6, ToIndex = 8, Weight = 1},
                new() {FromIndex = 7, ToIndex = 2, Weight = 2},
                new() {FromIndex = 8, ToIndex = 7, Weight = 7},
                new() {FromIndex = 8, ToIndex = 0, Weight = 8}
            };
            var graph = new UndirectedAdjacentListGraph<char>(new[] {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'i', 'h'},
                edges.Cast<IEdge>().ToArray());
            var (vertices, mstEdges) = graph.ConstructMinimumSpanningTree();

            WriteLine(string.Join(" ", vertices));
            foreach (var edge in mstEdges.OrderBy(e => e.Weight).ThenBy(e => e.FromIndex))
            {
                WriteLine(edge);
            }

            WriteLine();
            WriteLine("Kruskal");
            var graph2 = new UndirectedAdjacentListGraph<char>(new[] {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'i', 'h'},
                edges.Cast<IEdge>().ToArray());
            var (vertices2, edges2) = graph2.KruskalMinimumSpanningTree();
            WriteLine(string.Join(" ", vertices2));
            foreach (var edge in edges2.OrderBy(e => e.Weight).ThenBy(e => e.FromIndex))
            {
                WriteLine(edge);
            }

            WriteLine();
            WriteLine("Prim");

            var graph3 = new UndirectedAdjacentListGraph<PrimNode<char>>(
                new[] {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'i', 'h'}.Select(c => new PrimNode<char> {Data = c})
                    .ToArray(),
                edges.Cast<IEdge>().ToArray());


            var (vertices3, edges3) =
                graph3.PrimMinimumSpanningTree(graph3.Vertices.FirstOrDefault(v => v.Data.Data == 'a'));
            WriteLine(string.Join(" ", vertices3));
            foreach (var edge in edges3.OrderBy(e => e.Weight).ThenBy(e => e.FromIndex))
            {
                WriteLine(edge);
            }
        }

        public static void RunTopologicalSort()
        {
            var initData = PrepareDfsInitData();
            var listGraph =
                new DirectedAdjacentListGraph<DfsSearchNodeData<int>>(
                    initData.Nodes,
                    initData.Edges.ToArray(),
                    (f, t) => new Edge(f, t));
            var result = listGraph.TopologicalSort();
            foreach (var node in result)
            {
                WriteLine(node.Index);
            }
        }


        public static void RunDfsOnListGraph()
        {
            var initData = PrepareDfsInitData();
            var listGraph =
                new DirectedAdjacentListGraph<DfsSearchNodeData<int>>(initData.Nodes, initData.Edges.ToArray(),
                    (f, t) => new Edge(f, t));
            listGraph.Dfs(
                forwardNode => { WriteLine($"Forward: {forwardNode.Index}, Color: {forwardNode.Data.Color}"); },
                backwardNode => { WriteLine($"Backward: {backwardNode.Index}, Color: {backwardNode.Data.Color}"); });
        }

        public static void RunDfsOnMatrixGraph()
        {
            var initData = PrepareDfsInitData();
            var matrixGraph =
                new DirectedAdjacentMatrixGraph<DfsSearchNodeData<int>>(initData.Nodes, initData.Edges.ToArray());
            matrixGraph.Dfs(
                forwardNode => { WriteLine($"Forward: {forwardNode.Index}, Color: {forwardNode.Data.Color}"); },
                backwardNode => { WriteLine($"Backward: {backwardNode.Index}, Color: {backwardNode.Data.Color}"); });
        }


        private static void DoBfs(IGraph<BfsSearchNodeData<int>> graph)
        {
            graph.Bfs(graph.GetVertex(2), vertex =>
            {
                WriteLine("Visit");
                WriteLine(
                    $"Index: {vertex.Index}, Color: {vertex.Data.Color}, Distance: {vertex.Data.Distance}, Prev: {(vertex.Data.Previous as GraphVertex<BfsSearchNodeData<int>>)?.Index.ToString() ?? "<NONE>"}");
            });
        }

        public void RunBfs()
        {
            var initData = PrepareBfsInitData();
            var graph = new UndirectedAdjacentListGraph<BfsSearchNodeData<int>>(initData.Nodes,
                initData.Edges.ToArray());
            DoBfs(graph);
        }

        public void RunBfsOnMatrixGraph()
        {
            var initData = PrepareBfsInitData();
            var graph = new UndirectedAdjacentListGraph<BfsSearchNodeData<int>>(initData.Nodes,
                initData.Edges.ToArray());
            DoBfs(graph);
        }


        public void VerifyPathFinding()
        {
            var initData = PrepareBfsInitData();
            var listGraph =
                new UndirectedAdjacentListGraph<BfsSearchNodeData<int>>(initData.Nodes, initData.Edges.ToArray());
            var path = listGraph.GetPath(listGraph.GetVertex(2), listGraph.GetVertex(7), true);
            PrintVertices(path);

            var matrixGraph =
                new UndirectedAdjacentMatrixGraph<BfsSearchNodeData<int>>(initData.Nodes, initData.Edges.ToArray());
            path = matrixGraph.GetPath(matrixGraph.GetVertex(0), matrixGraph.GetVertex(7), true);
            PrintVertices(path);
        }

        private static void PrintVertices(IEnumerable<GraphVertex<BfsSearchNodeData<int>>> vertices)
        {
            WriteLine(string.Join(" ", vertices.Select(v => v.Index)));
        }

        private static BfsInitData PrepareBfsInitData()
        {
            var nodeData = new BfsSearchNodeData<int>[8];
            for (var i = 0; i < nodeData.Length; i++)
            {
                nodeData[i] = new BfsSearchNodeData<int>(i);
            }

            var edges = new List<IWeightedEdge>
            {
                new WeightedEdge(0, 1, 1),
                new WeightedEdge(1, 2, 1),
                new WeightedEdge(2, 3, 1),
                new WeightedEdge(3, 4, 1),
                new WeightedEdge(3, 5, 1),
                new WeightedEdge(4, 5, 1),
                new WeightedEdge(4, 6, 1),
                new WeightedEdge(5, 6, 1),
                new WeightedEdge(5, 7, 1),
                new WeightedEdge(6, 7, 1)
            };
            return new BfsInitData
            {
                Nodes = nodeData,
                Edges = edges
            };
        }

        private static DfsInitData PrepareDfsInitData()
        {
            var nodeData = new DfsSearchNodeData<int>[6];
            for (var i = 0; i < nodeData.Length; i++)
            {
                nodeData[i] = new DfsSearchNodeData<int>(i);
            }

            var edges = new List<IEdge>
            {
                new Edge(0, 1),
                new Edge(0, 2),
                new Edge(1, 2),
                new Edge(2, 3),
                new Edge(3, 1),
                new Edge(4, 3),
                new Edge(4, 5),
                new Edge(5, 5)
            };

            return new DfsInitData
            {
                Nodes = nodeData,
                Edges = edges
            };
        }

        public static void TestConnectedComponents()
        {
            var nodes = new DfsSearchNodeData<int>[8];
            for (var i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new DfsSearchNodeData<int>(i);
            }

            var edges = new List<IEdge>
            {
                new Edge(2, 0),
                new Edge(0, 1),
                new Edge(1, 2),
                new Edge(2, 3),
                new Edge(1, 3),
                new Edge(3, 4),
                new Edge(4, 3),
                new Edge(5, 4),
                new Edge(5, 6),
                new Edge(6, 5),
                new Edge(6, 7),
                new Edge(7, 7),
                new Edge(4, 7)
            };
            var graph = new DirectedAdjacentListGraph<DfsSearchNodeData<int>>(nodes, edges.ToArray(),
                (f, t) => new Edge(f, t));
            var connectedComponents = graph.ConnectedComponents();
            foreach (var component in connectedComponents.ConnectedComponents)
            {
                WriteLine(string.Join(" ", component.Select(c => c.Index)));
            }
        }

        private class BfsInitData
        {
            public BfsSearchNodeData<int>[] Nodes { get; init; }
            public IEnumerable<IEdge> Edges { get; init; }
        }

        private class DfsInitData
        {
            public DfsSearchNodeData<int>[] Nodes { get; init; }
            public IEnumerable<IEdge> Edges { get; init; }
        }
        */
        
    }
}