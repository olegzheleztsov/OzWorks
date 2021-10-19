// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.Sedgewick;

public class Ex_2_2_10<T> where T : IComparable<T>
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
        
        int auxIndex = hi;
        for (int arrIndex = mid + 1; arrIndex <= hi; arrIndex++)
        {
            _aux[auxIndex--] = array[arrIndex];
        }

        for (int arrIndex = lo; arrIndex <= mid; arrIndex++)
        {
            _aux[arrIndex] = array[arrIndex];
        }

        int auxLeft = lo, auxRight = hi;
        for (int i = lo; i <= hi; i++)
        {
            if (_aux[auxLeft].CompareTo(_aux[auxRight]) < 0)
            {
                array[i] = _aux[auxLeft];
                auxLeft++;
            }
            else
            {
                array[i] = _aux[auxRight];
                auxRight--;
            }
        }
    }
}