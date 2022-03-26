// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet779
{
    public int KthGrammar(int n, int k) {
        if (n == 1)
        {
            return 0;
        }

        var parent = KthGrammar(n - 1, (int)Math.Ceiling((double)k / 2));
        if (parent == 1)
        {
            return k % 2 != 0 ? 1 : 0;
        }
        else
        {
            return k % 2 != 0 ? 0 : 1;
        }
    }
}