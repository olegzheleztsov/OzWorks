// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Oz.LeetCode.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oz.LeetCode.TopQuestions;

public class TopQuestionSolutions
{
    public string ConvertToTitle(int columnNumber)
    {
        var stringBuilder = new StringBuilder();
        while (columnNumber > 0)
        {
            var num = columnNumber % 26;
            if (num == 0)
            {
                stringBuilder.Insert(0, 'Z');
            }
            else
            {
                var c = (char)(num - 1 + 'A');
                stringBuilder.Insert(0, c);
            }

            if (num == 0)
            {
                num = 26;
            }

            columnNumber -= num;
            columnNumber /= 26;
        }

        return stringBuilder.ToString();
    }


    public int[] TwoSum(int[] numbers, int target)
    {
        var left = 0;
        var right = numbers.Length - 1;
        var sum = numbers[left] + numbers[right];
        while (true)
        {
            if (sum == target)
            {
                break;
            }

            if (sum < target)
            {
                left++;
            }
            else
            {
                right--;
            }

            sum = numbers[left] + numbers[right];
        }

        return new[] {left + 1, right + 1};
    }

    public bool IsBalanced(TreeNode root)
    {
        if (root == null)
        {
            return true;
        }

        if (root.left == null && root.right == null)
        {
            return true;
        }

        if (root.left == null && root.right != null)
        {
            return Height(root.right) <= 1;
        }

        if (root.left != null && root.right == null)
        {
            return Height(root.left) <= 1;
        }

        return Math.Abs(Height(root.left) - Height(root.right)) <= 1 &&
               IsBalanced(root.left) && IsBalanced(root.right);
    }

    private int Height(TreeNode node)
    {
        if (node == null)
        {
            return 0;
        }

        if (node.left == null && node.right == null)
        {
            return 1;
        }

        return 1 + Math.Max(Height(node.left), Height(node.right));
    }


    public int MinDepth(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }

        if (root.left == null && root.right == null)
        {
            return 1;
        }

        if (root.left != null && root.right == null)
        {
            return 1 + MinDepth(root.left);
        }

        if (root.left == null && root.right != null)
        {
            return 1 + MinDepth(root.right);
        }

