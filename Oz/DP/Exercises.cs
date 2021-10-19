// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Linq;

namespace Oz.DP;

public static class Exercises
{
    public static bool IsInterleavingDp(string a, string b, string c)
    {
        var m = a.Length;
        var n = b.Length;

        if (c.Length != m + n)
        {
            return false;
        }

        var mat = new bool[m + 1, n + 1];
        mat[0, 0] = true;

        for (var i = 1; i <= m; i++)
        {
            if (a[i - 1] != c[i - 1])
            {
                mat[i, 0] = false;
            }
            else
            {
                mat[i, 0] = mat[i - 1, 0];
            }
        }

        for (var j = 1; j <= n; j++)
        {
            if (b[j - 1] != c[j - 1])
            {
                mat[0, j] = false;
            }
            else
            {
                mat[0, j] = mat[0, j - 1];
            }
        }

        for (var i = 1; i <= m; i++)
        {
            for (var j = 1; j <= n; j++)
            {
                if (a[i - 1] == c[i + j - 1] && b[j - 1] != c[i + j - 1])
                {
                    mat[i, j] = mat[i - 1, j];
                }
                else if (a[i - 1] != c[i + j - 1] && b[j - 1] == c[i + j - 1])
                {
                    mat[i, j] = mat[i, j - 1];
                }
                else if (a[i - 1] == c[i + j - 1] && b[j - 1] == c[i + j - 1])
                {
                    mat[i, j] = mat[i - 1, j] || mat[i, j - 1];
                }
                else
                {
                    mat[i, j] = false;
                }
            }
        }

        return mat[m, n];
    }

    public static bool IsInterleaving(string a, string b, string c)
    {
        if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b) && string.IsNullOrEmpty(c))
        {
            return true;
        }

        if (string.IsNullOrEmpty(c))
        {
            return false;
        }

        if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
        {
            return false;
        }

        var first = false;
        var second = false;

        if (a is {Length: > 0} && a[0] == c[0])
        {
            first = IsInterleaving(a[1..], b, c[1..]);
        }

        if (b is {Length: > 0} && b[0] == c[0])
        {
            second = IsInterleaving(a, b[1..], c[1..]);
        }

        return first || second;
    }


    public static int EditDistance(string str1, string str2)
    {
        if (str1 == null && str2 == null)
        {
            return 0;
        }

        if (string.IsNullOrEmpty(str1))
        {
            return str2.Length;
        }

        if (string.IsNullOrEmpty(str2))
        {
            return str1.Length;
        }

        if (str1[0] == str2[0])
        {
            return EditDistance(str1[1..], str2[1..]);
        }

        var d = EditDistance(str1[1..], str2);
        var u = EditDistance(str1[1..], str2[1..]);
        var i = EditDistance(str1, str2[1..]);
        return new[] {d, u, i}.Min() + 1;
    }

    public static int EditDistDp(string s1, string s2, int m, int n)
    {
        var editD = new int[m + 1, n + 1];
        for (var j = 0; j <= n; j++)
        {
            editD[0, j] = j;
        }

        for (var i = 0; i <= m; i++)
        {
            editD[i, 0] = i;
        }

        for (var i = 1; i <= m; i++)
        {
            for (var j = 1; j <= n; j++)
            {
                if (s1[i - 1] == s2[j - 1])
                {
                    editD[i, j] = editD[i - 1, j - 1];
                }
                else
                {
                    editD[i, j] = new[] {editD[i, j - 1], editD[i - 1, j], editD[i - 1, j - 1]}.Min() + 1;
                }
            }
        }

        return editD[m, n];
    }


    public static int MaxSubStringLengthDp(string str)
    {
        var sum = new int[str.Length, str.Length];
        var maxLen = 0;

        for (var i = 0; i < str.Length; i++)
        {
            sum[i, i] = str[i] - '0';
        }

        for (var len = 2; len <= str.Length; len++)
        {
            for (var i = 0; i < str.Length - len + 1; i++)
            {
                var j = i + len - 1;
                var k = len / 2;
                sum[i, j] = sum[i, j - k] + sum[j - k + 1, j];
                if (len % 2 == 0 && sum[i, j - k] == sum[j - k + 1, j] && len > maxLen)
                {
                    maxLen = len;
                }
            }
        }

        return maxLen;
    }

    public static int NumberOfWays(int total)
    {
        return total switch
        {
            1 => 1,
            2 => 2,
            _ => NumberOfWaysInner(0, 0, total)
        };

        int NumberOfWaysInner(int row, int col, int n)
        {
            if (row == n - 1 && col < n - 1)
            {
                return 1;
            }

            if (row < n - 1 && col == n - 1)
            {
                return 1;
            }

            var num1 = 0;
            if (row + 1 < n)
            {
                num1 = NumberOfWaysInner(row + 1, col, n);
            }

            var num2 = 0;
            if (col + 1 < n)
            {
                num2 = NumberOfWaysInner(row, col + 1, n);
            }

            return num1 + num2;
        }
    }

    public static void PrintTable(int n)
    {
        PrintTableInner(n, 1);

        static void PrintTableInner(int n, int index)
        {
            Console.WriteLine($"{n} * {index} = {n * index}{Environment.NewLine}");
            if (index + 1 < 11)
            {
                PrintTableInner(n, index + 1);
            }
        }
    }


    public static int FactorialRecursive(int n)
    {
        if (n == 0)
        {
            return 1;
        }

        return n * FactorialRecursive(n - 1);
    }

    public static int FactorialIterative(int n)
    {
        if (n == 0)
        {
            return 1;
        }

        var result = 1;
        for (var i = 1; i <= n; i++)
        {
            result *= i;
        }

        return result;
    }

    public static int[] SubarraySumRecursive(int[] array)
    {
        var copy = new int[array.Length];
        Array.Copy(array, copy, array.Length);
        Compute(copy, copy.Length - 1);
        return copy;

        static int Compute(int[] arr, int index)
        {
            if (index == 0)
            {
                return arr[0];
            }

            arr[index] += Compute(arr, index - 1);
            return arr[index];
        }
    }

    public static int[] SubarraySumNonRecursive(int[] array)
    {
        var copy = new int[array.Length];
        Array.Copy(array, copy, array.Length);

        for (var i = 1; i < copy.Length; i++)
        {
            copy[i] += copy[i - 1];
        }

        return copy;
    }
}