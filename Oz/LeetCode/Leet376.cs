// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Linq;

namespace Oz.LeetCode;

public class Leet376
{
    public int WiggleMaxLength(int[] nums)
    {
        if (nums == null || !nums.Any())
        {
            return 0;
        }

        if (nums.Length == 1)
        {
            return 1;
        }

        var diff = new int[nums.Length - 1];
        for (var i = 1; i < nums.Length; i++)
        {
            diff[i - 1] = nums[i] - nums[i - 1];
        }

        var current = diff[0];
        var max = current == 0 ? 0 : 1;

        for (var j = 1; j < diff.Length; j++)
        {
            if (diff[j] == 0)
            {
                continue;
            }

            if (Math.Sign(current) != Math.Sign(diff[j]))
            {
                max++;
                current = diff[j];
            }
        }

        return max + 1;
    }
}