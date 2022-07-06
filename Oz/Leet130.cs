// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

public sealed class Leet130
{
    public void Solve(char[][] board)
    {
        var rows = board.Length;
        var cols = board[0].Length;
        var visited = new bool[rows, cols];
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (board[i][j] == 'X')
                {
                    visited[i, j] = true;
                }
            }
        }

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (board[i][j] == 'O' && IsOnBorder(i, j, rows, cols))
                {
                    if (!visited[i, j])
                    {
                        Visit(board, visited, i, j);
                    }
                }
            }
        }

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (board[i][j] == 'O' && !visited[i, j])
                {
                    board[i][j] = 'X';
                }
            }
        }
    }

    private void Visit(char[][] board, bool[,] visited, int row, int col)
    {
        var rows = board.Length;
        var cols = board[0].Length;
        var queue = new Queue<(int r, int c)>();
        queue.Enqueue((row, col));
        visited[row, col] = true;

        while (queue.Count > 0)
        {
            var (r, c) = queue.Dequeue();
            int[] dr = {1, -1, 0, 0};
            int[] dc = {0, 0, 1, -1};
            for (var i = 0; i < 4; i++)
            {
                var nr = r + dr[i];
                var nc = c + dc[i];

                if (nr >= 0 && nr < rows && nc >= 0 && nc < cols && !visited[nr, nc])
                {
                    if (board[nr][nc] == 'O')
                    {
                        visited[nr, nc] = true;
                        queue.Enqueue((nr, nc));
                    }
                }
            }
        }
    }

    private bool IsOnBorder(int r, int c, int rows, int cols) =>
        r == 0 || r == rows - 1 || c == 0 || c == cols - 1;
}