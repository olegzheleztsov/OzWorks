// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet1
{
    public int[] TwoSum(int[] nums, int target)
    {
        var memo = new Dictionary<int, int>();

        for (var i = 0; i < nums.Length; i++)
        {
            if (memo.ContainsKey(nums[i]))
            {
                return new[] {memo[nums[i]], i};
            }

            if (!memo.ContainsKey(target - nums[i]))
            {
                memo.Add(target - nums[i], i);
            }
        }

        throw new Exception();
    }
}