﻿// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _611
{
    public int TriangleNumber(int[] nums) {
     
        if(nums == null || nums.Length < 3)
            return 0;
        
        int res = 0;
        // O(nlogn)
        Array.Sort(nums);
        
        // O(n^2)
        for(int k = nums.Length - 1; k >= 2; k--)
        {
            int left = 0, right = k - 1;
            while(left < right)
            {
                if(nums[left] + nums[right] > nums[k])
                {
                    res += right - left;
                    right--;
                }
                else
                {
                    left++;
                }
            }
        }
        
        return res;
    }
}