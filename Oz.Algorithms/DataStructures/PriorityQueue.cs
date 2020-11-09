using System;

namespace Oz.Algorithms.DataStructures
{
    public class PriorityQueue<T>
    {
        private readonly Heap<PriorityNode> _heap;

        public PriorityQueue()
        {
            _heap = new Heap<PriorityNode>(new PriorityNode[] { }, node => node.Priority,
                Comparisions.StandardComparision);
        }

        public T Maximum()
        {
            return _heap[0].Data;
        }

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

        public int Length => _heap.HeapSize;

        private int Priority(int index)
        {
            return _heap.Key(index);
        }

        private void IncreasePriority(int index, int priority)
        {
            if (priority < Priority(index))
            {
                throw new ArgumentException(
                    $"Priority can't lowered, priority: {priority}, existing priortity: {Priority(index)}");
            }

            _heap[index].Priority = priority;

            while (index > 0 && Priority(_heap.Parent(index).index) < Priority(index))
            {
                var parentIndex = _heap.Parent(index).index;
                var t = _heap[index];
                _heap[index] = _heap[parentIndex];
                _heap[parentIndex] = t;
                index = parentIndex;
            }
        }

        public void Insert(T value, int priority)
        {
            _heap.HeapSize++;
            _heap[_heap.HeapSize - 1] = new PriorityNode(int.MinValue, value);
            IncreasePriority(_heap.HeapSize - 1, priority);
        }

        private class PriorityNode
        {
            public PriorityNode(int priority, T data)
            {
                Priority = priority;
                Data = data;
            }

            public int Priority { get; set; }
            public T Data { get; }
        }
    }
}