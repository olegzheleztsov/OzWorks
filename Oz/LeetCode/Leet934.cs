// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet934
{
    public int ShortestBridge(int[][] grid)
    {
        if (grid == null || grid.Length == 0)
        {
            return 0;
        }

        int result = int.MaxValue,
            i = 0,
            j = 0;
        int[] dx = {0, 0, 1, -1},
            dy = {1, -1, 0, 0};
        var q1 = new Queue<int[]>();
        var q2 = new Queue<Tuple<int[], int>>();
        var visited = new bool[grid.Length, grid[0].Length];

        for (i = 0; i < grid.Length; i++)
        {
            for (j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == 1)
                {
                    break;
                }
            }

            if (j < grid[0].Length && grid[i][j] == 1)
            {
                break;
            }
        }

        grid[i][j] = 2;
        q1.Enqueue(new[] {i, j});

        while (q1.Count > 0)
        {
            var cur = q1.Dequeue();

            for (var k = 0; k < 4; k++)
            {
                int newX = cur[0] + dx[k],
                    newY = cur[1] + dy[k];

                if (newX > -1 && newX < grid.Length && newY > -1 && newY < grid[0].Length && !visited[newX, newY])
                {
                    visited[newX, newY] = true;

                    if (grid[newX][newY] == 1)
                    {
                        grid[newX][newY] = 2;
                        q1.Enqueue(new[] {newX, newY});
                    }
                    else if (grid[newX][newY] == 0)
                    {
                        q2.Enqueue(new Tuple<int[], int>(new[] {newX, newY}, 1));
                    }
                }
            }
        }

        while (q2.Count > 0)
        {
            var cur = q2.Dequeue();

            for (var k = 0; k < 4; k++)
            {
                int newX = cur.Item1[0] + dx[k],
                    newY = cur.Item1[1] + dy[k];

                if (newX > -1 && newX < grid.Length && newY > -1 && newY < grid[0].Length && !visited[newX, newY])
                {
                    visited[newX, newY] = true;

                    if (grid[newX][newY] == 1)
                    {
                        result = Math.Min(result, cur.Item2);
                    }
                    else if (grid[newX][newY] == 0)
                    {
                        q2.Enqueue(new Tuple<int[], int>(new[] {newX, newY}, cur.Item2 + 1));
                    }
                }
            }
        }

        return result;
    }
}