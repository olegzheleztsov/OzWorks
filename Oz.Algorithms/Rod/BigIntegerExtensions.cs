using System;
using System.Collections.Generic;
using System.Numerics;

namespace Oz.Algorithms.Rod
{
    public static class BigIntegerExtensions
    {
        /// <summary>
        /// Exponentiate numbers (find value^factor)
        /// </summary>
        public static BigInteger Exponentiate(this BigInteger value, BigInteger exponent)
        {
            if (exponent < 0)
            {
                throw new ArgumentException($"Exponent should be greater or equal 0");
            }
            
            BigInteger result = 1;
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
        
        public static BigInteger Exponentiate(this BigInteger value, BigInteger exponent, BigInteger modulus)
        {
            if (exponent < 0)
            {
                throw new ArgumentException("Exponent should be greater or equal 0");
            }
            BigInteger result = 1;
            var factor = value;
            while (exponent != 0)
            {
                if (exponent % 2 == 1)
                {
                    result = (result * factor) % modulus;
                }

                factor = (factor * factor) % modulus;
                exponent /= 2;
            }

            return result;
        }
        
        public static bool IsPrime(this BigInteger p, int maxTests)
        {
            var randomSource = new DefaultRandomSource();
            for (int test = 1; test <= maxTests; test++)
            {
                var testNumber = randomSource.RandomBigInteger(1, p - 1);
                if (testNumber.Exponentiate(p - 1, p) != 1)
                {
                    return false;
                }
            }

            return true;
        }
        
        
    }
}