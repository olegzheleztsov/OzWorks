// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;

namespace Leetcode.Solutions;

public class _108
{
    public TreeNode SortedArrayToBST(int[] nums) =>
        BuildTree(nums, 0, nums.Length - 1);

    private TreeNode BuildTree(int[] nums, int left, int right)
    {
        if (left >= right)
        {
            return null;
        }

        var mid = (left + right) / 2;
        var node = new TreeNode(nums[mid])
        {
            left = BuildTree(nums, left, mid - 1),
            right = BuildTree(nums, mid + 1, right)
        };
        return node;
    }
}