// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections;

namespace Leetcode.Solutions;

public class _973
{
    public int[][] KClosest(int[][] points, int k)
    {
        var pointQueue = new MinPriorityQueue<int[]>();

        foreach (var point in points)
        {
            pointQueue.Insert(point, Math.Sqrt((point[0] * point[0]) + (point[1] * point[1])));
        }

        var index = 0;
        var result = new List<int[]>();
        while (pointQueue.Length > 0 && index < k)
        {
            result.Add(pointQueue.ExtractMinimum());
            index++;
        }

        return result.ToArray();
    }

    public class MinPriorityQueue<T> : IEnumerable<T>
    {
        private readonly Heap<PriorityNode<T>> _heap;

        public MinPriorityQueue() =>
            _heap = new Heap<PriorityNode<T>>(new PriorityNode<T>[]
                {
                },
                node => node.Priority,
                (a, b) => a.CompareTo(b));

        public int Length => _heap.HeapSize;


        public bool IsEmpty => Length <= 0;

        public IEnumerator<T> GetEnumerator() =>
            _heap.OrderBy(e => e.Priority).Select(element => element.Data).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public T Minimum()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Queue doesn't contain elements");
            }

            return _heap[0].Data;
        }

        public T ExtractMinimum()
        {
            if (IsEmpty)
            {
                throw new IndexOutOfRangeException("Queue doesn't contain elements");
            }

            var minValue = _heap[0];
            _heap[0] = _heap[_heap.HeapSize - 1];
            _heap.HeapSize--;
            _heap.MinHeapify(0);
            return minValue.Data;
        }

        public double Priority(int index) =>
            _heap.Key(index);

        public void DecreasePriority(int index, double priority)
        {
            if (priority > Priority(index))
            {
                throw new ArgumentException(
                    $"Priority can't increased. Target priority: {priority}, Current priority: {Priority(index)}");
            }

            _heap[index].Priority = priority;
            while (index > 0 && Priority(_heap.Parent(index).index) >= Priority(index))
            {
                var parentIndex = _heap.Parent(index).index;
                (_heap[index], _heap[parentIndex]) = (_heap[parentIndex], _heap[index]);
                index = parentIndex;
            }
        }

        public void Insert(T value, double priority)
        {
            _heap.HeapSize++;
            _heap[_heap.HeapSize - 1] = new PriorityNode<T>(int.MaxValue, value);
            DecreasePriority(_heap.HeapSize - 1, priority);
        }

        public int GetIndex(T element)
        {
            var index = -1;
            for (var i = 0; i < Length; i++)
            {
                if (element.Equals(_heap[i].Data))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        public int? GetIndex(Func<T, bool> condition)
        {
            for (var i = 0; i < Length; i++)
            {
                if (condition(_heap[i].Data))
                {
                    return i;
                }
            }

            return null;
        }
    }

    public class PriorityNode<T>
    {
        public PriorityNode(double priority, T data)
        {
            Priority = priority;
            Data = data;
        }

        public double Priority { get; set; }
        public T Data { get; }
    }

    public class Heap<T> : IEnumerable<T>
    {
        private readonly Comparison<double> _comparison;
        private readonly Func<T, double> _keySelector;
        private T[] _data;
        private int _heapSize;

        public Heap(T[] data, Func<T, double> keySelector, Comparison<double> comparison)
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

        public double Key(int index) =>
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

        public static Heap<T> MaxHeap(T[] data, Func<T, double> keySelector, Comparison<double> comparison)
        {
            var heap = new Heap<T>(data, keySelector, comparison);
            for (var i = (int)Math.Floor(heap.HeapSize / 2.0); i >= 0; i--)
            {
                heap.MaxHeapify(i);
            }

            return heap;
        }

        public static Heap<T> MinHeap(T[] data, Func<T, double> keySelector)
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