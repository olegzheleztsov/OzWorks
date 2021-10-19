// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Linq;

namespace Oz.Grokking;

public static class GAlgo
{
    private static int[] Histogram(int[] array, int histLength)
    {
        var hist = new int[histLength];

        for (var i = 0; i < hist.Length; i++)
        {
            var count = array.Count(e => e == i);
            hist[i] = count;
        }

        return hist;
    }

    public static void HistogramTest()
    {
        int[] array = {1, 2, 3, 4, 5, 3, 5, 3, 5, 2};
        var hist = Histogram(array, 6);
        Console.WriteLine(string.Join(" ", hist));
        Console.WriteLine($"Hist sum: {hist.Sum()}, arr length: {array.Length}");
    }

    public static int Log2Approximation(int number)
    {
        var result = 0;
        var num = 1;

        while (num <= number)
        {
            num *= 2;
            result++;
        }

        return result - 1;
    }

    private static void PrintTransposed(int[,] array)
    {
        var rowCount = array.GetUpperBound(0) + 1;
        var columnCount = array.GetUpperBound(1) + 1;

        for (var j = 0; j < columnCount; j++)
        {
            for (var i = 0; i < rowCount; i++)
            {
                Console.Write($"{array[i, j]}\t");
            }

            Console.WriteLine();
        }
    }

    public static void PrintTransposedTest()
    {
        var array = new int[3, 5];
        var counter = 1;
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 5; j++)
            {
                array[i, j] = counter++;
            }
        }

        PrintTransposed(array);
    }


    public static int? BinarySearch<T>(T[] array, T item, Comparison<T> comparison)
    {
        var low = 0;
        var high = array.Length - 1;


        while (low <= high)
        {
            var mid = (low + high) / 2;
            var guess = array[mid];
            if (comparison(guess, item) == 0)
            {
                return mid;
            }

            if (comparison(guess, item) > 0)
            {
                high = mid - 1;
            }
            else
            {
                low = mid + 1;
            }
        }

        return null;
    }
}