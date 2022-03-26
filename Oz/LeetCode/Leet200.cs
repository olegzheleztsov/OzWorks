// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet200
{
    public int NumIslands(char[][] grid)
    {
        var rows = grid.Length;
        var cols = grid[0].Length;
        var visited = new bool[rows, cols];

        var counter = 0;
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (!visited[i, j] && grid[i][j] == '1')
                {
                    counter++;
                    VisitIsland(grid, visited, i, j);
                }
            }
        }

        return counter;
    }

    private void VisitIsland(char[][] grid, bool[,] visited, int row, int col)
    {
        var rows = grid.Length;
        var cols = grid[0].Length;
        var queue = new Queue<(int r, int c)>();
        visited[row, col] = true;
        queue.Enqueue((row, col));
        while (queue.Count > 0)
        {
            var (r, c) = queue.Dequeue();
            if (r - 1 >= 0 && grid[r - 1][c] == '1' && !visited[r - 1, c])
            {
                visited[r - 1, c] = true;
                queue.Enqueue((r - 1, c));
            }

            if (r + 1 < rows && grid[r + 1][c] == '1' && !visited[r + 1, c])
            {
                visited[r + 1, c] = true;
                queue.Enqueue((r + 1, c));
            }

            if (c - 1 >= 0 && grid[r][c - 1] == '1' && !visited[r, c - 1])
            {
                visited[r, c - 1] = true;
                queue.Enqueue((r, c - 1));
            }

            if (c + 1 < cols && grid[r][c + 1] == '1' && !visited[r, c + 1])
            {
                visited[r, c + 1] = true;
                queue.Enqueue((r, c + 1));
            }
        }
    }
}