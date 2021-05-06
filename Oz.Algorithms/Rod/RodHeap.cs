using System;
using static System.Math;

namespace Oz.Algorithms.Rod
{
    public class RodHeap<T>
    {
        private readonly T[] _values;
        private Comparison<T> _comparison;
        private int _heapSize;

        public RodHeap(T[] values, Comparison<T> comparison, int heapSize)
        {
            _values = values;
            _comparison = comparison;
            _heapSize = heapSize;
            MakeHeap(_values, _heapSize, _comparison);
        }

        private void MakeHeap(T[] values, int heapSize, Comparison<T> comparison)
        {
            for (var i = 0; i < heapSize; i++)
            {
                var index = i;
                while (index != 0)
                {
                    var parentIndex = ParentIndex(index);
                    if (comparison(values[index], values[parentIndex]) <= 0)
                    {
                        break;
                    }
                    Util.Exchange(ref values[index], ref values[parentIndex]);
                    index = parentIndex;
                }
            }
        }

        public void HeapSort()
        {
            for (var i = _heapSize - 1; i >= 0; i--)
            {
                Util.Exchange(ref _values[0], ref _values[_heapSize - 1]);
                _heapSize--;
                
                var index = 0;
                while (true)
                {
                    var child1 = LeftChildIndex(index);
                    var child2 = RightChildIndex(index);
                    if (child1 >= _heapSize)
                    {
                        child1 = index;
                    }

                    if (child2 >= _heapSize)
                    {
                        child2 = index;
                    }

                    if (_comparison(_values[index], _values[child1]) >= 0 &&
                        _comparison(_values[index], _values[child2]) >= 0)
                    {
                        break;
                    }

                    var swapChild = _comparison(_values[child1], _values[child2]) > 0
                        ? child1
                        : child2;
                    Util.Exchange(ref _values[index], ref _values[swapChild]);
                    index = swapChild;
                }
            }
        }

        public int HeapSize => _heapSize;

        public T RemoveTopItem()
        {
            var result = _values[0];
            _values[0] = _values[_heapSize - 1];
            var index = 0;
            while (true)
            {
                var child1 = LeftChildIndex(index);
                var child2 = RightChildIndex(index);
                if (child1 >= _heapSize)
                {
                    child1 = index;
                }

                if (child2 >= _heapSize)
                {
                    child2 = index;
                }

                if (_comparison(_values[index], _values[child1]) >= 0 &&
                    _comparison(_values[index], _values[child2]) >= 0)
                {
                    break;
                }

                var swapChild = _comparison(_values[child1], _values[child2]) > 0
                    ? child1
                    : child2;
                Util.Exchange(ref _values[index], ref _values[swapChild]);
                index = swapChild;
            }

            _heapSize--;
            return result;
        }

        public int LeftChildIndex(int index)
            => 2 * index + 1;

        public int RightChildIndex(int index)
            => 2 * index + 2;

        public int ParentIndex(int index)
            => (int) Floor((index - 1) / 2.0);

        public T LeftChild(int index)
        {
            return _values[LeftChildIndex(index)];
        }

        public T RightChild(int index)
        {
            return _values[RightChildIndex(index)];
        }

        public T Parent(int index)
        {
            return _values[ParentIndex(index)];
        }
    }
}