// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

public class Leet33
{
    public void Test()
    {
        int[] nums = {5, 1, 3};
        var result = Search(nums, 5);
    }

    public int Search(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0)
        {
            return -1;
        }

        var offset = FindMinIndex(nums);
        return BinarySearch(nums, target, offset);
    }

    private int FindMinIndex(int[] nums)
    {
        var n = nums.Length;
        var start = 0;
        var end = n - 1;
        while (start < end)
        {
            var mid = start + ((end - start) / 2);
            if (nums[mid] > nums[end])
            {
                start = mid + 1;
            }
            else
            {
                end = mid;
            }
        }

        return start;
    }

    private int BinarySearch(int[] nums, int target, int offset)
    {
        var n = nums.Length;
        var start = 0;
        var end = n - 1;
        while (start < end)
        {
            var mid = start + ((end - start) / 2);
            var realMid = (mid + offset) % n;
            if (nums[realMid] < target)
            {
                start = mid + 1;
            }
            else
            {
                end = mid;
            }
        }

        var index = (start + offset) % n;
        return nums[index] == target ? index : -1;
    }
}