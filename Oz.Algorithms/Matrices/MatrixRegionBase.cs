using System;

namespace Oz.Algorithms.Matrices
{
    public class MatrixRegionBase<T>
    {
        private readonly MatrixBase<T> _matrixBase;
        private readonly int _startColumn;
        private readonly int _startRow;


        public MatrixRegionBase(MatrixBase<T> matrixBase, int startRow, int startColumn, int rows, int columns)
        {
            _matrixBase = matrixBase;
            _startRow = startRow;
            _startColumn = startColumn;
            Rows = rows;
            Columns = columns;
        }

        public MatrixRegionBase(MatrixRegionBase<T> matrixRegionBase, int offsetRow, int offsetColumn, int rows,
            int columns)
        {
            _matrixBase = matrixRegionBase._matrixBase;
            _startRow = matrixRegionBase._startRow + offsetRow;
            _startColumn = matrixRegionBase._startColumn + offsetColumn;
            Rows = rows;
            Columns = columns;
        }

        public T this[int row, int column]
        {
            get
            {
                AssertIndices(row, column);
                return _matrixBase[_startRow + row, _startColumn + column];
            }
            set
            {
                AssertIndices(row, column);
                _matrixBase[_startRow + row, _startColumn + column] = value;
            }
        }

        public int Rows { get; }

        public int Columns { get; }

        private void AssertIndices(int row, int column)
        {
            if (row < 0 || row >= Rows)
            {
                throw new IndexOutOfRangeException($"{nameof(row)}: {row}");
            }

            if (column < 0 || column >= Columns)
            {
                throw new IndexOutOfRangeException($"{nameof(column)}: {column}");
            }
        }
    }
}