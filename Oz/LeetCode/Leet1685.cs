// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Linq;

namespace Oz.LeetCode;

public class Leet1685
{
    public int[] GetSumAbsoluteDifferences(int[] nums)
    {
        if (nums.Length == 0)
        {
            return nums;
        }

        var result = new int[nums.Length];
        var prefixSum = 0;
        var suffixSum = nums.Skip(1).Sum();
        result[0] = suffixSum - ((nums.Length - 1) * nums[0]);
        for (var i = 1; i < nums.Length; i++)
        {
            prefixSum += nums[i - 1];
            suffixSum -= nums[i];
            result[i] = (i * nums[i]) - prefixSum + (suffixSum - ((nums.Length - i - 1) * nums[i]));
        }

        return result;
    }
}