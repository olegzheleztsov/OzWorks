// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet242
{
    public bool IsAnagram(string s, string t)
    {

        if (s.Length != t.Length)
        {
            return false;
        }
        
        Dictionary<char, int> tMap = new Dictionary<char, int>();
        foreach (char c in t)
        {
            if (tMap.ContainsKey(c))
            {
                tMap[c]++;
            }
            else
            {
                tMap.Add(c, 1);
            }
        }

        foreach (var c in s)
        {
            if (!tMap.ContainsKey(c))
            {
                return false;
            }

            tMap[c]--;
        }

        return tMap.Values.All(v => v == 0);
    }
}