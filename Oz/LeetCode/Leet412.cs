// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet412
{
    public IList<string> FizzBuzz(int n)
    {
        string[] arr = new string[n];

        for (int i = 0; i < n; i++)
        {
            int num = i + 1;
            if (num % 3 == 0 && num % 5 == 0)
            {
                arr[i] = "FizzBuzz";
            } else if (num % 3 == 0)
            {
                arr[i] = "Fizz";
            } else if (num % 5 == 0)
            {
                arr[i] = "Buzz";
            }
            else
            {
                arr[i] = num.ToString();
            }
        }

        return arr.ToList();
    }
}