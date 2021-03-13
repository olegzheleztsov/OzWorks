using FluentAssertions;
using Oz.Algorithms.Matrices;
using Xunit;

namespace Oz.Algorithms.Tests.Matrices
{
    public class LeastSquaresMethodTests
    {
        [Fact]
        public void Should_Fit_Mults_Correctly()
        {
            var points = new[]
            {
                new Point(-1, 2),
                new Point(1, 1),
                new Point(2, 1),
                new Point(3, 0),
                new Point(5, 3)
            };
            LeastSquaresMethod leastSquaresMethod = new LeastSquaresMethod();
            var result = leastSquaresMethod.Find(points, 2);

            var expected = new float[] {1.2f, -0.757f, 0.214f, 0, 0};
            result.Should().Equal(expected, (l, r) => Util.Approximately(l, r));
        }
    }
}