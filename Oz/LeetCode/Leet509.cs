// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet509
{
    public int Fib(int n)
    {
        if (n == 0)
        {
            return 0;
        }

        if (n == 1)
        {
            return 1;
        }

        var fib = new int[n + 1];
        fib[0] = 0;
        fib[1] = 1;

        for (var i = 2; i <= n; i++)
        {
            fib[i] = fib[i - 1] + fib[i - 2];
        }

        return fib[n];
    }
}