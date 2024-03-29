﻿// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _1696
{
    public int MaxResult(int[] nums, int k)
    {
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        var scores = new int[nums.Length];
        var monoQueue = new LinkedList<int>();

        scores[0] = nums[0];
        monoQueue.AddLast(0);

        for (var i = 1; i < nums.Length; i++)
        {
            while (monoQueue.First != null && monoQueue.Count > 0 && monoQueue.First.Value < i - k)
            {
                monoQueue.RemoveFirst();
            }

            if (monoQueue.First != null)
            {
                scores[i] = nums[i] + scores[monoQueue.First.Value];
            }

            while (monoQueue.Last != null && monoQueue.Count > 0 && scores[monoQueue.Last.Value] < scores[i])
            {
                monoQueue.RemoveLast();
            }

            monoQueue.AddLast(i);
        }

        return scores[nums.Length - 1];
    }
}