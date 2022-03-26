// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet1905
{
    public int CountSubIslands(int[][] grid1, int[][] grid2) {
        int rows = grid2.Length;
        int columns = grid2[0].Length;
        bool[,] visited = new bool[rows, columns];
        
        int countOfSubIslands = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (grid2[i][j] == 1 && !visited[i, j])
                {
                    if (IsSubIsland(grid1, grid2, visited, i, j))
                    {
                        countOfSubIslands++;
                    }
                }
            }
        }

        return countOfSubIslands;
    }

    private bool IsSubIsland(int[][] grid1, int[][] grid2, bool[,] visited, int row, int col)
    {
        int rows = grid2.Length;
        int columns = grid2[0].Length;
        bool isSubIsland = true;

        Queue<(int r, int c)> queue = new Queue<(int r, int c)>();
        visited[row, col] = true;

        if (grid1[row][col] != 1)
        {
            isSubIsland = false;
        }
        queue.Enqueue((row, col));

        while (queue.Count > 0)
        {
            var (r, c) = queue.Dequeue();

            if (r - 1 >= 0 && !visited[r - 1, c] && grid2[r - 1][c] == 1)
            {
                if (grid1[r - 1][c] != 1)
                {
                    isSubIsland = false;
                }

                visited[r - 1, c] = true;
                queue.Enqueue((r-1, c));
            }

            if (r + 1 < rows && !visited[r + 1, c] && grid2[r + 1][c] == 1)
            {
                if (grid1[r + 1][c] != 1)
                {
                    isSubIsland = false;
                }

                visited[r + 1, c] = true;
                queue.Enqueue((r+1, c));
            }

            if (c - 1 >= 0 && !visited[r, c - 1] && grid2[r][c - 1] == 1)
            {
                if (grid1[r][c - 1] != 1)
                {
                    isSubIsland = false;
                }

                visited[r, c - 1] = true;
                queue.Enqueue((r, c-1));
            }

            if (c + 1 < columns && !visited[r, c + 1] && grid2[r][c + 1] == 1)
            {
                if (grid1[r][c + 1] != 1)
                {
                    isSubIsland = false;
                }

                visited[r, c + 1] = true;
                queue.Enqueue((r, c + 1));
            }
        }

        return isSubIsland;
    }
}