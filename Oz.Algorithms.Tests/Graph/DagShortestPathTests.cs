#region

using System.Linq;
using FluentAssertions;
using Oz.Algorithms.Graph;
using Xunit;

#endregion

namespace Oz.Algorithms.Tests.Graph
{
    public class DagShortestPathTests
    {
        [Fact]
        public void Dag_Should_Execute_Correctly()
        {
            BellmanFordNodeData<char>[] vertices =
            {
                new('r'),
                new('s'),
                new('t'),
                new('x'),
                new('y'),
                new('z')
            };

            IEdge<BellmanFordNodeData<char>>[] edges =
            {
                new Edge<BellmanFordNodeData<char>>(0, 1, 5),
                new Edge<BellmanFordNodeData<char>>(0, 2, 3),
                new Edge<BellmanFordNodeData<char>>(1, 2, 2),
                new Edge<BellmanFordNodeData<char>>(1, 3, 6),
                new Edge<BellmanFordNodeData<char>>(2, 3, 7),
                new Edge<BellmanFordNodeData<char>>(2, 4, 4),
                new Edge<BellmanFordNodeData<char>>(2, 5, 2),
                new Edge<BellmanFordNodeData<char>>(3, 4, -1),
                new Edge<BellmanFordNodeData<char>>(3, 5, 1),
                new Edge<BellmanFordNodeData<char>>(4, 5, -2)
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

            var dagShortestPath = new DagShortestPath<char>(graph);
            var r = GetVertex(graph, 'r');
            var s = GetVertex(graph, 's');
            var t = GetVertex(graph, 't');
            var x = GetVertex(graph, 'x');
            var y = GetVertex(graph, 'y');
            var z = GetVertex(graph, 'z');
            dagShortestPath.Run(s);

            z.Data.Parent.Should().BeSameAs(y);
            y.Data.Parent.Should().BeSameAs(x);
            x.Data.Parent.Should().BeSameAs(s);
            t.Data.Parent.Should().BeSameAs(s);
            s.Data.Parent.Should().BeNull();
            r.Data.Parent.Should().BeNull();

            z.Data.Distance.Should().Be(3);
            y.Data.Distance.Should().Be(5);
            x.Data.Distance.Should().Be(6);
            t.Data.Distance.Should().Be(2);
            s.Data.Distance.Should().Be(0);
            r.Data.Distance.Should().Be(Util.IntegerPositiveInfinity);
        }

        private GraphVertex<BellmanFordNodeData<char>> GetVertex(IGraph<BellmanFordNodeData<char>> graph, char ch)
        {
            return graph.Vertices.FirstOrDefault(v => v.Data.Data == ch);
        }
    }
}