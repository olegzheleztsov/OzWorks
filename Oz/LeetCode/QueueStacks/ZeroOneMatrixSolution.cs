#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Oz.LeetCode.QueueStacks
{
    public class ZeroOneMatrixSolution
    {
        public int[][] UpdateMatrix(int[][] mat)
        {
            foreach (var row in mat)
            {
                for (var j = 0; j < row.Length; j++)
                {
                    if (row[j] > 0)
                    {
                        row[j] = -1;
                    }
                }
            }

            Queue<(int r, int c)> queue = new Queue<(int r, int c)>();
            HashSet<(int r, int c)> visited = new HashSet<(int r, int c)>();

            for (int i = 0; i < mat.Length; i++)
            {
                for (int j = 0; j < mat[i].Length; j++)
                {
                    if (mat[i][j] == 0)
                    {
                        queue.Enqueue((i, j));
                        visited.Add((i, j));
                    }
                }
            }

            while (queue.Count > 0)
            {
                var (r, c) = queue.Dequeue();
                if (IsValidIndex(mat, r - 1, c) && !visited.Contains((r - 1, c)))
                {
                    mat[r - 1][c] = mat[r][c] + 1;
                    visited.Add((r - 1, c));
                    queue.Enqueue((r-1, c));
                }
                if (IsValidIndex(mat, r + 1, c) && !visited.Contains((r + 1, c)))
                {
                    mat[r + 1][c] = mat[r][c] + 1;
                    visited.Add((r + 1, c));
                    queue.Enqueue((r+1, c));
                }
                if (IsValidIndex(mat, r, c - 1) && !visited.Contains((r, c - 1)))
                {
                    mat[r][c-1] = mat[r][c] + 1;
                    visited.Add((r, c-1));
                    queue.Enqueue((r, c-1));
                }
                if (IsValidIndex(mat, r, c + 1) && !visited.Contains((r, c + 1)))
                {
                    mat[r][c+1] = mat[r][c] + 1;
                    visited.Add((r, c+1));
                    queue.Enqueue((r, c+1));
                }
            }

            return mat;
        }

 
        private static bool IsValidIndex(int[][] mat, int r, int c)
        {
            return r >= 0 && r < mat.Length && c >= 0 && c < mat[r].Length;
        }
    }
}