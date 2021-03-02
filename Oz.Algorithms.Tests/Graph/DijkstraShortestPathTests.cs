using System.Linq;
using FluentAssertions;
using Oz.Algorithms.Graph;
using Xunit;

namespace Oz.Algorithms.Tests.Graph
{
    public class DijkstraShortestPathTests
    {
        [Fact]
        public void Dijkstra_Should_Execute_Correctly()
        {
            var vertices = new[]
            {
                new BellmanFordNodeData<char>('s'),
                new BellmanFordNodeData<char>('t'),
                new BellmanFordNodeData<char>('x'),
                new BellmanFordNodeData<char>('y'),
                new BellmanFordNodeData<char>('z')
            };

            IEdge<BellmanFordNodeData<char>>[] edges = {
                new Edge<BellmanFordNodeData<char>>(0, 1, 10),
                new Edge<BellmanFordNodeData<char>>(0, 3, 5),
                new Edge<BellmanFordNodeData<char>>(1, 2, 1),
                new Edge<BellmanFordNodeData<char>>(1, 3, 2),
                new Edge<BellmanFordNodeData<char>>(2, 4, 4),
                new Edge<BellmanFordNodeData<char>>(3, 1, 3),
                new Edge<BellmanFordNodeData<char>>(3, 2, 9),
                new Edge<BellmanFordNodeData<char>>(3, 4, 2),
                new Edge<BellmanFordNodeData<char>>(4, 2, 6),
                new Edge<BellmanFordNodeData<char>>(4, 0, 7)
            };
            
            int WeightFunction(GraphVertex<BellmanFordNodeData<char>> firstVertex,
                GraphVertex<BellmanFordNodeData<char>> secondVertex)
            {
                var edge = edges.FirstOrDefault(e => e.FromIndex == firstVertex.Index
                                                     && e.ToIndex == secondVertex.Index);
                if (edge?.UserData != null)
                {
                    return (int) edge.UserData;
                }

                return Util.IntegerPositiveInfinity;
            }

            var graph =
                new DirectedAdjacentListGraph<BellmanFordNodeData<char>>(
                    vertices, edges, null, WeightFunction);

            var s = GetVertex(graph, 's');
            var t = GetVertex(graph, 't');
            var x = GetVertex(graph, 'x');
            var y = GetVertex(graph, 'y');
            var z = GetVertex(graph, 'z');

            var dijkstraShortestPath = new DijkstraShortestPath<char>(graph);
            dijkstraShortestPath.Run(s);
            
            x.Data.Parent.Should().BeSameAs(t);
            z.Data.Parent.Should().BeSameAs(y);
            t.Data.Parent.Should().BeSameAs(y);
            y.Data.Parent.Should().BeSameAs(s);
            s.Data.Parent.Should().BeNull();
            x.Data.Distance.Should().Be(9);
            z.Data.Distance.Should().Be(7);
            t.Data.Distance.Should().Be(8);
            y.Data.Distance.Should().Be(5);
            s.Data.Distance.Should().Be(0);
        }
        
        private GraphVertex<BellmanFordNodeData<char>> GetVertex(IGraph<BellmanFordNodeData<char>> graph, char ch)
        {
            return graph.Vertices.FirstOrDefault(v => v.Data.Data == ch);
        }
    }
}