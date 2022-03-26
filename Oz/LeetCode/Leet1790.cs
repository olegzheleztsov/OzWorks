// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet1790
{
    public bool AreAlmostEqual(string s1, string s2)
    {
        if(s1 == null && s2 == null)
            return true;
        else if(s1 == null || s2 == null)
            return false;

        List<int> diff = new List<int>();

        for (int i = 0; i < s1.Length; i++)
        {
            if (s1[i] != s2[i])
            {
                diff.Add(i);
                if (diff.Count > 2)
                {
                    return false;
                }
            }
        }

        return diff.Count == 0 || diff.Count == 2 && s1[diff[0]] == s2[diff[1]] && s1[diff[1]] == s2[diff[0]];
    }
}