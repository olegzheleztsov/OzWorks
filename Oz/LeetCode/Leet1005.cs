// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Linq;

namespace Oz.LeetCode;

public class Leet1005
{
    public void Test()
    {
        int[] nums = {2, -3, -1, 5, -4};
        var k = 2;
        var result = LargestSumAfterKNegations(nums, k);
        Console.WriteLine(result);
    }

    public int LargestSumAfterKNegations(int[] nums, int k)
    {
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        nums = nums.OrderBy(Math.Abs).ToArray();

        k %= nums.Length * 2;

        var count = 0;

        for (var i = nums.Length - 1; i >= 0; i--)
        {
            if (nums[i] < 0)
            {
                nums[i] = -nums[i];
                count++;
                if (count >= k)
                {
                    break;
                }
            }
        }

        var remain = k - count;
        if (remain > 0 && remain % 2 == 1)
        {
            var minAbsIndex = -1;
            var minAbsVal = int.MaxValue;
            for (var i = 0; i < nums.Length; i++)
            {
                if (Math.Abs(nums[i]) < minAbsVal)
                {
                    minAbsVal = Math.Abs(nums[i]);
                    minAbsIndex = i;
                }
            }

            if (minAbsIndex >= 0)
            {
                nums[minAbsIndex] = -nums[minAbsIndex];
            }
        }

        return nums.Sum();
    }
}