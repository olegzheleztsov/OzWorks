// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet423
{
    public string OriginalDigits(string s)
    {
        var charMap = new Dictionary<char, int>();

        foreach (var c in s)
        {
            if (charMap.ContainsKey(c))
            {
                charMap[c]++;
            }
            else
            {
                charMap.Add(c, 1);
            }
        }

        return string.Empty;
    }

    private int GetSymbolCount(Dictionary<char, int> map, char c)
    {
        if (map.ContainsKey(c))
        {
            return map[c];
        }

        return 0;
    }
}