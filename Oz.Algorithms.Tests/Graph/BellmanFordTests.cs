#region

using System.Linq;
using FluentAssertions;
using Oz.Algorithms.Graph;
using Xunit;
// ReSharper disable PossibleNullReferenceException

#endregion

namespace Oz.Algorithms.Tests.Graph
{
    public class BellmanFordTests
    {
        [Fact]
        public void BellmanFord_Should_Execute_Correctly()
        {
            var vertices = new[]
            {
                new BellmanFordNodeData<char>('s'),
                new BellmanFordNodeData<char>('t'),
                new BellmanFordNodeData<char>('x'),
                new BellmanFordNodeData<char>('y'),
                new BellmanFordNodeData<char>('z')
            };
            IEdge<BellmanFordNodeData<char>>[] edges =
            {
                new Edge<BellmanFordNodeData<char>>(0, 1, 6),
                new Edge<BellmanFordNodeData<char>>(1, 2, 5),
                new Edge<BellmanFordNodeData<char>>(2, 1, -2),
                new Edge<BellmanFordNodeData<char>>(1, 3, 8),
                new Edge<BellmanFordNodeData<char>>(1, 4, -4),
                new Edge<BellmanFordNodeData<char>>(0, 3, 7),
                new Edge<BellmanFordNodeData<char>>(4, 2, 7),
                new Edge<BellmanFordNodeData<char>>(4, 0, 2),
                new Edge<BellmanFordNodeData<char>>(3, 2, -3),
                new Edge<BellmanFordNodeData<char>>(3, 4, 9)
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

            var bellmanFord = new BellmanFord<char>(graph);

            var z = graph.Vertices.FirstOrDefault(v => v.Data.Data == 'z');
            var t = graph.Vertices.FirstOrDefault(v => v.Data.Data == 't');
            var x = graph.Vertices.FirstOrDefault(v => v.Data.Data == 'x');
            var y = graph.Vertices.FirstOrDefault(v => v.Data.Data == 'y');
            var s = graph.Vertices.FirstOrDefault(v => v.Data.Data == 's');
            
            var result = bellmanFord.Run(s);

            
            result.Should().BeTrue();
            z.Data.Parent.Should().BeSameAs(t);
            t.Data.Parent.Should().BeSameAs(x);
            x.Data.Parent.Should().BeSameAs(y);
            y.Data.Parent.Should().BeSameAs(s);
            s.Data.Parent.Should().BeNull();
            z.Data.Distance.Should().Be(-2);
            t.Data.Distance.Should().Be(2);
            x.Data.Distance.Should().Be(4);
            y.Data.Distance.Should().Be(7);
            s.Data.Distance.Should().Be(0);
        }
    }
}