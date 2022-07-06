// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

/*
 * 1482. Minimum Number of Days to Make m Bouquets
 * You are given an integer array bloomDay, an integer m and an integer k.

You want to make m bouquets. To make a bouquet, you need to use k adjacent flowers from the garden.

The garden consists of n flowers, the ith flower will bloom in the bloomDay[i] and then can be used in exactly one bouquet.

Return the minimum number of days you need to wait to be able to make m bouquets from the garden. If it is impossible to make m bouquets return -1.
 */
public class _1482
{
    public int MinDays(int[] bloomDay, int m, int k)
    {
        var flowersRequired = m * k;
        var flowersAvailable = bloomDay.Length;

        if (flowersAvailable < flowersRequired)
        {
            return -1;
        }

        var min = int.MaxValue;
        var max = int.MinValue;

        foreach (var flower in bloomDay)
        {
            min = Math.Min(min, flower);
            max = Math.Max(max, flower);
        }

        var low = min;
        var high = max;

        while (low < high)
        {
            var mid = low + ((high - low) / 2);
            var currentBloomed = 0;
            var bouquets = 0;

            foreach (var flower in bloomDay)
            {
                if (flower <= mid)
                {
                    currentBloomed++;
                }
                else
                {
                    currentBloomed = 0;
                }

                if (currentBloomed == k)
                {
                    currentBloomed = 0;
                    bouquets++;
                }
            }

            if (bouquets >= m)
            {
                high = mid;
            }
            else
            {
                low = mid + 1;
            }
        }

        return low;
    }
}