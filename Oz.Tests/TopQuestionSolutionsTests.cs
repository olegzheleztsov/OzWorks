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
    }
}