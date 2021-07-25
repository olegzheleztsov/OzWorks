// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;

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
                int firstIndex = 0;
                int secondIndex = 0;
                int index = 0;
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
    }
}