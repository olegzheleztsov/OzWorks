#region

using System;
using System.Collections.Generic;
using static System.Math;

#endregion

namespace Oz.Algorithms.Rod
{
    public static class IntExtensions
    {
        /// <summary>
        ///     Exponentiate numbers (find value^factor)
        /// </summary>
        public static int Exponentiate(this int value, int exponent)
        {
            if (exponent < 0)
            {
                throw new ArgumentException("Exponent should be greater or equal 0");
            }

            checked
            {
                var result = 1;
                var factor = value;
                while (exponent != 0)
                {
                    if (exponent % 2 == 1)
                    {
                        result *= factor;
                    }

                    factor *= factor;
                    exponent /= 2;
                }

                return result;
            }
        }

        public static int Exponentiate(this int value, int exponent, int modulus)
        {
            if (exponent < 0)
            {
                throw new ArgumentException("Exponent should be greater or equal 0");
            }

            var result = 1;
            var factor = value;
            checked
            {
                while (exponent != 0)
                {
                    if (exponent % 2 == 1)
                    {
                        result = result * factor % modulus;
                    }

                    factor = factor * factor % modulus;
                    exponent /= 2;
                }
            }

            return result;
        }

        /// <summary>
        ///     Returns factors of the number
        /// </summary>
        /// <param name="number">Number to be factored</param>
        /// <returns>Collection of the factors</returns>
        /// <exception cref="ArgumentException">Throws when number less than 1</exception>
        public static IEnumerable<int> FindFactors(this int number)
        {
            if (number < 1)
            {
                throw new ArgumentException("Number must be greater than 1");
            }

            var factors = new List<int>();
            var i = 2;
            while (i < number)
            {
                while (number % i == 0)
                {
                    factors.Add(i);
                    number /= i;
                }

                i++;
            }

            if (number > 1)
            {
                factors.Add(number);
            }

            factors.Sort();
            return factors;
        }

        /// <summary>
        ///     Returns factors of the number (optimized version)
        /// </summary>
        /// <param name="number">Number to be factored</param>
        /// <returns>Collection of the factors</returns>
        /// <exception cref="ArgumentException">Throws when number less than 1</exception>
        public static IEnumerable<int> FindFactorsImproved(this int number)
        {
            if (number < 1)
            {
                throw new ArgumentException("Number must be greater than 1");
            }

            var factors = new List<int>();

            while (number % 2 == 0)
            {
                factors.Add(2);
                number /= 2;
            }

            var i = 3;
            var maxFactor = (int) Sqrt(number);

            while (i <= maxFactor)
            {
                while (number % i == 0)
                {
                    factors.Add(i);
                    number /= i;
                    maxFactor = (int) Sqrt(number);
                }

                i += 2;
            }

            if (number > 1)
            {
                factors.Add(number);
            }

            factors.Sort();
            return factors;
        }

        public static IEnumerable<long> FindPrimes(this long maxNumber)
        {
            var isComposite = new bool[maxNumber + 1];
            for (long i = 4; i <= maxNumber; i += 2)
            {
                isComposite[i] = true;
            }

            var nextPrime = 3L;
            var stopAt = (long) Sqrt(maxNumber);

            while (nextPrime <= stopAt)
            {
                for (var i = nextPrime * 2; i <= maxNumber; i += nextPrime)
                {
                    isComposite[i] = true;
                }

                nextPrime += 2;
                while (nextPrime <= maxNumber && isComposite[nextPrime])
                {
                    nextPrime += 2;
                }
            }

            var primes = new List<long>();
            for (long i = 2; i <= maxNumber; i++)
            {
                if (!isComposite[i])
                {
                    primes.Add(i);
                }
            }

            return primes;
        }
    }
}