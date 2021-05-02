#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Oz.Algorithms.Arrays
{
    public class LowerTriangularArray<T> : IEnumerable<T>
    {
        private readonly T[] _array;

        public LowerTriangularArray(int matrixSize)
        {
            MatrixSize = matrixSize;
            _array = new T[matrixSize * (matrixSize + 1) / 2];
        }

        public int MatrixSize { get; }

        public T this[int row, int column]
        {
            get
            {
                var index = row * (row + 1) / 2 + column;

                if (!CheckIndex(index, row, column))
                {
                    throw new IndexOutOfRangeException($"Indices {row}x{column} invalid");
                }

                return _array[index];
            }
            set
            {
                var index = row * (row + 1) / 2 + column;

                if (!CheckIndex(index, row, column))
                {
                    throw new IndexOutOfRangeException($"Indices {row}x{column} invalid");
                }

                _array[index] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) _array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool CheckIndex(int testIndex, int row, int column)
        {
            return testIndex < _array.Length && column <= row;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < MatrixSize; i++)
            {
                for (var j = 0; j <= i; j++)
                {
                    stringBuilder.Append($"{this[i, j],10}");
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}