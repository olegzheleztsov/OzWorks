// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet139
{
    public bool WordBreak(string s, IList<string> wordDict)
    {
        var dp = new bool[s.Length + 1];
        dp[s.Length] = true;

        for (var i = s.Length - 1; i >= 0; i--)
        {
            foreach (var word in wordDict)
            {
                if (i + word.Length <= s.Length && s.Substring(i, word.Length) == word)
                {
                    dp[i] = dp[i + word.Length];
                }

                if (dp[i])
                {
                    break;
                }
            }
        }

        return dp[0];
    }

    public static void Test()
    {
        var leet139 = new Leet139();
        leet139.WordBreak("aaaaaaa", new List<string> {"aaaa", "aaa"});
    }
}