// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet852
{
    public int PeakIndexInMountainArray(int[] arr)
    {
        int minIndex = 1;
        int maxIndex = arr.Length - 2;

        while (minIndex < maxIndex)
        {
            int mid = minIndex + (maxIndex - minIndex) / 2;

            if (IsPeak(arr, mid))
            {
                return mid;
            }

            if (arr[mid - 1] < arr[mid] && arr[mid] < arr[mid + 1])
            {
                minIndex = mid + 1;
            }

            if (arr[mid - 1] > arr[mid] && arr[mid] > arr[mid + 1])
            {
                maxIndex = mid - 1;
            }
        }

        if (IsPeak(arr, minIndex))
        {
            return minIndex;
        }

        return maxIndex;
    }

    private bool IsPeak(int[] nums, int i)
    {
        return nums[i - 1] < nums[i] && nums[i] > nums[i + 1];
    }
}

public class Leet35
{
    public int SearchInsert(int[] nums, int target)
    {

        int minIndex = 0;
        int maxIndex = nums.Length - 1;

        while (minIndex < maxIndex)
        {
            int mid = minIndex + (maxIndex - minIndex) / 2;
            if (nums[mid] == target)
            {
                return mid;
            }

            if (nums[mid] < target)
            {
                minIndex = mid + 1;
            }
            else
            {
                maxIndex = mid - 1;
            }
        }

        if (target > nums[minIndex])
        {
            return minIndex + 1;
        }

        return minIndex;
    }
    
    
}
public class Solution {
    private readonly int[] _nums;
    private readonly int[] _current;
    private readonly Random _random = new Random();
    
    public Solution(int[] nums)
    {
        _nums = nums;
        _current = new int[nums.Length];
        Array.Copy(nums, _current, nums.Length);
    }
    
    public int[] Reset() =>
        _nums;

    public int[] Shuffle() {
        for (int i = 0; i < _nums.Length; i++)
        {
            var x = _random.Next(_nums.Length);
            var y = _random.Next(_nums.Length);
            (_current[x], _current[y]) = (_current[y], _current[x]);
        }

        return _current;
    }
}

public class Leet201
{
    public int RangeBitwiseAnd(int left, int right)
    {
        int tailingZeroCnt = 0;
        while (left != right)
        {
            tailingZeroCnt++;
            left >>= 1;
            right >>= 1;
        }

        return left << tailingZeroCnt;
    }
}

public class Leet343
{
    public int IntegerBreak(int n)
    {
        if (n == 2)
        {
            return 1;
        }

        checked
        {
            int[] dp = new int[n + 1];
            dp[0] = dp[1] = dp[2] = 1;
            for (int i = 3; i <= n; i++)
            {
                for (int j = 1; j < i; j++)
                {
                    int prevProd = dp[i - j];
                    dp[i] = Math.Max(dp[i], j * (i - j));
                    dp[i] = Math.Max(dp[i], j * prevProd);
                }
            }

            return dp[n];
        }
    }
}

public class Leet322
{
    public int CoinChange(int[] coins, int amount)
    {

        Dictionary<int, int> amountCountMap = new Dictionary<int, int>();
        return Find(coins, amount, amountCountMap);
    }

    private int Find(int[] coins, int amount, Dictionary<int, int> memo)
    {
        if (amount == 0)
        {
            return 0;
        }

        if (memo.ContainsKey(amount))
        {
            return memo[amount];
        }

        int minCoin = int.MaxValue;
        foreach(var coin in coins)
        {
            int newAmount = amount - coin;
            if (newAmount >= 0)
            {
                int val = Find(coins, newAmount, memo);
                if (val >= 0)
                {
                    minCoin = Math.Min(minCoin, val);
                }
            }
        }

        memo[amount] = minCoin == int.MaxValue ? -1 : minCoin + 1;
        return memo[amount];
    }
}


