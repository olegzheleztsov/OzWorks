// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet1395
{
    public int NumTeams(int[] rating)
    {
        var count = 0;
        for (var i = 0; i < rating.Length; i++)
        {
            for (var j = i + 1; j < rating.Length; j++)
            {
                for (var k = j + 1; k < rating.Length; k++)
                {
                    if (rating[i] < rating[j] && rating[j] < rating[k])
                    {
                        count++;
                    }

                    if (rating[i] > rating[j] && rating[j] > rating[k])
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }
}