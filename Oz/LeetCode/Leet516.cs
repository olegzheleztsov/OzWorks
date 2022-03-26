// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet516
{
    public int LongestPalindromeSubseq(string s)
    {
        var length = s.Length;
        var dp = new int[length, length];
        var offset = 0;

        for (var i = 0; i < length; i++, offset++)
        {
            var x = 0;
            for (var y = offset; y < length; y++, x++)
            {
                var val = 0;
                if (x == y)
                {
                    val = 1;
                }
                else if (s[x] == s[y])
                {
                    val = 2 + dp[x + 1, y - 1];
                }
                else
                {
                    val = dp[x, y - 1] > dp[x + 1, y] ? dp[x, y - 1] : dp[x + 1, y];
                }

                dp[x, y] = val;
            }
        }

        return dp[0, length - 1];
    }
}