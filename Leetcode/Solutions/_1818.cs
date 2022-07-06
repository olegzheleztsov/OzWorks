// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

/*
 * 1818. Minimum Absolute Sum Difference
 * You are given two positive integer arrays nums1 and nums2, both of length n.

The absolute sum difference of arrays nums1 and nums2 is defined as the sum of |nums1[i] - nums2[i]| for each 0 <= i < n (0-indexed).

You can replace at most one element of nums1 with any other element in nums1 to minimize the absolute sum difference.

Return the minimum absolute sum difference after replacing at most one element in the array nums1. Since the answer may be large, return it modulo 109 + 7.

|x| is defined as:

x if x >= 0, or
-x if x < 0.
 */
public class _1818
{
    public int MinAbsoluteSumDiff(int[] nums1, int[] nums2)
    {
        var arr = new int[100001];
        var left = 100000;
        var right = 1;
        foreach (var i in nums1)
        {
            arr[i]++;
            left = Math.Min(left, i);
            right = Math.Max(right, i);
        }

        var max = 0;
        long sum = 0;
        for (var i = 0; i < nums1.Length; i++)
        {
            var abs = nums1[i] >= nums2[i] ? nums1[i] - nums2[i] : nums2[i] - nums1[i];
            sum += abs;

            var j = nums2[i];
            if (arr[j] > 0)
            {
                //len=0, we can minus the whole abs
                max = Math.Max(max, abs);
            }
            else
            {
                //len is the closest nums1 element to nums2[i] which can reduce sum
                var len = 1;
                while ((j - len >= left || j + len <= right) && len < abs && abs - len > max)
                {
                    if ((j - len >= left && arr[j - len] > 0)
                        || (j + len <= right && arr[j + len] > 0))
                    {
                        max = abs - len;
                        break;
                    }

                    len++;
                }
            }
        }

        sum -= max;
        return (int)(sum % 1000000007);
    }
}