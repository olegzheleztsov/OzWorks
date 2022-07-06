// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _28
{
    public int StrStr(string haystack, string needle)
    {
        if (string.IsNullOrEmpty(needle))
        {
            return 0;
        }

        for (var i = 0; i < haystack.Length - needle.Length + 1; i++)
        {
            if (haystack[i] == needle[0])
            {
                if (Check(i, haystack, needle))
                {
                    return i;
                }
            }
        }

        return -1;
    }

    private bool Check(int ind, string haystack, string needle)
    {
        var sourceLen = haystack.Length;
        for (var i = ind; i < ind + needle.Length; i++)
        {
            if (i < sourceLen)
            {
                if (haystack[i] != needle[i - ind])
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}