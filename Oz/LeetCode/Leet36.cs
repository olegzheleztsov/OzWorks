// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet36
{
    public bool IsValidSudoku(char[][] board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!IsSquareValid(board, i, j))
                {
                    return false;
                }
            }
        }

        for (int i = 0; i < 9; i++)
        {
            if (!IsRowValid(board, i))
            {
                return false;
            }

            if (!IsColumnValid(board, i))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsColumnValid(char[][] board, int column)
    {
        HashSet<char> numbersHash = new HashSet<char>();

        for (int i = 0; i < 9; i++)
        {
            var c = board[i][column];
            if (c == '.')
            {
                continue;
            }

            if (numbersHash.Contains(c))
            {
                return false;
            }

            numbersHash.Add(c);
        }

        return true;
    }

    private bool IsRowValid(char[][] board, int row)
    {
        HashSet<char> numbersHash = new HashSet<char>();
        for (int j = 0; j < 9; j++)
        {
            var c = board[row][j];
            if (c == '.')
            {
                continue;
            }

            if (numbersHash.Contains(c))
            {
                return false;
            }

            numbersHash.Add(c);
        }

        return true;
    }

    private bool IsSquareValid(char[][] board, int startRow, int startCol)
    {
        HashSet<char> numbersHash = new HashSet<char>();
        
        for (int i = 3 * startRow; i < 3 * startRow + 3; i++)
        {
            for (int j = 3 * startCol; j < 3 * startCol + 3; j++)
            {
                char c = board[i][j];
                if (c == '.')
                {
                    continue;
                }

                if (numbersHash.Contains(c))
                {
                    return false;
                }

                numbersHash.Add(c);
            }
        }

        return true;
    }
}