// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.Algorithms.Sort.V2;

public static class Shell
{
    public static void Sort<T>(T[] array, Action<T[]> arrayExchangeAction = null) where T : IComparable<T>
    {
        var h = 1;
        while (h < array.Length / 3)
        {
            h = (3 * h) + 1;
        }

        while (h >= 1)
        {
            for (var i = h; i < array.Length; i++)
            {
                for (var j = i; j >= h && array[j].CompareTo(array[j - h]) < 0; j -= h)
                {
                    (array[j], array[j - h]) = (array[j - h], array[j]);
                    arrayExchangeAction?.Invoke(array);
                }
            }

            h /= 3;
        }
    }
}