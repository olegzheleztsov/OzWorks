using System;
using System.Collections.Generic;
using Oz.Algorithms.Matrices;
using Xunit;

namespace Oz.Algorithms.Tests.Matrices
{
    public class FloatMatricesTests
    {
        [Fact]
        public void Should_Comparision_Works_Expectedly()
        {
            var matr1 = new FloatMatrix(2, 2, new[] {1f, 1, 1, 1});
            var matr2 = new FloatMatrix(2, 2, new[] {1f, 1, 1, 1});
            var matr3 = new FloatMatrix(2, 2, new[] {1.001f, 1, 1, 1});
            var matr4 = new FloatMatrix(1, 1, new[] {1f});

            // ReSharper disable once EqualExpressionComparison
           
            Assert.True(matr1.Equals(matr1));
            Assert.True(matr1 == matr2);
            Assert.True(matr1.Equals(matr2));
            Assert.True(matr1 != matr3);
            Assert.True(matr1 != null);
            Assert.True(matr1 != matr4);
            Assert.True(!matr1.Equals(null));
            Assert.True(!matr1.Equals(matr4));
            Assert.True(null != matr1);
        }

        [Fact]
        public void Should_Recursive_Multiply_Works_Correctly()
        {
            var mat1 = new FloatMatrix(2, 2, new float[] {1, 2, -1, 3});
            var mat2 = new FloatMatrix(2, 2, new float[] {2, 1, 4, -1});
            var result1 = new FloatMatrix(2, 2, new float[] {10, -1, 10, -4});


            Assert.Equal(result1, FloatMatrix.MultiplyRecursively(mat1, mat2));
            Assert.True(result1 == FloatMatrix.MultiplyRecursively(mat1, mat2));
        }

        [Fact]
        public void Should_Standard_Multiplication_Works_Correctly()
        {
            var mat3 = new FloatMatrix(3, 2, new float[] {1, 2, 3, 4, -1, -2});
            var mat4 = new FloatMatrix(2, 3, new float[] {1, 2, 4, 1, -1, 2});
            var result2 = new FloatMatrix(3, 3, new float[] {3, 0, 8, 7, 2, 20, -3, 0, -8});
            Assert.Equal(result2, mat3 * mat4);
        }

        [Fact]
        public void Should_Fast_Multiply_Works_Correctly()
        {
            var mat1 = new FloatMatrix(2, 2, new float[] {1, 2, -1, 3});
            var mat2 = new FloatMatrix(2, 2, new float[] {2, 1, 4, -1});
            var result1 = new FloatMatrix(2, 2, new float[] {10, -1, 10, -4});
            Assert.Equal(result1, FloatMatrix.MultiplyRecursively(mat1, mat2));
            Assert.True(result1 == FloatMatrix.MultiplyFast(mat1, mat2));
        }

        [Fact]
        public void Should_Work_Optimal_Multiplication_Same_Way_As_Normal_Multiplication()
        {
            var matrices = new List<FloatMatrix>
            {
                new FloatMatrix(30, 35),
                new FloatMatrix(35, 15),
                new FloatMatrix(15, 5),
                new FloatMatrix(5, 10),
                new FloatMatrix(10, 20),
                new FloatMatrix(20, 25)
            };

            var randomSource = new DefaultRandomSource();
            foreach (var matrix in matrices)
            {
                for (var i = 0; i < matrix.Rows; i++)
                {
                    for (var j = 0; j < matrix.Columns; j++)
                    {
                        matrix[i, j] = randomSource.RandomFloat * 2;
                    }
                }
            }

            var expected = new FloatMatrix(matrices[0]);
            for (var i = 1; i < matrices.Count; i++)
            {
                expected *= matrices[i];
            }

            var actual1 = matrices.MultiplyByOptimalWay(DynamicProcedureKind.TopDown);
            var actual2 = matrices.MultiplyByOptimalWay(DynamicProcedureKind.BottomUp);
            const float eps = 1f;
            for (var i = 0; i < expected.Rows; i++)
            {
                for (var j = 0; j < expected.Columns; j++)
                {
                    Assert.True(Math.Abs(expected[i, j] - actual1[i, j]) < eps);
                    Assert.True(Math.Abs(expected[i, j] - actual2[i, j]) < eps);
                }
            }
        }
    }
}