// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Linq;

public class Leet713
{
    public int NumSubarrayProductLessThanK(int[] nums, int k)
    {
        var start = 0;
        var end = 0;
        var mult = 1 * nums[end];
        var opt = new int[nums.Length];
        while (end < nums.Length)
        {
            while (start <= end)
            {
                if (mult >= k)
                {
                    mult /= nums[start];
                    start++;
                }
                else
                {
                    break;
                }
            }

            if (start > end)
            {
                opt[end] = 0;
                end++;
                start = end;
                if (end < nums.Length)
                {
                    mult = nums[end];
                }
            }
            else
            {
                opt[end] = end - start + 1;
                end++;
                if (end < nums.Length)
                {
                    mult *= nums[end];
                }
            }
        }

        return opt.Sum();
    }
}