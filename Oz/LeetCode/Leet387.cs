// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet387
{
    public int FirstUniqChar(string s)
    {
        Dictionary<char, List<int>> mapIndices = new Dictionary<char, List<int>>();

        for (int i = 0; i < s.Length; i++)
        {
            if (mapIndices.ContainsKey(s[i]))
            {
                mapIndices[s[i]].Add(i);
            }
            else
            {
                mapIndices.Add(s[i], new List<int>(){i});
            }
        }

        int numCandidates = mapIndices.Values.Where(l => l.Count == 1).Count();
        if (numCandidates == 0)
        {
            return -1;
        }

        int minIndex = int.MaxValue;
        foreach (var kvp in mapIndices)
        {
            if (kvp.Value.Count == 1)
            {
                if (kvp.Value[0] < minIndex)
                {
                    minIndex = kvp.Value[0];
                }
            }
        }

        return minIndex;
    }
    
    
}