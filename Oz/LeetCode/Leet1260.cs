// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet1260
{
    public IList<IList<int>> ShiftGrid(int[][] grid, int k)
    {
        var temp = new int[grid.Length];

        for (var i = 0; i < k; i++)
        {
            CopyLastColumnToArray(grid, temp);
            ShiftRight(grid);
            ShiftArray(temp);
            CopyFirstColumnFromArray(grid, temp);
        }

        IList<IList<int>> result = new List<IList<int>>();
        foreach (var row in grid)
        {
            result.Add(row);
        }

        return result;
    }

    private void ShiftRight(int[][] grid)
    {
        for (var j = grid[0].Length - 1; j > 0; j--)
        {
            for (var i = 0; i < grid.Length; i++)
            {
                grid[i][j] = grid[i][j - 1];
            }
        }
    }

    private void ShiftArray(int[] array)
    {
        var temp = array[array.Length - 1];
        for (var i = array.Length - 2; i >= 0; i--)
        {
            array[i + 1] = array[i];
        }

        array[0] = temp;
    }

    private void CopyLastColumnToArray(int[][] grid, int[] array)
    {
        for (var i = 0; i < array.Length; i++)
        {
            var row = grid[i];
            array[i] = row[row.Length - 1];
        }
    }

    private void CopyFirstColumnFromArray(int[][] grid, int[] array)
    {
        for (var i = 0; i < array.Length; i++)
        {
            grid[i][0] = array[i];
        }
    }
}