// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _860
{
    public bool LemonadeChange(int[] bills)
    {
        Dictionary<int, int> cash = new Dictionary<int, int>()
        {
            [5] = 0,
            [10] = 0,
            [20] = 0
        };

        foreach (var bill in bills)
        {
            if (bill == 5)
            {
                cash[5]++;
            } else if (bill == 10)
            {
                if (cash[5] < 1)
                {
                    return false;
                }

                cash[5]--;
                cash[10]++;
            }
            else
            {
                if (cash[10] > 0 && cash[5] > 0)
                {
                    cash[10]--;
                    cash[5]--;
                    cash[20]++;
                } else if (cash[5] >= 3)
                {
                    cash[5] -= 3;
                    cash[20]++;
                }
                else
                {
                    return false;
                }
            }
        }

        return true;
    }
}