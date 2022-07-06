// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _1886
{
    public bool FindRotation(int[][] mat, int[][] target)
    {
        for (var i = 0; i < 4; i++)
        {
            if (IsEquals(mat, target))
            {
                return true;
            }

            RotateMatrix(mat);
        }

        return false;
    }

    private bool IsEquals(int[][] mat1, int[][] mat2)
    {
        var rows = mat1.Length;
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < rows; j++)
            {
                if (mat1[i][j] != mat2[i][j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void RotateMatrix(int[][] mat)
    {
        SwapRows(mat);
        Transpose(mat);
    }

    private void Transpose(int[][] mat)
    {
        var rows = mat.Length;

        for (var i = 0; i < rows; i++)
        {
            for (var j = i + 1; j < rows; j++)
            {
                (mat[i][j], mat[j][i]) = (mat[j][i], mat[i][j]);
            }
        }
    }

    private void SwapRows(int[][] mat)
    {
        var rows = mat.Length;
        var columns = mat[0].Length;

        var dr = 0;
        var ur = rows - 1;
        while (dr < ur)
        {
            SwapRow(mat, dr, ur);
            dr++;
            ur--;
        }
    }

    private void SwapRow(int[][] mat, int r1, int r2)
    {
        var columns = mat[0].Length;

        for (var j = 0; j < columns; j++)
        {
            (mat[r1][j], mat[r2][j]) = (mat[r2][j], mat[r1][j]);
        }
    }
}