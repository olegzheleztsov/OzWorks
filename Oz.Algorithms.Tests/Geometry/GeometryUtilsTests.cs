using FluentAssertions;
using Oz.Algorithms.Geometry;
using Xunit;

namespace Oz.Algorithms.Tests.Geometry
{
    public class GeometryUtilsTests
    {
        [Fact]
        public void Should_Correctly_Determine_Intersections()
        {
            var baseSegment = new LineSegment(new Point2d(2, 2), new Point2d(5, 5));
            var intersectSegment1 = new LineSegment(new Point2d(2, 5), new Point2d(4, 1));
            var intersectSegment2 = new LineSegment(new Point2d(4, 7), new Point2d(5, 5));
            var nonIntersectSegment1 = new LineSegment(new Point2d(8, 3), new Point2d(11, 1));
            var nonIntersectSegment2 = new LineSegment(new Point2d(7, 7), new Point2d(9, 5));

            GeometryUtils.IsLineSegmentsIntersect(baseSegment, intersectSegment1).Should().BeTrue();
            GeometryUtils.IsLineSegmentsIntersect(baseSegment, intersectSegment2).Should().BeTrue();
            GeometryUtils.IsLineSegmentsIntersect(baseSegment, nonIntersectSegment1).Should().BeFalse();
            GeometryUtils.IsLineSegmentsIntersect(baseSegment, nonIntersectSegment2).Should().BeFalse();
        }
    }
}