// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _1898
{
    public int MaximumRemovals(string s, string p, int[] removable)
    {
        var left = 0;
        var right = removable.Length - 1;
        while (left < right)
        {
            var mid = left + (right - left + 1) / 2;
            if (IsSubsequence(mid, s, p, removable))
            {
                left = mid;
            }
            else
            {
                right = mid - 1;
            }
        }

        return IsSubsequence(left, s, p, removable) ? left + 1 : 0;
    }
    private bool IsSubsequence(int maxIndex, string s, string p, int[] arr)
    {
        var hash = new HashSet<int>();
        for (int k = 0; k <= maxIndex; k++)
        {
            hash.Add(arr[k]);
        }

        var i = 0; 
        var j = 0;
        while (i < s.Length && j < p.Length)
        {
            if (!hash.Contains(i) && s[i] == p[j])
            {
                j++;
            }

            i++;
        }

        return j == p.Length;
    }
}