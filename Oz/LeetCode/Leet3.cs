// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet3
{
    public int LengthOfLongestSubstring(string s)
    {
        var includedChars = new Dictionary<char, int>();

        var slidingStart = 0;
        var slidingEnd = 0;
        var count = 0;

        while (slidingEnd < s.Length)
        {
            if (!includedChars.ContainsKey(s[slidingEnd]))
            {
                includedChars.Add(s[slidingEnd], slidingEnd);
            }
            else
            {
                var targetIndex = includedChars[s[slidingEnd]];
                while (slidingStart <= targetIndex)
                {
                    includedChars.Remove(s[slidingStart]);
                    slidingStart++;
                }

                includedChars[s[slidingEnd]] = slidingEnd;
            }

            var test = slidingEnd - slidingStart + 1;
            if (test > count)
            {
                count = test;
            }

            slidingEnd++;
        }

        return count;
    }
}