        return 1 + Math.Min(MinDepth(root.left), MinDepth(root.right));
    }

    public int MySqrt(int x)
    {
        if (x == 1)
        {
            return 1;
        }

        var lower = 0;
        var upper = x;

        while (lower < upper && lower != upper - 1)
        {
            var test = (lower + upper) / 2;
            checked
            {
                try
                {
                    if (test * test == x)
                    {
                        return test;
                    }

                    if (test * test > x)
                    {
                        upper = test;
                    }
                    else
                    {
                        lower = test;
                    }
                }
                catch (OverflowException)
                {
                    upper = test;
                }
            }
        }

        return lower;
    }

    public string AddBinary(string a, string b)
    {
        string longNumber;
        string shortNumber;
        if (a.Length == b.Length)
        {
            longNumber = a;
            shortNumber = b;
        }
        else
        {
            longNumber = a.Length > b.Length ? a : b;
            shortNumber = a.Length < b.Length ? a : b;
        }

        var result = new List<int>();
        var memory = 0;
        var longIndex = longNumber.Length - 1;
        var shortIndex = shortNumber.Length - 1;

        while (shortIndex >= 0 || longIndex >= 0)
        {
            if (shortIndex >= 0)
            {
                var n1 = shortNumber[shortIndex] - '0';
                var n2 = longNumber[longIndex] - '0';
                var current = n1 + n2 + memory;

                memory = 0;
                while (current >= 2)
                {
                    current -= 2;
                    memory++;
                }

                result.Insert(0, current == 0 ? 0 : 1);
                shortIndex--;
                longIndex--;
            }
            else
            {
                var current = longNumber[longIndex] - '0' + memory;
                if (current == 0)
                {
                    result.Insert(0, 0);
                    memory = 0;
                }
                else if (current == 1)
                {
                    result.Insert(0, 1);
                    memory = 0;
                }
                else
                {
                    result.Insert(0, 0);
                    memory = 1;
                }

                longIndex--;
            }
        }

        if (memory > 0)
        {
            result.Insert(0, 1);
        }

        return string.Join(string.Empty, result);
    }

    public int[][] GenerateMatrix(int n)
    {
        var result = new int[n][];
        for (var i = 0; i < result.Length; i++)
        {
            result[i] = new int[n];
        }

        var direction = Direction.Right;

        int minRow = 0, maxRow = n - 1;
        int minCol = 0, maxCol = n - 1;

        var num = 1;
        while (num <= n * n)
        {
            int r;
            int c;
            switch (direction)
            {
                case Direction.Right:
                {
                    for (c = minCol; c <= maxCol; c++)
                    {
                        result[minRow][c] = num;
                        num++;
                    }

                    minRow++;
                    direction = Direction.Down;
                }
                    break;
                case Direction.Down:
                {
                    for (r = minRow; r <= maxRow; r++)
                    {
                        result[r][maxCol] = num;
                        num++;
                    }

                    maxCol--;
                    direction = Direction.Left;
                }
                    break;
                case Direction.Left:
                {
                    for (c = maxCol; c >= minCol; c--)
                    {
                        result[maxRow][c] = num;
                        num++;
                    }

                    maxRow--;
                    direction = Direction.Up;
                }
                    break;
                case Direction.Up:
                {
                    for (r = maxRow; r >= minRow; r--)
                    {
                        result[r][minCol] = num;
                        num++;
                    }

                    minCol++;
                    direction = Direction.Right;
                }
                    break;
            }
        }

        return result;
    }


    public int LengthOfLastWord(string s)
    {
        var length = 0;

        var index = s.Length - 1;
        while (index >= 0 && s[index] == ' ')
        {
            index--;
        }

        while (index >= 0 && s[index] != ' ')
        {
            length++;
            index--;
        }

        return length;
    }

    public int Reverse(int x)
    {
        var result = 0;
        var isNegative = x < 0;


        try
        {
            checked
            {
                x = Math.Abs(x);
                while (x > 0)
                {
                    result = (result * 10) + (x % 10);
                    x /= 10;
                }
            }
        }
        catch (OverflowException)
        {
            return 0;
        }


        if (isNegative)
        {
            result = -result;
        }

        return result;
    }

    public int FirstUniqChar(string s)
    {
        var stat = new Dictionary<char, int>();
        foreach (var c in s)
        {
            if (stat.ContainsKey(c))
            {
                stat[c]++;
            }
            else
            {
                stat.Add(c, 1);
            }
        }

        for (var i = 0; i < s.Length; i++)
        {
            if (stat[s[i]] == 1)
            {
                return i;
            }
        }

        return -1;
    }

    public bool IsAnagram(string s, string t)
    {
        var counts = new Dictionary<char, int>();
        foreach (var c in s)
        {
            if (!counts.ContainsKey(c))
            {
                counts.Add(c, 1);
            }
            else
            {
                counts[c]++;
            }
        }

        foreach (var c in t)
        {
            if (counts.ContainsKey(c))
            {
                counts[c]--;
                if (counts[c] == 0)
                {
                    counts.Remove(c);
                }
            }
            else
            {
                counts.Add(c, 1);
            }
        }

        return counts.Count == 0;
    }

    public static bool IsPalindrome(string s)
    {
        var cleanStr = string.Join(string.Empty, s.Where(char.IsLetterOrDigit)).ToLower();
        var i = 0;
        var j = cleanStr.Length - 1;
        while (i < j)
        {
            if (cleanStr[i] != cleanStr[j])
            {
                return false;
            }

            i++;
            j--;
        }

        return true;
    }


    public int RomanToInt(string s)
    {
        var numberMap = new Dictionary<char, int>
        {
            ['I'] = 1,
            ['V'] = 5,
            ['X'] = 10,
            ['L'] = 50,
            ['C'] = 100,
            ['D'] = 500,
            ['M'] = 1000
        };

        var result = 0;
        for (var i = 0; i < s.Length; i++)
        {
            var currentChar = s[i];
            var isSpecialTreatment = false;
            if (currentChar == 'I')
            {
                var nextIndex = i + 1;
                if (nextIndex < s.Length)
                {
                    switch (s[nextIndex])
                    {
                        case 'V':
                            result += numberMap['V'] - numberMap['I'];
                            i = nextIndex;
                            isSpecialTreatment = true;
                            break;
                        case 'X':
                            result += numberMap['X'] - numberMap['I'];
                            i = nextIndex;
                            isSpecialTreatment = true;
                            break;
                    }
                }
            }
            else if (currentChar == 'X')
            {
                var nextIndex = i + 1;
                if (nextIndex < s.Length)
                {
                    switch (s[nextIndex])
                    {
                        case 'L':
                            result += numberMap['L'] - numberMap['X'];
                            i = nextIndex;
                            isSpecialTreatment = true;
                            break;
                        case 'C':
                            result += numberMap['C'] - numberMap['X'];
                            i = nextIndex;
                            isSpecialTreatment = true;
                            break;
                    }
                }
            }
            else if (currentChar == 'C')
            {
                var nextIndex = i + 1;
                if (nextIndex < s.Length)
                {
                    switch (s[nextIndex])
                    {
                        case 'D':
                            result += numberMap['D'] - numberMap['C'];
                            i = nextIndex;
                            isSpecialTreatment = true;
                            break;
                        case 'M':
                            result += numberMap['M'] - numberMap['C'];
                            i = nextIndex;
                            isSpecialTreatment = true;
                            break;
                    }
                }
            }

            if (!isSpecialTreatment)
            {
                result += numberMap[currentChar];
            }
        }

        return result;
    }

    public string LongestCommonPrefix(string[] strings)
    {
        var shortestString = strings[0];
        for (var i = 1; i < strings.Length; i++)
        {
            if (strings[i].Length < shortestString.Length)
            {
                shortestString = strings[i];
            }
        }

        var prefix = string.Empty;

        var index = 0;
        foreach (var c in shortestString)
        {
            if (strings.Any(s => s[index] != c))
            {
                return prefix;
            }

            index++;
            prefix += c;
        }

        return prefix;
    }

    public int LengthOfLongestSubstring(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return 0;
        }

        int count = 0, firstIndex = 0, lastIndex = 0;

        var substring = new HashSet<char>();

        while (lastIndex < s.Length)
        {
            if (substring.Contains(s[lastIndex]))
            {
                substring.Remove(s[firstIndex]);
                firstIndex++;
            }
            else
            {
                substring.Add(s[lastIndex]);
                lastIndex++;
                count = Math.Max(count, lastIndex - firstIndex);
            }
        }

        return count;
    }

    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        if (nums1.Length == 0 && nums2.Length == 0)
        {
            return 0;
        }

        int[] mergedArray;

        if (nums1.Length == 0)
        {
            mergedArray = nums2;
        }
        else if (nums2.Length == 0)
        {
            mergedArray = nums1;
        }
        else
        {
            mergedArray = new int[nums1.Length + nums2.Length];
            var firstIndex = 0;
            var secondIndex = 0;
            var index = 0;
            while (firstIndex < nums1.Length && secondIndex < nums2.Length)
            {
                if (nums1[firstIndex] <= nums2[secondIndex])
                {
                    mergedArray[index] = nums1[firstIndex];
                    index++;
                    firstIndex++;
                }
                else
                {
                    mergedArray[index] = nums2[secondIndex];
                    index++;
                    secondIndex++;
                }
            }

            while (firstIndex < nums1.Length)
            {
                mergedArray[index] = nums1[firstIndex];
                index++;
                firstIndex++;
            }

            while (secondIndex < nums2.Length)
            {
                mergedArray[index] = nums2[secondIndex];
                index++;
                secondIndex++;
            }
        }

        if (mergedArray.Length % 2 == 1)
        {
            return mergedArray[mergedArray.Length / 2];
        }

        return (mergedArray[(mergedArray.Length / 2) - 1] + mergedArray[mergedArray.Length / 2]) / 2.0;
    }

    private enum Direction
    {
        Right,
        Down,
        Left,
        Up
    }
}