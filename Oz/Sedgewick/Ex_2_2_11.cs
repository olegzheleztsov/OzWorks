// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.Sedgewick;

public class Ex_2_2_11<T> where T : IComparable<T>
{
    private const int Cutoff = 15;

    public void Sort(T[] array)
    {
        var aux = new T[array.Length];
        Array.Copy(array, aux, array.Length);
        SortInner(aux, array, 0, array.Length - 1);
    }

    private void SortInner(T[] array, T[] aux, int low, int high)
    {
        if (high - low <= Cutoff)
        {
            InsertionSort(aux, low, high);
            return;
        }

        var mid = low + ((high - low) / 2);
        SortInner(aux, array, low, mid);
        SortInner(aux, array, mid + 1, high);

        if (array[mid].CompareTo(array[mid + 1]) <= 0)
        {
            Array.Copy(array, low, aux, low, high - low + 1);
        }

        MergeInner(array, aux, low, mid, high);
    }

    private void InsertionSort(T[] array, int low, int high)
    {
        for (var i = low; i <= high; i++)
        {
            for (var j = i; j > low && array[j - 1].CompareTo(array[j]) > 0; j--)
            {
                (array[j - 1], array[j]) = (array[j], array[j - 1]);
            }
        }
    }

    private void MergeInner(T[] array, T[] aux, int lo, int mid, int hi)
    {
        var auxIndex = hi;
        for (var arrIndex = mid + 1; arrIndex <= hi; arrIndex++)
        {
            aux[auxIndex--] = array[arrIndex];
        }

        for (var arrIndex = lo; arrIndex <= mid; arrIndex++)
        {
            aux[arrIndex] = array[arrIndex];
        }

        int auxLeft = lo, auxRight = hi;
        for (var i = lo; i <= hi; i++)
        {
            if (aux[auxLeft].CompareTo(aux[auxRight]) < 0)
            {
                array[i] = aux[auxLeft];
                auxLeft++;
            }
            else
            {
                array[i] = aux[auxRight];
                auxRight--;
            }
        }
    }
}