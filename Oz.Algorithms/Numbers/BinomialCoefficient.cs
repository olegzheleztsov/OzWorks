using System;

namespace Oz.Algorithms.Numbers
{
    public class BinomialCoefficient
    {
        private readonly int _k;
        private readonly int _n;

        public BinomialCoefficient(int k, int n)
        {
            _k = k;
            _n = n;
            if (_n < 0)
            {
                throw new ArgumentException(nameof(n));
            }

            if (_k < 0 || _k > _n)
            {
                throw new ArgumentException(nameof(k));
            }
        }

        public int Value => Find(_k, _n);

        private static int Find(int k, int n)
        {
            if (k == 0)
            {
                return 1;
            }

            if (k == n)
            {
                return 1;
            }

            if (k > n)
            {
                return 0;
            }

            return Find(k, n - 1) + Find(k - 1, n - 1);
        }
    }
}