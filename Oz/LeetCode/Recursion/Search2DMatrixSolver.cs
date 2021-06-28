using System;

namespace Oz.LeetCode.Recursion
{
    public class Search2DMatrixSolver
    {
        private bool SearchMatrix(int[][] matrix, int target)
        {
            switch (matrix.Length)
            {
                case 0:
                    return false;
                case 1 when matrix[0].Length == 1:
                    return matrix[0][0] == target;
            }

            int lastRow = matrix.Length - 1;
            int lastCol = matrix[0].Length - 1;
            return SearchInSubMatrix(matrix, target, 0, lastRow, 
                0, lastCol, lastRow + 1, lastCol + 1);
        }

        private static bool SearchInSubMatrix(int[][] matrix, int target, int rowMin, int rowMax, 
            int colMin, int colMax, 
            int totalRows, int totalColumns)
        {
            if (rowMin > rowMax || colMin > colMax)
            {
                return false;
            }

            var lastRowIndex = totalRows - 1;
            var lastColumnIndex = totalColumns - 1;
            if (rowMin == rowMax && colMin == colMax && rowMin <= lastRowIndex && rowMin >= 0 && colMin <= lastColumnIndex &&
                colMin >= 0)
            {
                return matrix[rowMin][colMin] == target;
            }

            var pivotRow = (rowMin + rowMax) / 2;
            var pivotColumn = (colMin + colMax) / 2;
            if (matrix[pivotRow][pivotColumn] == target)
            {
                return true;
            }

            if (Math.Abs(rowMax - rowMin) <= 1 && Math.Abs(colMax - colMin) <= 1)
            {
                if (matrix[rowMin][colMin] == target)
                {
                    return true;
                }

                if (matrix[rowMin][colMax] == target)
                {
                    return true;
                }

                if (matrix[rowMax][colMin] == target)
                {
                    return true;
                }

                return matrix[rowMax][colMax] == target;
            }

            if (target < matrix[pivotRow][pivotColumn])
            {
                return SearchInSubMatrix(matrix, target, rowMin, pivotRow, colMin, pivotColumn, totalRows, totalColumns)
                       || SearchInSubMatrix(matrix, target, rowMin, pivotRow, pivotColumn + 1, colMax, totalRows, totalColumns)
                       || SearchInSubMatrix(matrix, target, pivotRow + 1, rowMax, colMin, pivotColumn, totalRows, totalColumns);
            }

            return SearchInSubMatrix(matrix, target, pivotRow, rowMax, pivotColumn, colMax, totalRows, totalColumns)
                   || SearchInSubMatrix(matrix, target, rowMin, pivotRow - 1, pivotColumn, colMax, totalRows, totalColumns)
                   || SearchInSubMatrix(matrix, target, pivotRow, rowMax, colMin, pivotColumn - 1, totalRows, totalColumns);
        }
        

        public static void TestSearch()
        {
            int[][] matrix = new int[5][];
            matrix[0] = new[] {1, 4, 7, 11, 15};
            matrix[1] = new[] {2, 5, 8, 12, 19};
            matrix[2] = new[] {3, 6, 9, 16, 22};
            matrix[3] = new[] {10, 13, 14, 17, 24};
            matrix[4] = new[] {18, 21, 23, 26, 30};
            Console.WriteLine($"find 5: {new Search2DMatrixSolver().SearchMatrix(matrix, 5)}");

            Console.WriteLine($"find 100: {new Search2DMatrixSolver().SearchMatrix(matrix, 100)}");
        }
    }
}