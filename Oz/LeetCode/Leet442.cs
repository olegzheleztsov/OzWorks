// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet442
{
    public IList<int> FindDuplicates(int[] nums)
    {
        var duplicates = new List<int>();
        for (var i = 0; i < nums.Length; i++)
        {
            var index = Math.Abs(nums[i]) - 1;
            if (nums[index] < 0)
            {
                duplicates.Add(Math.Abs(nums[i]));
            }
            else
            {
                nums[index] = -nums[index];
            }
        }

        return duplicates;
    }
}