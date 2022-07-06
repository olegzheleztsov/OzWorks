// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

/*
 * 287. Find the Duplicate Number
 * Given an array of integers nums containing n + 1 integers where each integer is in the range [1, n] inclusive.
 * There is only one repeated number in nums, return this repeated number.
 * You must solve the problem without modifying the array nums and uses only constant extra space.
    Input: nums = [1,3,4,2,2]
    Output: 2
 */
public class _287
{
    public int FindDuplicate(int[] nums)
    {
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        var n = nums.Length - 1;
        var left = 1;
        var right = nums.Length;
        while (left < right)
        {
            var mid = left + ((right - left) / 2);
            var cnt = nums.Count(num => num <= mid);

            if (cnt <= mid)
            {
                left = mid + 1;
            }
            else
            {
                right = mid;
            }
        }

        return right;
    }
}