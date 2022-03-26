// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet976
{
    public int LargestPerimeter(int[] nums)
    {
        Array.Sort(nums);
        for (var i = nums.Length - 1; i > 1; i--)
        {
            if (nums[i] < nums[i - 1] + nums[i - 2])
            {
                return nums[i] + nums[i - 1] + nums[i - 2];
            }
        }

        return 0;
    }
    
}