public class Leet72
{
    public int MinDistance(string word1, string word2) {
        if (string.IsNullOrEmpty(word1) && string.IsNullOrEmpty(word2))
        {
            return 0;
        }

        if (string.IsNullOrEmpty(word1))
        {
            return word2.Length;
        }

        if (string.IsNullOrEmpty(word2))
        {
            return word1.Length;
        }

        if (word1 == word2)
        {
            return 0;
        }

        int[,] dp = new int[word1.Length + 1, word2.Length + 1];
        for (int i = 0; i <= word1.Length; i++)
        {
            dp[i, word2.Length] = word1.Length - i;
        }

        for (int j = 0; j <= word2.Length; j++)
        {
            dp[word1.Length, j] = word2.Length - j;
        }

        
        for (int i = word1.Length - 1; i >= 0; i--)
        {
            for (int j = word2.Length - 1; j >= 0; j--)
            {
                if (word1[i] == word2[j])
                {
                    dp[i, j] = dp[i + 1, j + 1];
                }
                else
                {
                    dp[i, j] = Math.Min(Math.Min(dp[i + 1, j], dp[i, j + 1]), dp[i + 1, j + 1]) + 1;
                }
            }
        }

        return dp[0, 0];
    }
}
public class Leet583
{
    public int MinDistance(string word1, string word2)
    {
        int len1 = word1.Length;
        int len2 = word2.Length;

        int[,] dp = new int[len1 + 1, len2 + 1];
        for (int i = 1; i <= len1; i++)
        {
            for (int j = 1; j <= len2; j++)
            {
                if (word1[i - 1] == word2[j - 1])
                {
                    dp[i, j] = dp[i - 1, j - 1] + 1;
                }
                else
                {
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                }
            }
        }

        return len1 + len2 - 2 * dp[len1, len2];
    }
}
public class Leet1143
{
    public int LongestCommonSubsequence(string text1, string text2)
    {
        if (string.IsNullOrEmpty(text1) ||
            string.IsNullOrEmpty(text2))
        {
            return 0;
        }
        
        int[,] dp = new int[text1.Length, text2.Length];

        for (int i = 0; i < text1.Length; i++)
        {
            if (text1.Substring(0, i + 1).Contains(text2[0]))
            {
                dp[i, 0] = 1;
            }
            else
            {
                dp[i, 0] = 0;
            }
        }

        for (int j = 1; j < text2.Length; j++)
        {
            if (text2.Substring(0, j + 1).Contains(text1[0]))
            {
                dp[0, j] = 1;
            }
            else
            {
                dp[0, j] = 0;
            }
        }
        
        
        for(int i = 1; i < text1.Length; i++ )
        {
            for (int j = 1; j < text2.Length; j++)
            {
                if (text1[i] == text2[j])
                {
                    dp[i, j] = dp[i - 1, j - 1] + 1;
                }
                else
                {
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                }
            }
        }

        return dp[text1.Length - 1, text2.Length - 1];
    }
}
public class Leet673
{
    public int FindNumberOfLIS(int[] nums)
    {
        var dp = nums
            .Select(_ => new[] {1, 1})
            .ToArray();
        var max = 1;

        for (var i = 1; i < nums.Length; i++)
        {
            for (int j = 0, last = 0; j < i; j++)
            {
                if (nums[i] <= nums[j] || dp[j][0] + 1 < dp[i][0])
                {
                    continue;
                }

                if (last == dp[j][0])
                {
                    dp[i][1] += dp[j][1];
                }
                else
                {
                    dp[i] = new[] {dp[j][0] + 1, dp[j][1]};
                }

                (max, last) = (Math.Max(max, dp[i][0]), dp[j][0]);
            }
        }

        return dp.Sum(e => e[0] == max ? e[1] : 0);
    }
}

public class Leet79
{
    public bool Exist(char[][] board, string word)
    {
        if (board == null || board.Length == 0 || word == null || word == string.Empty)
        {
            return false;
        }


        for (var i = 0; i < board.Length; i++)
        {
            for (var j = 0; j < board[0].Length; j++)
            {
                if (board[i][j] != word[0])
                {
                    continue;
                }

                var visited = new bool[board.Length, board[0].Length];
                if (Backtrack(board, visited, word, i, j, 0))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool Backtrack(char[][] board, bool[,] visited, string word, int r, int c, int k)
    {
        int[] dr = {1, -1, 0, 0};
        int[] dc = {0, 0, 1, -1};

        if (board[r][c] == word[k])
        {
            if (k == word.Length - 1)
            {
                return true;
            }

            visited[r, c] = true;
            for (var l = 0; l < 4; l++)
            {
                var nr = r + dr[l];
                var nc = c + dc[l];

                if (nr >= 0 && nr < board.Length &&
                    nc >= 0 && nc < board[0].Length &&
                    !visited[nr, nc] &&
                    Backtrack(board, visited, word, nr, nc, k + 1))
                {
                    return true;
                }
            }

            visited[r, c] = false;
        }

        return false;
    }
}