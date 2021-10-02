// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.Algorithms.Sort.V2;

public class Insertion
{
    public static void Sort<T>(T[] array) where T : IComparable<T>
    {
        for (var i = 1; i < array.Length; i++)
        {
            for (var j = i; j > 0 && array[j].LessThan(array[j - 1]); j--)
            {
                (array[j], array[j - 1]) = (array[j - 1], array[j]);
            }
        }
    }
}