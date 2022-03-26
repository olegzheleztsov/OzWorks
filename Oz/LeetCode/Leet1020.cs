// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet1020
{
    public int NumEnclaves(int[][] grid)
    {
        int rows = grid.Length;
        int columns = grid[0].Length;
        bool[,] visited = new bool[rows, columns];

        int totalCount = 0;
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (!visited[i, j] && grid[i][j] == 1)
                {
                    var (isEnclave, count) = IsEnclave(grid, visited, i, j);
                    if (isEnclave)
                    {
                        totalCount += count;
                    }
                }
            }
        }

        return totalCount;
    }

    private (bool isEnclave, int count) IsEnclave(int[][] grid, bool[,] visited, int row, int column)
    {
        int rows = grid.Length;
        int columns = grid[0].Length;
        Queue<(int r, int c)> queue = new Queue<(int r, int c)>();
        visited[row, column] = true;
        queue.Enqueue((row, column));
        bool isEnclave = true;
        if (row == 0 || row == rows - 1)
        {
            isEnclave = false;
        }

        if (column == 0 || column == columns - 1)
        {
            isEnclave = false;
        }
        int count = 1;
        
        while (queue.Count > 0)
        {
            var (r, c) = queue.Dequeue();

            if (r - 1 >= 0 && !visited[r - 1, c] && grid[r - 1][c] == 1)
            {
                if (r - 1 == 0)
                {
                    isEnclave = false;
                }

                visited[r - 1, c] = true;
                count++;
                queue.Enqueue((r-1, c));
            }

            if (r + 1 < rows && !visited[r + 1, c] && grid[r + 1][c] == 1)
            {
                if (r + 1 == rows - 1)
                {
                    isEnclave = false;
                }

                visited[r + 1, c] = true;
                count++;
                queue.Enqueue((r+1, c));
            }

            if (c - 1 >= 0 && !visited[r, c - 1] && grid[r][c - 1] == 1)
            {
                if (c - 1 == 0)
                {
                    isEnclave = false;
                }

                visited[r, c - 1] = true;
                count++;
                queue.Enqueue((r, c- 1));
            }

            if (c + 1 < columns && !visited[r, c + 1] && grid[r][c + 1] == 1)
            {
                if (c + 1 == columns - 1)
                {
                    isEnclave = false;
                }

                visited[r, c + 1] = true;
                count++;
                queue.Enqueue((r, c + 1));
            }
        }

        return (isEnclave, count);
    }
}