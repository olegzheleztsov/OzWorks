// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

public class Leet34
{
    public int[] SearchRange(int[] nums, int target) =>
        new[] {FindFirstIndex(nums, target), FindLastIndex(nums, target)};

    private int FindFirstIndex(int[] nums, int target)
    {
        if (nums.Length == 0)
        {
            return -1;
        }

        int start = 0, end = nums.Length - 1;
        while (start < end)
        {
            var mid = start + ((end - start) / 2);
            if (nums[mid] < target)
            {
                start = mid + 1;
            }
            else
            {
                end = mid;
            }
        }

        return nums[start] == target ? start : -1;
    }

    private int FindLastIndex(int[] nums, int target)
    {
        if (nums.Length == 0)
        {
            return -1;
        }

        int start = 0, end = nums.Length - 1;
        while (start < end)
        {
            var mid = start + ((end - start + 1) / 2);
            if (nums[mid] > target)
            {
                end = mid - 1;
            }
            else
            {
                start = mid;
            }
        }

        return nums[start] == target ? start : -1;
    }
}