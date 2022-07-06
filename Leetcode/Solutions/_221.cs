// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _221
{
    public int MaximalSquare(char[][] matrix)
    {
        int rows = matrix.Length;
        int columns = matrix[0].Length;
        int[,] dp = new int[rows, columns];

        dp[0, 0] = matrix[0][0] == '1' ? 1 : 0;

        int global = dp[0, 0];
        
        for (int i = 1; i < rows; i++)
        {
            dp[i, 0] = matrix[i][0] == '1' ? 1 : 0;
            global = Math.Max(global, dp[i, 0]);
        }

        for (int j = 1; j < columns; j++)
        {
            dp[0, j] = matrix[0][j] == '1' ? 1 : 0;
            global = Math.Max(global, dp[0, j]);
        }

        for (int i = 1; i < rows; i++)
        {
            for (int j = 1; j < columns; j++)
            {
                if (matrix[i][j] == '1')
                {
                    dp[i, j] = Math.Min(dp[i - 1, j], Math.Min(dp[i - 1, j - 1], dp[i, j - 1])) + 1;
                    global = Math.Max(global, dp[i, j]);
                }
            }
        }

        return global * global;
    }
}