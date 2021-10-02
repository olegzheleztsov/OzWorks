// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Linq;

namespace Oz.DP
{
    public static class Exercises
    {
        public static bool IsInterleavingDP(string A, string B, string C)
        {
            int M = A.Length;
            int N = B.Length;

            if(C.Length != (M + N))
            {
                return false;
            }

            bool[,] Mat = new bool[M + 1, N + 1];
            Mat[0, 0] = true;

            for(int i = 1; i <= M; i++ )
            {
                if(A[i-1] != C[i-1])
                {
                    Mat[i, 0] = false;
                } else
                {
                    Mat[i, 0] = Mat[i - 1, 0];
                }
            }

            for(int j = 1; j <= N; j++)
            {
                if(B[j-1] != C[j-1])
                {
                    Mat[0, j] = false;
                } else
                {
                    Mat[0, j] = Mat[0, j - 1];
                }
            }

            for(int i = 1; i <= M; i++ )
            {
                for(int j = 1; j <= N; j++)
                {
                    if(A[i-1] == C[i + j - 1] && B[j-1] != C[i + j - 1])
                    {
                        Mat[i, j] = Mat[i - 1, j];
                    } else if(A[i-1] != C[i+j-1] && B[j-1] == C[i + j - 1])
                    {
                        Mat[i, j] = Mat[i, j - 1];
                    } else if(A[i-1] == C[i + j -1] && B[j-1] == C[i + j - 1])
                    {
                        Mat[i, j] = Mat[i - 1, j] || Mat[i, j - 1];
                    } else
                    {
                        Mat[i, j] = false;
                    }
                }
            }
            return Mat[M, N];
        }
    
        public static bool IsInterleaving(string A, string B, string C)
        {
            if(string.IsNullOrEmpty(A) && string.IsNullOrEmpty(B) && string.IsNullOrEmpty(C))
            {
                return true;
            }
            if(string.IsNullOrEmpty(C))
            {
                return false;
            }
            if(string.IsNullOrEmpty(A) && string.IsNullOrEmpty(B))
            {
                return false;
            }

            bool first = false;
            bool second = false;

            if (A.Length > 0 && A[0] == C[0])
            {
                first = IsInterleaving(A.Substring(1), B, C.Substring(1));
            }
            if(B.Length > 0 && B[0] == C[0])
            {
                second = IsInterleaving(A, B.Substring(1), C.Substring(1));
            }
            return first || second;
        }


        public static int EditDistance(string str1, string str2) {
            if(str1 == null && str2 == null) {
                return 0;
            }
            if(string.IsNullOrEmpty(str1)) {
                return str2.Length;
            }
            if(string.IsNullOrEmpty(str2)) {
                return str1.Length;
            }

            if (str1[0] == str2[0])
            {
                return EditDistance(str1[1..], str2[1..]);
            }

            int d, u, i;
            d = EditDistance(str1[1..], str2);
            u = EditDistance(str1[1..], str2[1..]);
            i = EditDistance(str1, str2[1..]);
            return new int[] { d, u, i }.Min() + 1;
        }

        public static int EditDistDP(string s1, string s2, int m, int n) {
            int[,] editD = new int[m + 1, n + 1];
            for (int j = 0; j <= n; j++)
            {
                editD[0, j] = j;
            }
            for (int i = 0; i <= m; i++)
            {
                editD[i, 0] = i;
            }

            for (int i = 1; i <= m; i++)
            { 
                for(int j = 1; j <= n; j++)
                {
                    if(s1[i-1] == s2[j-1])
                    {
                        editD[i, j] = editD[i - 1, j - 1];
                    } else
                    {
                        editD[i, j] = new int[] { editD[i, j - 1], editD[i - 1, j], editD[i - 1, j - 1] }.Min() + 1;
                    }
                }
            }
            return editD[m, n];
        }


        public static int MaxSubStringLengthDP(string str)
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
            if (total == 1)
            {
                return 1;
            }

            if (total == 2)
            {
                return 2;
            }

            return NumberOfWaysInner(0, 0, total);

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
}