// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet567
{
    public bool CheckInclusion(string s1, string s2)
    {
        var permutation = new Dictionary<char, int>();
        var s2Map = new Dictionary<char, int>();

        foreach (var c in s1)
        {
            if (permutation.ContainsKey(c))
            {
                permutation[c]++;
            }
            else
            {
                permutation.Add(c, 1);
            }
        }

        var startSliding = 0;
        var endSliding = 0;
        while (endSliding < s2.Length)
        {
            if (!s2Map.ContainsKey(s2[endSliding]))
            {
                s2Map.Add(s2[endSliding], 1);
            }
            else
            {
                s2Map[s2[endSliding]]++;
            }

            if (endSliding - startSliding + 1 == s1.Length)
            {
                if (Match(permutation, s2Map))
                {
                    return true;
                }

                s2Map[s2[startSliding]]--;
                startSliding++;
            }

            endSliding++;
        }

        return false;
    }

    private bool Match(Dictionary<char, int> s1Map, Dictionary<char, int> s2Map)
    {
        foreach (var (c, count) in s1Map)
        {
            if (!s2Map.ContainsKey(c) || s2Map[c] != count)
            {
                return false;
            }
        }

        return true;
    }
}