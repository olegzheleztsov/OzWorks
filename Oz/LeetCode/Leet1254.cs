// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet1254
{
    public int ClosedIsland(int[][] grid)
    {
        var rows = grid.Length;
        var cols = grid[0].Length;
        var visited = new bool[rows, cols];

        var count = 0;
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (!visited[i, j] && grid[i][j] == 0)
                {
                    if (IsClosed(grid, visited, i, j))
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    private bool IsClosed(int[][] grid, bool[,] visited, int row, int col)
    {
        var rows = grid.Length;
        var cols = grid[0].Length;
        var queue = new Queue<(int r, int c)>();
        visited[row, col] = true;
        queue.Enqueue((row, col));
        var closed = true;

        while (queue.Count > 0)
        {
            var (r, c) = queue.Dequeue();
            if (r - 1 >= 0 && grid[r - 1][c] == 0 && !visited[r - 1, c])
            {
                visited[r - 1, c] = true;
                queue.Enqueue((r - 1, c));
            }
            else if (r - 1 < 0)
            {
                closed = false;
                break;
            }

            if (r + 1 < rows && grid[r + 1][c] == 0 && !visited[r + 1, c])
            {
                visited[r + 1, c] = true;
                queue.Enqueue((r + 1, c));
            }
            else if (r + 1 >= rows)
            {
                closed = false;
                break;
            }

            if (c - 1 >= 0 && grid[r][c - 1] == 0 && !visited[r, c - 1])
            {
                visited[r, c - 1] = true;
                queue.Enqueue((r, c - 1));
            }
            else if (c - 1 < 0)
            {
                closed = false;
                break;
            }

            if (c + 1 < cols && grid[r][c + 1] == 0 && !visited[r, c + 1])
            {
                visited[r, c + 1] = true;
                queue.Enqueue((r, c + 1));
            }
            else if (c + 1 >= cols)
            {
                closed = false;
                break;
            }
        }

        return closed;
    }
}