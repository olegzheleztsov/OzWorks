// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.Algorithms.Sedgewick;

public class MaxPriorityQueue<T> : Heap<T> where T : IComparable<T>
{
    private int _currentSize = 0;
    
    public MaxPriorityQueue(int heapSize) : base(heapSize)
    {
    }

    public bool IsEmpty =>
        _currentSize == 0;

    public int Size =>
        _currentSize;

    public void Insert(T obj)
    {
        _currentSize++;
        if (_currentSize > _heapSize)
        {
            Resize(_heapSize * 2);
        }
        _array[_currentSize] = obj;
        Swim(_currentSize);
    }

    public T DeleteMax()
    {
        var maxElement = _array[1];
        Exchange(1, _currentSize--);
        _array[_currentSize + 1] = default;
        Sink(1);
        if (_currentSize > 1 && _currentSize < _heapSize / 2)
        {
            Resize(_heapSize / 2);
        }
        return maxElement;
    }
}