// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet413
{
    public int NumberOfArithmeticSlices(int[] nums)
    {
        var dp = new int[nums.Length];
        var res = 0;

        for (var i = 2; i < nums.Length; i++)
        {
            if (nums[i] - nums[i - 1] == nums[i - 1] - nums[i - 2])
            {
                dp[i] = 1 + dp[i - 1];
                res += dp[i];
            }
        }

        return res;
    }
}