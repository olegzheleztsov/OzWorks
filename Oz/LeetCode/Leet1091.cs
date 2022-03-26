// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet1091
{
    public int ShortestPathBinaryMatrix(int[][] grid)
    {
        var rows = grid.Length;
        var columns = grid[0].Length;
        var state = new VisitedPath[rows, columns];
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                state[i, j] = new VisitedPath();
            }
        }
        Bfs(grid, state, 0, 0);
        return state[rows - 1, columns - 1].visited ? state[rows - 1, columns - 1].path + 1 : -1;
    }

    private void Bfs(int[][] grid, VisitedPath[,] state, int startRow, int startCol)
    {
        var rows = grid.Length;
        var columns = grid[0].Length;
        state[startRow, startCol].visited = true;
        state[startRow, startCol].path = 0;
        var queue = new Queue<(int r, int s)>();
        queue.Enqueue((startRow, startCol));
        if (grid[startRow][startCol] == 1)
        {
            return;
        }

        int[] dr = {1, -1, 0, 0, -1, 1, -1, 1};
        int[] dc = {0, 0, 1, -1, -1, -1, 1, 1};
        while (queue.Count > 0)
        {
            var (r, c) = queue.Dequeue();

            for (var i = 0; i < 8; i++)
            {
                var nr = r + dr[i];
                var nc = c + dc[i];

                if (nr >= 0 && nr < rows && nc >= 0 && nc < columns &&
                    !state[nr, nc].visited && grid[nr][nc] == 0)
                {
                    state[nr, nc].path = Math.Min(state[nr, nc].path, state[r, c].path + 1);
                    state[nr, nc].visited = true;
                    queue.Enqueue((nr, nc));
                }
            }
        }
        
    }

    public class VisitedPath
    {
        public int path = int.MaxValue;
        public bool visited;
    }
}