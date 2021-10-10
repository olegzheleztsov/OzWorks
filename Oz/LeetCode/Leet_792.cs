// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet_792
{
    public int NumMatchingSubseq(string s, string[] words)
    {

        var charIndices = new Dictionary<char, List<int>>();

        for (int i = 0; i < s.Length; i++)
        {
            if (charIndices.ContainsKey(s[i]))
            {
                charIndices[s[i]].Add(i);
            }
            else
            {
                charIndices.Add(s[i], new List<int>(){i});
            }
        }

        int count = 0;
        foreach (var word in words)
        {
            if (IsSubsequence(word, charIndices))
            {
                count++;
            }
        }

        return count;
    }

    private bool IsSubsequence(string word, Dictionary<char, List<int>> indices)
    {
        int lastIndex = -1;
        for (int i = 0; i < word.Length; i++)
        {
            if (!indices.ContainsKey(word[i]))
            {
                return false;
            }
            else
            {
                var list = indices[word[i]];
                bool found = false;
                foreach (var ind in list)
                {
                    if (ind > lastIndex)
                    {
                        lastIndex = ind;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    return false;
                }
            }
        }

        return true;
    }
}