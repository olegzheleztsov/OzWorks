// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet91
{
    public static void Test()
    {
        var obj = new Leet91();
        obj.NumDecodings("27");
    }

    public int NumDecodings(string s)
    {
        var dp = new Dictionary<int, int> {[s.Length] = 1};
        return Dfs(0);

        int Dfs(int i)
        {
            if (dp.ContainsKey(i))
            {
                return dp[i];
            }

            if (s[i] == '0')
            {
                return 0;
            }

            var res = Dfs(i + 1);
            if (i + 1 < s.Length && (s[i] == '1' || (s[i] == '2' && "0123456".Contains(s[i + 1]))))
            {
                res += Dfs(i + 2);
            }

            dp[i] = res;
            return res;
        }
    }
}