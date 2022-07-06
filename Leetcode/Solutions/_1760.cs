// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _1760
{
    public int MinimumSize(int[] nums, int maxOperations)
    {
        var left = 1;
        var right = int.MaxValue;
        var answer = 0;

        while (left <= right)
        {
            var mid = left + ((right - left) / 2);
            if (IsPossible(mid, nums, maxOperations))
            {
                answer = mid;
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }

        return answer;
    }

    private static bool IsPossible(int mid, int[] nums, int ops)
    {
        foreach (var num in nums)
        {
            if (num % mid == 0)
            {
                ops -= (num / mid) - 1;
            }
            else
            {
                ops -= num / mid;
            }
        }

        return ops >= 0;
    }
}