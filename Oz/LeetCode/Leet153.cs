// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet153
{
    public int FindMin(int[] nums) {
        if (nums.Length == 1)
        {
            return nums[0];
        }
        if (nums[0] < nums[^1])
        {
            return nums[0];
        }

        int leftIndex = 0;
        int rightIndex = nums.Length - 1;

        while (leftIndex < rightIndex)
        {
            int midIndex = (leftIndex + rightIndex) / 2;
            if (nums[leftIndex] < nums[midIndex])
            {
                leftIndex = midIndex;
            } else if (nums[rightIndex] > nums[midIndex])
            {
                rightIndex = midIndex;
            }

            if (Math.Abs(rightIndex - leftIndex) == 1)
            {
                break;
            }
        }

        return Math.Min(nums[leftIndex], nums[rightIndex]);
    }
}