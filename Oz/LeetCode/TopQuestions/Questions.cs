// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Oz.LeetCode.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace Oz.LeetCode.TopQuestions;

public class Questions
{
    public IList<int> FindSubstring(string s, string[] words)
    {
        var wordLen = words.Sum(w => w.Length);
        if (wordLen > s.Length)
        {
            return new List<int>();
        }

        var result = new List<int>();
        foreach (var indices in GetPermutations(words.Length - 1))
        {
            var subStr = GetConcatenatedString(words, indices);
            var targetIndx = s.IndexOf(subStr, StringComparison.Ordinal);
            if (targetIndx >= 0)
            {
                result.Add(targetIndx);
            }
        }

        return result;
    }

    private string GetConcatenatedString(string[] words, List<int> indexes)
    {
        var sb = new StringBuilder();
        foreach (var ind in indexes)
        {
            sb.Append(words[ind]);
        }

        return sb.ToString();
    }

    private List<List<int>> GetPermutations(int number)
    {
        if (number == 0)
        {
            return new List<List<int>> {new() {number}};
        }

        var prevPerms = GetPermutations(number - 1);
        var result = new List<List<int>>();
        foreach (var perm in prevPerms)
        {
            for (var i = 0; i < perm.Count; i++)
            {
                var newPerm = new List<int>(perm);
                newPerm.Insert(i, number);
                result.Add(newPerm);
            }
        }

        return result;
    }


    public int MajorityElement(int[] nums)
    {
        var countMap = new Dictionary<int, int>();
        foreach (var n in nums)
        {
            if (countMap.ContainsKey(n))
            {
                countMap[n]++;
            }
            else
            {
                countMap.Add(n, 1);
            }
        }

        var majorCount = nums.Length / 2;
        int? target = null;

        foreach (var kvp in countMap)
        {
            if (kvp.Value > majorCount)
            {
                if (target == null || kvp.Key > target.Value)
                {
                    target = kvp.Key;
                }
            }
        }

        if (target != null)
        {
            return target.Value;
        }

        return 0;
    }

    public int Tribonacci(int n)
    {
        var numbers = new int[38];
        numbers[0] = 0;
        numbers[1] = 1;
        numbers[2] = 1;
        if (n == 0)
        {
            return numbers[0];
        }

        if (n == 1 || n == 2)
        {
            return numbers[1];
        }

        for (var i = 3; i <= n; i++)
        {
            numbers[i] = numbers[i - 1] + numbers[i - 2] + numbers[i - 3];
        }

        return numbers[n];
    }


    public int MaxWidthOfVerticalArea(int[][] points)
    {
        int Comparison(int[] a, int[] b) =>
            a[0].CompareTo(b[0]);

        Array.Sort(points, (Comparison<int[]>) Comparison);

        var distance = 0;

        for (var i = 1; i < points.Length; i++)
        {
            if (points[i][0] != points[i - 1][0])
            {
                var test = points[i][0] - points[i - 1][0];
                if (test > distance)
                {
                    distance = test;
                }
            }
        }

        return distance;
    }

    public int[] FrequencySort(int[] nums)
    {
        var frequencies = new Dictionary<int, int>();
        foreach (var number in nums)
        {
            if (frequencies.ContainsKey(number))
            {
                frequencies[number]++;
            }
            else
            {
                frequencies.Add(number, 1);
            }
        }

        int Comparision(int a, int b)
        {
            if (frequencies[a] < frequencies[b])
            {
                return -1;
            }

            if (frequencies[a] > frequencies[b])
            {
                return 1;
            }

            if (a > b)
            {
                return -1;
            }

            if (a < b)
            {
                return 1;
            }

            return 0;
        }

        Array.Sort(nums, Comparision);
        return nums;
    }


    public int Jump(int[] nums)
    {
        if (nums.Length <= 1)
        {
            return 0;
        }

        var jumpCount = new int[nums.Length];
        jumpCount[^1] = 0;
        for (var i = 0; i < jumpCount.Length - 1; i++)
        {
            if (nums[i] == 0)
            {
                jumpCount[i] = -1;
            }
            else
            {
                jumpCount[i] = int.MaxValue;
            }
        }

        for (var i = jumpCount.Length - 2; i >= 0; i--)
        {
            if (nums[i] > 0)
            {
                if (i + nums[i] >= nums.Length - 1)
                {
                    jumpCount[i] = 1;
                }
                else
                {
                    for (var j = i + 1; j <= i + nums[i]; j++)
                    {
                        if (jumpCount[j] >= 0 && jumpCount[j] + 1 < jumpCount[i])
                        {
                            jumpCount[i] = jumpCount[j] + 1;
                        }
                    }

                    if (jumpCount[i] == int.MaxValue)
                    {
                        jumpCount[i] = -1;
                    }
                }
            }
        }

        return jumpCount[0];
    }


