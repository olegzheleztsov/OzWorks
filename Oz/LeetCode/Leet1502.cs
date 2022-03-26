﻿// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet1502
{
    public bool CanMakeArithmeticProgression(int[] arr) {
        Array.Sort(arr);
        int diff = arr[1] - arr[0];

        for (int i = 2; i < arr.Length; i++)
        {
            if (arr[i] - arr[i - 1] != diff)
            {
                return false;
            }
        }

        return true;
    }
}