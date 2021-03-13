using System;
using System.Text;

namespace Oz.Algorithms.Matrices
{
    public class IntegerMatrix : MatrixBase<int>, IEquatable<IntegerMatrix>, ICloneable
    {
        public IntegerMatrix(int[,] array2D)
            : base(array2D)
        {
        }

        public IntegerMatrix(byte[,] array2D)
            : base(array2D.ConvertToIntegerArray2D())
        {
        }

        public IntegerMatrix(int rows, int columns)
            : base(rows, columns)
        {
        }

        public IntegerMatrix(int rows, int columns, int[] array)
            : base(rows, columns, array)
        {
        }

        public IntegerMatrix(IntegerMatrix otherMatrix)
            : this(otherMatrix.Rows, otherMatrix.Columns)
        {
            Array.Copy(otherMatrix._array, _array, Rows * Columns);
        }

        public object Clone()
        {
            return new IntegerMatrix(this);
        }
        

        public bool Equals(IntegerMatrix other)
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
                    if (this[i, j] != other[i, j])
                    {
                        isEqual = false;
                        break;
                    }
                }
            }

            return isEqual;
        }

        public IntegerMatrix Multiply(IntegerMatrix other)
        {
            if (Columns != other.Rows)
            {
                throw new ArgumentException($"Dimensions invalid: ({other.Rows}, {other.Columns})");
            }

            var result = new IntegerMatrix(Rows, other.Columns);
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < other.Columns; j++)
                {
                    var r = 0;
                    for (var k = 0; k < Columns; k++)
                    {
                        r += this[i, k] * other[k, j];
                    }

                    result[i, j] = r;
                }
            }

            return result;
        }

        private IntegerMatrix _MultiplyRecursively(IntegerMatrix other)
        {
            if (Rows != Columns || other.Rows != other.Columns || Rows != other.Rows)
            {
                throw new ArgumentException("Recursive multiplication allowed only between square matrices");
            }
            if (!Util.IsPowerOf2(Rows))
            {
                throw new ArgumentException("Matrix should be  power of 2");
            }
            
            var first = new IntegerMatrixRegion(this, 0, 0, Rows, Columns);
            var second = new IntegerMatrixRegion(other, 0, 0, other.Rows, other.Columns);
            var resultMatrix = new IntegerMatrix(Rows, other.Columns);
            var result = new IntegerMatrixRegion(resultMatrix, 0, 0, Rows, other.Columns);
            IntegerMatrixRegion.MultiplyRegions(first, second, result);
            return resultMatrix;
        }

        private IntegerMatrix _FastMultiply(IntegerMatrix other)
        {
            if (Rows != Columns || other.Rows != other.Columns || Rows != other.Rows)
            {
                throw new ArgumentException("Recursive multiplication allowed only between square matrices");
            }

            if (!Util.IsPowerOf2(Rows))
            {
                throw new ArgumentException("Matrix should be  power of 2");
            }

            var first = new IntegerMatrixRegion(this, 0, 0, Rows, Columns);
            var second = new IntegerMatrixRegion(other, 0, 0, other.Rows, other.Columns);
            var resultMatrix = new IntegerMatrix(Rows, other.Columns);
            var result = new IntegerMatrixRegion(resultMatrix, 0, 0, Rows, other.Columns);
            IntegerMatrixRegion.StrassenFastMultiply(first, second, result);
            return resultMatrix;
        }

        public static IntegerMatrix operator *(IntegerMatrix first, IntegerMatrix second)
        {
            if (first.IsSquareMatrix && second.IsSquareMatrix && first.Rows == second.Rows)
            {
                return MultiplyFast(first, second);
            }

            return first.Multiply(second);
        }

        public static bool operator ==(IntegerMatrix first, IntegerMatrix second)
        {
            return first?.Equals(second) ?? ReferenceEquals(second, null);
        }

        public static bool operator !=(IntegerMatrix first, IntegerMatrix second)
        {
            if (ReferenceEquals(first, null))
            {
                return !ReferenceEquals(second, null);
            }

            return !first.Equals(second);
        }

        public static IntegerMatrix MultiplyRecursively(IntegerMatrix first, IntegerMatrix second)
        {
            return first._MultiplyRecursively(second);
        }

        public static IntegerMatrix MultiplyFast(IntegerMatrix first, IntegerMatrix second)
        {
            return first._FastMultiply(second);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((IntegerMatrix) obj);
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
                        hashCode = this[i, j].GetHashCode();
                    }
                    else
                    {
                        hashCode ^= this[i, j].GetHashCode();
                    }
                }
            }

            return hashCode;
        }


    }
}