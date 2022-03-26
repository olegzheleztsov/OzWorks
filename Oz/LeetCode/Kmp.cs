// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Kmp
{
    public bool Search(string pattern, string text)
    {
        var lps = ComputeLps(pattern);
        var j = 0;
        var i = 0;

        while (i < text.Length)
        {
            if (pattern[j] == text[i])
            {
                j++;
                i++;
            }

            if (j == pattern.Length)
            {
                return true;
            }

            if (i < text.Length && pattern[j] != text[i])
            {
                if (j != 0)
                {
                    j = lps[j - 1];
                }
                else
                {
                    i++;
                }
            }
        }

        return false;
    }

    private int[] ComputeLps(string pattern)
    {
        var lps = new int[pattern.Length];
        var length = 0;
        var i = 1;
        lps[0] = 0;

        while (i < pattern.Length)
        {
            if (pattern[i] == pattern[length])
            {
                length++;
                lps[i] = length;
                i++;
            }
            else
            {
                if (length != 0)
                {
                    length = lps[length - 1];
                }
                else
                {
                    lps[i] = length;
                    i++;
                }
            }
        }

        return lps;
    }
}