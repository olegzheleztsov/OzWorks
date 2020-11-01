using System;
using System.Collections.Generic;
using System.Linq;
using Oz.Algorithms.Sort;

namespace Oz.Algorithms.Numerics
{
    public class ShuffledArray<T>
    {
        private readonly int[] _priorityArray;
        private readonly IRandomSource _randomSource;
        private readonly T[] _sourceArray;

        public ShuffledArray(T[] sourceArray) : this(sourceArray, new DefaultRandomSource())
        {
        }

        public ShuffledArray(T[] sourceArray, IRandomSource randomSource)
        {
            _sourceArray = sourceArray ?? throw new NullReferenceException(nameof(sourceArray));
            _randomSource = randomSource;
            _priorityArray = new int[_sourceArray.Length];
            FillPriorities();
        }

        private T[] Value
        {
            get
            {
                if (_sourceArray.Length == 0)
                {
                    return new T[] { };
                }

                var array = _sourceArray.Zip(_priorityArray, (val, priority) => new KeyValuePair<T, int>(val, priority))
                    .ToArray();
                var sorter = new MergeSorter<KeyValuePair<T, int>>();
                sorter.Sort(array, kvp => kvp.Value);
                return array.Select(kvp => kvp.Key).ToArray();
            }
        }

        private void FillPriorities()
        {
            var n3 = _priorityArray.Length * _priorityArray.Length * _priorityArray.Length;
            for (var i = 0; i < _priorityArray.Length; i++)
            {
                _priorityArray[i] = _randomSource.RandomValue(0, n3);
            }
        }

        public static implicit operator T[](ShuffledArray<T> source)
        {
            return source.Value;
        }
    }
}