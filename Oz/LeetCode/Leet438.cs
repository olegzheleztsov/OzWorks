// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet438
{
    /*
     * Given two strings s and p, return an array of all the start indices of p's anagrams in s. You may return the answer in any order.
     * An Anagram is a word or phrase formed by rearranging the letters of a different word or phrase, typically using all the original letters exactly once.
     */
    public IList<int> FindAnagrams(string s, string p)
    {
        var results = new List<int>();
        var countMap = new Dictionary<char, int>();
        RestoreCountMap(countMap, p);

        int i = 0, j = 0;

        while (j < s.Length)
        {
            if (countMap.ContainsKey(s[j]) && countMap[s[j]] > 0)
            {
                countMap[s[j]]--;
                if (IsMapZero(countMap))
                {
                    results.Add(i);
                    countMap[s[i]]++;
                    i++;
                }

                j++;
            }
            else if (countMap.ContainsKey(s[j]) && countMap[s[j]] == 0)
            {
                countMap[s[i]]++;
                i++;
            }
            else
            {
                j++;
                i = j;
                RestoreCountMap(countMap, p);
            }
        }

        return results;
    }

    private static bool IsMapZero(Dictionary<char, int> countMap) =>
        countMap.Values.All(v => v == 0);

    private static void RestoreCountMap(Dictionary<char, int> countMap, string p)
    {
        countMap.Clear();
        foreach (var c in p)
        {
            if (countMap.ContainsKey(c))
            {
                countMap[c]++;
            }
            else
            {
                countMap.Add(c, 1);
            }
        }
    }
}