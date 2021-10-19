namespace Oz.LeetCode.Recursion;

public class TotalNQueensSolver
{
    public int TotalNQueens(int n)
    {
        if (n == 1)
        {
            return 1;
        }

        var field = new bool[n, n];
        return BacktrackNQueens(field, 0, 0);
    }

    private int BacktrackNQueens(bool[,] field, int row, int count)
    {
        var size = field.GetUpperBound(0) + 1;
        for (var col = 0; col < size; col++)
        {
            if (IsNotUnderAttack(field, row, col))
            {
                field[row, col] = true;
                if (row + 1 == size)
                {
                    count++;
                }
                else
                {
                    count = BacktrackNQueens(field, row + 1, count);
                }

                field[row, col] = false;
            }
        }

        return count;
    }

    private bool IsNotUnderAttack(bool[,] field, int row, int col)
    {
        var size = field.GetUpperBound(0) + 1;
        var count = 0;
        for (var i = 0; i < size; i++)
        {
            if (field[row, i])
            {
                count++;
            }
        }

        if (count > 0)
        {
            return false;
        }

        count = 0;
        for (var i = 0; i < size; i++)
        {
            if (field[i, col])
            {
                count++;
            }
        }

        if (count > 0)
        {
            return false;
        }

        count = 0;
        var r = row - 1;
        var c = col - 1;
        while (r >= 0 && r < size && c >= 0 && c < size)
        {
            if (field[r, c])
            {
                count++;
            }

            r--;
            c--;
        }

        r = row;
        c = col;
        while (r >= 0 && r < size && c >= 0 && c < size)
        {
            if (field[r, c])
            {
                count++;
            }

            r++;
            c++;
        }

        if (count > 0)
        {
            return false;
        }

        count = 0;
        r = row - 1;
        c = col + 1;
        while (r >= 0 && r < size && c >= 0 && c < size)
        {
            if (field[r, c])
            {
                count++;
            }

            r--;
            c++;
        }

        r = row;
        c = col;
        while (r >= 0 && r < size && c >= 0 && c < size)
        {
            if (field[r, c])
            {
                count++;
            }

            r++;
            c--;
        }

        return count <= 0;
    }
}