using Oz.Algorithms;
using Oz.Algorithms.Graph;
using System.Linq;
using static System.Console;

namespace Oz.Graph;

public class GraphCase
{
    public void SlowAllPairsShortestPaths()
    {
        _ = new GraphVertex<int>[] {new(0, 0), new(1, 1), new(2, 2), new(3, 3), new(4, 4), new(5, 5)};

        var edges = new Edge<int>[]
        {
            new(0, 1), new(0, 2), new(0, 4), new(1, 4), new(1, 3), new(2, 1), new(3, 2), new(3, 0), new(4, 3),
            new(4, 5)
        };

        var weights = new (int FirstIndex, int SecondIndex, int Weight)[]
        {
            (0, 1, 3), (0, 2, 8), (0, 4, -4), (1, 4, 7), (1, 3, 1), (2, 1, 4), (3, 2, -5), (3, 0, 2), (4, 3, 6),
            (4, 5, 1)
        };

        int WeightFunc(GraphVertex<int> v1, GraphVertex<int> v2)
        {
            foreach (var w in weights)
            {
                if (w.FirstIndex == v1.Index && w.SecondIndex == v2.Index)
                {
                    return w.Weight;
                }
            }

            return Util.IntegerPositiveInfinity;
        }

        var graph = new DirectedAdjacentMatrixGraph<int>(
            new[] {0, 1, 2, 3, 4, 5},
            edges.Cast<IEdge<int>>().ToArray(), WeightFunc);
        var result = graph.SlowAllPairsShortestPaths();
        WriteLine(result);

        var fasterResult = graph.FasterAllPairsShortestPaths();
        WriteLine(fasterResult);

        WriteLine("DISTANCES");
        var distances = graph.FloydWarshall();
        WriteLine(distances);

        WriteLine("Transitive closure");
        var transitions = graph.TransitiveClosure();
        WriteLine(transitions);

        WriteLine("Floyd warshall mod");
        var (fPredecessors, fDistances) = graph.FloydWarshallMod();
        WriteLine(fDistances);
        WriteLine(fPredecessors);
        var fPath = graph.GetPath(fPredecessors, 0, 2);
        WriteLine($"Path 0 -> 2: {(fPath == null ? "No" : string.Join(" ", fPath))}");
        var fPath20 = graph.GetPath(fPredecessors, 2, 0);
        WriteLine($"Path 2 -> 0: {(fPath20 == null ? "No" : string.Join(" ", fPath20))}");

        BellmanFordNodeData<int>[] bfVertices = {new(0), new(1), new(2), new(3), new(4), new(5)};

        IEdge<BellmanFordNodeData<int>>[] bfEdges =
        {
            new Edge<BellmanFordNodeData<int>>(0, 1, 3), 
            new Edge<BellmanFordNodeData<int>>(0, 2, 8), 
            new Edge<BellmanFordNodeData<int>>(0, 4, -4), 
            new Edge<BellmanFordNodeData<int>>(1, 4, 7), 
            new Edge<BellmanFordNodeData<int>>(1, 3, 1), 
            new Edge<BellmanFordNodeData<int>>(2, 1, 4), 
            new Edge<BellmanFordNodeData<int>>(3, 2, -5),
            new Edge<BellmanFordNodeData<int>>(3, 0, 2), 
            new Edge<BellmanFordNodeData<int>>(4, 3, 6), 
            new Edge<BellmanFordNodeData<int>>(4, 5, 1)
        };

        int BfWeightFunc(GraphVertex<BellmanFordNodeData<int>> v1, GraphVertex<BellmanFordNodeData<int>> v2)
        {
            var edge = bfEdges.FirstOrDefault(e => e.FromIndex == v1.Index
                                                   && e.ToIndex == v2.Index);
            if (edge != null)
            {
                return (int)edge.UserData;
            }

            return 0;
        }

        var jonshonGraph =
            new DirectedAdjacentListGraph<BellmanFordNodeData<int>>(bfVertices, bfEdges, null, BfWeightFunc);
        var johnsonSearchPath = new JohnsonSearchPath<int>(jonshonGraph);
        var jResult = johnsonSearchPath.Run();
        if (jResult == null)
        {
            WriteLine("Johnson failed");
        }
        else
        {
            WriteLine("JOHNSON:");
            WriteLine(jResult);
        }
    }


    public void DijkstraTestPath()
    {
        IEdge<BellmanFordNodeData<char>>[] edges =
        {
            new Edge<BellmanFordNodeData<char>>(0, 1), 
            new Edge<BellmanFordNodeData<char>>(0, 2), 
            new Edge<BellmanFordNodeData<char>>(1, 2), 
            new Edge<BellmanFordNodeData<char>>(1, 3), 
            new Edge<BellmanFordNodeData<char>>(2, 3), 
            new Edge<BellmanFordNodeData<char>>(2, 4), 
            new Edge<BellmanFordNodeData<char>>(2, 5), 
            new Edge<BellmanFordNodeData<char>>(3, 4), 
            new Edge<BellmanFordNodeData<char>>(3, 5),
            new Edge<BellmanFordNodeData<char>>(4, 5)
        };

        var weights = new (int FirstIndex, int SecondIndex, int Weight)[]
        {
            (0, 1, 5), (0, 2, 3), (1, 2, 2), (1, 3, 6), (2, 3, 7), (2, 4, 4), (2, 5, 2), (3, 4, -1), (3, 5, 1),
            (4, 5, 2)
        };

        int WeightFunc(GraphVertex<BellmanFordNodeData<char>> v1, GraphVertex<BellmanFordNodeData<char>> v2)
        {
            foreach (var w in weights)
            {
                if (w.FirstIndex == v1.Index && w.SecondIndex == v2.Index)
                {
                    return w.Weight;
                }
            }

            return Util.IntegerPositiveInfinity;
        }

        BellmanFordNodeData<char>[] vertices = {new('A'), new('B'), new('C'), new('D'), new('E'), new('F')};

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

        var
            graph = new DirectedAdjacentListGraph<BellmanFordNodeData<char>>(vertices, edges, EdgeGen, WeightFunc);

        var dijkstraShortestPath = new DijkstraShortestPath<char>(graph);
        dijkstraShortestPath.Run(graph.Vertices.FirstOrDefault(v => v.Data.Data == 'B'));
        foreach (var v in graph)
        {
            WriteLine(v.Data.ToString());
        }
    }
}