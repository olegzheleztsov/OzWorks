// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Oz.LeetCode.QueueStacks;
using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet542
{
    public int[][] UpdateMatrix(int[][] mat) {
        int rows = mat.Length;
        int columns = mat[0].Length;
        VisitedPath[,] visitedPaths = new VisitedPath[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                visitedPaths[i, j] = new VisitedPath() {path = (mat[i][j] == 0) ? 0 : 1000000, visited = false};
            }
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (mat[i][j] == 1 && !visitedPaths[i, j].visited)
                {
                    Bsf(mat, visitedPaths, i, j);
                }
            }
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                mat[i][j] = visitedPaths[i, j].path;
            }
        }

        return mat;
    }

    private void Bsf(int[][] mat, VisitedPath[,] visitedPaths, int startRow, int startColumn)
    {
        int rows = mat.Length;
        int columns = mat[0].Length;
        
        Queue<(int r, int c)> queue = new Queue<(int r, int c)>();

        int[] dr = {1, -1, 0, 0};
        int[] dc = {0, 0, 1, -1};
        visitedPaths[startRow, startColumn].visited = true;
        for (int i = 0; i < 4; i++)
        {
            var nr = startRow + dr[i];
            var nc = startColumn + dc[i];
            if (nr >= 0 && nr < rows && nc >= 0 && nc < columns)
            {
                if (mat[nr][nc] == 0)
                {
                    visitedPaths[startRow, startColumn].path = 1;
                    break;
                }
                else
                {
                    if (visitedPaths[nr, nc].visited)
                    {
                        visitedPaths[startRow, startColumn].path = Math.Min(visitedPaths[startRow, startColumn].path,
                            visitedPaths[nr, nc].path + 1);
                    }
                }
            }
        }
        //visitedPaths[startRow, startColumn].path = 1;

        while (queue.Count > 0)
        {
            var (r, c) = queue.Dequeue();

            for (int i = 0; i < 4; i++)
            {
                var nr = r + dr[i];
                var nc = c + dc[i];
                if (nr >= 0 && nr < rows && nc >= 0 && nc < columns && !visitedPaths[nr, nc].visited &&
                    mat[nr][nc] == 1)
                {
                    visitedPaths[nr, nc].path = Math.Min(visitedPaths[nr, nc].path, visitedPaths[r, c].path + 1);
                    visitedPaths[nr, nc].visited = true;
                    queue.Enqueue((nr, nc));
                }
            }
        }
    }
    
    public class VisitedPath
    {
        public int path = 1000000;
        public bool visited;
    }
}