// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

/*
 * 1630. Arithmetic subarrays
 * A sequence of numbers is called arithmetic if it consists of at least two elements, and the difference between every two consecutive elements is the same. More formally, a sequence s is arithmetic if and only if s[i+1] - s[i] == s[1] - s[0] for all valid i.

For example, these are arithmetic sequences:

1, 3, 5, 7, 9
7, 7, 7, 7
3, -1, -5, -9
The following sequence is not arithmetic:

1, 1, 2, 5, 7
You are given an array of n integers, nums, and two arrays of m integers each, l and r, representing the m range queries, where the ith query is the range [l[i], r[i]]. All the arrays are 0-indexed.

Return a list of boolean elements answer, where answer[i] is true if the subarray nums[l[i]], nums[l[i]+1], ... , nums[r[i]] can be rearranged to form an arithmetic sequence, and false otherwise.
 */
public class _1630
{
    public IList<bool> CheckArithmeticSubarrays(int[] nums, int[] l, int[] r)
    {
        var statuses = new List<bool>();
        var rangeCount = l.Length;
        for (var i = 0; i < rangeCount; i++)
        {
            var min = l[i];
            var max = r[i];
            var subArray = GetSubArray(nums, min, max);
            statuses.Add(IsCanRearrangeToArithmetic(subArray));
        }

        return statuses;
    }

    private int[] GetSubArray(int[] nums, int min, int max)
    {
        var subArrayList = new List<int>();
        for (var i = min; i <= max; i++)
        {
            subArrayList.Add(nums[i]);
        }

        return subArrayList.ToArray();
    }

    private bool IsCanRearrangeToArithmetic(int[] array)
    {
        Array.Sort(array);
        var differences = new List<int>();
        for (var i = 0; i < array.Length - 1; i++)
        {
            differences.Add(array[i + 1] - array[i]);
            if (differences.Count <= 1)
            {
                continue;
            }

            var diffCount = differences.Count;
            if (differences[diffCount - 1] != differences[diffCount - 2])
            {
                return false;
            }
        }

        return true;
    }
}