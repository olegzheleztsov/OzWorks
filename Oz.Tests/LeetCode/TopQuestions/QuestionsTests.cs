// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using FluentAssertions;
using Oz.LeetCode.TopQuestions;
using Xunit;

namespace Oz.Tests.LeetCode.TopQuestions
{
    public class QuestionsTests
    {
        [Fact]
        public void Should_Is_Happy_Correct()
        {
            Questions questions = new Questions();
            questions.IsHappy(19).Should().BeTrue();
            questions.IsHappy(2).Should().BeFalse();
        }

        [Fact]
        public void Should_Correctly_Frequency_Sort()
        {
            int[] nums = { 1, 1, 2, 2, 2, 3 };
            Questions questions = new Questions();
            questions.FrequencySort(nums);
            nums.Should().BeEquivalentTo(new int[] { 3, 1, 1, 2, 2, 2 });

            nums = new int[] { -1, 1, -6, 4, 5, -6, 1, 4, 1 };
            questions.FrequencySort(nums);
            nums.Should().BeEquivalentTo(new int[] { 5, -1, 4, 4, -6, -6, 1, 1, 1 });
        }

        [Fact]
        public void Should_Find_Max_Distance_Of_Vertical_Area()
        {
            int[][] points = new int[6][];
            points[0] = new int[] { 3, 1 };
            points[1] = new int[] { 9, 0 };
            points[2] = new int[] { 1, 0 };
            points[3] = new int[] { 1, 4 };
            points[4] = new int[] { 5, 3 };
            points[5] = new int[] { 8, 8 };

            Questions questions = new Questions();
            int maxWidth = questions.MaxWidthOfVerticalArea(points);
            maxWidth.Should().Be(3);
        }
    }
}