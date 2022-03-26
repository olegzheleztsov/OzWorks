// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet1926
{
    public int NearestExit(char[][] maze, int[] entrance)
    {
        var queue = new Queue<(int r, int c)>();
        var rows = maze.Length;
        var columns = maze[0].Length;

        var visted = new bool[rows, columns];
        queue.Enqueue((entrance[0], entrance[1]));
        visted[entrance[0], entrance[1]] = true;
        var result = 0;

        int[] dr = {1, -1, 0, 0};
        int[] dc = {0, 0, 1, -1};

        while (queue.Count > 0)
        {
            var cnt = queue.Count;
            result++;

            for (var i = 0; i < cnt; i++)
            {
                var (r, c) = queue.Dequeue();

                for (var k = 0; k < 4; k++)
                {
                    var nr = r + dr[k];
                    var nc = c + dc[k];

                    if (nr >= 0 && nr < rows && nc >= 0 && nc < columns && !visted[nr, nc] && maze[nr][nc] != '+')
                    {
                        if (IsExit(maze, nr, nc))
                        {
                            return result;
                        }

                        visted[nr, nc] = true;
                        queue.Enqueue((nr, nc));
                    }
                }
            }
        }

        return -1;
    }


    private static bool IsExit(IReadOnlyList<char[]> maze, int i, int j)
    {
        var rows = maze.Count;
        var columns = maze[0].Length;
        return maze[i][j] == '.' && (i == 0 || i == rows - 1 || j == 0 || j == columns - 1);
    }
}