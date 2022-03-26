// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

/*
 * You are given two integer arrays nums1 and nums2 sorted in ascending order and an integer k.
   Define a pair (u, v) which consists of one element from the first array and one element from the second array.
   Return the k pairs (u1, v1), (u2, v2), ..., (uk, vk) with the smallest sums.
 */
public class Leet373
{
    public IList<IList<int>> KSmallestPairs(int[] nums1, int[] nums2, int k)
    {
        var priorityQueue = new MaxPriorityQueue<Pair>(k);

        for (var i = 0; i < nums1.Length; i++)
        {
            for (var j = 0; j < nums2.Length; j++)
            {
                var first = nums1[i];
                var second = nums2[j];

                if (priorityQueue.Size < k)
                {
                    priorityQueue.Insert(new Pair {First = first, Second = second});
                }
                else if (priorityQueue.Size == k)
                {
                    var newPair = new Pair {First = first, Second = second};
                    if (newPair.CompareTo(priorityQueue.Max) < 0)
                    {
                        priorityQueue.DeleteMax();
                        priorityQueue.Insert(newPair);
                    }
                }
            }
        }

        var result = new List<IList<int>>();
        while (!priorityQueue.IsEmpty)
        {
            var elem = priorityQueue.DeleteMax();
            result.Add(new List<int> {elem.First, elem.Second});
        }

        return result;
    }

    public class MaxPriorityQueue<T> : Heap<T> where T : IComparable<T>
    {
        public MaxPriorityQueue(int heapSize) : base(heapSize)
        {
        }

        public bool IsEmpty =>
            Size == 0;

        public int Size { get; private set; }

        public T Max => _array[1];

        public void Insert(T obj)
        {
            Size++;
            if (Size > _heapSize)
            {
                Resize(_heapSize * 2);
            }

            _array[Size] = obj;
            Swim(Size);
        }

        public T DeleteMax()
        {
            var maxElement = _array[1];
            Exchange(1, Size--);
            _array[Size + 1] = default;
            Sink(1);
            if (Size > 1 && Size < _heapSize / 2)
            {
                Resize(_heapSize / 2);
            }

            return maxElement;
        }
    }

    public class Heap<T> where T : IComparable<T>
    {
        protected T[] _array;
        protected int _heapSize;

        public Heap(int heapSize)
        {
            _array = new T[heapSize + 1];
            _heapSize = heapSize;
        }

        protected void Resize(int targetSize)
        {
            var oldSize = _heapSize;
            var newArray = new T[targetSize + 1];
            Array.Copy(_array, 1, newArray, 1, Math.Min(_heapSize, targetSize));
            _heapSize = targetSize;
            _array = newArray;
        }

        protected void Exchange(int i, int j) =>
            (_array[i], _array[j]) = (_array[j], _array[i]);

        protected bool Less(int i, int j)
        {
            if (_array[i] == null || _array[j] == null)
            {
                return false;
            }

            return _array[i].CompareTo(_array[j]) < 0;
        }

        protected void Swim(int k)
        {
            while (k > 1 && Less(k / 2, k))
            {
                Exchange(k / 2, k);
                k /= 2;
            }
        }

        protected void Sink(int k)
        {
            while (2 * k <= _heapSize)
            {
                var j = 2 * k;
                if (j < _heapSize && Less(j, j + 1))
                {
                    j++;
                }

                if (!Less(k, j))
                {
                    break;
                }

                Exchange(k, j);
                k = j;
            }
        }
    }

    public class Pair : IComparable<Pair>
    {
        public int First { get; set; }
        public int Second { get; set; }

        public int CompareTo(Pair other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            return (First + Second).CompareTo(other.First + other.Second);
        }
    }
}