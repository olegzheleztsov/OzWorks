using System;

namespace Oz.LeetCode.Recursion;

public class SortArraySolver
{
    public int[] SortArray(int[] nums)
    {
        if (nums.Length < 2)
        {
            return nums;
        }

        MergeSort(nums, 0, nums.Length - 1);
        return nums;
    }

    private void MergeSort(int[] nums, int left, int right)
    {
        if (left >= right)
        {
            return;
        }

        var middle = (int)Math.Floor((double)(left + right) / 2);
        MergeSort(nums, left, middle);
        MergeSort(nums, middle + 1, right);
        Merge(nums, left, middle, right);
    }

    private void Merge(int[] nums, int left, int middle, int right)
    {
        var numberLeft = middle - left + 1;
        var numberRight = right - middle;
        var leftSubArray = new int[numberLeft];
        var rightSubArray = new int[numberRight];
        Array.Copy(nums, left, leftSubArray, 0, numberLeft);
        Array.Copy(nums, middle + 1, rightSubArray, 0, numberRight);

        var index = left;
        int leftPointer = 0, rightPointer = 0;
        while (leftPointer < numberLeft && rightPointer < numberRight)
        {
            if (leftSubArray[leftPointer] < rightSubArray[rightPointer])
            {
                nums[index] = leftSubArray[leftPointer];
                index++;
                leftPointer++;
            }
            else
            {
                nums[index] = rightSubArray[rightPointer];
                index++;
                rightPointer++;
            }
        }

        while (leftPointer < numberLeft)
        {
            nums[index] = leftSubArray[leftPointer];
            index++;
            leftPointer++;
        }

        while (rightPointer < numberRight)
        {
            nums[index] = rightSubArray[rightPointer];
            index++;
            rightPointer++;
        }
    }
}