// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.LeetCode;

/// <summary>
///     https://leetcode.com/problems/smallest-range-ii/
/// </summary>
public class Leet_910
{
    public int SmallestRangeII(int[] nums, int k)
    {
        Array.Sort(nums);
        var answer = nums[^1] - nums[0];

        for (var i = 0; i < nums.Length - 1; i++)
        {
            var current = nums[i];
            var next = nums[i + 1];
            var high = Math.Max(nums[^1] - k, current + k);
            var low = Math.Min(nums[0] + k, next - k);
            answer = Math.Min(answer, high - low);
        }

        return answer;
    }
}