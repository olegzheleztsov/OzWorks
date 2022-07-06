// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _64
{
    public int MinPathSum(int[][] grid)
    {
        int rows = grid.Length;
        int columns = grid[0].Length;

        int[,] dp = new int[rows, columns];
        dp[0, 0] = grid[0][0];

        for (int i = 1; i < rows; i++)
        {
            dp[i, 0] = dp[i - 1, 0] + grid[i][0];
        }

        for (int j = 1; j < columns; j++)
        {
            dp[0, j] = dp[0, j - 1] + grid[0][j];
        }

        for (int i = 1; i < rows; i++)
        {
            for (int j = 1; j < columns; j++)
            {
                dp[i, j] = Math.Min(dp[i - 1, j], dp[i, j - 1]) + grid[i][j];
            }
        }

        return dp[rows - 1, columns - 1];
    }
}