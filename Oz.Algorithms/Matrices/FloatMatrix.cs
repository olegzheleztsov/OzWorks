using System;
using System.Text;

namespace Oz.Algorithms.Matrices
{
    public class FloatMatrix : MatrixBase<float>
    {
        public FloatMatrix(float[,] array2D) : base(array2D)
        {
        }

        public FloatMatrix(int rows, int columns) : base(rows, columns)
        {
        }

        public FloatMatrix(int rows, int columns, float[] array) : base(rows, columns, array)
        {
        }

        public FloatMatrix Multiply(FloatMatrix other)
        {
            if (Columns != other.Rows)
            {
                throw new ArgumentException($"Dimensions invalid: ({other.Rows}, {other.Columns})");
            }

            var result = new FloatMatrix(Rows, other.Columns);
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < other.Columns; j++)
                {
                    var r = 0f;
                    for (var k = 0; k < Columns; k++)
                    {
                        r += this[i, k] * other[k, j];
                    }

                    result[i, j] = r;
                }
            }

            return result;
        }

        public FloatMatrix MultiplyRecursively(FloatMatrix other)
        {
            var first = new FloatMatrixRegion(this, 0, 0, Rows, Columns);
            var second = new FloatMatrixRegion(other, 0, 0, other.Rows, other.Columns);
            var resultMatrix = new FloatMatrix(Rows, other.Columns);
            var result =
                new FloatMatrixRegion(resultMatrix, 0, 0, Rows, other.Columns);
            FloatMatrixRegion.MultiplyRegions(first, second, result);
            return resultMatrix;
        }

        public FloatMatrix FastMultiply(FloatMatrix other)
        {
            var first = new FloatMatrixRegion(this, 0, 0, Rows, Columns);
            var second = new FloatMatrixRegion(other, 0, 0, other.Rows, other.Columns);
            var resultMatrix = new FloatMatrix(Rows, other.Columns);
            var result =
                new FloatMatrixRegion(resultMatrix, 0, 0, Rows, other.Columns);
            FloatMatrixRegion.StrassenFastMultiply(first, second, result);
            return resultMatrix;
        }

        public static FloatMatrix operator *(FloatMatrix first, FloatMatrix second)
        {
            return first.Multiply(second);
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