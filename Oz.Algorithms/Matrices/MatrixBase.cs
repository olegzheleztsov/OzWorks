using System;
using System.Text;

namespace Oz.Algorithms.Matrices
{
    public class MatrixBase<T>
    {
        protected readonly T[] _array;

        public MatrixBase(T[,] array2D)
        {
            Rows = array2D.GetUpperBound(0) + 1;
            Columns = array2D.GetUpperBound(1) + 1;
            _array = new T[Rows * Columns];
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    _array[i * Columns + j] = array2D[i, j];
                }
            }
        }

        public MatrixBase(int rows, int columns)
            : this(rows, columns, new T[rows * columns])
        {
        }

        public MatrixBase(int rows, int columns, T[] array)
        {
            if (array.Length != rows * columns)
            {
                throw new ArgumentException("Invalid array length");
            }

            Rows = rows;
            Columns = columns;
            _array = array;
        }

        public MatrixBase(int rows, int columns, T initialValue)
            : this(rows, columns, new T[rows * columns])
        {
            Array.Fill(_array, initialValue);
        }

        public bool IsSquareMatrix => Rows == Columns;
        
        public T this[int row, int column]
        {
            get
            {
                AssertIndices(row, column);
                return _array[Columns * row + column];
            }
            set
            {
                AssertIndices(row, column);
                _array[Columns * row + column] = value;
            }
        }

        public int Rows { get; }

        public int Columns { get; }

        public T[,] Transposed
        {
            get
            {
                if (Rows != Columns)
                {
                    throw new InvalidOperationException("Only square matrices can be transposed");
                }

                var result = new T[Rows, Columns];
                for (var i = 0; i < Rows; i++)
                {
                    for (var j = 0; j < Columns; j++)
                    {
                        result[i, j] = this[j, i];
                    }
                }

                return result;
            }
        }

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

        public T[,] ToArray2D()
        {
            var array2d = new T[Rows, Columns];
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    array2d[i, j] = this[i, j];
                }
            }

            return array2d;
        }
        
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    stringBuilder.Append(j == Columns - 1 ? $"{this[i, j]}" : $"{this[i, j]}, ");
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}