// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet18
{
    public IList<IList<int>> FourSum(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0)
        {
            return new List<IList<int>>();
        }

        var result = new List<IList<int>>();
        Array.Sort(nums);

        for (var i = 0; i < nums.Length - 3; i++)
        {
            if (i > 0 && nums[i - 1] == nums[i])
            {
                continue;
            }

            for (var j = i + 1; j < nums.Length - 2; j++)
            {
                if (j > i + 1 && nums[j - 1] == nums[j])
                {
                    continue;
                }

                var k = j + 1;
                var l = nums.Length - 1;

                while (k < l)
                {
                    if (k > j + 1 && nums[k - 1] == nums[k])
                    {
                        k++;
                        continue;
                    }

                    if (l < nums.Length - 1 && nums[l] == nums[l + 1])
                    {
                        l--;
                        continue;
                    }

                    var sum = nums[i] + nums[j] + nums[k] + nums[l];
                    if (sum == target)
                    {
                        result.Add(new List<int> {nums[i], nums[j], nums[k], nums[l]});
                        k++;
                        l--;
                    }
                    else if (sum > target)
                    {
                        l--;
                    }
                    else
                    {
                        k++;
                    }
                }
            }
        }

        return result;
    }
}