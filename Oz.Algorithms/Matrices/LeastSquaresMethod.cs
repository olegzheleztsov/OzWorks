#region

using System;
using System.Linq;
using System.Text;

#endregion

namespace Oz.Algorithms.Matrices
{
    public class LeastSquaresMethod
    {
        public Vector Find(Point[] points, int degree)
        {
            var matrix = CreatePolinomeMatrix(points.Select(p => p.X).ToArray(), degree);
            var pseudoInvertedMatrix = (matrix.TransposedMatrix * matrix).Inverted * matrix.TransposedMatrix;
            var values = new Vector(points.Select(p => p.Y).ToArray());
            var result = pseudoInvertedMatrix * values;
            return result;
        }

        public string GetPrintablePolinomeString(Vector vector)
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < vector.Count; i++)
            {
                stringBuilder.Append($"{vector[i]} * X{i}{(i == vector.Count - 1 ? string.Empty : " +")}");
            }

            return stringBuilder.ToString();
        }

        private static FloatMatrix CreatePolinomeMatrix(float[] points, int degree)
        {
            var matrix = new FloatMatrix(points.Length, degree + 1);
            for (var i = 0; i < points.Length; i++)
            {
                for (var k = 0; k < matrix.Columns; k++)
                {
                    matrix[i, k] = MathF.Pow(points[i], k);
                }
            }

            return matrix;
        }
    }
}