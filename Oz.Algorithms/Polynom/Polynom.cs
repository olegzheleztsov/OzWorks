#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oz.Algorithms.Matrices;

#endregion

namespace Oz.Algorithms.Polynom
{
    public class Polynom
    {
        public Polynom(IEnumerable<Point> points, int boundDegree)
        {
            Representation = new PointValueRepresentation(points, boundDegree);
            RepresentationKind = RepresentationKind.Points;
        }

        public Polynom(IEnumerable<float> coefficients, int boundDegree)
        {
            Representation = new CoefficientPolynomRepresentation(coefficients, boundDegree);
            RepresentationKind = RepresentationKind.Coefficients;
        }

        private Polynom(IPolynomRepresentation representation)
        {
            Representation = representation;
            RepresentationKind = representation switch
            {
                CoefficientPolynomRepresentation => RepresentationKind.Coefficients,
                PointValueRepresentation => RepresentationKind.Points,
                _ => throw new ArgumentException($"Unsupported representation kind: {representation.GetType().Name}")
            };
        }

        public Polynom(Polynom other, int boundDegree)
        {
            switch (other.RepresentationKind)
            {
                case RepresentationKind.Coefficients:
                {
                    if (other.Representation is CoefficientPolynomRepresentation coeffsRepresentation)
                    {
                        var values = coeffsRepresentation.Coefficients;
                        Representation = new CoefficientPolynomRepresentation(values, boundDegree);
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }

                    RepresentationKind = RepresentationKind.Coefficients;
                }
                    break;
                case RepresentationKind.Points:
                {
                    if (other.Representation is PointValueRepresentation pointsRepresentation)
                    {
                        var points = pointsRepresentation.Points;
                        Representation = new PointValueRepresentation(points, boundDegree);
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }

                    RepresentationKind = RepresentationKind.Points;
                }
                    break;
                default:
                    // ReSharper disable once CA2208
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal IPolynomRepresentation Representation { get; }

        public RepresentationKind RepresentationKind { get; }

        public int BoundDegree => Representation.BoundDegree;

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            var target = this;
            if (RepresentationKind != RepresentationKind.Coefficients)
            {
                target = ToCoefficientRepresentation();
            }

            if (!(target.Representation is CoefficientPolynomRepresentation representation))
            {
                throw new InvalidOperationException(
                    $"Representation should be of type: {nameof(CoefficientPolynomRepresentation)}");
            }

            var coefficients = representation.Coefficients;
            for (var i = 0; i < coefficients.Length; i++)
            {
                if (!Util.Approximately(coefficients[i], 0))
                {
                    stringBuilder.Append(
                        $"{coefficients[i]}X^{i}{(i != coefficients.Length - 1 ? " + " : string.Empty)}");
                }
            }

            return stringBuilder.ToString();
        }

        public Polynom ToCoefficientRepresentation()
        {
            if (RepresentationKind != RepresentationKind.Points)
            {
                return new Polynom(this, BoundDegree);
            }

            var matrix = new FloatMatrix(BoundDegree, BoundDegree);
            var points = (Representation as PointValueRepresentation)?.Points ?? Array.Empty<Point>();
            for (var i = 0; i < BoundDegree; i++)
            {
                for (var j = 0; j < BoundDegree; j++)
                {
                    matrix[i, j] = MathF.Pow((float) points[i].X.Real, j);
                }
            }

            var vector = new Vector(points.Select(p => (float) p.Y.Real).ToArray());
            var result = matrix.Inverted * vector;
            return new Polynom(result, BoundDegree);
        }

        public static Polynom operator *(Polynom first, Polynom second)
        {
            var firstCoef = first.ToCoefficientRepresentation();
            var secondCoef = second.ToCoefficientRepresentation();

            var maxDegree = Math.Max(firstCoef.BoundDegree, secondCoef.BoundDegree);
            if (!Util.IsPowerOf2(maxDegree))
            {
                maxDegree = Util.NextPowerOf2(maxDegree);
            }

            maxDegree *= 2;

            var first2N = new Polynom(firstCoef, maxDegree);
            var second2N = new Polynom(secondCoef, maxDegree);

            var firstValues = first2N.Fft();
            var secondValues = second2N.Fft();
            var firstValuesRepresentation = firstValues.Representation as PointValueRepresentation;
            var secondValuesRepresentation = secondValues.Representation as PointValueRepresentation;
            var multRepr = firstValuesRepresentation * secondValuesRepresentation;
            var resultPoly = new Polynom(multRepr.Points, multRepr.Points.Length);
            var finalPolynom = resultPoly.InvertDft();
            var finalCoefficients = (finalPolynom.Representation as CoefficientPolynomRepresentation)?.Coefficients;
            var discardedZeros = DiscardZerosFromRight(finalCoefficients);
            return new Polynom(discardedZeros, discardedZeros.Length);
        }
        

        public static Polynom operator +(Polynom first, Polynom second)
        {
            var firstCoefficientRepresentation = first.ToCoefficientRepresentation();
            var secondCoefficientRepresentation = second.ToCoefficientRepresentation();
            var firstRepr1 = firstCoefficientRepresentation.Representation as CoefficientPolynomRepresentation;
            var secondRepr2 = secondCoefficientRepresentation.Representation as CoefficientPolynomRepresentation;
            return new Polynom(firstRepr1 + secondRepr2);
        }

        public static Polynom operator -(Polynom first)
        {
            var firstAsCoeffs = first.ToCoefficientRepresentation();
            var repr = firstAsCoeffs.Representation as CoefficientPolynomRepresentation;
            return new Polynom(-repr);
        }

        public static Polynom operator -(Polynom first, Polynom second)
        {
            var firstAsCoeffs = first.ToCoefficientRepresentation();
            var secondAsCoeffs = second.ToCoefficientRepresentation();
            var repr1 = firstAsCoeffs.Representation as CoefficientPolynomRepresentation;
            var repr2 = secondAsCoeffs.Representation as CoefficientPolynomRepresentation;
            return new Polynom(repr1 - repr2);
        }

        private static float[] DiscardZerosFromRight(float[] array)
        {
            switch (array.Length)
            {
                case 0:
                    return Array.Empty<float>();
                case 1 when Util.Approximately(array[0], 0f):
                    return Array.Empty<float>();
            }

            var index = array.Length - 1;
            for (;index >= 0; index--)
            {
                if (!Util.Approximately(array[index], 0f))
                {
                    break;
                }
            }

            var result = new float[index + 1];
            Array.Copy(array, result, result.Length);
            return result;
        }
    }
}