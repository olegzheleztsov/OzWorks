// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet304
{
    public class NumMatrix
    {
        private readonly int[,] _dp;
        private readonly int[][] _matrix;

        public NumMatrix(int[][] matrix)
        {
            _matrix = matrix;
            var rows = matrix.Length;
            var columns = matrix[0].Length;
            _dp = new int[rows, columns];

            _dp[0, 0] = _matrix[0][0];

            for (var i = 1; i < rows; i++)
            {
                _dp[i, 0] = _dp[i - 1, 0] + matrix[i][0];
            }

            for (var j = 1; j < columns; j++)
            {
                _dp[0, j] = _dp[0, j - 1] + matrix[0][j];
            }

            for (var i = 1; i < rows; i++)
            {
                for (var j = 1; j < columns; j++)
                {
                    _dp[i, j] = _dp[i - 1, j] + _dp[i, j - 1] - _dp[i - 1, j - 1] + matrix[i][j];
                }
            }
        }

        public int SumRegion(int row1, int col1, int row2, int col2)
        {
            int first = 0, second = 0, third = 0;
            if (col1 - 1 >= 0)
            {
                first = _dp[row2, col1 - 1];
            }

            if (row1 - 1 >= 0)
            {
                second = _dp[row1 - 1, col2];
            }

            if (row1 - 1 >= 0 && col1 - 1 >= 0)
            {
                third = _dp[row1 - 1, col1 - 1];
            }

            return _dp[row2, col2] - first - second + third;
        }
    }
}