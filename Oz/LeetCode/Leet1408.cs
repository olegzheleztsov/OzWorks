// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet1408
{
    public IList<string> StringMatching(string[] words)
    {
        var result = new HashSet<string>();

        for (var i = 0; i < words.Length; i++)
        {
            for (var j = 0; j < words.Length; j++)
            {
                if (i != j && words[j].Contains(words[i]))
                {
                    result.Add(words[i]);
                }
            }
        }

        return result.ToList();
    }
}