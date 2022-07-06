// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;

public class Leet39
{
    public IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        var results = new List<IList<int>>();
        Array.Sort(candidates);
        Combine(candidates, target, new List<int>(), results, 0);
        return results;
    }

    private void Combine(int[] candidates, int target, List<int> current, List<IList<int>> results, int start)
    {
        if (target == 0)
        {
            results.Add(new List<int>(current));
        }
        else
        {
            for (var i = start; i < candidates.Length; i++)
            {
                var newTarget = target - candidates[i];
                if (newTarget >= 0)
                {
                    current.Add(candidates[i]);
                    Combine(candidates, newTarget, current, results, i);
                    current.RemoveAt(current.Count - 1);
                }
            }
        }
    }
}