    public string IntToRoman(int num)
    {
        var numberMap = new Dictionary<int, string>
        {
            [1] = "I",
            [5] = "V",
            [10] = "X",
            [50] = "L",
            [100] = "C",
            [500] = "D",
            [1000] = "M"
        };

        var sb = new StringBuilder();

        Aggregate(num, ref sb);
        return sb.ToString();

        void Aggregate(int number, ref StringBuilder stringBuilder)
        {
            if (number <= 0)
            {
                return;
            }

            if (number >= 1000)
            {
                stringBuilder.Append(numberMap[1000]);
                number -= 1000;
                Aggregate(number, ref stringBuilder);
            }
            else if (number >= 900)
            {
                stringBuilder.Append(numberMap[100] + numberMap[1000]);
                number -= 900;
                Aggregate(number, ref stringBuilder);
            }
            else if (number >= 500)
            {
                stringBuilder.Append(numberMap[500]);
                number -= 500;
                Aggregate(number, ref stringBuilder);
            }
            else if (number >= 400)
            {
                stringBuilder.Append(numberMap[100] + numberMap[500]);
                number -= 400;
                Aggregate(number, ref stringBuilder);
            }
            else if (number >= 100)
            {
                stringBuilder.Append(numberMap[100]);
                number -= 100;
                Aggregate(number, ref stringBuilder);
            }
            else if (number >= 90)
            {
                stringBuilder.Append(numberMap[10] + numberMap[100]);
                number -= 90;
                Aggregate(number, ref stringBuilder);
            }
            else if (number >= 50)
            {
                stringBuilder.Append(numberMap[50]);
                number -= 50;
                Aggregate(number, ref stringBuilder);
            }
            else if (number >= 40)
            {
                stringBuilder.Append(numberMap[10] + numberMap[50]);
                number -= 40;
                Aggregate(number, ref stringBuilder);
            }
            else if (number >= 10)
            {
                stringBuilder.Append(numberMap[10]);
                number -= 10;
                Aggregate(number, ref stringBuilder);
            }
            else if (number == 9)
            {
                stringBuilder.Append(numberMap[1] + numberMap[10]);
                number -= 9;
                Aggregate(number, ref stringBuilder);
            }
            else if (number >= 5)
            {
                stringBuilder.Append(numberMap[5]);
                number -= 5;
                Aggregate(number, ref stringBuilder);
            }
            else if (number == 4)
            {
                stringBuilder.Append(numberMap[1] + numberMap[5]);
                number -= 4;
                Aggregate(number, ref stringBuilder);
            }
            else
            {
                stringBuilder.Append(numberMap[1]);
                number--;
                Aggregate(number, ref stringBuilder);
            }
        }
    }

    /// <summary>
    ///     https://leetcode.com/problems/jewels-and-stones/
    /// </summary>
    public int NumJewelsInStones(string jewels, string stones)
    {
        var jewelsSet = new HashSet<char>();
        foreach (var c in jewels)
        {
            jewelsSet.Add(c);
        }

        var counter = 0;
        foreach (var c in stones)
        {
            if (jewelsSet.Contains(c))
            {
                counter++;
            }
        }

        return counter;
    }

    /// <summary>
    ///     https://leetcode.com/problems/flatten-binary-tree-to-linked-list/
    /// </summary>
    public void Flatten(TreeNode root)
    {
        if (root == null)
        {
            return;
        }

        var stack = new Stack<TreeNode>();
        TreeNode head = null;
        TreeNode last = null;
        stack.Push(root);

        while (stack.Count > 0)
        {
            var current = stack.Pop();

            if (current.right != null)
            {
                stack.Push(current.right);
            }

            if (current.left != null)
            {
                stack.Push(current.left);
            }


            if (head == null)
            {
                head = last = current;
                current.left = current.right = null;
            }
            else
            {
                last.right = current;
                current.left = null;
                last = current;
            }
        }
    }

    public void PrintTreeAsList(TreeNode root)
    {
        while (root != null)
        {
            Write($"{root.val}, ");
            root = root.right;
        }
    }

    /// <summary>
    ///     https://leetcode.com/problems/invert-binary-tree/
    /// </summary>
    public TreeNode InvertTree(TreeNode root)
    {
        if (root == null)
        {
            return null;
        }

        (root.left, root.right) = (root.right, root.left);
        InvertTree(root.left);
        InvertTree(root.right);
        return root;
    }

    public void PreorderPrint(TreeNode root)
    {
        var queue = new Queue<TreeNode>();
        if (root != null)
        {
            queue.Enqueue(root);
        }

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            Write($"{node.val}, ");
            if (node.left != null)
            {
                queue.Enqueue(node.left);
            }

            if (node.right != null)
            {
                queue.Enqueue(node.right);
            }
        }

