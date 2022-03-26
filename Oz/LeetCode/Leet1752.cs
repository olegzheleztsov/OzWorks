// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet1752
{
    /*
     * Given an array nums, return true if the array was originally sorted in non-decreasing order, then rotated some number of positions (including zero). Otherwise, return false.
     * There may be duplicates in the original array.
     * Note: An array A rotated by x positions results in an array B of the same length such that A[i] == B[(i+x) % A.length], where % is the modulo operation.
     */
    public bool Check(int[] nums)
    {
        var rotIndex = -1;

        for (var i = 0; i < nums.Length - 1; i++)
        {
            if (nums[i] <= nums[i + 1])
            {
                continue;
            }

            if (rotIndex < 0)
            {
                rotIndex = i + 1;
            }
            else
            {
                return false;
            }
        }

        if (rotIndex < 0)
        {
            return true;
        }

        return nums[nums.Length - 1] <= nums[0];
    }
}