// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

/*
 * 1283. Find the Smallest Divisor Given a Threshold
 * Given an array of integers nums and an integer threshold, we will choose a positive integer divisor, divide all the array by it, and sum the division's result. Find the smallest divisor such that the result mentioned above is less than or equal to threshold.

Each result of the division is rounded to the nearest integer greater than or equal to that element. (For example: 7/3 = 3 and 10/2 = 5).

The test cases are generated so that there will be an answer.
 */
public class _1283
{
    public int SmallestDivisor(int[] nums, int threshold)
    {
        int start = 1;
        int end = int.MaxValue;

        while (start < end)
        {
            int mid = start + (end - start) / 2;
            if (IsValid(nums, threshold, mid))
            {
                end = mid;
            }
            else
            {
                start = mid + 1;
            }
        }

        return start;
    }

    private bool IsValid(int[] nums, int threshold, int divisor)
    {
        int sum = 0;
        foreach (var num in nums)
        {
            sum += (int)Math.Ceiling((double)num / divisor);
        }

        return sum <= threshold;
    }
}