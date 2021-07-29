using FluentAssertions;
using Oz.LeetCode.TopQuestions;
using System;
using Xunit;

namespace Oz.Tests
{
    public class TopQuestionSolutionsTests
    {
        [Theory]
        [InlineData("III", 3)]
        [InlineData("IV", 4)]
        [InlineData("IX", 9)]
        [InlineData("LVIII", 58)]
        [InlineData("MCMXCIV", 1994)]
        public void Should_Correctly_Convert_Roman_To_Int(string s, int number)
        {
            var solutions = new TopQuestionSolutions();
            var result = solutions.RomanToInt(s);
            result.Should().Be(number);
        }

        [Fact]
        public void Should_Correctly_Find_Common_Longest_Prefix()
        {
            string[] strs = {"flower", "flow", "flight"};
            var solutions = new TopQuestionSolutions();
            var prefix = solutions.LongestCommonPrefix(strs);
            prefix.Should().Be("fl");
        }

        [Fact]
        public void Should_Return_Empty_Prefix()
        {
            string[] strs = {"dog", "racecar", "car"};
            var solutions = new TopQuestionSolutions();
            var prefix = solutions.LongestCommonPrefix(strs);
            prefix.Should().BeEmpty();
        }

        [Theory]
        [InlineData("abcabcbb", 3)]
        [InlineData("bbbbb", 1)]
        [InlineData("pwwkew", 3)]
        [InlineData("", 0)]
        public void Should_Correctly_Find_Longest_Substring(string source, int length)
        {
            var solutions = new TopQuestionSolutions();
            var actualLength = solutions.LengthOfLongestSubstring(source);
            actualLength.Should().Be(length);
        }

        [Fact]
        public void Should_Correctly_Find_Median_Sorted_Arrays()
        {
            var solutions = new TopQuestionSolutions();

            int[] nums1 = { 1, 3 };
            int[] nums2 = { 2 };
            solutions.FindMedianSortedArrays(nums1, nums2).Should().BeApproximately(2.0, 0.01);

            nums1 = new int[] { 1, 2 };
            nums2 = new int[] { 3, 4 };
            solutions.FindMedianSortedArrays(nums1, nums2).Should().BeApproximately(2.5, 0.01);

            nums1 = new int[] { 0, 0 };
            nums2 = new int[] { 0, 0 };
            solutions.FindMedianSortedArrays(nums1, nums2).Should().BeApproximately(0.0, 0.01);

            nums1 = Array.Empty<int>();
            nums2 = new int[] { 1 };
            solutions.FindMedianSortedArrays(nums1, nums2).Should().BeApproximately(1.0, 0.01);

            nums1 = new int[] { 2 };
            nums2 = Array.Empty<int>();
            solutions.FindMedianSortedArrays(nums1, nums2).Should().BeApproximately(2.0, 0.01);
        }

        [Theory]
        [InlineData("hello", "ll", 2)]
        [InlineData("aaaaa", "bba", -1)]
        [InlineData("", "", 0)]
        [InlineData("a", "a", 0)]
        public void Should_Correctly_Find_Substring(string haystack, string needle, int index)
        {
            var solutions = new TopQuestionSolutions();
            var actualResult = solutions.StrStr(haystack, needle);
            actualResult.Should().Be(index);
        }

        [Fact]
        public void Should_Search_Insert_Correctly()
        {
            var solutions = new TopQuestionSolutions();
            int[] nums = { 1, 3, 5, 6 };
            int target = 5;
            solutions.SearchInsert(nums, target).Should().Be(2);

            nums = new[] { 1, 3, 5, 6 };
            target = 2;
            solutions.SearchInsert(nums, target).Should().Be(1);

            nums = new[] { 1, 3, 5, 6 };
            target = 7;
            solutions.SearchInsert(nums, target).Should().Be(4);

            nums = new[] { 1, 3, 5, 6 };
            target = 0;
            solutions.SearchInsert(nums, target).Should().Be(0);

            nums = new[] { 1 };
            target = 0;
            solutions.SearchInsert(nums, target).Should().Be(0);
        }
    }
}