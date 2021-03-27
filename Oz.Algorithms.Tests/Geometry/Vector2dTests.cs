using FluentAssertions;
using Oz.Algorithms.Geometry;
using Xunit;

namespace Oz.Algorithms.Tests.Geometry
{
    public class Vector2dTests
    {
        [Fact]
        public void Should_Correctly_Find_Relative_Orientation()
        {
            Vector2d v0 = new Vector2d(new Point2d(2, 2), new Point2d(3, 5));
            Vector2d v1 = new Vector2d(new Point2d(2, 2), new Point2d(5, 3));
            v0.IsCounterClockwiseRespectTo(v1).Should().BeTrue();
            v1.IsClockwiseRespectTo(v0).Should().BeTrue();
        }
    }
}