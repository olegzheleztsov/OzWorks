// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _875
{
    public int MinEatingSpeed(int[] piles, int h)
    {
        var left = 1;
        var right = 1000000000;
        while (left < right)
        {
            var mid = left + ((right - left) / 2);
            var hourSum = 0;
            foreach (var pile in piles)
            {
                hourSum += (pile + mid - 1) / mid;
            }

            if (hourSum > h)
            {
                left = mid + 1;
            }
            else
            {
                right = mid;
            }
        }

        return left;
    }
}