// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.Algorithms.Sedgewick;

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

    protected bool Less(int i, int j) =>
        _array[i].CompareTo(_array[j]) < 0;

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