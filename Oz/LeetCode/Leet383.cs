// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet383
{
    public bool CanConstruct(string ransomNote, string magazine)
    {
        Dictionary<char, int> map = new Dictionary<char, int>();

        foreach (char c in magazine)
        {
            if (map.ContainsKey(c))
            {
                map[c]++;
            }
            else
            {
                map.Add(c, 1);
            }
        }

        foreach (char c in ransomNote)
        {
            if (map.ContainsKey(c) && map[c] > 0)
            {
                map[c]--;
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}