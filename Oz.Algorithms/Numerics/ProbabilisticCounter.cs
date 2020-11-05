using System;
using System.Collections;
using System.Collections.Generic;

namespace Oz.Algorithms.Numerics
{
    public class ProbabilisticCounter : IEnumerator<int>, IEnumerable<int>
    {
        private readonly Func<int, int> _getValue;
        private readonly int _maxPower;
        private readonly IRandomSource _randomSource;
        private int _currentIndex;

        public ProbabilisticCounter(int maxPower, Func<int, int> getValue, IRandomSource randomSource = null)
        {
            _maxPower = maxPower;
            _getValue = getValue;
            _randomSource = randomSource;
            _randomSource = randomSource ?? new DefaultRandomSource();
        }

        public IEnumerator<int> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public bool MoveNext()
        {
            if (_currentIndex <= 0)
            {
                _currentIndex++;
            }
            else
            {
                var next = _getValue(_currentIndex + 1);
                var cur = _getValue(_currentIndex);
                var incrementProbability = 1.0 / (next - cur);
                if (_randomSource.RandomDouble < incrementProbability)
                {
                    _currentIndex++;
                }
            }

            return _currentIndex < 1 << _maxPower;
        }

        public void Reset()
        {
            _currentIndex = -1;
        }

        public int Current => _getValue(_currentIndex);

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}