using System;
using System.Collections.Generic;
using System.Linq;
using Oz.Algorithms.Graph;
using static System.Console;

namespace Oz.Graph
{
    public class GraphCase
    {
        private BfsInitData PrepareBfsInitData()
        {
            var nodeData = new BfsSearchNodeData<int>[8];
            for (var i = 0; i < nodeData.Length; i++)
            {
                nodeData[i] = new BfsSearchNodeData<int>(i);
            }

            var edges = new List<(int, int)>
            {
                (0, 1),
                (1, 2),
                (2, 3),
                (3, 4),
                (3, 5),
                (4, 5),
                (4, 6),
                (5, 6),
                (5, 7),
                (6, 7)
            };
            return new BfsInitData
            {
                Nodes = nodeData,
                Edges = edges
            };
        }

        private DfsInitData PrepareDfsInitData()
        {
            var nodeData = new DfsSearchNodeData<int>[6];
            for (int i = 0; i < nodeData.Length; i++)
            {
                nodeData[i] = new DfsSearchNodeData<int>(i);
            }
            
            var edges = new List<(int, int)>
            {
                (0, 1),
                (0, 2),
                (1, 2),
                (2, 3),
                (3, 1),
                (4, 3),
                (4, 5),
                (5, 5)
            };

            return new DfsInitData()
            {
                Nodes = nodeData,
                Edges = edges
            };
        }

        public void RunTopologicalSort()
        {
            var initData = PrepareDfsInitData();
            var listGraph = new AdjacentListGraph<DfsSearchNodeData<int>>(initData.Nodes, initData.Edges, true);
            var result = listGraph.TopologicalSort();
            foreach (var node in result)
            {
                Console.WriteLine(node.Index);
            }
        }


        public void RunDfsOnListGraph()
        {
            var initData = PrepareDfsInitData();
            var listGraph = new AdjacentListGraph<DfsSearchNodeData<int>>(initData.Nodes, initData.Edges, true);
            listGraph.Dfs((forwardNode) =>
            {
                Console.WriteLine($"Forward: {forwardNode.Index}, Color: {forwardNode.Data.Color}");
            }, backwardNode =>
            {
                Console.WriteLine($"Backward: {backwardNode.Index}, Color: {backwardNode.Data.Color}");
            });
        }

        public void RunDfsOnMatrixGraph()
        {
            var initData = PrepareDfsInitData();
            var matrixGraph = new AdjacentMatrixGraph<DfsSearchNodeData<int>>(initData.Nodes, initData.Edges, true);
            matrixGraph.Dfs((forwardNode) =>
            {
                Console.WriteLine($"Forward: {forwardNode.Index}, Color: {forwardNode.Data.Color}");
            }, backwardNode =>
            {
                Console.WriteLine($"Backward: {backwardNode.Index}, Color: {backwardNode.Data.Color}");
            });
        }
        

        private void DoBfs(IGraph<BfsSearchNodeData<int>> graph)
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
            var graph = new AdjacentListGraph<BfsSearchNodeData<int>>(initData.Nodes, initData.Edges, false);
            DoBfs(graph);
        }

        public void RunBfsOnMatrixGraph()
        {
            var initData = PrepareBfsInitData();
            var graph = new AdjacentListGraph<BfsSearchNodeData<int>>(initData.Nodes, initData.Edges, false);
            DoBfs(graph);
        }



        public void VerifyPathFinding()
        {
            var initData = PrepareBfsInitData();
            var listGraph = new AdjacentListGraph<BfsSearchNodeData<int>>(initData.Nodes, initData.Edges, false);
            var path = listGraph.GetPath(listGraph.GetVertex(2), listGraph.GetVertex(7), true);
            PrintVertices(path);

            var matrixGraph = new AdjacentMatrixGraph<BfsSearchNodeData<int>>(initData.Nodes, initData.Edges, false);
            path = matrixGraph.GetPath(matrixGraph.GetVertex(0), matrixGraph.GetVertex(7), true);
            PrintVertices(path);
        }

        private void PrintVertices(IEnumerable<GraphVertex<BfsSearchNodeData<int>>> vertices)
        {
            Console.WriteLine(string.Join(" ", vertices.Select(v => v.Index)));
        }

        public void TestConnectedComponents()
        {
            DfsSearchNodeData<int>[] nodes = new DfsSearchNodeData<int>[8];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new DfsSearchNodeData<int>(i);
            }

            List<(int, int)> edges = new List<(int, int)>()
            {
                (2, 0),
                (0, 1),
                (1, 2),
                (2, 3),
                (1, 3),
                (3, 4),
                (4, 3),
                (5, 4),
                (5, 6),
                (6, 5),
                (6, 7),
                (7, 7),
                (4, 7)
            };
            var graph = new AdjacentListGraph<DfsSearchNodeData<int>>(nodes, edges, true);
            var connectedComponents = graph.ConnectedComponents();
            foreach (var component in connectedComponents.ConnectedComponents)
            {
                Console.WriteLine(string.Join(" ", component.Select(c => c.Index)));
            }
        }

        private class BfsInitData
        {
            public BfsSearchNodeData<int>[] Nodes { get; init; }
            public IEnumerable<(int, int)> Edges { get; init; }
        }
        
        private class DfsInitData
        {
            public DfsSearchNodeData<int>[] Nodes { get; init; }
            public IEnumerable<(int, int)> Edges { get; init; }
        }
    }
}