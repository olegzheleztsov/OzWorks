// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace Oz.LeetCode.TopQuestions
{
    public class TopQuestionSolutions
    {
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

        public string LongestCommonPrefix(string[] strs)
        {
            var shortestString = strs[0];
            for (var i = 1; i < strs.Length; i++)
            {
                if (strs[i].Length < shortestString.Length)
                {
                    shortestString = strs[i];
                }
            }

            var prefix = string.Empty;

            var index = 0;
            foreach (var c in shortestString)
            {
                if (strs.Any(s => s[index] != c))
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
            if(string.IsNullOrEmpty(s))
            {
                return 0;
            }

            int count = 0, firstIndex = 0, lastIndex = 0;

            var substring = new HashSet<char>();

            while(lastIndex < s.Length)
            {
                if (substring.Contains(s[lastIndex]))
                {
                    substring.Remove(s[firstIndex]);
                    firstIndex++;
                } else
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
            if(nums1.Length == 0 && nums2.Length == 0 )
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

            if(mergedArray.Length % 2 == 1)
            {
                return mergedArray[mergedArray.Length / 2];
            } else
            {
                return (mergedArray[mergedArray.Length / 2 - 1] + mergedArray[mergedArray.Length / 2]) / 2.0;
            }
        }

        /// <summary>
        /// Implement strStr().Return the index of the first occurrence of needle in haystack, or -1 if needle is not part of haystack.
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needle"></param>
        /// <returns></returns>
        public int StrStr(string haystack, string needle)
        {
            if (string.IsNullOrEmpty(needle))
            {
                return 0;
            }
            if (string.IsNullOrEmpty(haystack))
            {
                return -1;
            }
            if (haystack.Length < needle.Length)
            {
                return -1;
            }
            if (haystack.Length == needle.Length)
            {
                return haystack == needle ? 0 : -1;
            }

            for (var i = 0; i <= (haystack.Length - needle.Length); i++)
            {
                var isEqual = true;
                for (var j = 0; j < needle.Length; j++)
                {
                    if (needle[j] != haystack[i + j])
                    {
                        isEqual = false;
                        break;
                    }
                }
                if (isEqual)
                {
                    return i;
                }
            }
            return -1;
        }

        enum Sign { Positive, Negative }
        public int Divide(int dividend, int divisor)
        {
            var sign = Sign.Positive;
            if(divisor == 0)
            {
                return int.MaxValue;
            }
            if(divisor == -1 && dividend == int.MinValue)
            {
                return int.MaxValue;
            }

            if((dividend < 0 && divisor > 0) || (dividend > 0 && divisor < 0))
            {
                sign = Sign.Negative;
            }

            var result = DivideInner(Math.Abs((long)dividend), Math.Abs((long)divisor));
            if(sign == Sign.Negative)
            {
                return -result;
            }
            return result;

            int DivideInner(long dividend, long divisor )
            {
                if(dividend < divisor)
                {
                    return 0;
                }
                if(dividend == divisor)
                {
                    return 1;
                }
                var tempDivisor = divisor;
                var multiples = 0;
                while (tempDivisor <= dividend)
                {
                    multiples++;
                    tempDivisor <<= 1;
                }

                multiples--;
                tempDivisor >>= 1;
                var nextDivident = dividend - tempDivisor;

                return DivideInner(nextDivident, divisor) | (1 << multiples);
            }
        }

        public int SearchInsert(int[] nums, int target)
        {

            return FindIndex(nums, 0, nums.Length - 1, target);

            int FindIndex(int[] nums, int lowerIndex, int upperIndex, int target)
            {
                if(upperIndex == lowerIndex)
                {
                    if(upperIndex == nums.Length - 1 && target > nums[upperIndex])
                    {
                        return upperIndex + 1;
                    }
                    if(upperIndex == 0 && target < nums[0])
                    {
                        return 0;
                    }
                    return lowerIndex;
                }

                var midIndex = (lowerIndex + upperIndex) / 2;
                if(target == nums[midIndex])
                {
                    return midIndex;
                } else if(target < nums[midIndex])
                {
                    return FindIndex(nums, lowerIndex, midIndex, target);
                } else
                {
                    return FindIndex(nums, midIndex + 1, upperIndex, target);
                }
            }
        }

        public string Multiply(string num1, string num2)
        {
            var num1Len = num1.Length;
            var num2Len = num2.Length;
            var products = new int[num1Len + num2Len];

            for (var i = num1Len - 1; i >= 0; i--)
            {
                for (var j = num2Len - 1; j >= 0; j--)
                {
                    var prodIndex = i + j;
                    var prodIndexCur = prodIndex + 1;

                    var sum = (num1[i] - '0') * (num2[j] - '0') + products[prodIndexCur];
                    products[prodIndex] += sum / 10;
                    products[prodIndexCur] = sum % 10;
                }
            }

            var stringBuilder = new StringBuilder();
            foreach (var num in products)
            {
                if (stringBuilder.Length != 0 || num != 0)
                {
                    stringBuilder.Append(num);
                }
            }

            return stringBuilder.Length == 0 ? "0" : stringBuilder.ToString();
        }
        
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            var result = new List<IList<int>>();
            foreach (var candidate in candidates)
            {
                var collection = new List<int>();
                if (candidate <= target)
                {
                    collection.Add(candidate);
                    Aggregate(result, collection, target - candidate);
                }
            }

            return result;

            void Aggregate(IList<IList<int>> allCollections,  List<int> collection, int currentTarget)
            {
                switch (currentTarget)
                {
                    case 0:
                        allCollections.Add(collection);
                        break;
                    case > 0:
                    {
                        foreach (var candidate in candidates)
                        {
                            if (candidate <= currentTarget)
                            {
                                var newCollection = new List<int>(collection)
                                {
                                    candidate
                                };
                                Aggregate(allCollections, newCollection, currentTarget - candidate);
                            }
                        }

                        break;
                    }
                }
            }
            

        }
        
        public int MaxSubArray(int[] nums)
        {
            int sum = 0;
            int maxSum = nums[0];

            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                if (nums[i] > sum)
                {
                    sum = nums[i];
                }

                if (sum > maxSum)
                {
                    maxSum = sum;
                }
            }

            return maxSum;
        }
        
        public enum Direction
        {
            Right,
            Down,
            Left,
            Up
        }
        public IList<int> SpiralOrder(int[][] matrix)
        {
            int minRow = 0;
            int maxRow = matrix.Length - 1;
            int minCol = 0;
            int maxCol = matrix[0].Length - 1;

            int r = minRow;
            int c = minCol;
            Direction direction = Direction.Right;

            List<int> numbers = new List<int>();

            while (numbers.Count < matrix.Length * matrix[0].Length)
            {
                switch (direction)
                {
                    case Direction.Right:
                    {
                        c = minCol;
                        while (c <= maxCol)
                        {
                            numbers.Add(matrix[minRow][c]);
                            c++;
                        }

                        minRow++;
                        direction = Direction.Down;
                    }
                        break;
                    case Direction.Down:
                    {
                        r = minRow;
                        while (r <= maxRow)
                        {
                            numbers.Add(matrix[r][maxCol]);
                            r++;
                        }

                        maxCol--;
                        direction = Direction.Left;
                    }
                        break;
                    case Direction.Left:
                    {
                        c = maxCol;
                        while (c >= minCol)
                        {
                            numbers.Add(matrix[maxRow][c]);
                            c--;
                        }

                        maxRow--;
                        direction = Direction.Up;
                    }
                        break;
                    case Direction.Up:
                    {
                        r = maxRow;
                        while (r >= minRow)
                        {
                            numbers.Add(matrix[r][minCol]);
                            r--;
                        }

                        minCol++;
                        direction = Direction.Right;
                    }
                        break;
                }
            }

            return numbers;
        }
    }
}