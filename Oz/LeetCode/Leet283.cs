// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet283
{
    public void MoveZeroes(int[] nums)
    {

        int lastInsertedZeroPointer = nums.Length;
        int workerPointer = lastInsertedZeroPointer - 1;

        while (workerPointer >= 0)
        {
            if (nums[workerPointer] != 0)
            {
                workerPointer--;
            }
            else
            {
                for (int i = workerPointer; i < lastInsertedZeroPointer - 1; i++)
                {
                    nums[i] = nums[i + 1];
                }

                lastInsertedZeroPointer--;
                nums[lastInsertedZeroPointer] = 0;
                workerPointer--;
            }
        }
    }
}