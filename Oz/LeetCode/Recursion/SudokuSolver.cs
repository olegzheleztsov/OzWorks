namespace Oz.LeetCode.Recursion;

public class Solution
{
    public void SolveSudoku(char[][] board)
    {
        if (board == null || board.Length == 0)
        {
            return;
        }

        SolveSudokuInternal(board);
    }

    private bool SolveSudokuInternal(char[][] board)
    {
        for (var i = 0; i < board.Length; i++)
        {
            for (var j = 0; j < board[0].Length; j++)
            {
                if (board[i][j] == '.')
                {
                    for (var c = '1'; c <= '9'; c++)
                    {
                        if (IsValid(board, c, i, j))
                        {
                            board[i][j] = c;
                            if (SolveSudokuInternal(board))
                            {
                                return true;
                            }

                            board[i][j] = '.';
                        }
                    }

                    return false;
                }
            }
        }

        return true;
    }

    private static bool IsValid(char[][] board, char ch, int row, int col)
    {
        for (var i = 0; i < 9; i++)
        {
            if (board[row][i] == ch)
            {
                return false;
            }
        }

        for (var i = 0; i < 9; i++)
        {
            if (board[i][col] == ch)
            {
                return false;
            }
        }

        int r = row / 3 * 3, c = col / 3 * 3;

        for (var i = r; i < r + 3; i++)
        for (var j = c; j < c + 3; j++)
        {
            if (board[i][j] == ch)
            {
                return false;
            }
        }

        return true;
    }
}