// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet74
{

    public static void Test()
    {
        int[][] matrix = new int[1][];
        matrix[0] = new int[] {1};
        Leet74 obj = new Leet74();
        obj.SearchMatrix(matrix, 3);
    }
    
    public bool SearchMatrix(int[][] matrix, int target)
    {

        int rows = matrix.Length;
        
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
        int minIndex = 0;
        int maxIndex = matrix[0].Length - 1;
        while (minIndex <= maxIndex)
        {
            int midIndex = (minIndex + maxIndex) / 2;
            if (target == matrix[row][midIndex])
            {
                return midIndex;
            } else if (target < matrix[row][midIndex])
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
        int minIndex = 0;
        int maxIndex = matrix.Length - 1;

        while (minIndex <= maxIndex)
        {
            int midIndex = (minIndex + maxIndex) / 2;
            if (target == matrix[midIndex][col])
            {
                return midIndex;
            } else if (target < matrix[midIndex][col])
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