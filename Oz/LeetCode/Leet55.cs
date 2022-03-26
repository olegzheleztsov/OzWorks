// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet55
{
    public bool CanJump(int[] nums)
    {
        if (nums.Length == 1)
        {
            return true;
        }

        var dp = new int[nums.Length];
        dp[0] = nums[0] > 0 ? 1 : 0;

        for (var i = 1; i < nums.Length; i++)
        {
            if (dp[i - 1] == 0)
            {
                return false;
            }

            if (nums[i - 1] == 0)
            {
                continue;
            }

            for (var k = 1; k <= nums[i - 1]; k++)
            {
                if (i - 1 + k < nums.Length)
                {
                    dp[i - 1 + k]++;
                }
            }
        }

        return dp[nums.Length - 1] > 0;
    }

    public bool CanJumpGreedy(int[] nums)
    {
        var farthest = nums[0];

        for (var i = 0; i < nums.Length; i++)
        {
            if (i > farthest)
            {
                return false;
            }

            farthest = Math.Max(farthest, i + nums[i]);
            if (farthest > nums.Length)
            {
                return true;
            }
        }

        return true;
    }
}