// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet566
{
    public int[][] MatrixReshape(int[][] mat, int r, int c)
    {
        var rows = mat.Length;
        var columns = mat[0].Length;

        if (rows * columns != r * c)
        {
            return mat;
        }

        var result = new int[r][];

        for (var i = 0; i < r; i++)
        {
            result[i] = new int[c];
        }

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                var index = (columns * i) + j;

                var newRow = index / c;
                var newCol = index % c;
                result[newRow][newCol] = mat[i][j];
            }
        }

        return result;
    }
}