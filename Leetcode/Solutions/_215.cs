// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections;

namespace Leetcode.Solutions;

public class _215
{
    public int FindKthLargest(int[] nums, int k)
    {
        var priorityQueue = new MaxPriorityQueue<int>();

        foreach (var num in nums)
        {
            priorityQueue.Insert(num, num);
        }

        if (k > nums.Length)
        {
            throw new ArgumentException();
        }

        var maxElem = int.MinValue;
        for (var i = 0; i < k; i++)
        {
            maxElem = priorityQueue.ExtractMaximum();
        }

        return maxElem;
    }

    public class PriorityNode<T>
    {
        public PriorityNode(int priority, T data)
        {
            Priority = priority;
            Data = data;
        }

        public int Priority { get; set; }
        public T Data { get; }
    }

    public class MaxPriorityQueue<T>
    {
        private readonly Heap<PriorityNode<T>> _heap;

        public MaxPriorityQueue() =>
            _heap = new Heap<PriorityNode<T>>(new PriorityNode<T>[]
            {
            }, node => node.Priority, (a, b) => a.CompareTo(b));

        public int Length => _heap.HeapSize;

        public T Maximum() =>
            _heap[0].Data;

        public T ExtractMaximum()
        {
            if (_heap.HeapSize < 1)
            {
                throw new IndexOutOfRangeException();
            }

            var maxValue = _heap[0];
            _heap[0] = _heap[_heap.HeapSize - 1];
            _heap.HeapSize--;
            _heap.MaxHeapify(0);
            return maxValue.Data;
        }

        public void RemoveElement(T value, Comparison<T> comparison)
        {
            if (_heap.HeapSize < 1)
            {
                throw new IndexOutOfRangeException();
            }

            for (var i = 0; i < _heap.HeapSize; i++)
            {
                if (comparison(_heap[i].Data, value) != 0)
                {
                    continue;
                }

                _heap[i] = _heap[_heap.HeapSize - 1];
                _heap.HeapSize--;
                _heap.MaxHeapify(Math.Min(i, _heap.HeapSize - 1));
                return;
            }
        }

        public bool ContainsElement(T value, Comparison<T> comparison)
        {
            if (_heap.HeapSize < 1)
            {
                return false;
            }

            for (var i = 0; i < _heap.HeapSize; i++)
            {
                if (comparison(_heap[i].Data, value) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void Insert(T value, int priority)
        {
            _heap.HeapSize++;
            _heap[_heap.HeapSize - 1] = new PriorityNode<T>(int.MinValue, value);
            IncreasePriority(_heap.HeapSize - 1, priority);
        }

        private int Priority(int index) =>
            _heap.Key(index);

        private void IncreasePriority(int index, int priority)
        {
            if (priority < Priority(index))
            {
                throw new ArgumentException(
                    $"Priority can't lowered, priority: {priority}, existing priority: {Priority(index)}");
            }

            _heap[index].Priority = priority;

            while (index > 0 && Priority(_heap.Parent(index).index) < Priority(index))
            {
                var parentIndex = _heap.Parent(index).index;
                (_heap[index], _heap[parentIndex]) = (_heap[parentIndex], _heap[index]);
                index = parentIndex;
            }
        }
    }

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
            for (var i = 0; i < HeapSize; i++)
            {
                yield return _data[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public int Key(int index) =>
            _keySelector(_data[index]);

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

        public bool HasParent(int index) =>
            IsIndexInRange(ComputeParentIndex(index));

        public bool HasLeft(int index) =>
            IsIndexInRange(ComputeLeftIndex(index));

        public bool HasRight(int index) =>
            IsIndexInRange(ComputeRightIndex(index));

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
                (_data[index], _data[smallestIndex]) = (_data[smallestIndex], _data[index]);
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
                (_data[index], _data[largestIndex]) = (_data[largestIndex], _data[index]);
                MaxHeapify(largestIndex);
            }
        }

        public static Heap<T> MaxHeap(T[] data, Func<T, int> keySelector, Comparison<int> comparison)
        {
            var heap = new Heap<T>(data, keySelector, comparison);
            for (var i = (int)Math.Floor(heap.HeapSize / 2.0); i >= 0; i--)
            {
                heap.MaxHeapify(i);
            }

            return heap;
        }

        public static Heap<T> MinHeap(T[] data, Func<T, int> keySelector)
        {
            var heap = new Heap<T>(data, keySelector, (a, b) => a.CompareTo(b));
            for (var i = (int)Math.Floor(heap.HeapSize / 2.0); i >= 0; i--)
            {
                heap.MinHeapify(i);
            }

            return heap;
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

        private bool IsIndexInRange(int index) =>
            index >= 0 && index < HeapSize;

        private static int ComputeParentIndex(int index)
        {
            var parentIndex = (int)Math.Floor((double)(index - 1) / 2);
            return parentIndex;
        }

        private static int ComputeLeftIndex(int index)
        {
            var leftIndex = (2 * index) + 1;
            return leftIndex;
        }

        private static int ComputeRightIndex(int index)
        {
            var rightIndex = (2 * index) + 2;
            return rightIndex;
        }
    }
}