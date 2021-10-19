// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.Algorithms.Sort.V2;

public class Merge<T> where T : IComparable<T>
{
    private T[] _aux;

    public void Sort(T[] array)
    {
        _aux = new T[array.Length];
        Sort(array, 0, array.Length - 1);
    }

    public void SortBu(T[] array)
    {
        _aux = new T[array.Length];
        for (var sz = 1; sz < array.Length; sz = sz + sz)
        {
            for (var lo = 0; lo < array.Length - sz; lo += sz + sz)
            {
                MergeProc(array, lo, lo + sz - 1, Math.Min(lo + sz + sz - 1, array.Length - 1));
            }
        }
    }

    private void Sort(T[] array, int lo, int hi)
    {
        if (hi <= lo)
        {
            return;
        }

        var mid = lo + ((hi - lo) / 2);
        Sort(array, lo, mid);
        Sort(array, mid + 1, hi);
        MergeProc(array, lo, mid, hi);
    }

    private void MergeProc(T[] array, int lo, int mid, int hi)
    {
        int i = lo, j = mid + 1;

        for (var k = lo; k <= hi; k++)
        {
            _aux[k] = array[k];
        }

        for (var k = lo; k <= hi; k++)
        {
            if (i > mid)
            {
                array[k] = _aux[j++];
            }
            else if (j > hi)
            {
                array[k] = _aux[i++];
            }
            else if (_aux[j].CompareTo(_aux[i]) < 0)
            {
                array[k] = _aux[j++];
            }
            else
            {
                array[k] = _aux[i++];
            }
        }
    }
}