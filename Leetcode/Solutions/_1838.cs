// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

/*
 * 1838. Frequency of the Most Frequent Element
 * The frequency of an element is the number of times it occurs in an array.

You are given an integer array nums and an integer k. In one operation, you can choose an index of nums and increment the element at that index by 1.

Return the maximum possible frequency of an element after performing at most k operations.
 */
public class _1838
{
    public int MaxFrequency(int[] nums, int k)
    {
        Array.Sort(nums);
        var output = 0;
        var left = 0;
        var sum = 0;

        for (var right = 0; right < nums.Length; right++)
        {
            sum += nums[right];
            while ((nums[right] * (right - left + 1)) - sum > k)
            {
                sum -= nums[left];
                left++;
            }

            output = Math.Max(output, right - left + 1);
        }

        return output;
    }
}