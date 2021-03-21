#region

using System;
using System.Linq;
using System.Numerics;
using Oz.Algorithms.Numerics;

#endregion

namespace Oz.Algorithms.Polynom
{
    public static class PolynomExtensions
    {
        /// <summary>
        /// Takes any polynom and transforms it to point representation in points of squares of -1 using fast fourier transform
        /// </summary>
        /// <param name="polynom">Source polynom</param>
        /// <returns>Points representation of polynom extended to power of 2 degree</returns>
        /// <exception cref="InvalidOperationException">Throws when source polynom representation is not <see cref="CoefficientPolynomRepresentation"/> by some reason</exception>
        public static Polynom Fft(this Polynom polynom)
        {
            polynom = polynom.ToCoefficientRepresentation();
            if (!(polynom.Representation is CoefficientPolynomRepresentation polynomRepresentation))
            {
                throw new InvalidOperationException(
                    $"Representation of the polynom should be {nameof(CoefficientPolynomRepresentation)}");
            }

            var sourceCoefficients = polynomRepresentation!.Coefficients.Select(v => v).ToArray();

            if (!Util.IsPowerOf2(sourceCoefficients.Length))
            {
                sourceCoefficients = sourceCoefficients.Extend(Util.NextPowerOf2(sourceCoefficients.Length), 0.0f);
            }

            var values = FftImpl(sourceCoefficients);
            var points = values.Select((t, i) => new Point(Util.RootOfUnit(values.Length, i), t)).ToList();

            return new Polynom(points, points.Count);
        }

        /// <summary>
        /// Inverts discrete fourier transform. Takes polynom with point representation and converts it to coefficient representation
        /// </summary>
        /// <param name="polynom">Points polynom</param>
        /// <returns>Coefficient polynom</returns>
        /// <exception cref="ArgumentException">Throws when input polynom is not points polynom</exception>
        /// <exception cref="InvalidOperationException">Throws when underlying representation is not points representation</exception>
        public static Polynom InvertDft(this Polynom polynom)
        {
            if (polynom.RepresentationKind != RepresentationKind.Points)
            {
                throw new ArgumentException("Polynom should be in points representation");
            }

            static Complex OmegaMinusKj(int k, int j, int n)
            {
                return new(Math.Cos(2.0 * Math.PI * k * j / n), -Math.Sin(2.0 * Math.PI * k * j / n));
            }

            var coefficients = new Complex[polynom.BoundDegree];
            if (!(polynom.Representation is PointValueRepresentation pointValueRepresentation))
            {
                throw new InvalidOperationException("Representation should not be null");
            }

            for (var j = 0; j < polynom.BoundDegree; j++)
            {
                var val = new Complex(0, 0);
                for (var k = 0; k < polynom.BoundDegree; k++)
                {
                    val += pointValueRepresentation!.Points[k].Y * OmegaMinusKj(k, j, polynom.BoundDegree);
                }

                val *= new Complex(1.0 / polynom.BoundDegree, 0);
                coefficients[j] = val;
            }

            return new Polynom(coefficients.Select(c => (float) c.Real).ToArray(), coefficients.Length);
        }

        /// <summary>
        /// Actual fast fourier transform, that transforms coefficient array to points 
        /// </summary>
        /// <param name="inputCoefficients">Input coefficients</param>
        /// <returns>Points with squares of -1 as base and values at this points</returns>
        private static Complex[] FftImpl(float[] inputCoefficients)
        {
            var degree = inputCoefficients.Length;
            if (degree == 1)
            {
                return new[] {new Complex(inputCoefficients[0], 0)};
            }

            var omegan = new Complex(MathF.Cos(2 * MathF.PI / degree), MathF.Sin(2 * MathF.PI / degree));
            var omega = new Complex(1, 0);
            var evenCoefficients = inputCoefficients.GetEvenIndices();
            var oddCoefficients = inputCoefficients.GetOddIndices();
            var evenValues = FftImpl(evenCoefficients);
            var oddValues = FftImpl(oddCoefficients);

            var result = new Complex[degree];
            for (var k = 0; k <= degree / 2 - 1; k++)
            {
                result[k] = evenValues[k] + omega * oddValues[k];
                result[k + degree / 2] = evenValues[k] - omega * oddValues[k];
                omega *= omegan;
            }

            return result;
        }
    }
}