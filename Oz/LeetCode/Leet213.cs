// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet213
{
    public int Rob(int[] nums)
    {
        if (nums.Length == 1)
        {
            return nums[0];
        }

        if (nums.Length == 2)
        {
            return Math.Max(nums[0], nums[1]);
        }

        var days1 = new List<int>(nums);
        days1.RemoveAt(0);
        var val1 = RobInner(days1);

        var days2 = new List<int>(nums);
        days2.RemoveAt(nums.Length - 1);
        var val2 = RobInner(days2);
        return Math.Max(val1, val2);
    }

    private int RobInner(List<int> days)
    {
        var dp = new int[days.Count];
        dp[0] = days[0];
        dp[1] = Math.Max(days[0], days[1]);
        for (var i = 2; i < days.Count; i++)
        {
            dp[i] = Math.Max(dp[i - 1], dp[i - 2] + days[i]);
        }

        return dp[days.Count - 1];
    }
}