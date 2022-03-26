// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet202
{
    public bool IsHappy(int n) {
        if (n == 1)
        {
            return true;
        }

        HashSet<int> hash = new HashSet<int>();
        hash.Add(n);
        
        while (true)
        {
            n = GetNext(n);
            if (n == 1)
            {
                return true;
            }

            if (hash.Contains(n))
            {
                return false;
            }

            hash.Add(n);
        }
    }

    private int GetNext(int n)
    {
        int result = 0;

        while (n != 0)
        {
            int digit = n % 10;
            result += digit * digit;
            n -= digit;
            n /= 10;
        }

        return result;
    }
}