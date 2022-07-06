// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;

namespace Oz;

public class Leet1351
{
    public void Test()
    {
        var grid = new int[2][];
        grid[0] = new[] {3, 2};
        grid[1] = new[] {1, 0};
        var result = CountNegatives(grid);
        Console.WriteLine(result);
    }

    private int CountNegatives(int[][] grid)
    {
        var numRows = grid.Length;
        var total = 0;
        for (var i = 0; i < numRows; i++)
        {
            total += CountNegativesInRow(i, grid);
        }

        return total;
    }

    private int CountNegativesInRow(int row, int[][] grid)
    {
        var len = grid[row].Length;
        var min = 0;
        var max = len - 1;
        int mid;

        while (min < max)
        {
            mid = min + ((max - min) / 2);
            if (grid[row][mid] == 0)
            {
                while (mid < len && grid[row][mid] >= 0)
                {
                    mid++;
                }

                if (mid >= len)
                {
                    return 0;
                }

                return len - mid;
            }

            if (grid[row][mid] < 0)
            {
                max = mid - 1;
            }
            else
            {
                min = mid + 1;
            }
        }

        while (min < len && grid[row][min] >= 0)
        {
            min++;
        }

        if (min >= len)
        {
            return 0;
        }

        return len - min;
    }
}