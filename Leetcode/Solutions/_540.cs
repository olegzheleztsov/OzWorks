// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _540
{
    public int SingleNonDuplicate(int[] nums)
    {
        if (nums.Length == 1)
        {
            return nums[0];
        }

        var l = 0;
        var r = nums.Length - 1;
        while (l <= r)
        {
            if (r == l)
            {
                return nums[l];
            }

            var mid = l + ((r - l) / 2);
            if (nums[mid] == nums[mid + 1])
            {
                if (((mid - l) & 1) == 1)
                {
                    r = mid - 1;
                }
                else
                {
                    l = mid;
                }
            }
            else
            {
                if (((mid - l) & 1) == 1)
                {
                    l = mid + 1;
                }
                else
                {
                    r = mid;
                }
            }
        }

        return -1;
    }
}