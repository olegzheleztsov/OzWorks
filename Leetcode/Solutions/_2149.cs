// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _2149
{
    public int[] RearrangeArray(int[] nums)
    {
        var negatives = new int[nums.Length / 2];
        var positives = new int[nums.Length / 2];
        var bigIndex = 0;
        var negativeIndex = 0;
        var positiveIndex = 0;

        while (bigIndex < nums.Length)
        {
            if (nums[bigIndex] < 0)
            {
                negatives[negativeIndex++] = nums[bigIndex];
            }
            else
            {
                positives[positiveIndex++] = nums[bigIndex];
            }

            bigIndex++;
        }

        bigIndex = 0;
        for (var i = 0; i < nums.Length / 2; i++)
        {
            nums[bigIndex++] = positives[i];
            nums[bigIndex++] = negatives[i];
        }

        return nums;
    }
}