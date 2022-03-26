// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet231
{
    public bool IsPowerOfTwo(int n)
    {
        if (n == 1)
        {
            return true;
        }

        var x = 1;


        for (var i = 0; i < 30; i++)
        {
            x <<= 1;
            if (n == x)
            {
                return true;
            }
        }

        return false;
    }
}