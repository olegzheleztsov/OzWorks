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
    }
}