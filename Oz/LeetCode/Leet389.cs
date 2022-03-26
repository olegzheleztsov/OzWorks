// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet389
{
    public char FindTheDifference(string s, string t)
    {

        Dictionary<char, int> frequency = new Dictionary<char, int>();

        foreach (char c in t)
        {
            if (frequency.ContainsKey(c))
            {
                frequency[c]++;
            }
            else
            {
                frequency.Add(c, 1);
            }
        }

        foreach (char c in s)
        {
            if (frequency.ContainsKey(c))
            {
                frequency[c]--;
                if (frequency[c] == 0)
                {
                    frequency.Remove(c);
                }
            }
        }

        return frequency.First().Key;
    }
}