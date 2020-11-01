using System;
using Oz.Algorithms.Numerics;
using Xunit;

namespace Oz.Algorithms.Tests
{
    public class InterviewTests
    {
        [Fact]
        public void Should_Correctly_Hire_Employee()
        {
            int[] candidates = {3, 5, 2, 1, 4};
            var interview = new Interview<int>(candidates, c => c);
            var (candidate, index, hireCount) = interview.BestCandidate;
            Assert.Equal(5, candidate);
            Assert.Equal(1, index);
            Assert.Equal(2, hireCount);
        }

        [Fact]
        public void Should_Throws_When_Candidates_Are_Null()
        {
            int[] candidates = null;
            Assert.Throws<NullReferenceException>(() => new Interview<int>(candidates, c => c));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1500)]
        public void Should_Throws_When_Candidates_Out_Of_Range(int size)
        {
            var candidates = new int[size];
            Assert.Throws<ArgumentException>(() => new Interview<int>(candidates, c => c));
        }
    }
}