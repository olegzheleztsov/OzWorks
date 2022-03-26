// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet189
{
    public void Rotate(int[] nums, int k) {
        
        if (k == 0)
        {
            return;
        }

        k %=  nums.Length;
        Reverse(nums, 0, nums.Length-1);
        Reverse(nums, 0, k-1);
        Reverse(nums, k, nums.Length-1);
    }

    private void Reverse(int[] nums, int lower, int upper)
    {
        for (int i = lower, j = upper; i < j; i++, j--)
        {
            (nums[i], nums[j]) = (nums[j], nums[i]);
        }
    }
}