// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _459
{
    public bool RepeatedSubstringPattern(string s)
    {
        var patternLength = s.Length / 2;

        while (patternLength > 0)
        {
            if (IsFromPattern(s, s[..patternLength]))
            {
                return true;
            }

            patternLength--;
        }

        return false;
    }

    private static bool IsFromPattern(string s, string pattern)
    {
        if (s.Length % pattern.Length != 0)
        {
            return false;
        }

        for (var i = 0; i < s.Length; i += pattern.Length)
        {
            if (s.Substring(i, pattern.Length) != pattern)
            {
                return false;
            }
        }

        return true;
    }
}