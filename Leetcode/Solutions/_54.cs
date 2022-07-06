// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _54
{
    public IList<int> SpiralOrder(int[][] matrix)
    {
        List<int> result = new List<int>();
        Direction direction = Direction.Right;
        int left = 0;
        int right = matrix[0].Length - 1;
        int up = 0;
        int down = matrix.Length - 1;

        while (left <= right && up <= down)
        {
            switch (direction)
                    {
                        case Direction.Right:
                        {
                            for (int i = left; i <= right; i++)
                            {
                                result.Add(matrix[up][i]);
                            }
            
                            up++;
                            direction = Direction.Down;
                        }
                            break;
                        case Direction.Down:
                        {
                            for (int i = up; i <= down; i++)
                            {
                                result.Add(matrix[i][right]);
                            }
            
                            right--;
                            direction = Direction.Left;
                        }
                            break;
                        case Direction.Left:
                        {
                            for (int i = right; i >= left; i--)
                            {
                                result.Add(matrix[down][i]);
                            }
            
                            down--;
                            direction = Direction.Up;
                        }
                            break;
                        case Direction.Up:
                        {
                            for (int i = down; i >= up; i--)
                            {
                                result.Add(matrix[i][left]);
                            }
            
                            left++;
                            direction = Direction.Right;
                        }
                            break;
                    }
        }

        return result;
    }
    
    public enum Direction
    {
        Right,
        Down,
        Left,
        Up
    }
}