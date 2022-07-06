// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

/*
 * 275. H-Index II
 * Given an array of integers citations where citations[i] is the number of citations a researcher received for their ith paper and citations is sorted in an ascending order, return compute the researcher's h-index.

According to the definition of h-index on Wikipedia: A scientist has an index h if h of their n papers have at least h citations each, and the other n − h papers have no more than h citations each.

If there are several possible values for h, the maximum one is taken as the h-index.

You must write an algorithm that runs in logarithmic time.
 */
public class _275
{
    public int HIndex(int[] citations)
    {
        if (citations.Length == 0)
        {
            return 0;
        }

        var left = 0;
        var right = citations.Length - 1;
        while (left < right)
        {
            var mid = left + ((right - left) / 2);
            if (right - left == 1)
            {
                break;
            }

            var midItem = citations[mid];
            if (citations.Length - mid >= midItem)
            {
                left = mid;
            }
            else
            {
                right = mid;
            }
        }

        var res = int.MinValue;
        res = Math.Max(res, Math.Min(citations.Length - left, citations[left]));
        res = Math.Max(res, Math.Min(citations.Length - right, citations[right]));
        return res;
    }
}