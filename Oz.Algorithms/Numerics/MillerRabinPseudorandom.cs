using System;
using System.Numerics;

namespace Oz.Algorithms.Numerics
{
    public class MillerRabinPseudorandom
    {
        private readonly BigInteger _value;

        public MillerRabinPseudorandom(BigInteger value)
        {
            _value = value;
            if (_value % 2 == 0)
            {
                throw new ArgumentException("value should be odd");
            }

            if (_value < 3)
            {
                throw new ArgumentException("value should be greater or equals than 3");
            }
        }

        public bool MayBePrime(int tryCount)
        {
            var randomSource = new DefaultRandomSource();
            for (var j = 0; j < tryCount; j++)
            {
                var a = randomSource.RandomBigInteger(1, _value - 1);
                if (Witness(a, _value))
                {
                    return false;
                }
            }

            return true;
        }
        
        private bool Witness(BigInteger a, BigInteger n)
        {
            if (n % 2 == 0)
            {
                throw new ArgumentException($"n should be odd. Actual n: {n}");
            }

            var (t, u) = SplitEvenInteger(n - 1);
            var x0 = new ModularExponentiation(a, u, n).Value;
            for (var i = 0; i < t; i++)
            {
                var xPrev = x0;
                x0 = (xPrev * xPrev) % n;
                if (x0 == 1 && xPrev != 1 && xPrev != (n - 1))
                {
                    return true;
                }
            }

            return x0 != 1;
        }
        
        public static IntegerSplitting SplitEvenInteger(BigInteger number)
        {
            if (number % 2 != 0)
            {
                throw new ArgumentException($"Argument should be even. Actual argument: {number}");
            }

            BigInteger counter = 0;
            BigInteger num = number;
            while (num % 2 == 0)
            {
                num /= 2;
                counter++;
            }

            return new IntegerSplitting(counter, num);
        }
    }

    public record IntegerSplitting(BigInteger Power, BigInteger Mult);
}