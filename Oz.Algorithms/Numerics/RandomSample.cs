using System;
using System.Collections.Generic;

namespace Oz.Algorithms.Numerics
{
    public class RandomSample
    {
        private readonly int _m;
        private readonly int _n;
        private readonly IRandomSource _randomSource;

        public RandomSample(int m, int n, IRandomSource randomSource = null)
        {
            _m = m;
            _n = n;
            _randomSource = randomSource;
            if (_m > _n)
            {
                throw new ArgumentException("m must be less or equal than n");
            }

            if (_n < 1)
            {
                throw new ArgumentException("n mus be greater or equal than 1");
            }

            _randomSource = randomSource ?? new DefaultRandomSource();
        }

        public int[] Sample => FindSample(_m, _n).ToArray();

        private List<int> FindSample(int m, int n)
        {
            if (m == 0)
            {
                return new List<int>();
            }

            var list = FindSample(m - 1, n - 1);
            var i = _randomSource.RandomValue(1, n + 1);
            list.Add(list.Contains(i) ? n : i);

            return list;
        }
    }
}