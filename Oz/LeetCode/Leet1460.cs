// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

/*
 * Given two integer arrays of equal length target and arr.

In one step, you can select any non-empty sub-array of arr and reverse it. You are allowed to make any number of steps.

Return True if you can make arr equal to target, or False otherwise.
 */
public class Leet1460
{
    public bool CanBeEqual(int[] target, int[] arr)
    {
        for (var i = 0; i < target.Length; i++)
        {
            if (target[i] != arr[i])
            {
                var found = false;
                for (var j = i + 1; j < target.Length; j++)
                {
                    if (arr[j] == target[i])
                    {
                        Reverse(arr, i, j);
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void Reverse(int[] arr, int start, int end)
    {
        while (start < end)
        {
            (arr[start], arr[end]) = (arr[end], arr[start]);
            start++;
            end--;
        }
    }
}