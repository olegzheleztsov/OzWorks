using System;
using System.Collections.Generic;
using System.Text;

namespace Oz.Algorithms.Matrices
{
    public class FloatMatrix : MatrixBase<float>, IEquatable<FloatMatrix>, ICloneable
    {
        public FloatMatrix(byte[,] array2D) : base(array2D.ConvertToFloatArray2D()) {}
        public FloatMatrix(float[,] array2D) : base(array2D)
        {
        }

        public FloatMatrix(int rows, int columns) : base(rows, columns)
        {
        }

        public FloatMatrix(int rows, int columns, float[] array) : base(rows, columns, array)
        {
        }

        public FloatMatrix(FloatMatrix other) : this(other.Rows, other.Columns)
        {
            Array.Copy(other._array, _array, Rows * Columns);
        }
        
        public object Clone()
        {
            return new FloatMatrix(this);
        }

        public bool Equals(FloatMatrix other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(other, this))
            {
                return true;
            }

            if (other.Rows != Rows || other.Columns != Columns)
            {
                return false;
            }

            var isEqual = true;
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    if (Util.Compare(this[i, j], other[i, j]) != 0)
                    {
                        isEqual = false;
                        break;
                    }
                }
            }

            return isEqual;
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

        private FloatMatrix _MultiplyRecursively(FloatMatrix other)
        {
            if (Rows != Columns || other.Rows != other.Columns || Rows != other.Rows)
            {
                throw new ArgumentException("Recursive multiplication allowed only between square matrices");
            }

            var first = new FloatMatrixRegion(this, 0, 0, Rows, Columns);
            var second = new FloatMatrixRegion(other, 0, 0, other.Rows, other.Columns);
            var resultMatrix = new FloatMatrix(Rows, other.Columns);
            var result =
                new FloatMatrixRegion(resultMatrix, 0, 0, Rows, other.Columns);
            FloatMatrixRegion.MultiplyRegions(first, second, result);
            return resultMatrix;
        }

        private FloatMatrix _FastMultiply(FloatMatrix other)
        {
            if (Rows != Columns || other.Rows != other.Columns || Rows != other.Rows)
            {
                throw new ArgumentException("Recursive multiplication allowed only between square matrices");
            }

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
            if (first.IsSquareMatrix && second.IsSquareMatrix && first.Rows == second.Rows)
            {
                return MultiplyFast(first, second);
            }

            return first.Multiply(second);
        }

        public static bool operator ==(FloatMatrix first, FloatMatrix second)
        {
            return first?.Equals(second) ?? ReferenceEquals(second, null);
        }

        public static bool operator !=(FloatMatrix first, FloatMatrix second)
        {
            if (ReferenceEquals(first, null))
            {
                return !ReferenceEquals(second, null);
            }

            return !first.Equals(second);
        }

        public static FloatMatrix MultiplyRecursively(FloatMatrix first, FloatMatrix second)
        {
            return first._MultiplyRecursively(second);
        }

        public static FloatMatrix MultiplyFast(FloatMatrix first, FloatMatrix second)
        {
            return first._FastMultiply(second);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((FloatMatrix) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        hashCode = ((int) this[i, j]).GetHashCode();
                    }
                    else
                    {
                        hashCode ^= ((int) this[i, j]).GetHashCode();
                    }
                }
            }

            return hashCode;
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