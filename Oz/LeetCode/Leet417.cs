// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet417
{
    public IList<IList<int>> PacificAtlantic(int[][] heights)
    {
        var rows = heights.Length;
        var columns = heights[0].Length;

        var results = new List<IList<int>>();
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                if (IsValid(heights, i, j))
                {
                    results.Add(new List<int> {i, j});
                }
            }
        }

        return results;
    }


    private bool IsValid(int[][] heights, int startRow, int startCol)
    {
        var rows = heights.Length;
        var columns = heights[0].Length;
        var visited = new bool[rows, columns];

        var queue = new Queue<(int r, int c)>();
        visited[startRow, startCol] = true;
        var isReachPacific = false;
        var isReachAtlantic = false;

        if (startRow == 0 || startCol == 0)
        {
            isReachPacific = true;
        }

        if (startRow == rows - 1 || startCol == columns - 1)
        {
            isReachAtlantic = true;
        }

        if (isReachPacific && isReachAtlantic)
        {
            return true;
        }

        queue.Enqueue((startRow, startCol));

        int[] dr = {0, 0, 1, -1};
        int[] dc = {1, -1, 0, 0};

        while (queue.Count > 0)
        {
            var (r, c) = queue.Dequeue();

            for (var k = 0; k < dr.Length; k++)
            {
                var nr = r + dr[k];
                var nc = c + dc[k];

                if (nr >= 0 && nc >= 0 && nr < rows && nc < columns && !visited[nr, nc] &&
                    heights[nr][nc] <= heights[r][c])
                {
                    if (nr == 0 || nc == 0)
                    {
                        isReachPacific = true;
                    }

                    if (nr == rows - 1 || nc == columns - 1)
                    {
                        isReachAtlantic = true;
                    }

                    visited[nr, nc] = true;
                    queue.Enqueue((nr, nc));
                    if (isReachPacific && isReachAtlantic)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}