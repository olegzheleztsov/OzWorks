// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet88
{
    public void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        var result = new int[nums1.Length];

        var firstIndex = 0;
        var secondIndex = 0;
        var insertIndex = 0;

        while (firstIndex < m && secondIndex < n)
        {
            if (nums1[firstIndex] <= nums2[secondIndex])
            {
                result[insertIndex] = nums1[firstIndex];
                firstIndex++;
            }
            else
            {
                result[insertIndex] = nums2[secondIndex];
                secondIndex++;
            }

            insertIndex++;
        }

        while (firstIndex < m)
        {
            result[insertIndex] = nums1[firstIndex];
            insertIndex++;
            firstIndex++;
        }

        while (secondIndex < n)
        {
            result[insertIndex] = nums2[secondIndex];
            insertIndex++;
            secondIndex++;
        }

        for (var i = 0; i < nums1.Length; i++)
        {
            nums1[i] = result[i];
        }
    }
}