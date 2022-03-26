// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet350
{
    public int[] Intersect(int[] nums1, int[] nums2)
    {
        Dictionary<int, int> frequences = new Dictionary<int, int>();

        foreach (var num in nums1)
        {
            if (frequences.ContainsKey(num))
            {
                frequences[num]++;
            }
            else
            {
                frequences.Add(num, 1);
            }
        }


        List<int> result = new List<int>();
        foreach (var num in nums2)
        {
            if (frequences.ContainsKey(num) && frequences[num] > 0)
            {
                result.Add(num);
                frequences[num]--;
            }
        }

        return result.ToArray();
    }
}