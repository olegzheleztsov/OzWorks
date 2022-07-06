// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

public class Leet74_2
{
    public bool SearchMatrix(int[][] matrix, int target)
    {
        var rows = matrix.Length;

        var targetRow = SearchInColumn(matrix, 0, target);
        if (targetRow >= rows)
        {
            targetRow = rows - 1;
        }

        var result = SearchInRow(matrix, targetRow, target);
        if (result != null)
        {
            return true;
        }


        targetRow--;
        if (targetRow >= 0)
        {
            result = SearchInRow(matrix, targetRow, target);
            if (result != null)
            {
                return true;
            }
        }

        targetRow += 2;
        if (targetRow < matrix.Length)
        {
            result = SearchInRow(matrix, targetRow, target);
            if (result != null)
            {
                return true;
            }
        }

        return false;
    }

    private int? SearchInRow(int[][] matrix, int row, int target)
    {
        var minIndex = 0;
        var maxIndex = matrix[0].Length - 1;
        while (minIndex <= maxIndex)
        {
            var midIndex = (minIndex + maxIndex) / 2;
            if (target == matrix[row][midIndex])
            {
                return midIndex;
            }

            if (target < matrix[row][midIndex])
            {
                maxIndex = midIndex - 1;
            }
            else
            {
                minIndex = midIndex + 1;
            }
        }

        return null;
    }

    private int SearchInColumn(int[][] matrix, int col, int target)
    {
        var minIndex = 0;
        var maxIndex = matrix.Length - 1;

        while (minIndex <= maxIndex)
        {
            var midIndex = (minIndex + maxIndex) / 2;
            if (target == matrix[midIndex][col])
            {
                return midIndex;
            }

            if (target < matrix[midIndex][col])
            {
                maxIndex = midIndex - 1;
            }
            else
            {
                minIndex = midIndex + 1;
            }
        }

        return minIndex;
    }
}