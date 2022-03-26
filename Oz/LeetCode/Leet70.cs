// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet70
{
    public int ClimbStairs(int n)
    {
        if (n == 0)
        {
            return 0;
        }

        if (n == 1)
        {
            return 1;
        }

        if (n == 2)
        {
            return 2;
        }

        var dp = new int[n + 1];
        dp[0] = 0;
        dp[1] = 1;
        dp[2] = 2;

        for (var i = 3; i <= n; i++)
        {
            dp[i] = dp[i - 1] + dp[i - 2];
        }

        return dp[n];
    }
}