// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;

public class Leet40
{
    private readonly IList<IList<int>> res = new List<IList<int>>();

    public IList<IList<int>> CombinationSum2(int[] candidates, int target)
    {
        if (candidates == null || candidates.Length == 0)
        {
            return res;
        }

        Array.Sort(candidates);
        Backtrack(candidates, 0, new List<int>(), 0, target);

        return res;
    }

    private void Backtrack(int[] arr, int i, List<int> cur, int sum, int n)
    {
        for (var j = i; j < arr.Length; j++)
        {
            if (j - 1 >= i && arr[j - 1] == arr[j])
            {
                continue;
            }

            cur.Add(arr[j]);

            if (sum + arr[j] == n)
            {
                res.Add(new List<int>(cur));
            }
            else if (sum + arr[j] < n)
            {
                Backtrack(arr, j + 1, cur, sum + arr[j], n);
            }

            cur.RemoveAt(cur.Count - 1);
        }
    }
}