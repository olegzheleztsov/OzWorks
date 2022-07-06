// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;

public class Leet15
{
    public IList<IList<int>> ThreeSum(int[] nums)
    {
        var res = new List<IList<int>>();
        if (nums == null || nums.Length < 3)
        {
            return res;
        }

        Array.Sort(nums);

        for (var i = 0; i < nums.Length - 2; i++)
        {
            if (nums[i] > 0 || (i > 0 && nums[i] == nums[i - 1]))
            {
                continue;
            }

            var left = i + 1;
            var right = nums.Length - 1;

            while (left < right)
            {
                if (nums[i] + nums[left] + nums[right] == 0)
                {
                    res.Add(new List<int> {nums[i], nums[left], nums[right]});
                    left++;
                    right--;

                    while (left < right && nums[left] == nums[left - 1])
                    {
                        left++;
                    }

                    while (left < right && nums[right] == nums[right + 1])
                    {
                        right--;
                    }
                }
                else if (nums[i] + nums[left] + nums[right] > 0)
                {
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