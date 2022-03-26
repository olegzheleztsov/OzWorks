// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet1572
{
    public int DiagonalSum(int[][] mat)
    {
        var size = mat.Length;

        var sum = 0;
        for (var i = 0; i < size; i++)
        {
            sum += mat[i][i];
        }

        for (var i = 0; i < size; i++)
        {
            if (i != size - 1 - i || size % 2 == 0)
            {
                sum += mat[size - i - 1][i];
            }
        }

        return sum;
    }
}