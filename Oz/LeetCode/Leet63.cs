// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet63
{
    public int UniquePathsWithObstacles(int[][] obstacleGrid)
    {
        var rows = obstacleGrid.Length;
        var cols = obstacleGrid[0].Length;
        var dp = new int[rows, cols];

        if (obstacleGrid[0][0] == 1)
        {
            return 0;
        }

        dp[0, 0] = 1;

        for (var i = 1; i < rows; i++)
        {
            if (obstacleGrid[i][0] == 1)
            {
                dp[i, 0] = 0;
            }
            else
            {
                dp[i, 0] = dp[i - 1, 0];
            }
        }

        for (var j = 1; j < cols; j++)
        {
            if (obstacleGrid[0][j] == 1)
            {
                dp[0, j] = 0;
            }
            else
            {
                dp[0, j] = dp[0, j - 1];
            }
        }

        for (var i = 1; i < rows; i++)
        {
            for (var j = 1; j < cols; j++)
            {
                if (obstacleGrid[i][j] == 1)
                {
                    dp[i, j] = 0;
                }
                else
                {
                    dp[i, j] = dp[i, j - 1] + dp[i - 1, j];
                }
            }
        }

        return dp[rows - 1, cols - 1];
    }
}