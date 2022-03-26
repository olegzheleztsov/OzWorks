// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet740
{
    public int DeleteAndEarn(int[] nums)
    {
        int[] arr = new int[10001], dp = new int[10001];

        foreach (var num in nums)
        {
            arr[num]++;
        }

        dp[1] = arr[1];

        for (var i = 2; i < 10001; i++)
        {
            dp[i] = Math.Max(dp[i - 1], dp[i - 2] + (arr[i] * i));
        }

        return dp[10000];
    }
}