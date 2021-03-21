using System;
using System.Collections.Generic;
using System.Linq;

namespace Oz.Algorithms.Polynom
{
    internal class CoefficientPolynomRepresentation : IPolynomRepresentation
    {
        private readonly float[] _coefficients;

        public int BoundDegree => _coefficients.Length;

        public float[] Coefficients => _coefficients;

        private float this[int i]
        {
            get => _coefficients[i];
            set => _coefficients[i] = value;
        }
        
        internal CoefficientPolynomRepresentation(IEnumerable<float> coefficients, int boundDegree)
        {
            if (boundDegree < 1)
            {
                throw new ArgumentException($"Bound degree should be not less than 1. Current value: {boundDegree}");
            }
            
            coefficients ??= new List<float>();
            var tempCoefficients = coefficients.ToArray();
            _coefficients = new float[boundDegree];
            for (var i = 0; i < _coefficients.Length; i++)
            {
                if (i < tempCoefficients.Length)
                {
                    _coefficients[i] = tempCoefficients[i];
                }
                else
                {
                    _coefficients[i] = 0.0f;
                }
            }
        }

        internal CoefficientPolynomRepresentation(CoefficientPolynomRepresentation other, int boundDegree)
            : this(other._coefficients, boundDegree)
        {
        }

        internal CoefficientPolynomRepresentation(int boundDegree)
            : this(Array.Empty<float>(), boundDegree)
        {
        }

        public float GetValue(float x)
        {
            var result = _coefficients[^1];
            for (var i = _coefficients.Length - 1; i >= 1; i--)
            {
                result *= x;
                result += _coefficients[i - 1];
            }

            return result;
        }

        public static CoefficientPolynomRepresentation operator +(CoefficientPolynomRepresentation repr1,
            CoefficientPolynomRepresentation repr2)
        {
            var degree = Math.Max(repr1.BoundDegree, repr2.BoundDegree);
            var result = new float[degree];

            for (int i = 0; i < degree; i++)
            {
                var val1 = i < repr1.BoundDegree ? repr1[i] : 0.0f;
                var val2 = i < repr2.BoundDegree ? repr2[i] : 0.0f;
                result[i] = val1 + val2;
            }

            return new CoefficientPolynomRepresentation(result, degree);
        }

        public static CoefficientPolynomRepresentation operator -(CoefficientPolynomRepresentation repr1,
            CoefficientPolynomRepresentation repr2)
        {
            var repr2Neg = -repr2;
            return repr1 + repr2Neg;
        }

        public static CoefficientPolynomRepresentation operator -(CoefficientPolynomRepresentation repr)
        {
            var result = new float[repr.BoundDegree];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = -repr[i];
            }

            return new CoefficientPolynomRepresentation(result, repr.BoundDegree);
        }
        
        
    }
}