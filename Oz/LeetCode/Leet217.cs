// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet217
{
    public bool ContainsDuplicate(int[] nums)
    {
        HashSet<int> uniqueValues = new HashSet<int>();
        foreach (var num in nums)
        {
            if (uniqueValues.Contains(num))
            {
                return true;
            }

            uniqueValues.Add(num);
        }

        return false;
    }
}