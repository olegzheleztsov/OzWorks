// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

public class Leet1539
{
    public int FindKthPositive(int[] arr, int k)
    {
        if (arr == null || arr.Length == 0)
        {
            return 0;
        }

        var prev = 0;
        var count = 0;

        foreach (var elem in arr)
        {
            if (count + elem - prev - 1 >= k)
            {
                k -= count;
                while (k-- > 0)
                {
                    prev++;
                }

                return prev;
            }

            count += elem - prev - 1;

            prev = elem;
        }

        return arr[^1] + k - count;
    }
}