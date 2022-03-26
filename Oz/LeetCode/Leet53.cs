// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet53
{
    public int MaxSubArray(int[] nums)
    {
        if (nums.Length == 1)
        {
            return nums[0];
        }

        var maxSum = nums[0];
        var curSum = maxSum;
        for (var i = 1; i < nums.Length; i++)
        {
            curSum = Math.Max(curSum + nums[i], nums[i]);
            if (curSum > maxSum)
            {
                maxSum = curSum;
            }
        }

        return maxSum;
    }

    public static void Test()
    {
        int[] arr = {-2, 1, -3, 4, -1, 2, 1, -5, 4};
        var leet = new Leet53();
        var result = leet.MaxSubArray(arr);
    }
}