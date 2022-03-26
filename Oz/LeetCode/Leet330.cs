// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet330
{
    public int MinPatches(int[] nums, int n)
    {
        long miss = 1;
        var count = 0;
        var i = 0;
        while (miss <= n)
        {
            if (i < nums.Length && nums[i] <= miss)
            {
                miss += nums[i];
                i++;
            }
            else
            {
                miss += miss;
                count++;
            }
        }

        return count;
    }
}