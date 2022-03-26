// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet136
{
    public int SingleNumber(int[] nums)
    {
        var result = nums[0];
        for (var i = 1; i < nums.Length; i++)
        {
            result ^= nums[i];
        }

        return result;
    }
}