        WriteLine();
    }


    public int MaxArea(int[] height)
    {
        var leftIndex = 0;
        var rightIndex = height.Length - 1;
        var currentArea = GetArea(height, leftIndex, rightIndex);

        while (leftIndex < rightIndex)
        {
            var area = GetArea(height, leftIndex, rightIndex);
            currentArea = Math.Max(area, currentArea);
            if (height[leftIndex] < height[rightIndex])
            {
                leftIndex++;
            }
            else
            {
                rightIndex--;
            }
        }

        return currentArea;
    }

    private int GetArea(int[] height, int l, int r) =>
        (r - l) * Math.Min(height[l], height[r]);

    public int MinimumTotal(IList<IList<int>> triangle)
    {
        if (triangle.Count == 1)
        {
            return triangle[0][0];
        }

        if (triangle.Count == 2)
        {
            return Math.Min(triangle[0][0] + triangle[1][0], triangle[0][0] + triangle[1][1]);
        }

        return MinimumTotalHelp(triangle);
    }

    private int MinimumTotalHelp(IList<IList<int>> triangle)
    {
        var previous = new int[triangle.Count + 1];
        int[] current = null;
        for (var i = triangle.Count - 1; i > 0; i--)
        {
            var currentArray = triangle[i];
            current = new int[triangle[i].Count - 1];
            for (var j = 0; j < current.Length; j++)
            {
                current[j] = Math.Min(currentArray[j] + previous[j], currentArray[j] + previous[j + 1]);
            }

            previous = current;
        }

        if (current != null)
        {
            return current[0] + triangle[0][0];
        }

        return 0;
    }


    public IList<IList<int>> Generate(int numRows)
    {
        switch (numRows)
        {
            case 1:
                return new List<IList<int>> {new List<int> {1}};
            case 2:
                return new List<IList<int>> {new List<int> {1}, new List<int> {1, 1}};
        }

        var previousList = Generate(numRows - 1);
        var currentList = new int[numRows];
        currentList[0] = 1;
        currentList[^1] = 1;
        for (var i = 1; i < currentList.Length - 1; i++)
        {
            currentList[i] = previousList[numRows - 2][i - 1] + previousList[numRows - 2][i];
        }

        previousList.Add(currentList.ToList());
        return previousList;
    }

    public bool IsHappy(int n)
    {
        var memory = new HashSet<int>();

        while (!memory.Contains(n) && n != 1)
        {
            memory.Add(n);
            var numbers = GetNumbers(n);
            n = numbers.Sum(i => i * i);
        }

        if (n == 1)
        {
            return true;
        }

        return false;
    }

    private int[] GetNumbers(int n)
    {
        var numbers = new List<int>();

        while (n > 0)
        {
            var rem = n % 10;
            numbers.Add(rem);
            n -= rem;
            n /= 10;
        }

        return numbers.ToArray();
    }


    public int TrailingZeroes(int n)
    {
        var count = 0;
        while (n > 0)
        {
            n = n / 5;
            count += n;
        }

        return count;
    }

    /// <summary>
    ///     https://leetcode.com/problems/longest-palindromic-substring/submissions/
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public string LongestPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }

        var start = 0;
        var end = 0;

        for (var i = 0; i < s.Length; i++)
        {
            var len1 = LongestPalindromicSubstring(s, i, i);
            var len2 = LongestPalindromicSubstring(s, i, i + 1);
            var maxLen = Math.Max(len1, len2);
            if (maxLen > end - start + 1)
            {
                start = i - ((maxLen - 1) / 2);
                end = i + (maxLen / 2);
            }
        }

        return s[start..(end + 1)];
    }

    private int LongestPalindromicSubstring(string s, int left, int right)
    {
        if (string.IsNullOrEmpty(s) || left > right)
        {
            return 0;
        }

        while (left >= 0 && right < s.Length && s[left] == s[right])
        {
            left--;
            right++;
        }

        return right - left - 1;
    }


    public int CountPrimes(int n)
    {
        var count = 0;
        for (var i = 2; i < n; i++)
        {
            if (IsPrime(i))
            {
                count++;
            }
        }

        return count;
    }

    private bool IsPrime(int n)
    {
        switch (n)
        {
            case < 2:
                return false;
            case 2:
                return true;
        }

        if (n % 2 == 0)
        {
            return false;
        }

        var sqrtOfN = (int)Math.Ceiling(n / 2.0);

        for (var i = 3; i < sqrtOfN; i += 2)
        {
            if (n % i == 0)
            {
                return false;
            }
        }

        return true;
    }

    public TreeNode SortedArrayToBst(int[] nums)
    {
        if (nums == null || nums.Length == 0)
        {
            return null;
        }

        if (nums.Length == 1)
        {
            return new TreeNode(nums[0]);
        }

        return SortedSubArrayToBST(nums, 0, nums.Length - 1);
    }

    private TreeNode SortedSubArrayToBST(int[] nums, int leftIndex, int rightIndex)
    {
        if (rightIndex < leftIndex)
        {
            return null;
        }

        if (leftIndex == rightIndex)
        {
            return new TreeNode(nums[leftIndex]);
        }

        var midIndex = (int)Math.Ceiling((leftIndex + rightIndex) / 2.0);
        var root = new TreeNode(nums[midIndex])
        {
            left = SortedSubArrayToBST(nums, leftIndex, midIndex - 1),
            right = SortedSubArrayToBST(nums, midIndex + 1, rightIndex)
        };
        return root;
    }

    public void PrintBst(TreeNode node)
    {
        Inorder(node, n => Write($"{n.val}, "));
        WriteLine();

        void Inorder(TreeNode root, Action<TreeNode> visitor)
        {
            if (root == null)
            {
                return;
            }

            Inorder(root.left, visitor);
            visitor(root);
            Inorder(root.right, visitor);
        }
    }


    public IList<string> FullJustify(string[] words, int maxWidth)
    {
        var i = 0;
        var result = new List<string>();
        while (i < words.Length)
        {
            var j = i + 1;
            var lineLength = words[i].Length;
            while (j < words.Length && lineLength + (j - i - 1) + words[j].Length < maxWidth)
            {
                lineLength += words[j].Length;
                j++;
            }

            var diff = maxWidth - lineLength;
            if (j >= words.Length || j - i == 1)
            {
                result.Add(LeftJustify(words, diff, i, j));
            }
            else
            {
                result.Add(MiddleJustify(words, diff, i, j));
            }

            i = j;
        }

        return result;
    }

    private string MiddleJustify(string[] words, int diff, int i, int j)
    {
        var spacesBetween = diff / (j - i - 1);
        var extraSpaces = diff % (j - i - 1);
        var stringBuilder = new StringBuilder(words[i]);
        for (var k = i + 1; k < j; k++)
        {
            var extraCount = extraSpaces > 0 ? 1 : 0;
            extraSpaces--;
            var count = spacesBetween + extraCount;
            stringBuilder.Append(new string(' ', count) + words[k]);
        }

        return stringBuilder.ToString();
    }

    private string LeftJustify(string[] words, int diff, int i, int j)
    {
        var stringBuilder = new StringBuilder();
        var trailSpaces = diff - (j - i - 1);
        stringBuilder.Append(words[i]);
        for (var k = i + 1; k < j; k++)
        {
            stringBuilder.Append(" " + words[k]);
        }

        stringBuilder.Append(new string(' ', trailSpaces));
        return stringBuilder.ToString();
    }


    public int[] PlusOne(int[] digits)
    {
        var result = new int[digits.Length];
        var memory = 0;
        for (var i = digits.Length - 1; i >= 0; i--)
        {
            if (i == digits.Length - 1)
            {
                result[i] = digits[i] + 1;
            }
            else
            {
                result[i] = digits[i] + memory;
            }

            if (result[i] >= 10)
            {
                result[i] -= 10;
                memory = 1;
            }
            else
            {
                memory = 0;
            }
        }

        if (memory > 0)
        {
            var result2 = new List<int> {memory};
            result2.AddRange(result);
            return result2.ToArray();
        }

        return result;
    }

    public int DistributeCandies(int[] candyType)
    {
        var n2 = candyType.Length / 2;
        var uniqueCandies = new HashSet<int>();
        foreach (var candyKind in candyType)
        {
            if (!uniqueCandies.Contains(candyKind))
            {
                uniqueCandies.Add(candyKind);
            }
        }

        return Math.Min(uniqueCandies.Count, n2);
    }

    public ListNode DeleteDuplicates(ListNode head)
    {
        if (head == null)
        {
            return null;
        }

        var prev = head;
        var next = head.next;

        while (next != null)
        {
            if (prev.val == next.val)
            {
                prev.next = next.next;
                next = next.next;
            }
            else
            {
                prev = next;
                next = next.next;
            }
        }

        return head;
    }

    public void PrintList(ListNode head)
    {
        var pointer = head;
        while (pointer != null)
        {
            Write($"{pointer.val} - ");
            pointer = pointer.next;
        }

        WriteLine();
    }

    public class BstIterator
    {
        private readonly Stack<TreeNode> _stack = new();

        public BstIterator(TreeNode root)
        {
            while (root != null)
            {
                _stack.Push(root);
                root = root.left;
            }
        }

        public int Next()
        {
            var next = _stack.Pop();
            if (next.right != null)
            {
                var cur = next.right;
                while (cur != null)
                {
                    _stack.Push(cur);
                    cur = cur.left;
                }
            }

            return next.val;
        }

        public bool HasNext() =>
            _stack.Count > 0;
    }
}