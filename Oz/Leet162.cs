// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;

public class Leet162
{
    public int FindPeakElement(int[] nums)
    {
        var left = 0;
        var right = nums.Length - 1;
        if (nums.Length == 1)
        {
            return 0;
        }

        while (left < right)
        {
            var mid = (left + right) / 2;
            if (IsPeek(nums, mid))
            {
                return mid;
            }

            if (mid == 0)
            {
                left = mid + 1;
            }
            else if (mid == nums.Length - 1)
            {
                right = mid - 1;
            }
            else
            {
                if (nums[mid] < nums[mid + 1])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            if (Math.Abs(right - left) == 1)
            {
                if (IsPeek(nums, left))
                {
                    return left;
                }

                if (IsPeek(nums, right))
                {
                    return right;
                }

                break;
            }
        }

        if (IsPeek(nums, left))
        {
            return left;
        }

        if (IsPeek(nums, right))
        {
            return right;
        }

        return -1;
    }

    public bool IsPeek(int[] nums, int index)
    {
        if (index == 0)
        {
            return nums[0] > nums[1];
        }

        if (index == nums.Length - 1)
        {
            return nums[nums.Length - 2] < nums[nums.Length - 1];
        }

        return nums[index - 1] < nums[index] && nums[index] > nums[index + 1];
    }
}