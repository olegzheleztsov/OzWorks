// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet45
{
    public int Jump(int[] nums)
    {
        var dp = new int[nums.Length];

        if (nums.Length == 1)
        {
            return 1;
        }

        for (var i = 1; i <= nums.Length; i++)
        {
            dp[i - 1] = i;
        }

        for (var i = 0; i < nums.Length; i++)
        {
            for (var k = 1; k <= nums[i]; k++)
            {
                if (i + k < nums.Length)
                {
                    dp[i + k] = Math.Min(dp[i + k], dp[i] + 1);
                }
            }
        }

        return dp[nums.Length - 1];
    }
}