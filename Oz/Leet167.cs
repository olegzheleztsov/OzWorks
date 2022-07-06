// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;

public class Leet167
{
    public int[] TwoSum(int[] numbers, int target)
    {
        for (var i = 0; i < numbers.Length - 1; i++)
        {
            var ind = FindNumber(i + 1, target - numbers[i], numbers);
            if (ind >= 0)
            {
                return new[] {i + 1, ind + 1};
            }
        }

        throw new Exception();
    }

    private int FindNumber(int startIndex, int number, int[] numbers)
    {
        var start = startIndex;
        var end = numbers.Length - 1;

        while (start < end)
        {
            var mid = start + ((end - start) / 2);
            if (numbers[mid] == number)
            {
                return mid;
            }

            if (number < numbers[mid])
            {
                end = mid - 1;
            }
            else
            {
                start = mid + 1;
            }
        }

        if (numbers[start] == number)
        {
            return start;
        }

        return -1;
    }
}