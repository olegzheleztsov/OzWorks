using System;
using System.Linq;
using FluentAssertions;
using Oz.Algorithms.Numerics;
using Xunit;

namespace Oz.Algorithms.Tests.Numerics
{
    public class ArrayExtensionsTests
    {
        [Fact]
        public void Should_Correctly_Find_Modes()
        {
            int[] array = {1, 2, 3, 1, 3, 5};
            array.FindModes().OrderBy(val => val).Should().Equal(new[] {1, 3});
        }

        [Fact]
        public void Should_Correctly_Find_Modes_On_Empty_Collection()
        {
            var array = Array.Empty<int>();
            array.FindModes().OrderBy(v => v).Should().Equal(Array.Empty<int>());
        }

        [Fact]
        public void Should_Correctly_Find_Modes_When_Only_One_Mode()
        {
            int[] array = {1, 2, 1, 3};
            array.FindModes().OrderBy(v => v).Should().Equal(new[] {1});
        }

        [Fact]
        public void Should_Return_All_Elements_As_Modes_When_All_Elements_Are_Different()
        {
            int[] array = {3, 2, 1};
            array.FindModes().OrderBy(v => v).Should().Equal(new[] {1, 2, 3});
        }
    }
}