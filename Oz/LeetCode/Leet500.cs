// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet500
{
    public int LengthOfLIS(int[] nums)
    {
        var length = nums.Length;
        if (length == 0)
        {
            return 0;
        }

        var result = 1;
        var dp = new int[length];
        dp[0] = 1;

        for (var i = 1; i < length; i++)
        {
            var local = 1;
            for (var j = 0; j < i; j++)
            {
                if (nums[j] < nums[i])
                {
                    local = Math.Max(local, dp[j] + 1);
                }
            }

            result = Math.Max(result, local);
            dp[i] = local;
        }

        return result;
    }
}