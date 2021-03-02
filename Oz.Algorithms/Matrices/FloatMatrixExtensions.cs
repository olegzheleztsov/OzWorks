using System.Collections.Generic;

namespace Oz.Algorithms.Matrices
{
    public static class FloatMatrixExtensions
    {
        /// <summary>
        ///     Multiply collection of matrices by optimal way
        /// </summary>
        /// <param name="matrices">Collection of matrices to multiply</param>
        /// <param name="dynamicProcedureKind">Kind of dyn. procedure to be used</param>
        /// <returns>Result of multiplication</returns>
        public static FloatMatrix MultiplyByOptimalWay(this List<FloatMatrix> matrices,
            DynamicProcedureKind dynamicProcedureKind = DynamicProcedureKind.BottomUp)
        {
            var matrixChain = MatrixChainOrderFactory.Create(dynamicProcedureKind);
            var (costs, sequence) = matrixChain.Find(matrices);
            return Multiply(sequence, 1, matrices.Count, matrices);
        }

        /// <summary>
        ///     Returns string that represent optimal braces for matrix multiplication
        /// </summary>
        /// <param name="matrices">Matrices to be multiplied</param>
        /// <param name="dynamicProcedureKind">Kind of dyn. procedure to be used</param>
        /// <returns>String of ordered matrix multiplication</returns>
        public static string GetOptimalMultiplyMatrixString(this List<FloatMatrix> matrices,
            DynamicProcedureKind dynamicProcedureKind = DynamicProcedureKind.BottomUp)
        {
            var matrixChain = MatrixChainOrderFactory.Create(dynamicProcedureKind);
            var (_, sequence) = matrixChain.Find(matrices);
            var output = new List<string>();
            ComputeOptimalMatrixMultiplySequence(sequence, 1, matrices.Count, output);
            return string.Join(" ", output);
        }

        private static void ComputeOptimalMatrixMultiplySequence(int[,] sequence, int low, int high,
            ICollection<string> output)
        {
            if (low == high)
            {
                output.Add(low.ToString());
            }
            else
            {
                output.Add("(");
                ComputeOptimalMatrixMultiplySequence(sequence, low, sequence[low, high], output);
                ComputeOptimalMatrixMultiplySequence(sequence, sequence[low, high] + 1, high, output);
                output.Add(")");
            }
        }

        private static FloatMatrix Multiply(int[,] sequence, int low, int high, IList<FloatMatrix> matrices)
        {
            if (low == high)
            {
                return matrices[low - 1];
            }

            return Multiply(sequence, low, sequence[low, high], matrices)
                   * Multiply(sequence, sequence[low, high] + 1, high, matrices);
        }

        public static float[,] ConvertToFloatArray2D(this byte[,] matrix)
        {
            var rows = matrix.GetUpperBound(0) + 1;
            var columns = matrix.GetUpperBound(1) + 1;
            var result = new float[rows, columns];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    result[i, j] = matrix[i, j];
                }
            }

            return result;
        }

        public static int[,] ConvertToIntegerArray2D(this byte[,] matrix)
        {
            var rows = matrix.GetUpperBound(0) + 1;
            var columns = matrix.GetUpperBound(1) + 1;
            var result = new int[rows, columns];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    result[i, j] = matrix[i, j];
                }
            }

            return result;
        }

        public static int[,] ConvertToIntegerArray2D(this float[,] matrix)
        {
            var rows = matrix.GetUpperBound(0) + 1;
            var columns = matrix.GetUpperBound(1) + 1;
            var result = new int[rows, columns];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    result[i, j] = (int)matrix[i, j];
                }
            }

            return result;
        }
    }
}