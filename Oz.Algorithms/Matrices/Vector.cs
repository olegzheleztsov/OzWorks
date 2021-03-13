#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Oz.Algorithms.Matrices
{
    public class Vector : IEnumerable<float>
    {
        private readonly float[] _array;

        public Vector(int size)
            : this(new float[size])
        {
        }

        public Vector(float[] array)
        {
            _array = array;
        }

        public float this[int index]
        {
            get => _array[index];
            set => _array[index] = value;
        }

        public int Count => _array.Length;

        public IEnumerator<float> GetEnumerator()
        {
            return ((IEnumerable<float>) _array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static Vector operator *(FloatMatrix matrix, Vector vector)
        {
            if (matrix.Columns != vector.Count)
            {
                throw new ArgumentException(
                    $"Size of vector should be equal columns count in matrix. Matrix size: ({matrix.Rows},{matrix.Columns}), Vector size: {vector.Count}");
            }

            var result = new Vector(vector.Count);

            for (var i = 0; i < matrix.Rows; i++)
            {
                var sum = 0.0f;
                for (var j = 0; j < vector.Count; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }

                result[i] = sum;
            }

            return result;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("( ");
            stringBuilder.Append(string.Join(", ", _array));
            stringBuilder.Append(" )");
            return stringBuilder.ToString();
        }
    }
}