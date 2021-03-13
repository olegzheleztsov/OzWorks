#region

using System;
using System.Collections.Generic;

#endregion

namespace Oz.Algorithms.Matrices
{
    public class LinearSolver
    {
        public (FloatMatrix lower, FloatMatrix upper, IntegerMatrix permut) LupDecompose(FloatMatrix input)
        {
            if (!input.IsSquareMatrix)
            {
                throw new ArgumentException(
                    $"Input matrix should be square. Actual Size: ({input.Rows}, {input.Columns})");
            }

            var resultMatrix = new FloatMatrix(input);
            var size = resultMatrix.Rows;
            var permutation = new int[size];
            for (var i = 0; i < size; i++)
            {
                permutation[i] = i;
            }

            for (var k = 0; k < size; k++)
            {
                var p = 0f;
                var ks = -1;
                for (var i = k; i < size; i++)
                {
                    if (MathF.Abs(resultMatrix[i, k]) > p)
                    {
                        p = MathF.Abs(resultMatrix[i, k]);
                        ks = i;
                    }
                }

                if (p == 0f)
                {
                    throw new InvalidOperationException("Singular matrix");
                }

                Util.Exchange(ref permutation[k], ref permutation[ks]);
                for (var i = 0; i < size; i++)
                {
                    var temp = resultMatrix[k, i];
                    resultMatrix[k, i] = resultMatrix[ks, i];
                    resultMatrix[ks, i] = temp;
                }

                for (var i = k + 1; i < size; i++)
                {
                    resultMatrix[i, k] = resultMatrix[i, k] / resultMatrix[k, k];
                    for (var j = k + 1; j < size; j++)
                    {
                        resultMatrix[i, j] -= resultMatrix[i, k] * resultMatrix[k, j];
                    }
                }
            }

            return ConvertToLup(resultMatrix, permutation);
        }

        public LinearSystemResult LupSolve(FloatMatrix source, float[] b)
        {
            if (!source.IsSquareMatrix)
            {
                throw new ArgumentException($"Should be square matrix. Dimensions: ({source.Rows}, {source.Columns})");
            }

            if (source.Rows != b.Length)
            {
                throw new ArgumentException(
                    $"Size right coefficients should be the same as matrix size. Real size: {b.Length}");
            }

            var (lower, upper, permutationMatrix) = LupDecompose(source);
            return LupSolve(lower, upper, permutationMatrix, b);
        }

        public FloatMatrix ComputeInvertedMatrix(FloatMatrix sourceMatrix)
        {
            var (lower, upper, permutation) = LupDecompose(sourceMatrix);
            var invertedColumns = new List<float[]>();
            for (var i = 0; i < sourceMatrix.Rows; i++)
            {
                var e = GetBasisVector(i, sourceMatrix.Rows);
                var result = LupSolve(lower, upper, permutation, e);
                invertedColumns.Add(result.Solution);
            }

            return FloatMatrix.FromColumns(invertedColumns);
        }

        private LinearSystemResult LupSolve(FloatMatrix lower, FloatMatrix upper, IntegerMatrix permutationMatrix,
            float[] b)
        {
            var size = lower.Rows;
            var result = new float[size];
            var y = new float[size];

            var permutation = RepresentPermutationAsArray(permutationMatrix);
            for (var i = 0; i < size; i++)
            {
                var tempSum = 0.0f;
                for (var j = 0; j < i; j++)
                {
                    tempSum += lower[i, j] * y[j];
                }

                y[i] = b[permutation[i]] - tempSum;
            }

            for (var i = size - 1; i >= 0; i--)
            {
                var tempSum = 0.0f;
                for (var j = i + 1; j < size; j++)
                {
                    tempSum += upper[i, j] * result[j];
                }

                result[i] = (y[i] - tempSum) / upper[i, i];
            }

            return new LinearSystemResult(upper, lower, permutationMatrix, result);
        }


        private (FloatMatrix lower, FloatMatrix upper, IntegerMatrix permutation) ConvertToLup(
            FloatMatrix inputLuMatrix,
            IReadOnlyList<int> permutation)
        {
            var size = inputLuMatrix.Rows;
            var lowerMatrix = new FloatMatrix(size, size);
            var upperMatrix = new FloatMatrix(size, size);


            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    if (i > j)
                    {
                        lowerMatrix[i, j] = inputLuMatrix[i, j];
                        upperMatrix[i, j] = 0.0f;
                    }
                    else if (i == j)
                    {
                        lowerMatrix[i, j] = 1;
                        upperMatrix[i, j] = inputLuMatrix[i, j];
                    }
                    else
                    {
                        lowerMatrix[i, j] = 0.0f;
                        upperMatrix[i, j] = inputLuMatrix[i, j];
                    }
                }
            }

            var permMatrix = RepresentPermutationArrayAsIntegerMatrix(permutation);
            return (lowerMatrix, upperMatrix, permMatrix);
        }

        private IntegerMatrix RepresentPermutationArrayAsIntegerMatrix(IReadOnlyList<int> permutation)
        {
            var size = permutation.Count;
            var result = new IntegerMatrix(size, size);
            for (var i = 0; i < size; i++)
            {
                result[i, permutation[i]] = 1;
            }

            return result;
        }

        private int[] RepresentPermutationAsArray(IntegerMatrix permutation)
        {
            if (!permutation.IsSquareMatrix)
            {
                throw new ArgumentException(
                    $"Should be square matrix. Dimensions: ({permutation.Rows}, {permutation.Columns})");
            }

            var result = new int[permutation.Rows];
            for (var i = 0; i < result.Length; i++)
            {
                var targetIndex = -1;
                for (var j = 0; j < result.Length; j++)
                {
                    if (permutation[i, j] > 0)
                    {
                        targetIndex = j;
                        break;
                    }
                }

                if (targetIndex == -1)
                {
                    throw new InvalidOperationException("It is not permutation matrix");
                }

                result[i] = targetIndex;
            }

            return result;
        }

        private float[] GetBasisVector(int index, int size)
        {
            var e = new float[size];
            Array.Fill(e, 0.0f);
            e[index] = 1f;
            return e;
        }
    }
}