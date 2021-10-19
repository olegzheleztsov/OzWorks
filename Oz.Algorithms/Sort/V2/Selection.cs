// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.Algorithms.Sort.V2;

public static class Selection
{
    public static void Sort<T>(T[] array, Action<T[]> arrayExchangeAction = null) where T : IComparable<T>
    {
        for (var i = 0; i < array.Length; i++)
        {
            var minIndex = i;
            for (var j = i + 1; j < array.Length; j++)
            {
                if (array[j].LessThan(array[minIndex]))
                {
                    minIndex = j;
                }
            }

            (array[i], array[minIndex]) = (array[minIndex], array[i]);
            arrayExchangeAction?.Invoke(array);
        }
    }
}