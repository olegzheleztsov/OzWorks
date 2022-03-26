// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet1162
{
    public int MaxDistance(int[][] grid)
    {
        if (grid == null || grid.Length == 0)
        {
            return 0;
        }

        var res = -1;
        var queue = new Queue<(int r, int c)>();
        var visited = new bool[grid.Length, grid[0].Length];
        int[] dr = {0, 0, 1, -1};
        int[] dc = {1, -1, 0, 0};

        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == 1)
                {
                    queue.Enqueue((i, j));
                }
            }
        }

        while (queue.Count > 0)
        {
            var cnt = queue.Count;
            res++;
            while (cnt > 0)
            {
                var (r, c) = queue.Dequeue();

                for (var l = 0; l < 4; l++)
                {
                    var newR = r + dr[l];
                    var newC = c + dc[l];

                    if (newR > -1 && newR < grid.Length && newC > -1 && newC < grid[0].Length &&
                        !visited[newR, newC] && grid[newR][newC] == 0)
                    {
                        queue.Enqueue((newR, newC));
                        visited[newR, newC] = true;
                    }
                }

                cnt--;
            }
        }

        return res == 0 ? -1 : res;
    }
}