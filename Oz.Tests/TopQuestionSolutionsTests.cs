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

        [Fact]
        public void Should_AddBinary_Work_Correctly()
        {
            var a = "11";
            var b = "1";
            var solution = new TopQuestionSolutions();
            solution.AddBinary(a, b).Should().Be("100");

            a = "1010";
            b = "1011";
            solution.AddBinary(a, b).Should().Be("10101");
        }

        [Fact]
        public void Should_MySqrt_Works_Correctly()
        {
            var solution = new TopQuestionSolutions();
            solution.MySqrt(4).Should().Be(2);

            solution.MySqrt(8).Should().Be(2);
        }
    }
}