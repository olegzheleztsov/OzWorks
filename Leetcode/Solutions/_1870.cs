// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _1870
{
    public int MinSpeedOnTime(int[] dist, double hour)
    {
        var low = 1;
        var high = int.MaxValue;

        while (low < high)
        {
            int mid = low + (high - low) / 2;
            if (IsValid(mid, dist, hour))
            {
                low = mid + 1;
            }
            else
            {
                high = mid;
            }
        }

        return low == int.MaxValue ? -1 : low;
    }
    private bool IsValid(int mid, int[] dist, double hour)
    {
        double time = 0;
        for (int i = 0; i < dist.Length - 1; i++)
        {
            time += Math.Ceiling(((double)dist[i]) / mid);
        }

        time += ((double)dist[dist.Length - 1]) / mid;
        if (time > hour)
        {
            return true;
        }

        return false;
    }
}