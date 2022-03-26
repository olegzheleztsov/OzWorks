// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Oz.Algorithms;
using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet120
{
    public int MinimumTotal(IList<IList<int>> triangle)
    {
        var size = triangle.Count;

        var dp = new int[size, size];
        dp[0, 0] = triangle[0][0];

        for (var i = 1; i < size; i++)
        {
            dp[i, 0] = dp[i - 1, 0] + triangle[i][0];
        }

        for (var r = 1; r < size; r++)
        {
            for (var j = 1; j <= r; j++)
            {
                if (j < r)
                {
                    dp[r, j] = Math.Min(dp[r - 1, j], dp[r - 1, j - 1]) + triangle[r][j];
                }
                else
                {
                    dp[r, j] = dp[r - 1, j - 1] + triangle[r][j];
                }
            }
        }

        var result = int.MaxValue;
        for (var i = 0; i < size; i++)
        {
            if (dp[size - 1, i] < result)
            {
                result = dp[size - 1, i];
            }
        }

        Console.WriteLine(dp.Str());

        return result;
    }
}