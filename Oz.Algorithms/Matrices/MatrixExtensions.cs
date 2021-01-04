using System;
using System.Collections.Generic;
using System.Linq;

namespace Oz.Algorithms.Matrices
{
    public static class MatrixExtensions
    {
        public static int[] GetMatrixCollectionDimensions(this IEnumerable<FloatMatrix> matrices)
        {
            if (matrices == null)
            {
                throw new ArgumentException("Matrices can't be null");
            }
            return GetMatrixCollectionDimensions(matrices.Cast<MatrixBase<float>>().ToList());
        }
        public static int[] GetMatrixCollectionDimensions<T>(this List<MatrixBase<T>> matrices)
        {
            if (matrices == null)
            {
                throw new ArgumentException("Matrices can't be null");
            }

            if (matrices.Count < 2)
            {
                throw new ArgumentException("Matrix count can't be less than 2");
            }

            var dimensions = new int[matrices.Count + 1];
            for (var i = 0; i < matrices.Count; i++)
            {
                if (i == 0)
                {
                    dimensions[0] = matrices[0].Rows;
                }

                if (i > 0)
                {
                    if (matrices[i - 1].Columns != matrices[i].Rows)
                    {
                        throw new ArgumentException(
                            $"Invalid matrices dimensions. Matrix: {i - 1} has {matrices[i - 1].Columns} columns, Matrix: {i} has {matrices[i].Rows}");
                    }
                }

                dimensions[i + 1] = matrices[i].Columns;
            }

            return dimensions;
        }
    }
}