using System;

namespace Oz.Algorithms.DataStructures;

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