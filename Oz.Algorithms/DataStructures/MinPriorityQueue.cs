using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Oz.Algorithms.DataStructures
{
    /// <summary>
    ///     Min priority queue base on heap
    /// </summary>
    /// <typeparam name="T">Type of the queue elements</typeparam>
    public class MinPriorityQueue<T> : IEnumerable<T>
    {
        private readonly Heap<PriorityNode<T>> _heap;


        /// <summary>
        ///     Cons by default
        /// </summary>
        public MinPriorityQueue()
        {
            _heap = new Heap<PriorityNode<T>>(new PriorityNode<T>[] { },
                node => node.Priority,
                Comparisions.StandardComparision);
        }

        /// <summary>
        ///     Count of the elements in the queue
        /// </summary>
        public int Length => _heap.HeapSize;

        /// <summary>
        ///     Whether the queue is empty
        /// </summary>
        public bool IsEmpty => Length <= 0;

        /// <summary>
        ///     Returns minimum-priority element without extracting it
        /// </summary>
        /// <returns>Min-priority element</returns>
        /// <exception cref="InvalidOperationException">Throws when heap is empty</exception>
        public T Minimum()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Queue doesn't contain elements");
            }

            return _heap[0].Data;
        }

        /// <summary>
        ///     Remove and return min priority element from the queue
        /// </summary>
        /// <returns>Min-priority element of the queue</returns>
        /// <exception cref="IndexOutOfRangeException">Throws when element extracted from empty queue</exception>
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

        /// <summary>
        ///     Returns priority of the element at index
        /// </summary>
        /// <param name="index">Index of the element</param>
        /// <returns>Priority of the element</returns>
        public int Priority(int index)
        {
            return _heap.Key(index);
        }

        /// <summary>
        ///     Decrease priority of the element at index
        /// </summary>
        /// <param name="index">Element's index</param>
        /// <param name="priority">New priority of the element</param>
        /// <exception cref="ArgumentException">Throws when new priority is bigger than old priority</exception>
        public void DecreasePriority(int index, int priority)
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
                var temp = _heap[index];
                _heap[index] = _heap[parentIndex];
                _heap[parentIndex] = temp;
                index = parentIndex;
            }
        }

        /// <summary>
        ///     Inserts new element in the queue
        /// </summary>
        /// <param name="value">Element's value</param>
        /// <param name="priority">Element's priority</param>
        public void Insert(T value, int priority)
        {
            _heap.HeapSize++;
            _heap[_heap.HeapSize - 1] = new PriorityNode<T>(Util.IntegerPositiveInfinity, value);
            DecreasePriority(_heap.HeapSize - 1, priority);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _heap.OrderBy(e => e.Priority).Select(element => element.Data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
}