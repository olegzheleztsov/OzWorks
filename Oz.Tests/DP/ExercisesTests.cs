// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using FluentAssertions;
using Oz.DP;
using Xunit;

namespace Oz.Tests.DP
{
    public class ExercisesTests
    {
        [Theory]
        [InlineData("bcc", "bbca", "bbcbcac", true)]
        [InlineData("bcc", "bbcd", "bbcbcac", false)]
        public void Should_Correctly_Find_Interleaving_Strings(string A, string B, string C, bool isInterleav)
        {
            var answer = Exercises.IsInterleaving(A, B, C);
            answer.Should().Be(isInterleav);
        }

        [Theory]
        [InlineData("bcc", "bbca", "bbcbcac", true)]
        [InlineData("bcc", "bbcd", "bbcbcac", false)]
        public void Should_Correctly_Find_InterleavingDP_Strings(string A, string B, string C, bool isInterleav)
        {
            var answer = Exercises.IsInterleavingDP(A, B, C);
            answer.Should().Be(isInterleav);
        }
    }
}
