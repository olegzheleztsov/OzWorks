// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _1089
{
    public void DuplicateZeros(int[] arr)
    {
        int pointer = 0;
        while (pointer < arr.Length)
        {
            if (arr[pointer] == 0)
            {
                Shift(arr, pointer + 1);
                if (pointer + 1 < arr.Length)
                {
                    arr[pointer + 1] = 0;
                }
                pointer += 2;
            }
            else
            {
                pointer++;
            }
        }
    }

    private void Shift(int[] arr, int index)
    {
        for (int i = arr.Length - 1; i > index; i--)
        {
            arr[i] = arr[i - 1];
        }
    }
}