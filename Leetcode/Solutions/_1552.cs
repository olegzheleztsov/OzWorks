// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _1552
{
    public int MaxDistance(int[] position, int m)
    {
        Array.Sort(position);

        var low = 1;
        var high = position[^1] - position[0];

        while (low < high)
        {
            var mid = 1 + low + ((high - low) / 2);
            var count = 1;

            var previous = position[0];

            for (var i = 1; i < position.Length; i++)
            {
                if (position[i] - previous >= mid)
                {
                    previous = position[i];
                    count++;
                }
            }

            if (count >= m)
            {
                low = mid;
            }
            else
            {
                high = mid - 1;
            }
        }

        return low;
    }
}