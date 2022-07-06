// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _658
{
    public static void Test()
    {
        int[] arr =
        {
            0,0,1, 2,3,3,4,7,7,8
        };
        int x = 5;
        int k = 3;

        var result = new _658().FindClosestElements(arr, k, x);
        Console.WriteLine(string.Join(", ", result));
    }
    
    public IList<int> FindClosestElements(int[] arr, int k, int x)
    {
        var left = 0;
        var right = arr.Length - 1;
        var mid = -1;
        var found = false;
        while (left <= right)
        {
            mid = left + ((right - left) / 2);
            if (arr[mid] == x)
            {
                found = true;
                break;
            }

            if (arr[mid] > x)
            {
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }

        var result = new List<int>();
        if (found)
        {
            result.Add(arr[mid]);
            k--;
            left = mid - 1;
            right = mid + 1;
        }
        else
        {
            left = Math.Min(left, right);
            right = left + 1;
        }

        while (left >= 0 && right < arr.Length && k > 0)
        {
            var dLeft = Math.Abs(arr[left] - x);
            var dRight = Math.Abs(arr[right] - x);
            if (dLeft <= dRight)
            {
                result.Insert(0, arr[left]);
                left--;
            }
            else
            {
                result.Add(arr[right]);
                right++;
            }

            k--;
        }

        while (left >= 0 && k > 0)
        {
            result.Insert(0, arr[left]);
            left--;
            k--;
        }

        while (right < arr.Length && k > 0)
        {
            result.Add(arr[right]);
            right++;
            k--;
        }

        return result;
    }
}