// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;

public class Leet47
{
    public IList<IList<int>> PermuteUnique(int[] nums)
    {
        var results = new List<IList<int>>();
        Array.Sort(nums);
        var used = new bool[nums.Length];
        Backtracking(nums, new List<int>(), results, used);
        return results;
    }

    private void Backtracking(int[] nums, List<int> list, List<IList<int>> results, bool[] used)
    {
        if (list.Count == nums.Length)
        {
            results.Add(new List<int>(list));
            return;
        }

        for (var i = 0; i < nums.Length; i++)
        {
            if ((i > 0 && nums[i] == nums[i - 1] && used[i - 1]) || used[i])
            {
                continue;
            }

            list.Add(nums[i]);
            used[i] = true;
            Backtracking(nums, list, results, used);
            list.RemoveAt(list.Count - 1);
            used[i] = false;
        }
    }
}