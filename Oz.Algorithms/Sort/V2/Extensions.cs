// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.Algorithms.Sort.V2;

public static class Extensions
{
    public static bool LessThan<T>(this T left, T right) where T : IComparable<T> =>
        left.CompareTo(right) < 0;
}