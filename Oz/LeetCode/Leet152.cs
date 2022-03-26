// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Linq;

namespace Oz.LeetCode;

public class Leet152
{
    public int MaxProduct(int[] nums)
    {
        if (nums.Length == 0)
        {
            return 0;
        }

        if (nums.Length == 1)
        {
            return nums[0];
        }

        var minArr = new int[nums.Length];
        var maxArr = new int[nums.Length];

        minArr[0] = maxArr[0] = nums[0];

        for (var i = 1; i < nums.Length; i++)
        {
            maxArr[i] = Math.Max(Math.Max(maxArr[i - 1] * nums[i], minArr[i - 1] * nums[i]), nums[i]);
            minArr[i] = Math.Min(Math.Min(minArr[i - 1] * nums[i], maxArr[i - 1] * nums[i]), nums[i]);
        }

        return maxArr.Max();
    }

    public static void Test()
    {
        int[] arr = {-4, -3};
        var leet152 = new Leet152();
        leet152.MaxProduct(arr);
    }
}