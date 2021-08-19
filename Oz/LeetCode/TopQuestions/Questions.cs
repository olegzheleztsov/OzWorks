// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Oz.LeetCode.Trees;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Console;

namespace Oz.LeetCode.TopQuestions
{
    public class Questions
    {
        
        public bool IsHappy(int n)
        {
            HashSet<int> memory = new HashSet<int>();

            while (!memory.Contains(n) && (n != 1))
            {
                memory.Add(n);
                int[] numbers = GetNumbers(n);
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
            List<int> numbers = new List<int>();

            while (n > 0)
            {
                int rem = n % 10;
                numbers.Add(rem);
                n -= rem;
                n /= 10;
            }

            return numbers.ToArray();
        }

        public class BSTIterator
        {
            private Stack<TreeNode> stack = new Stack<TreeNode>();

            public BSTIterator(TreeNode root)
            {

                while(root != null)
                {
                    stack.Push(root);
                    root = root.left;
                }
            }

            public int Next()
            {
                var next = stack.Pop();
                if(next.right != null)
                {
                    var cur = next.right;
                    while(cur != null)
                    {
                        stack.Push(cur);
                        cur = cur.left;
                    }
                }
                return next.val;
            }

            public bool HasNext()
            {
                return stack.Count > 0;
            }
        }



        public int TrailingZeroes(int n)
        {
            int count = 0;
            while( n > 0)
            {
                n = n / 5;
                count += n;
            }
            return count;
        }

        /// <summary>
        /// https://leetcode.com/problems/longest-palindromic-substring/submissions/
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

            for(var i = 0; i <  s.Length; i++)
            {
                var len1 = LongestPalindromicSubstring(s, i, i);
                var len2 = LongestPalindromicSubstring(s, i, i + 1);
                var maxLen = Math.Max(len1, len2);
                if(maxLen > (end - start + 1))
                {
                    start = i - (maxLen - 1) / 2;
                    end = i + maxLen / 2;
                }
            }
            return s[start..(end + 1)];
        }

        private int LongestPalindromicSubstring(string s, int left, int right)
        {
            if(string.IsNullOrEmpty(s) || left > right)
            {
                return 0;
            }

            while(left >= 0 && right < s.Length && s[left] == s[right])
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
                var result2 = new List<int>();
                result2.Add(memory);
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
    }
}