// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Linq;

public class Leet1608
{
    public int SpecialArray(int[] nums)
    {
        int start = 0, end = nums.Length;
        while (start <= end)
        {
            var mid = start + ((end - start) / 2);
            var count = GetCount(nums, mid);
            if (count == mid)
            {
                return mid;
            }

            if (count > mid)
            {
                start = mid + 1;
            }
            else
            {
                end = mid - 1;
            }
        }

        return -1;
    }

    private int GetCount(int[] nums, int n) =>
        nums.Count(num => num >= n);
}