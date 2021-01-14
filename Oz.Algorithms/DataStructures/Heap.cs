using System;
using System.Collections;
using System.Collections.Generic;

namespace Oz.Algorithms.DataStructures
{
    public class Heap<T> : IEnumerable<T>
    {
        private readonly Comparison<int> _comparison;
        private readonly Func<T, int> _keySelector;
        private T[] _data;
        private int _heapSize;

        public Heap(T[] data, Func<T, int> keySelector, Comparison<int> comparison)
        {
            _data = data;
            _keySelector = keySelector;
            HeapSize = _data.Length;
            _comparison = comparison;
        }

        public int Key(int index) => _keySelector(_data[index]);

        public int HeapSize
        {
            get => _heapSize;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Invalid heap size: {value}. Max heap size should be: {_data.Length}");
                }

                if (value > _data.Length)
                {
                    var newArraySize = _data.Length;
                    if (newArraySize == 0)
                    {
                        newArraySize = 1;
                    }
                    while (newArraySize < value)
                    {
                        newArraySize <<= 1;
                    }

                    ResizeArrayToSize(newArraySize);
                }

                if (value > _data.Length)
                {
                    throw new InvalidOperationException(
                        $"Value should be less or equal than data length: {value}, data length: {_data.Length}");
                }

                _heapSize = value;
            }
        }


        public T this[int index]
        {
            get => _data[index];
            set => _data[index] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) _data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ResizeArrayToSize(int newSize)
        {
            var oldArray = _data;
            _data = new T[newSize];
            for (var i = 0; i < Math.Min(oldArray.Length, newSize); i++)
            {
                _data[i] = oldArray[i];
            }
        }

        public (T value, int index) Parent(int index)
        {
            if (HasParent(index))
            {
                var parentIndex = ComputeParentIndex(index);
                return (_data[parentIndex], parentIndex);
            }

            throw new ArgumentException($"No parent for index: {index}");
        }

        public (T value, int index) Left(int index)
        {
            if (HasLeft(index))
            {
                var leftIndex = ComputeLeftIndex(index);
                return (_data[leftIndex], leftIndex);
            }

            throw new ArgumentException($"No left for index: {index}");
        }

        public (T value, int index) Right(int index)
        {
            if (HasRight(index))
            {
                var rightIndex = ComputeRightIndex(index);
                return (_data[rightIndex], rightIndex);
            }

            throw new ArgumentException($"No right for index: {index}");
        }

        public bool HasParent(int index)
        {
            return IsIndexInRange(ComputeParentIndex(index));
        }

        public bool HasLeft(int index)
        {
            return IsIndexInRange(ComputeLeftIndex(index));
        }

        public bool HasRight(int index)
        {
            return IsIndexInRange(ComputeRightIndex(index));
        }

        private bool IsIndexInRange(int index)
        {
            return index >= 0 && index < HeapSize;
        }

        private static int ComputeParentIndex(int index)
        {
            var parentIndex = (int) Math.Floor((double) (index - 1) / 2);
            return parentIndex;
        }

        private static int ComputeLeftIndex(int index)
        {
            var leftIndex = 2 * index + 1;
            return leftIndex;
        }

        private static int ComputeRightIndex(int index)
        {
            var rightIndex = 2 * index + 2;
            return rightIndex;
        }


        public void MinHeapify(int index)
        {
            int leftIndex = -1, rightIndex = -1, smallestIndex = -1;
            if (HasLeft(index))
            {
                var (_, lIndex) = Left(index);
                leftIndex = lIndex;
            }

            if (HasRight(index))
            {
                var (_, rIndex) = Right(index);
                rightIndex = rIndex;
            }

            if (leftIndex >= 0 && _comparison(_keySelector(_data[leftIndex]), _keySelector(_data[index])) < 0)
            {
                smallestIndex = leftIndex;
            }
            else
            {
                smallestIndex = index;
            }

            if (rightIndex >= 0 && _comparison(_keySelector(_data[rightIndex]), _keySelector(_data[smallestIndex])) < 0)
            {
                smallestIndex = rightIndex;
            }

            if (smallestIndex >= 0 && smallestIndex != index)
            {
                var temp = _data[index];
                _data[index] = _data[smallestIndex];
                _data[smallestIndex] = temp;
                MinHeapify(smallestIndex);
            }
        }

        public void MaxHeapify(int index)
        {
            int leftIndex = -1, rightIndex = -1, largestIndex = -1;
            if (HasLeft(index))
            {
                var (_, lIndex) = Left(index);
                leftIndex = lIndex;
            }

            if (HasRight(index))
            {
                var (_, rIndex) = Right(index);
                rightIndex = rIndex;
            }

            if (leftIndex >= 0 && _comparison(_keySelector(_data[leftIndex]), _keySelector(_data[index])) > 0)
            {
                largestIndex = leftIndex;
            }
            else
            {
                largestIndex = index;
            }

            if (rightIndex >= 0 && _comparison(_keySelector(_data[rightIndex]), _keySelector(_data[largestIndex])) > 0)
            {
                largestIndex = rightIndex;
            }

            if (largestIndex >= 0 && largestIndex != index)
            {
                var temp = _data[index];
                _data[index] = _data[largestIndex];
                _data[largestIndex] = temp;
                MaxHeapify(largestIndex);
            }
        }

        public static Heap<T> MaxHeap(T[] data, Func<T, int> keySelector, Comparison<int> comparison)
        {
            var heap = new Heap<T>(data, keySelector, comparison);
            for (var i = (int) Math.Floor(heap.HeapSize / 2.0); i >= 0; i--)
            {
                heap.MaxHeapify(i);
            }

            return heap;
        }

        public static Heap<T> MinHeap(T[] data, Func<T, int> keySelector)
        {
            var heap = new Heap<T>(data, keySelector, Comparisions.StandardComparision);
            for (var i = (int) Math.Floor(heap.HeapSize / 2.0); i >= 0; i--)
            {
                heap.MinHeapify(i);
            }

            return heap;
        }
    }
}