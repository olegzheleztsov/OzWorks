// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _81
{
    public bool Search(int[] nums, int target)
    {
        var n = nums.Length;

        if (n == 0)
        {
            return false;
        }

        var left = 0;
        var right = n - 1;

        while (left < right)
        {
            var mid = left + ((right - left) / 2);

            if (nums[mid] == target)
            {
                return true;
            }

            if (nums[mid] < nums[right])
            {
                if (nums[mid] < target && target <= nums[right])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }
            else if (nums[mid] == nums[right])
            {
                right--;
            }
            else
            {
                if (nums[left] <= target && target < nums[mid])
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }
        }

        return nums[left] == target;
    }
}