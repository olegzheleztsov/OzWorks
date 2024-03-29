﻿// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet1314
{
    public int[][] MatrixBlockSum(int[][] mat, int K)
    {
        int r = mat.Length, c = mat[0].Length;
        var sum = new int[r + 1, c + 1];
        for (var i = 1; i <= r; i++)
        {
            for (var j = 1; j <= c; j++)
            {
                sum[i, j] = sum[i - 1, j] + sum[i, j - 1] - sum[i - 1, j - 1] + mat[i - 1][j - 1];
            }
        }

        var result = new int[r][];
        for (var i = 0; i < r; i++)
        {
            result[i] = new int[c];
            for (var j = 0; j < c; j++)
            {
                int x1 = Math.Max(0, i - K), y1 = Math.Max(0, j - K);
                int x2 = Math.Min(r, i + K + 1), y2 = Math.Min(c, j + K + 1);
                result[i][j] = sum[x2, y2] - sum[x1, y2] - sum[x2, y1] + sum[x1, y1];
            }
        }

        return result;
    }
}