// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet931
{
    public int MinFallingPathSum(int[][] matrix)
    {
        var length = matrix.Length;
        var dp = new int[length, length];

        for (var i = 0; i < length; i++)
        {
            dp[0, i] = matrix[0][i];
        }

        for (var r = 1; r < length; r++)
        {
            for (var c = 0; c < length; c++)
            {
                var leftCol = c - 1;
                var col = c;
                var rightCol = c + 1;
                dp[r, c] = GetMin(dp, length, r - 1, leftCol, col, rightCol) + matrix[r][c];
            }
        }

        var result = int.MaxValue;
        for (var i = 0; i < length; i++)
        {
            if (dp[length - 1, i] < result)
            {
                result = dp[length - 1, i];
            }
        }

        return result;
    }

    private int GetMin(int[,] matr, int length, int r, int c1, int c2, int c3)
    {
        int? first = null;
        int? second = null;
        int? third = null;
        if (c1 >= 0)
        {
            first = matr[r, c1];
        }

        second = matr[r, c2];
        if (c3 < length)
        {
            third = matr[r, c3];
        }

        if (first != null && third != null)
        {
            if (first < second && first < third)
            {
                return first.Value;
            }

            if (second < third)
            {
                return second.Value;
            }

            return third.Value;
        }

        if (first != null)
        {
            return first < second ? first.Value : second.Value;
        }

        return third < second ? third.Value : second.Value;
    }
}