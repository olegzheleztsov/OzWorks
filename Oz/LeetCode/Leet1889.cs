// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet1889
{
    public int MinWastedSpace(int[] packages, int[][] boxes)
    {
        var result = long.MaxValue;
        var mod = (int)1E9 + 7;

        Array.Sort(packages);
        var packageSpace = 0L;

        foreach (var t in packages)
        {
            packageSpace += t;
        }

        foreach (var box in boxes)
        {
            Array.Sort(box);
            if (box[^1] < packages[^1])
            {
                continue;
            }

            var currentWaste = 0L;
            var previousIndex = 0;

            foreach (var currentBox in box)
            {
                var index = BinarySearch(packages, currentBox + 1, previousIndex);
                currentWaste += currentBox * 1L * (index - previousIndex);
                previousIndex = index;
                if (index == packages.Length)
                {
                    break;
                }
            }

            result = Math.Min(result, currentWaste);

            if (currentWaste == 0)
            {
                return 0;
            }
        }

        return result == long.MaxValue ? -1 : (int)((result - packageSpace) % mod);
    }

    private int BinarySearch(int[] packages, int search, int low = 0)
    {
        var high = packages.Length;
        while (low < high)
        {
            var mid = low + ((high - low) / 2);
            if (packages[mid] < search)
            {
                low = mid + 1;
            }
            else
            {
                high = mid;
            }
        }

        return low;
    }
}