// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet198
{
    public int Rob(int[] nums)
    {
        switch (nums.Length)
        {
            case 0:
                return 0;
            case 1:
                return nums[0];
            case 2:
                return Math.Max(nums[0], nums[1]);
        }

        var dp = new int[nums.Length];
        dp[0] = nums[0];
        dp[1] = Math.Max(nums[0], nums[1]);

        for (var i = 2; i < nums.Length; i++)
        {
            dp[i] = Math.Max(dp[i - 1], dp[i - 2] + nums[i]);
        }

        return dp[nums.Length - 1];
    }
}