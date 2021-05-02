using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Oz.Algorithms.Arrays
{
    public class LeftUpperTriangularArray<T> : IEnumerable<T>
    {
        private readonly T[] _array;

        public LeftUpperTriangularArray(int matrixSize)
        {
            MatrixSize = matrixSize;
            _array = new T[matrixSize * (matrixSize + 1) / 2];
        }
        
        public int MatrixSize { get; }
        
        public T this[int row, int column]
        {
            get
            {
                var index = GetIndex(row, column);
                if (!CheckIndex(index, row, column))
                {
                    throw new IndexOutOfRangeException($"Indices {row}x{column} invalid");
                }

                return _array[index];
            }
            set
            {
                var index = GetIndex(row, column);
                if (!CheckIndex(index, row, column))
                {
                    throw new IndexOutOfRangeException($"Indices {row}x{column} invalid");
                }

                _array[index] = value;
            }
        }
        
        private int GetIndex(int row, int column)
        {
            var totalSize = MatrixSize * (MatrixSize + 1) / 2;
            var subMatrixSize = (MatrixSize - row - 1) * (MatrixSize - row) / 2;
            var columnSub = MatrixSize -row - column - 1;
            return totalSize - subMatrixSize - columnSub - 1;
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
            return testIndex < _array.Length && column < (MatrixSize - row);
        }

        public void PrintArray()
        {
            Console.WriteLine(string.Join(", ", _array));
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (var row = 0; row < MatrixSize; row++)
            {
                for (var column = 0; column < (MatrixSize - row); column++)
                {
                    stringBuilder.Append(CheckIndex(GetIndex(row, column), row, column)
                        ? $"{this[row, column],10}"
                        : $"{' ',10}");
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}