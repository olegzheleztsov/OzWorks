// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet1567
{
    public int GetMaxLen(int[] nums)
    {
        var result = 0;
        var pos = 0;
        var neg = 0;

        for (var i = 0; i < nums.Length; i++)
        {
            if (nums[i] == 0)
            {
                pos = 0;
                neg = 0;
            }
            else if (nums[i] > 0)
            {
                pos++;
                neg = neg == 0 ? 0 : neg + 1;
            }
            else
            {
                var tmp = pos;
                pos = neg == 0 ? 0 : neg + 1;
                neg = tmp + 1;
            }

            result = Math.Max(pos, result);
        }

        return result;
    }

    public static void Test()
    {
        int[] arr = {1, -2, -3, 4};
        new Leet1567().GetMaxLen(arr);
    }
}