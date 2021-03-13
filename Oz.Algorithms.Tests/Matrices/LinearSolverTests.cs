#region

using FluentAssertions;
using Oz.Algorithms.Matrices;
using Xunit;

#endregion

namespace Oz.Algorithms.Tests.Matrices
{
    public class LinearSolverTests
    {
        [Fact]
        public void Should_Decompose_Correctly()
        {
            var matrix = new FloatMatrix(4, 4, new[]
            {
                2, 0, 2, 0.6f,
                3, 3, 4, -2,
                5, 5, 4, 2,
                -1, -2, 3.4f, -1
            });
            var linearSolver = new LinearSolver();
            var (lower, upper, permutation) = linearSolver.LupDecompose(matrix);
            var expectedPermutation = new IntegerMatrix(4, 4, new[]
            {
                0, 0, 1, 0,
                1, 0, 0, 0,
                0, 0, 0, 1,
                0, 1, 0, 0
            });
            var expectedLower = new FloatMatrix(4, 4, new[]
            {
                1, 0, 0, 0,
                0.4f, 1, 0, 0,
                -0.2f, 0.5f, 1, 0,
                0.6f, 0, 0.4f, 1
            });
            var expectedUpper = new FloatMatrix(4, 4, new[]
            {
                5, 5, 4, 2,
                0, -2, 0.4f, -0.2f,
                0, 0, 4, -0.5f,
                0, 0, 0, -3
            });
            expectedPermutation.Should().Be(permutation);
            expectedLower.Should().Be(lower);
            expectedUpper.Should().Be(upper);
        }

        [Fact]
        public void Should_Solve_Equations_Correctly()
        {
            var source = new FloatMatrix(3, 3, new float[]
            {
                1, 2, 0,
                3, 4, 4,
                5, 6, 3
            });
            float[] b = {3, 7, 8};
            float[] expected = {-1.4f, 2.2f, 0.6f};
            var linearSolver = new LinearSolver();
            var result = linearSolver.LupSolve(source, b);
            expected.Should().Equal(result.Solution, (left, right) => Util.Approximately(left, right));
        }

        [Fact]
        public void Should_Correctly_Compute_Inverted_Matrix()
        {
            var source = new FloatMatrix(3, 3, new float[]
            {
                1, 2, 0,
                3, 4, 4,
                5, 6, 3
            });
            var linearSolver = new LinearSolver();
            var inverted = linearSolver.ComputeInvertedMatrix(source);
            var expected = new FloatMatrix(3, 3, new float[]
            {
                1, 0, 0,
                0, 1, 0,
                0, 0, 1
            });
            expected.Should().Be(source * inverted);
            expected.Should().Be(inverted * source);
        }
    }
}