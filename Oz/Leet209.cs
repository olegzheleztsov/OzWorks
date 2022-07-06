// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Linq;

public class Leet209
{
    public int MinSubArrayLen(int target, int[] nums)
    {
        var opt = new int[nums.Length];
        var start = 0;
        var end = 0;
        var sum = nums[end];

        while (end < nums.Length)
        {
            var wasIncreased = false;
            while (start <= end)
            {
                if (sum >= target)
                {
                    sum -= nums[start];
                    start++;
                    wasIncreased = true;
                }
                else
                {
                    if (wasIncreased)
                    {
                        start--;
                        sum += nums[start];
                    }

                    break;
                }
            }

            if (start > end)
            {
                opt[end] = nums[end] >= target ? 1 : -1;
                end++;
                start = end;
                if (end < nums.Length)
                {
                    sum = nums[end];
                }
            }
            else
            {
                if (sum >= target)
                {
                    opt[end] = end - start + 1;
                }
                else
                {
                    opt[end] = -1;
                }

                end++;
                if (end < nums.Length)
                {
                    sum += nums[end];
                }
            }
        }

        var seq = opt.Where(n => n > 0);
        var enumerable = seq as int[] ?? seq.ToArray();
        if (enumerable.Any())
        {
            return enumerable.Min();
        }

        return 0;
    }
}