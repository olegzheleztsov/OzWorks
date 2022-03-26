// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Text;

namespace Oz.LeetCode;

public class Leet1284
{
    public static void Test()
    {
        var mat = new int[2][];
        mat[0] = new[] {1, 0, 0};
        mat[1] = new[] {1, 0, 0};
        var leet1284 = new Leet1284();
        leet1284.MinFlips(mat);
    }

    public int MinFlips(int[][] mat)
    {
        var rows = mat.Length;
        var cols = mat[0].Length;

        var queue = new Queue<Matrix>();
        var visited = new HashSet<int>();
        var initial = new Matrix(mat);
        var steps = 0;
        if (initial.IsZero)
        {
            return steps;
        }

        queue.Enqueue(initial);
        visited.Add(initial.GetHashCode());

        while (queue.Count > 0)
        {
            var size = queue.Count;

            for (var qIndex = 0; qIndex < size; qIndex++)
            {
                var matr = queue.Dequeue();
                if (matr.IsZero)
                {
                    return steps;
                }

                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < cols; j++)
                    {
                        var newMatr = matr.Flip(i, j);

                        if (visited.Contains(newMatr.GetHashCode()))
                        {
                            continue;
                        }

                        visited.Add(newMatr.GetHashCode());
                        queue.Enqueue(newMatr);
                    }
                }
            }

            steps++;
        }

        return -1;
    }

    public class Matrix
    {
        public int[,] _data;
        private int _hashCode;

        public Matrix(int[][] mat)
        {
            var rows = mat.Length;
            var columns = mat[0].Length;
            _data = new int[rows, columns];

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    _data[i, j] = mat[i][j];
                }
            }

            ComputeHashCode();
        }

        public Matrix(int[,] data)
        {
            _data = data;
            ComputeHashCode();
        }

        public bool IsZero
        {
            get
            {
                var sum = 0;
                var rows = _data.GetUpperBound(0) + 1;
                var columns = _data.GetUpperBound(1) + 1;

                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < columns; j++)
                    {
                        sum += _data[i, j];
                    }
                }

                return sum == 0;
            }
        }

        public override int GetHashCode() =>
            _hashCode;

        private void ComputeHashCode()
        {
            var sb = new StringBuilder();
            var rows = _data.GetUpperBound(0) + 1;
            var columns = _data.GetUpperBound(1) + 1;

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    sb.Append(_data[i, j].ToString());
                }
            }

            _hashCode = Convert.ToInt32(sb.ToString(), 2);
        }

        public Matrix Flip(int row, int col)
        {
            var rows = _data.GetUpperBound(0) + 1;
            var cols = _data.GetUpperBound(1) + 1;
            var newData = new int[rows, cols];

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    newData[i, j] = _data[i, j];
                }
            }

            newData[row, col] = FlipValue(_data[row, col]);

            var leftCol = col - 1;
            var rightCol = col + 1;
            var topRow = row - 1;
            var bottomRow = row + 1;

            if (leftCol >= 0)
            {
                newData[row, leftCol] = FlipValue(_data[row, leftCol]);
            }

            if (rightCol < cols)
            {
                newData[row, rightCol] = FlipValue(_data[row, rightCol]);
            }

            if (topRow >= 0)
            {
                newData[topRow, col] = FlipValue(_data[topRow, col]);
            }

            if (bottomRow < rows)
            {
                newData[bottomRow, col] = FlipValue(_data[bottomRow, col]);
            }

            return new Matrix(newData);

            int FlipValue(int val)
            {
                return val == 0 ? 1 : 0;
            }
        }
    }
}