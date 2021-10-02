// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Oz.Grokking;
using Xunit;
using FluentAssertions;


namespace Oz.Tests.Grokking
{
    public class GAlgoTests
    {
        [Fact]
        public void Should_Correctly_Binary_Search()
        {
            int[] arr = { 1, 2, 3, 4, 5 };
            var result = GAlgo.BinarySearch(arr, 4, (a, b) => a.CompareTo(b));
            result.Value.Should().Be(3);
        }

        [Fact]
        public void Should_Return_Null_When_Not_Found()
        {
            int[] arr = { 1, 2, 3, 4, 5 };
            var result = GAlgo.BinarySearch(arr, 40, (a, b) => a.CompareTo(b));
            result.Should().BeNull();
        }
    }
}
